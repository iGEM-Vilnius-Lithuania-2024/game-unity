using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private PlasmidManager plasmidManager;
    
    void Start()
    {
        plasmidManager = GameObject.Find("PlasmidManager").GetComponent<PlasmidManager>();
    }
    
    public void openEquipPopUp(GameObject item)
    {
        plasmidManager.OpenItemDescription(item, false);
    }
    
    public void openUnequipPopUp(GameObject item)
    {
        plasmidManager.OpenItemDescription(item, true);
    }
    
    public void equipItem()
    {
        plasmidManager.EquipItem();
    }
    
    public void unequipItem()
    {
        plasmidManager.UnequipItem();
    }
}