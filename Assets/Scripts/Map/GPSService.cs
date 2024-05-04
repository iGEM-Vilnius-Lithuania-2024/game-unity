using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class GPSService : MonoBehaviour
{
    public Text gpsOut;
    public bool isUpdating;
    public float lat;
    public float lon;
    public float gmcLat = 54.7223f;
    public float gmcLon = 25.3264f;
    
    // private void Update()
    // {
    //     if (!isUpdating)
    //     {
    //         StartCoroutine(GetLocation());
    //         isUpdating = !isUpdating;
    //     }
    // }

    public (float, float) GetLocation()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }

        if (!Input.location.isEnabledByUser)
            return (gmcLat, gmcLon);
        
        Input.location.Start();
        
        lat = Input.location.lastData.latitude;
        lon = Input.location.lastData.longitude;
        
        isUpdating = !isUpdating;
        Input.location.Stop();

        return (lat, lon);
    }
}