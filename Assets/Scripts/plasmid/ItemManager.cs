using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private PlasmidManager plasmidManager;
    
    void Start()
    {
        plasmidManager = GameObject.Find("PlasmidManager").GetComponent<PlasmidManager>();
    }
    
    public void openItemDescription(GameObject item)
    {
        plasmidManager.OpenItemDescription(item);
    }
    
    public void equipItem()
    {
        plasmidManager.EquipItem();
    }
}