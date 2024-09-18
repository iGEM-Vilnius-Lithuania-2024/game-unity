using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject item;
    public EndlessTip TipATK;
    public EndlessTip TipHP;
    public EndlessTip TipREG;
    
    private PlasmidManager plasmidManager;
    
    void Start()
    {
        plasmidManager = GameObject.Find("PlasmidManager").GetComponent<PlasmidManager>();
    }
    
    public void OpenEquipPopUp(GameObject item)
    {
        plasmidManager.OpenItemDescription(item, false, -1);
    }
    
    public void OpenUnequipPopUp(int slot)
    {
        plasmidManager.OpenItemDescription(item, true, slot);
    }
    
    public void EquipItem(int slot)
    {
        plasmidManager.EquipItem(slot);
    }
    
    public void UnequipItem()
    {
        plasmidManager.UnequipItem();
    }
    
    public void MergeItems()
    {
        plasmidManager.MergeItems();
    }

    public void OpenGeneSlotEquipPopUp()
    {
        plasmidManager.OpenGeneSlotEquipPopUp();
    }
    
    public void CloseGeneSlotEquipPopUp()
    {
        plasmidManager.CloseGeneSlotEquipPopUp();
    }

    public void OpenTip()
    {
        plasmidManager.OpenTip();
    }
    
    public void OpenTipATK()
    {
        TipATK.TriggerTip();
    }
    
    public void OpenTipHP()
    {
        TipHP.TriggerTip();
    }
    
    public void OpenTipREG()
    {
        TipREG.TriggerTip();
    }
}