using UnityEngine;

public class TextRotator : MonoBehaviour
{
    private Transform _transform;
    private readonly Vector3 _offset = new Vector3(0, 180, 0);

    void Start() {
        _transform = GameObject.Find("Main Camera").GetComponent<Transform>();
    }
    
    void Update() {
        transform.LookAt(_transform);
        transform.Rotate(_offset);
    }
}
