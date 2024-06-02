using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class BattleManager : MonoBehaviour
{
    
    public GameObject skinBacteria;
    public GameObject waterBacteria;
    public GameObject woodBacteria;
    
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
            SpawnObject();
            _isObjectSpawned = true;
            StartCoroutine(SwitchSceneAfterDelay());
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
                break;
            case Surface.Water:
                ARPlaceObject(waterBacteria);
                break;
            case Surface.Wood:
                ARPlaceObject(woodBacteria);
                break;
        }
    }
    
    IEnumerator SwitchSceneAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        
        SceneManager.LoadScene(0);
    }
}
