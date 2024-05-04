using System;
using System.Collections.Generic;
using UnityEngine;
using Color = System.Drawing.Color;

public class GridService : MonoBehaviour
{
    private List<Polyline> polylines = new List<Polyline>();

    public void DrawGrid(float centerLat, float centerLon)
    {
        float spacing = 0.002f; // Grid spacing
        int halfLines = 5; // Number of lines on each side of the center

        // Calculate rounded center coordinates
        float roundedCenterLat = (float)Math.Round(centerLat / spacing) * spacing;
        float roundedCenterLon = (float)Math.Round(centerLon / spacing) * spacing;

        // Calculate the start and end coordinates for latitude and longitude
        float startLat = roundedCenterLat - halfLines * spacing;
        float endLat = roundedCenterLat + halfLines * spacing;
        float startLng = roundedCenterLon - halfLines * spacing;
        float endLng = roundedCenterLon + halfLines * spacing;

        // Draw horizontal lines (constant latitude)
        for (float lat = startLat; lat <= endLat; lat += spacing)
        {
            DrawPolyline((lat, startLng), (lat, endLng));
        }

        // Draw vertical lines (constant longitude)
        for (float lng = startLng; lng <= endLng; lng += spacing)
        {
            DrawPolyline((startLat, lng), (endLat, lng));
        }
    }

    private void DrawPolyline((float, float) point1, (float, float) point2)
    {
        Polyline polyline = new Polyline(new PolylineOptions());
        PolylineOptions polylineOptions = new PolylineOptions();
        polylineOptions.Add(point1.Item1, point1.Item2);
        polylineOptions.Add(point2.Item1, point2.Item2);
        polylineOptions.Color = Color.FromArgb(80, 211, 211, 211);
        polylineOptions.Width = 0.5f;
        polylineOptions.Visible = true;
        polylineOptions.Geodesic = true;

        polyline.Options = polylineOptions;
        polylines.Add(polyline);
    }
    
    public List<Polyline> GetPolylines()
    {
        return polylines;
    }
}