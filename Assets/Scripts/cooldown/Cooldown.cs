using UnityEngine;
using Image = UnityEngine.UI.Image;

public class Cooldown : MonoBehaviour
{
    public Image cooldownImage;
    public float cooldownTime = 10f;
    
    void Start()
    {
        cooldownImage.fillAmount = 1;
    }
    
    void Update()
    {
        cooldownImage.fillAmount -= 1 / cooldownTime * Time.deltaTime;
    }
}