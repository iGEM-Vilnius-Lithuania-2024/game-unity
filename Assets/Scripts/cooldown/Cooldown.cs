using UnityEngine;
using Image = UnityEngine.UI.Image;

public class Cooldown : MonoBehaviour
{
    public Image cooldownImage;
    private float _remainingTime = 300f;
    
    
    void Update()
    {
        cooldownImage.fillAmount -= 1 / 300f * Time.deltaTime;
    }

    public void SetRemainingTime(float time)
    {
        _remainingTime = time;
        cooldownImage.fillAmount = _remainingTime / 300f;
    }
}