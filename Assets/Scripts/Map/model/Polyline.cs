using UnityEngine;

public class Polyline : MonoBehaviour
{
    public PolylineOptions Options { get; set; }

    public Polyline(PolylineOptions options)
    {
        Options = options;
    }
}