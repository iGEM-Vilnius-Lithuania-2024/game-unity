using Mapbox.Unity.Location;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    ILocationProvider _locationProvider;
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
        SceneManager.LoadSceneAsync(sceneId);
    }
    
    public void MoveToSceneWithScanInfo(int sceneId)
    {
        if (IsOnCooldown())
        {
            Debug.LogError("You are on cooldown.");
        }
        else
        {
            ScanInfoStatic.scanPosition = LocationProvider.CurrentLocation.LatitudeLongitude;
            SceneManager.LoadSceneAsync(sceneId);   
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
}
