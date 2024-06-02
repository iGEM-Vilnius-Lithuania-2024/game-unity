using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CaptureClassifier : MonoBehaviour
{
    public string certFilePath = "Assets/Scripts/certs/igem-game-client.pfx";
    public string certPassword = "A0RRpnUoTMtPw2ROGK6o";
    public string caCertFilePath = "Assets/Scripts/certs/igem-game-ca.crt";
    public string url = "https://178.128.136.164";
    public Button scanButton;
    public GameObject spinner;

    private readonly int WIDTH = 224;
    private Camera _camera;
    
    private bool _takeScreenshotOnNextFrame;

    private void Start()
    {
        _camera = gameObject.GetComponent<Camera>();
        Button btn = scanButton.GetComponent<Button>();
        btn.onClick.AddListener(CaptureRectangle);
    }

    private async void OnPostRender()
    {
        if (_takeScreenshotOnNextFrame)
        {
            _takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = _camera.targetTexture;

            Texture2D renderResult =
                new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            renderResult.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);

            spinner.SetActive(true);
            
            byte[] byteArray = renderResult.EncodeToPNG();

            RenderTexture.ReleaseTemporary(renderTexture);
            _camera.targetTexture = null;

            await IdentifySurfaceAndSpawnObject(byteArray);
        }
    }

    private void CaptureRectangle()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            _camera.targetTexture = RenderTexture.GetTemporary(WIDTH, WIDTH, 100);
            _takeScreenshotOnNextFrame = true;
        }
    }

    private async Task IdentifySurfaceAndSpawnObject(byte[] data)
    {
        X509Certificate2 clientCertificate = new X509Certificate2(certFilePath, certPassword);

        HttpClientHandler handler = new HttpClientHandler();
        handler.ClientCertificates.Add(clientCertificate);
        handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true;

        using (HttpClient client = new HttpClient(handler))
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new ByteArrayContent(data);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

            HttpResponseMessage response = await client.SendAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                foreach (Surface surface in Enum.GetValues(typeof(Surface)))
                {
                    string lowerCaseSurface = surface.ToString().ToLower();
                    if (result.Contains(lowerCaseSurface))
                    {
                        MainManager.Instance.detectedSurface = surface;
                        SceneManager.LoadSceneAsync(2);
                    }
                }
            }
        }
    }
}
