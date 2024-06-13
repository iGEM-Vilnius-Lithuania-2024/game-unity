using System.Collections;
using Mapbox.Unity.Location;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private ILocationProvider _locationProvider;
    
    public GameObject onCooldownDialog;

    ILocationProvider LocationProvider
    {
        get
        {
            if (_locationProvider == null)
            {
                _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
            }

            return _locationProvider;
        }
    }
    
    public void MoveToScene(int sceneId)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable && SceneManager.GetActiveScene().buildIndex != sceneId)
        {
            SceneManager.LoadSceneAsync(sceneId);
        }
    }
    
    public void MoveToSceneWithScanInfo(int sceneId)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable && SceneManager.GetActiveScene().buildIndex != sceneId)
        {
            if (IsOnCooldown())
            {
                StartCoroutine(OpenCloseDialog(onCooldownDialog));
            }
            else
            {
                ScanInfoStatic.scanPosition = LocationProvider.CurrentLocation.LatitudeLongitude;
                SceneManager.LoadSceneAsync(sceneId);
            }
        }
    }
    
    private bool IsOnCooldown()
    {
        GameObject[] cooldownHexes = GameObject.FindGameObjectsWithTag("cooldown-hex");
        if (cooldownHexes.Length == 0)
        {
            return false;
        }
        Vector2d location = LocationProvider.CurrentLocation.LatitudeLongitude;
        Vector3 locationIG = LocationProviderFactory.Instance.mapManager.GeoToWorldPosition(location);
        foreach (GameObject cooldownHex in cooldownHexes)
        {
            float distance = Vector3.Distance(locationIG, cooldownHex.transform.position);
            if (distance < 32f)
            {
                return true;
            }
        }
        return false;
    }
    
    IEnumerator OpenCloseDialog(GameObject dialog)
    {
        dialog.SetActive(true);
        yield return new WaitForSeconds(5f);
        dialog.SetActive(false);
    }
}
