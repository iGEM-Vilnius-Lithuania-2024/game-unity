using UnityEngine;
using Image = UnityEngine.UI.Image;

public class Cooldown : MonoBehaviour
{
    public Image cooldownImage;
    private float _maxTime = 300f;
    private float _remainingTime = 300f;
    
    
    void Update()
    {
        cooldownImage.fillAmount -= 1 / _maxTime * Time.deltaTime;
    }

    public void SetRemainingTime(float time, float maxTime)
    {
        _remainingTime = time;
        _maxTime = maxTime;
        cooldownImage.fillAmount = _remainingTime / _maxTime;
    }
}