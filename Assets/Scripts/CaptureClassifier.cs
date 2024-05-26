using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class CaptureClassifier : MonoBehaviour
{
    public string certFilePath = "Assets/Scripts/certs/igem-game-client.pfx";
    public string certPassword = "A0RRpnUoTMtPw2ROGK6o";
    public string caCertFilePath = "Assets/Scripts/certs/igem-game-ca.crt";
    public string url = "https://143.244.198.78";
    public GameObject skinBacteria;
    public GameObject waterBacteria;
    public GameObject woodBacteria;
    public GameObject placementIndicator;
    public Button scanButton;

    private readonly int WIDTH = 224;
    private Camera _camera;
    private GameObject _spawnedObject;
    private Pose _placementPose;
    private ARRaycastManager _arRaycastManager;
    private bool _placementPoseIsValid;
    private bool _takeScreenshotOnNextFrame;

    private void Start()
    {
        _arRaycastManager = FindObjectOfType<ARRaycastManager>();
        _camera = gameObject.GetComponent<Camera>();
        Button btn = scanButton.GetComponent<Button>();
        btn.onClick.AddListener(SaveScanInfo);
        btn.onClick.AddListener(CaptureRectangle);
    }

    private void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
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

            byte[] byteArray = renderResult.EncodeToPNG();

            RenderTexture.ReleaseTemporary(renderTexture);
            _camera.targetTexture = null;

            await IdentifySurfaceAndSpawnObject(byteArray);
        }
    }

    private void CaptureRectangle()
    {
        if (_placementPoseIsValid)
        {
            _camera.targetTexture = RenderTexture.GetTemporary(WIDTH, WIDTH, 100);
            _takeScreenshotOnNextFrame = true;
        }
    }

    private void SaveScanInfo()
    {
        SaveSystem.SaveScanInfo(DateTime.Now, ScanInfoStatic.scanPosition);
    }

    private void UpdatePlacementIndicator()
    {
        if (_placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = _camera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        _arRaycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        _placementPoseIsValid = hits.Count > 0;
        if (_placementPoseIsValid)
        {
            _placementPose = hits[0].pose;
        }
    }

    private void ARPlaceObject(GameObject objectToSpawn)
    {
        _spawnedObject = Instantiate(objectToSpawn, _placementPose.position, _placementPose.rotation);
    }
    
    private async Task IdentifySurfaceAndSpawnObject(byte[] data)
    {
        X509Certificate2 clientCertificate = new X509Certificate2(certFilePath, certPassword);
        X509Certificate2 caCertificate = new X509Certificate2(caCertFilePath);

        HttpClientHandler handler = new HttpClientHandler();
        handler.ClientCertificates.Add(clientCertificate);
        handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
        {
            return true;
        };

        using (HttpClient client = new HttpClient(handler))
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new ByteArrayContent(data);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

            HttpResponseMessage response = await client.SendAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                if (result.Contains("skin"))
                {
                    ARPlaceObject(skinBacteria);
                }
                else if (result.Contains("water"))
                {
                    ARPlaceObject(waterBacteria);
                }
                else if (result.Contains("wood"))
                {
                    ARPlaceObject(woodBacteria);
                }
            }
        }
    }
}
