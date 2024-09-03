using UnityEngine;

public class EndlessTip : MonoBehaviour
{
    public void TriggerTip()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}