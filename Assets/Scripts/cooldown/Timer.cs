using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float timeRemaining;
    
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = timeRemaining.ToString("F2");
        }
        else if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            destroyHex();
        }
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void destroyHex()
    {
        var parentObject = this.GetComponentInParent<Transform>();
        Destroy(parentObject.gameObject);
    }
}