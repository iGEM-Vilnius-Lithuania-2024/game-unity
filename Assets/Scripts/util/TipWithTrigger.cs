using UnityEngine;

public class TipWithTrigger : MonoBehaviour
{
    public int id;
    
    public void TriggerTip()
    {
        if (SaveSystem.IsTipOpen(id))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void Close()
    {
        SaveSystem.CloseTip(id);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}