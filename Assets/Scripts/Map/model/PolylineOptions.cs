using System.Collections.Generic;
using System.Drawing;

public class PolylineOptions
{
    public List<(float, float)> Points { get; set; }
    public Color Color { get; set; }
    public float Width { get; set; }
    public bool Visible { get; set; }
    public bool Geodesic { get; set; }

    public PolylineOptions()
    {
        Points = new List<(float, float)>();
    }

    public void Add(float lat, float lon)
    {
        Points.Add((lat, lon));
    }
}