using System.Collections.Generic;
using System.Linq;
using bacteria;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class PreBattleManager : MonoBehaviour
{
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
        Vector3 scale = _spawnedObject.transform.localScale;
        _spawnedObject.transform.localScale = new Vector3(scale.x * 0.02f, scale.y * 0.02f, scale.z * 0.02f);
    }

    
    private void SpawnObject()
    {
        Surface detectedSurface = MainManager.Instance.detectedSurface;

        var filteredBacteria = BacteriaList.bacterias
            .Where(b => b.surfaces.Contains(detectedSurface))
            .ToList();

        BacteriaInfo selectedBacteria = filteredBacteria[Random.Range(0, filteredBacteria.Count)];
        MainManager.Instance.selectedBacteria = selectedBacteria;

        ARPlaceObject(Resources.Load<GameObject>("Bacterias/" + selectedBacteria.name));
        SetInfoDialogText("\n" + selectedBacteria.description);
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
