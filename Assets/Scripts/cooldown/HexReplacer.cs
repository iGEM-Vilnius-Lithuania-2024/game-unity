using System;
using System.Collections;
using UnityEngine;

using System.Collections.Generic;
using Mapbox.Unity.Location;
using Mapbox.Utils;

public class HexReplacer : MonoBehaviour
{
    public GameObject replacementPrefab;
    private List<Vector3> positionsToReplace = new List<Vector3>();
    private List<DateTime> scanTimes = new List<DateTime>();
    private bool isInitialized = false;
    private TimeSpan cooldownTime = TimeSpan.FromSeconds(30);

    void Start()
    {
        StartCoroutine(InitializationRoutine());
    }

    IEnumerator InitializationRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        LoadPositions();
        isInitialized = true;
    }

    private void LoadPositions()
    {
        var scanInfos = SaveSystem.LoadScanInfo();
        if (scanInfos.Count > 0)
        {
            var map = LocationProviderFactory.Instance.mapManager;
            foreach (var scan in scanInfos)
            {
                scanTimes.Add(DateTime.Parse(scan.scanTime));
                Vector2d positionIRL = new Vector2d((float)scan.scanPosition[0], (float)scan.scanPosition[1]);
                Vector3 positionIG = map.GeoToWorldPosition(positionIRL);
                
                positionsToReplace.Add(positionIG);
            }
        }
        else
        {
            Debug.LogError("No scan information available.");
        }
    }

    void LateUpdate()
    {
        if (isInitialized)
        {
            for (int i = 0; i < positionsToReplace.Count; i++)
            {
                ReplaceClosestHex(positionsToReplace[i], scanTimes[i]);
            }
            isInitialized = false;
        }
    }

    public void ReplaceClosestHex(Vector3 fromPosition, DateTime scanTime)
    {
        GameObject[] hexes = GameObject.FindGameObjectsWithTag("hex");
        GameObject closestHex = null;
        float shortestDistance = float.MaxValue;

        foreach (GameObject hex in hexes)
        {
            float distance = Vector3.Distance(fromPosition, hex.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestHex = hex;
            }
        }

        if (closestHex != null && replacementPrefab != null)
        {
            Timer timer = replacementPrefab.GetComponent<Timer>();
            TimeSpan elapsedTime = DateTime.Now - scanTime;
            float timeRemaining = (float)(cooldownTime - elapsedTime).TotalSeconds;
            timer.timeRemaining = timeRemaining;
            
            Cooldown cooldown = replacementPrefab.GetComponent<Cooldown>();
            cooldown.cooldownTime = timeRemaining;
            
            if (timeRemaining >= 0)
            {
                Vector3 position = closestHex.transform.position;
                Quaternion rotation = closestHex.transform.rotation;
                GameObject hexInstance = Instantiate(replacementPrefab, position, rotation);
                hexInstance.tag = "cooldown-hex";
            }
        }
        else
        {
            Debug.LogError("No 'hex' tagged object found or replacementPrefab is not assigned.");
        }
    }
}
