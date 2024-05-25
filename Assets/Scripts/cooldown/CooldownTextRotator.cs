using UnityEngine;

public class CooldownTextRotator : MonoBehaviour
{
    private Transform _playerTransform;

    void Start() {
        _playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    
    void Update() {
        Debug.Log(-_playerTransform.rotation.eulerAngles.y);
        float playerYRotation = _playerTransform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(90, 0, -playerYRotation);
    }
}
