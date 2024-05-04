using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class MapService : MonoBehaviour
{
    public string apiKey;
    public int zoom = 17;
    public enum resolution { low = 1, high = 2 };
    public int mapResolution = 2;
    public enum type { roadmap, satellite, gybrid, terrain };
    public type mapType = type.roadmap;
    private string url = "";
    private int mapWidth = 640;
    private int mapHeight = 640;
    private bool mapIsLoading = false; //not used. Can be used to know that the map is loading 
    private Rect rect;

    private string apiKeyLast;
    private float latLast = -33.85660618894087f;
    private float lonLast = 151.21500701957325f;
    private int zoomLast = 12;
    private int mapResolutionLast = 2;
    private type mapTypeLast = type.roadmap;
    private bool updateMap = true;
    private GPSService _gpsService = new GPSService();
    
    void Start()
    {
        StartCoroutine(GetGoogleMap());
        rect = gameObject.GetComponent<RawImage>().rectTransform.rect;
        mapWidth = (int)Math.Round(rect.width);
        mapHeight = (int)Math.Round(rect.height);
    }
    
    void Update()
    {
        (float, float) coordinates = _gpsService.GetLocation();
        if (updateMap && (apiKeyLast != apiKey || !Mathf.Approximately(latLast, coordinates.Item1) || !Mathf.Approximately(lonLast, coordinates.Item2) || zoomLast != zoom || mapResolutionLast != mapResolution || mapTypeLast != mapType))
        {
            rect = gameObject.GetComponent<RawImage>().rectTransform.rect;
            mapWidth = (int)Math.Round(rect.width);
            mapHeight = (int)Math.Round(rect.height);
            StartCoroutine(GetGoogleMap());
            updateMap = false;
        }
    }


    private IEnumerator GetGoogleMap()
    {
        (float, float) coordinates = _gpsService.GetLocation();
        var gridService = new GridService();
        gridService.DrawGrid(coordinates.Item1, coordinates.Item2);

        var pathParameter = new StringBuilder("path=color:0x000000|weight:2");
        foreach (var polyline in gridService.GetPolylines())
        {
            foreach (var point in polyline.Options.Points)
            {
                pathParameter.Append("|" + point.Item1.ToString(new CultureInfo("en-US")) + "," + point.Item2.ToString(new CultureInfo("en-US")));
            }
        }

        url = "https://maps.googleapis.com/maps/api/staticmap?center=" + coordinates.Item1 + "," + coordinates.Item2 + "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight + "&" + pathParameter.ToString() + "&scale=" + mapResolution + "&maptype=" + mapType + "&key=" + apiKey;

        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                var texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                gameObject.GetComponent<RawImage>().texture = texture;

                apiKeyLast = apiKey;
                latLast = coordinates.Item1;
                lonLast = coordinates.Item2;
                zoomLast = zoom;
                mapResolutionLast = mapResolution;
                mapTypeLast = mapType;
                updateMap = true;
            }
        }
    }

}