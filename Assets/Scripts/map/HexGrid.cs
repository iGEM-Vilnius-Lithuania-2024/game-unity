
using System;
using Mapbox.Unity.Location;
using Mapbox.Utils;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public GameObject hex;
    private bool _isInitialized;
    private bool _gridSpawned = false;

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

    Vector3 _targetPosition;

    void Start()
    {
        LocationProviderFactory.Instance.mapManager.OnInitialized += () => _isInitialized = true;
    }

    void LateUpdate()
    {
        if (_isInitialized && !_gridSpawned)
        {
            var map = LocationProviderFactory.Instance.mapManager;
            Vector2d location = LocationProvider.CurrentLocation.LatitudeLongitude;

            Vector2d centralHexLocation =
                new Vector2d(Math.Round(location.x / 2, 3) * 2, Math.Round(location.y / 3, 3) * 3);

            for (int i = -20; i < 20; i++)
            {
                for (int j = -20; j < 20; j++)
                {
                    Vector2d newHexLocation = new Vector2d(centralHexLocation.x + 0.002 * i, centralHexLocation.y + 0.003 * j);
                    if (Math.Abs(j) % 2 == 0)
                        newHexLocation.x += 0.001f;
                    Vector3 newHexPosition = map.GeoToWorldPosition(newHexLocation);

                    newHexPosition.y = 1;
                    GameObject hexInstance = Instantiate(hex, newHexPosition, Quaternion.Euler(90,0,0));
                    hexInstance.tag = "hex";
                }
            }
            _gridSpawned = true;
        }
    }
}