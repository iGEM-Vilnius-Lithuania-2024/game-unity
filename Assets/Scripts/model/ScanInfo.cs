using System;
using Mapbox.Utils;
using UnityEngine;

[System.Serializable]
public class ScanInfo
{
    public String scanTime;
    public float[] scanPosition;
    
    public ScanInfo(DateTime scanTime, Vector2d scanPosition)
    {
        this.scanTime = scanTime.ToString();
        this.scanPosition = new float[2];
        this.scanPosition[0] = (float)scanPosition.x;
        this.scanPosition[1] = (float)scanPosition.y;
    }
}