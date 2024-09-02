using UnityEngine;

public class Tip : MonoBehaviour
{
    public int id;
    
    void Start()
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
