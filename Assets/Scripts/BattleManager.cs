using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class BattleManager : MonoBehaviour
{
    
    public GameObject skinBacteria;
    public GameObject waterBacteria;
    public GameObject woodBacteria;
    public GameObject moveToSpawnDialog;
    public GameObject battleButton;
    public GameObject runButton;
    public GameObject infoDialog;
    public GameObject infoDialogText;
    
    private GameObject _spawnedObject;
    private Pose _placementPose;
    private ARRaycastManager _arRaycastManager;
    private bool _placementPoseIsValid;
    private bool _isObjectSpawned;
    private Camera _camera;

    private void Start()
    {
        _arRaycastManager = FindObjectOfType<ARRaycastManager>();
        _camera = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        UpdatePlacementPose();
        if (_placementPoseIsValid && !_isObjectSpawned)
        {
            battleButton.SetActive(true);
            runButton.SetActive(true);
            moveToSpawnDialog.SetActive(false);
            _isObjectSpawned = true;
            SpawnObject();
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

    
    private void SpawnObject()
    {
        switch (MainManager.Instance.detectedSurface)
        {
            case Surface.Skin:
                ARPlaceObject(skinBacteria);
                SetInfoDialogText("Corynebacterium loves to call the <b>skin</b> its home, especially in warm, moist areas. It is like the friendly neighbor of your skin's microbiome, helping to keep harmful bacteria in check.");
                break;
            case Surface.Water:
                ARPlaceObject(waterBacteria);
                SetInfoDialogText("Shigella loves to hang out in contaminated <b>water</b>, especially where sanitation isn't the best. It is a sneaky bacteria that can cause quite a tummy upset.");
                break;
            case Surface.Wood:
                ARPlaceObject(woodBacteria);
                SetInfoDialogText("Liberibacter is commonly found lurking in <b>wood</b>, especially in trees and plants. It is a mischievous bacteria that can cause serious trouble for plants, like the infamous citrus greening disease.");
                break;
        }
    }
    
    private void SetInfoDialogText(string newText)
    {
        infoDialog.SetActive(true);
        TextMeshProUGUI childText = infoDialogText.GetComponentInChildren<TextMeshProUGUI>();
        if (childText != null)
        {
            childText.text = newText;
        }
    }
}
