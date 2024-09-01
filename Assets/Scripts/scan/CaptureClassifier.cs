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
    public string certFileBase64 = "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";
    public string certPassword = "A0RRpnUoTMtPw2ROGK6o";
    public string caCertFileBase64 = "LS0tLS1CRUdJTiBDRVJUSUZJQ0FURS0tLS0tCk1JSURFVENDQWZtZ0F3SUJBZ0lVT2dNaEo4RnlVTk56OTdlNXdISkczQTJKZUFBd0RRWUpLb1pJaHZjTkFRRUwKQlFBd0Z6RVZNQk1HQTFVRUF3d01hV2RsYlMxbllXMWxJRU5CTUNBWERUSTBNRFV5TlRFeU1qRXlNVm9ZRHpJeApNalF3TlRBeE1USXlNVEl4V2pBWE1SVXdFd1lEVlFRRERBeHBaMlZ0TFdkaGJXVWdRMEV3Z2dFaU1BMEdDU3FHClNJYjNEUUVCQVFVQUE0SUJEd0F3Z2dFS0FvSUJBUURiWFo1QjlOS1V3bE13VFJ3WE1rTWFsMmM2S2VYWGU1bE4KeWk0SmRaRHlDWjlzYktLYzBRTStQNlorTjNlaC8zQkFhYVVsL0JzSzJsUmx4RXlQK0tDaVVSVFpwVS9MNHZ0awpLV216ZVJtMnlRRE5wdkhEeUNkUG1yWVlaSmVjaVc0aXZROXJNQzYvN1l2MmFOREYxL2c4QmNkMWJMTm8waWs3CnVWL1Z3N0laV1ZxZEpsa3ZuaWN6T3FQdHRJZTk1RGd2WGlQeVRoNHBZVEtIZndWcWNqbmo5ZjVkdjJaVHh0Z08KWG05ci9CRVhrSVpqU3lZQUg3TUprTTNiUytQRkpMcnhtZ1pvM1dSSWdHQzZFYlJwb0xOcURxVk1paThhZnVReQpsaU9Td3QzdEEwYVJaU3JnNk5LL2Qzdmh4VlQ0bStqR2Z4UUNuc2ZyWmszMy9sK2Q0aVVmQWdNQkFBR2pVekJSCk1CMEdBMVVkRGdRV0JCVGowMjB2TDBNS01TVElMcUE3TmE1NmFkVnFDVEFmQmdOVkhTTUVHREFXZ0JUajAyMHYKTDBNS01TVElMcUE3TmE1NmFkVnFDVEFQQmdOVkhSTUJBZjhFQlRBREFRSC9NQTBHQ1NxR1NJYjNEUUVCQ3dVQQpBNElCQVFEQWdVKzVDeHI3VzJNNTJYYWY4WStTb0xYc0ZYVDFWVkNUdU14TTJOa3F0N1BCTm9pbHFUZlpiY010CjZNaTJsUEZJR0R6dDlkUnB5bjdaZERLNVJTVXljZ0NkMWpONm94YlNzVFFLVkZYT1VpSzlrekxRMkc2dFFYVk8KMFFPM1BJMDh0R283Z0ovdGFmcTJDcmNhWHg0dVpSSTIrUStDaWJQaWYvTHMxQlhXNWh6L2dSUEdBLzJndnljTApTZGlBNGFrajBMS1l6M2RQamc3MVhianBzdlV2d3NCcm1MMU5LeUpwaDFNa0hac3VDTXBNQ09NTUZ5YjRLY1ZRCjAzbDJYTjBGZGcvSzJNSnJDb0g2YWNReW5FMG5haGRQNzNYZjloN0tqenBEcmVmQnJRVkhZQTRKbnl6SkVkK3oKNFZ4bU1hRURXZjhZeUdlai84a1pkSklKVjJMSgotLS0tLUVORCBDRVJUSUZJQ0FURS0tLS0tCg==";
    public string url = "https://67.207.73.147";
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
        byte[] certBytes = Convert.FromBase64String(certFileBase64);
        X509Certificate2 clientCertificate = new X509Certificate2(certBytes, certPassword);

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
                        MainManager.Instance.detectedSurface = Surface.Animal;
                        SceneManager.LoadSceneAsync(2);
                    }
                }
            }
        }
    }
}
