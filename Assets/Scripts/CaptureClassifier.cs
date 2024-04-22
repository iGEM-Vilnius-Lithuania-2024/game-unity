using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class CaptureClassifier : MonoBehaviour
{
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
    
    private void Start() {
        _arRaycastManager = FindObjectOfType<ARRaycastManager>();
        _camera = gameObject.GetComponent<Camera>();
        Button btn = scanButton.GetComponent<Button>();
        btn.onClick.AddListener(CaptureRectangle);
    }

    private void Update() {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    private void OnPostRender()
    {
        if (_takeScreenshotOnNextFrame)
        {
            _takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = _camera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            renderResult.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0 , 0);

            byte[] byteArray = renderResult.EncodeToPNG();

            RenderTexture.ReleaseTemporary(renderTexture);
            _camera.targetTexture = null;

            StartCoroutine(IdentifySurfaceAndSpawnObject(byteArray));
        }
    }
    
    private void CaptureRectangle() {
        if (_placementPoseIsValid) {
            _camera.targetTexture = RenderTexture.GetTemporary(WIDTH, WIDTH, 100);
            _takeScreenshotOnNextFrame = true;
        }
    }
    
    private void UpdatePlacementIndicator() {
        if(_placementPoseIsValid) {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
        }
        else {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose() {
        var screenCenter = _camera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        _arRaycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        _placementPoseIsValid = hits.Count > 0;
        if(_placementPoseIsValid) {
            _placementPose = hits[0].pose;
        }
    }

    private void ARPlaceObject(GameObject objectToSpawn) {
        _spawnedObject = Instantiate(objectToSpawn, _placementPose.position, _placementPose.rotation);
    }

    IEnumerator IdentifySurfaceAndSpawnObject(byte[] data)
    {
        UnityWebRequest request = new UnityWebRequest("https://starfish-app-hpmjn.ondigitalocean.app", "POST");
        request.uploadHandler = new UploadHandlerRaw(data);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "image/png");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string result = request.downloadHandler.text;
            if (result.Contains("skin")) {
                ARPlaceObject(skinBacteria);
            }
            else if (result.Contains("water")) {
                ARPlaceObject(waterBacteria);
            }
            else if (result.Contains("wood")) {
                ARPlaceObject(woodBacteria);
            }
        }
    }
}
