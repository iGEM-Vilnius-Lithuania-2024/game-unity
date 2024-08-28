using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlasmidManager : MonoBehaviour
{
    public Player player;
    public GameObject item;
    public GameObject panel;
    public GameObject equipPopup;
    public GameObject geneSlotEquipPopup;

    public Button equipSlot1;
    public Button equipSlot2;
    public Button equipSlot3;
    public Button equipSlot4;
    public TMP_Text itemTypeText;
    public TMP_Text itemDescriptionText;
    public GameObject equipButton;
    public GameObject unequipButton;
    public Button mergeButton;
    public Image itemIcon;
    public Image perkIcon;
    public TMP_Text perkBonus;
    public TMP_Text perkDescription;
    
    public Button slot1;
    public Button slot2;
    public Button slot3;
    public Button slot4;
    public Button slot5;
    public Button slot6;

    public ItemKey lastOpenItem;

    void Start()
    {
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        if (player != null)
        {
            player.applyPerks();
            SaveSystem.SavePlayerInfo(player);
            
            foreach (Transform child in panel.transform)
            {
                Destroy(child.gameObject);
            }
            
            slot1.image.sprite = Resources.Load<Sprite>("items/empty");
            slot1.name = "slotEmpty";
            slot2.image.sprite = Resources.Load<Sprite>("items/empty");
            slot2.name = "slotEmpty";
            slot3.image.sprite = Resources.Load<Sprite>("items/empty");
            slot3.name = "slotEmpty";
            slot4.image.sprite = Resources.Load<Sprite>("items/empty");
            slot4.name = "slotEmpty";
            slot5.image.sprite = Resources.Load<Sprite>("items/empty");
            slot5.name = "slotEmpty";
            slot6.image.sprite = Resources.Load<Sprite>("items/empty");
            slot6.name = "slotEmpty";
            
            int slotsUnlocked = 0;
            
            player.items = player.items.OrderByDescending(item => item.id.Item2).ToList();
            
            foreach (ItemKey _item in player.items)
            {
                if (_item.isEquipped)
                { 
                    if (_item.equipedSlot == 1)
                    {
                        slot1.image.sprite = Resources.Load<Sprite>(Items.items[_item.id].iconPath);
                        slot1.name = _item.id.Item1 + "_" + _item.id.Item2;
                    }
                    else if (_item.equipedSlot == 2)
                    {
                        slot2.image.sprite = Resources.Load<Sprite>(Items.items[_item.id].iconPath);
                        slot2.name = _item.id.Item1 + "_" + _item.id.Item2;
                        slotsUnlocked = ((PromoterItem)Items.items[_item.id]).slotsUnlocked;
                    }
                    else if (_item.equipedSlot == 3)
                    {
                        slot3.image.sprite = Resources.Load<Sprite>(Items.items[_item.id].iconPath);
                        slot3.name = _item.id.Item1 + "_" + _item.id.Item2;
                    }
                    else if (_item.equipedSlot == 4)
                    {
                        slot4.image.sprite = Resources.Load<Sprite>(Items.items[_item.id].iconPath);
                        slot4.name = _item.id.Item1 + "_" + _item.id.Item2;
                    }
                    else if (_item.equipedSlot == 5)
                    {
                        slot5.image.sprite = Resources.Load<Sprite>(Items.items[_item.id].iconPath);
                        slot5.name = _item.id.Item1 + "_" + _item.id.Item2;
                    }
                    else if (_item.equipedSlot == 6)
                    {
                        slot6.image.sprite = Resources.Load<Sprite>(Items.items[_item.id].iconPath);
                        slot6.name = _item.id.Item1 + "_" + _item.id.Item2;
                    }
                }
                else
                {
                    GameObject newItem = Instantiate(item, panel.transform);

                    newItem.name = _item.id.Item1 + "_" + _item.id.Item2;
                    newItem.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(Items.items[_item.id].iconPath);
                }
            }

            if (slotsUnlocked < 4)
            {
                slot6.interactable = false;
            }
            if (slotsUnlocked < 3)
            {
                slot5.interactable = false;
            }
            if (slotsUnlocked < 2)
            {
                slot4.interactable = false;
            }
            if (slotsUnlocked < 1)
            {
                slot3.interactable = false;
            }

            if (slotsUnlocked > 0)
            {
                slot3.interactable = true;
            }
            if (slotsUnlocked > 1)
            {
                slot4.interactable = true;
            }
            if (slotsUnlocked > 2)
            {
                slot5.interactable = true;
            }
            if (slotsUnlocked > 3)
            {
                slot6.interactable = true;
            }
        }
    }
    
    public void OpenItemDescription(GameObject item, bool isEquiped, int equipSlot)
    {
        if (item.name == "slotEmpty")
        {
            return;
        }
        int id1 = Int32.Parse(item.name.Split('_')[0]); 
        int id2 = Int32.Parse(item.name.Split('_')[1]);
        Item _item = Items.items[new Tuple<int, int>(id1, id2)];
        lastOpenItem = new ItemKey(new Tuple<int, int>(id1, id2), isEquiped, equipSlot);
        
        if (isEquiped)
        {
            equipButton.SetActive(false);
            unequipButton.SetActive(true);
        }
        else
        {
            equipButton.SetActive(true);
            unequipButton.SetActive(false);
        }
        
        equipPopup.SetActive(true);

        itemTypeText.text = _item.name;
        itemIcon.sprite = Resources.Load<Sprite>(_item.iconPath);
        itemDescriptionText.text = _item.description;

        if (_item.type == ItemType.Promoter)
        {
            perkIcon.sprite = Resources.Load<Sprite>("items/perks/promoter");
            perkDescription.text = "Unlocks " + ((PromoterItem)_item).slotsUnlocked + " extra gene slots";
        }
        else if (_item.type == ItemType.Ori)
        {
            perkIcon.sprite = Resources.Load<Sprite>("items/perks/ori");
            perkDescription.text = "Multiplies all genes bonus by " + ((OriItem)_item).multiplier;
        } 
        else
        {
            perkIcon.sprite = Resources.Load<Sprite>("items/perks/gene" + "_" + ((GeneItem)_item).attribute.ToString().ToLower());
            perkDescription.text = "Increases " + ((GeneItem)_item).attribute + " by " + ((GeneItem)_item).boost;
        }

        if (lastOpenItem.id.Item2 < 3)
        {
            int count = 0;
            foreach (ItemKey i in player.items)
            {
                if (i.id.Equals(lastOpenItem.id))
                {
                    count++;
                }
            }
            if (count < 3)
            {
                mergeButton.interactable = false;
            }
            else
            {
                mergeButton.interactable = true;
            }
        }
        else
        {
            mergeButton.gameObject.SetActive(false);
        }
    }
    
    public void CloseItemDescription()
    {
        equipPopup.SetActive(false);
    }
    
    public void EquipItem(int slot)
    {
        int equipSlot = slot;
        int previousSlotsUnlocked = 0;
        int currentSlotsUnlocked = 0;

        if (Items.items[lastOpenItem.id].type == ItemType.Ori)
        {
            equipSlot = 1;
        }
        else if (Items.items[lastOpenItem.id].type == ItemType.Promoter)
        {
            equipSlot = 2;
        }

        if (equipSlot == -1)
        {
            OpenGeneSlotEquipPopUp();
            return;
        }

        if (slot == 3 && !slot3.interactable || slot == 4 && !slot4.interactable || slot == 5 && !slot5.interactable || slot == 6 && !slot6.interactable)
        {
            return; //TODO: Show info message
        }

        bool isEquipped = false;
        bool isUnEquipped = false;
        foreach (var item in player.items)
        {
            if (item.equipedSlot == equipSlot && !isUnEquipped)
            {
                item.isEquipped = false;
                item.equipedSlot = -1;
                isUnEquipped = true;
                if (equipSlot == 2)
                {
                    previousSlotsUnlocked = ((PromoterItem)Items.items[item.id]).slotsUnlocked;
                }
            }
            if (item.id.Equals(lastOpenItem.id) && !isEquipped)
            {
                item.isEquipped = true;
                item.equipedSlot = equipSlot;
                isEquipped = true;
                if (equipSlot == 2)
                {
                    currentSlotsUnlocked = ((PromoterItem)Items.items[item.id]).slotsUnlocked;
                }
            }
            if (isEquipped && isUnEquipped)
            {
                break;
            }
        }

        if (previousSlotsUnlocked > currentSlotsUnlocked)
        {
            foreach (var item in player.items)
            {
                if (item.equipedSlot > 2 + currentSlotsUnlocked)
                {
                    item.isEquipped = false;
                    item.equipedSlot = -1;
                }
            }
        }
        
        UpdateUI();
        CloseItemDescription();
        CloseGeneSlotEquipPopUp();
    }
    
    public void UnequipItem()
    {
        if (lastOpenItem.equipedSlot == 2)
        {
            foreach (var item in player.items)
            {
                if (item.equipedSlot == 3 || item.equipedSlot == 4 || item.equipedSlot == 5 || item.equipedSlot == 6)
                {
                    item.isEquipped = false;
                    item.equipedSlot = -1;
                }
            }
        }

        foreach (var item in player.items)
        {
            if (item.id.Equals(lastOpenItem.id))
            {
                item.isEquipped = false;
                item.equipedSlot = -1;
            }
        }

        UpdateUI();
        CloseItemDescription();
    }
    
    public void MergeItems()
    {
        int count = 3;
        for (int i = player.items.Count - 1; i >= 0; i--)
        {
            if (player.items[i].id.Equals(lastOpenItem.id) && count > 0)
            {
                count--;
                player.items.RemoveAt(i);
            }
        }
        
        Tuple<int, int> newId = new Tuple<int, int>(lastOpenItem.id.Item1, lastOpenItem.id.Item2 + 1);
        
        player.items.Add(new ItemKey(newId, lastOpenItem.isEquipped, lastOpenItem.equipedSlot));
        UpdateUI();
        CloseItemDescription();

        GameObject newItem = new GameObject();
        newItem.name = newId.Item1 + "_" + newId.Item2;
        OpenItemDescription(newItem, lastOpenItem.isEquipped, lastOpenItem.equipedSlot);
        newItem.Destroy();
    }

    public void OpenGeneSlotEquipPopUp()
    {
        geneSlotEquipPopup.SetActive(true);

        int equippedPromoterSlotsUnlocked = 0;
        foreach (var item in player.items)
        {
            if (item.equipedSlot == 2)
            {
                equippedPromoterSlotsUnlocked = ((PromoterItem)Items.items[item.id]).slotsUnlocked;
                break;
            }
        }

        if (equippedPromoterSlotsUnlocked < 4)
        {
            equipSlot4.interactable = false;
        }
        if (equippedPromoterSlotsUnlocked < 3)
        {
            equipSlot3.interactable = false;
        }
        if (equippedPromoterSlotsUnlocked < 2)
        {
            equipSlot2.interactable = false;
        }
        if (equippedPromoterSlotsUnlocked < 1)
        {
            equipSlot1.interactable = false;
        }
    }
    
    public void CloseGeneSlotEquipPopUp()
    {
        geneSlotEquipPopup.SetActive(false);
    }
}