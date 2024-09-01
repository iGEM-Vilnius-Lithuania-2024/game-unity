using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration;
    
    public float shakeMagnitude;
    
    public float dampingSpeed;

    private Quaternion initialRotation;
    private float initialShakeMagnitude;

    void OnEnable()
    {
        initialRotation = transform.localRotation;
        initialShakeMagnitude = shakeMagnitude;
    }

    public void StartShake()
    {
        shakeMagnitude = initialShakeMagnitude;
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float yRotation = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y + yRotation, initialRotation.eulerAngles.z);

            elapsed += Time.deltaTime;

            shakeMagnitude = Mathf.MoveTowards(shakeMagnitude, 0f, dampingSpeed * Time.deltaTime);

            yield return null;
        }

        transform.localRotation = initialRotation;
    }
}