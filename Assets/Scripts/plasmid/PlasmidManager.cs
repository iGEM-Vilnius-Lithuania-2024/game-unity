using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlasmidManager : MonoBehaviour
{
    public Player player;
    public GameObject item;
    public GameObject panel;
    public GameObject equipPopup;
    
    public TMP_Text itemTypeText;
    public TMP_Text itemDescriptionText;
    public GameObject equipButton;
    public GameObject unequipButton;
    public Image itemIcon;
    public Image perkIcon;
    public TMP_Text perkBonus;
    public TMP_Text perkDescription;
    
    public Image slot1;
    public Image slot2;
    public Image slot3;
    public Image slot4;

    public Item lastOpenItem;

    void Start()
    {
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        if (player != null)
        {
            SaveSystem.SavePlayerInfo(player);
            if (player.equippedItem1 != null)
            {
                slot1.name = player.equippedItem1.type.Name + "_" + player.equippedItem1.rarity;
                slot1.sprite = Resources.Load<Sprite>("items/" + slot1.name);
            }
            else
            {
                slot1.sprite = Resources.Load<Sprite>("items/empty");
                slot1.name = "slotEmpty";
            }

            if (player.equippedItem2 != null)
            {
                slot2.name = player.equippedItem2.type.Name + "_" + player.equippedItem2.rarity;
                slot2.sprite = Resources.Load<Sprite>("items/" + slot2.name);
            }
            else
            {
                slot2.sprite = Resources.Load<Sprite>("items/empty");
                slot2.name = "slotEmpty";
            }

            if (player.equippedItem3 != null)
            {
                slot3.name = player.equippedItem3.type.Name + "_" + player.equippedItem3.rarity;
                slot3.sprite = Resources.Load<Sprite>("items/" + slot3.name);
            }
            else
            {
                slot3.sprite = Resources.Load<Sprite>("items/empty");
                slot3.name = "slotEmpty";
            }

            if (player.equippedItem4 != null)
            {
                slot4.name = player.equippedItem4.type.Name + "_" + player.equippedItem4.rarity;
                slot4.sprite = Resources.Load<Sprite>("items/" + slot4.name);
            }
            else
            {
                slot4.sprite = Resources.Load<Sprite>("items/empty");
                slot4.name = "slotEmpty";
            }

            foreach (Transform child in panel.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Item _item in player.items)
            {
                GameObject newItem = Instantiate(item, panel.transform);
                newItem.name = _item.type + "_" + _item.rarity;
                
                newItem.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("items/" + newItem.name);
            }
        }
    }
    
    public void OpenItemDescription(GameObject item, bool isEquiped)
    {
        if (item.name == "slotEmpty")
        {
            return;
        }
        string itemType = item.name.Split('_')[0]; 
        string itemRarity = item.name.Split('_')[1];
        ItemType _itemType = ItemType.GetByName(itemType);
        ItemRarity _itemRarity = (ItemRarity)Enum.Parse(typeof(ItemRarity), itemRarity);
        lastOpenItem = new Item(_itemType, _itemRarity);
        string itemDescription = _itemType.Description;
        
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

        itemTypeText.text = itemType;
        itemIcon.sprite = Resources.Load<Sprite>("items/" + item.name);
        itemDescriptionText.text = itemDescription;
        
        perkIcon.sprite = Resources.Load<Sprite>("items/perks/" + itemType);
        if (_itemType == ItemType.Ori)
        {
            perkBonus.text = "-" + (int)_itemRarity + "%";
        }
        else
        {
            perkBonus.text = "+" + (int)_itemRarity + "%";
        }
        perkDescription.text = _itemType.PerkDescription;
    }
    
    public void CloseItemDescription()
    {
        equipPopup.SetActive(false);
    }
    
    public void EquipItem()
    {
        if (lastOpenItem.type == ItemType.Promoter)
        {
            if (player.equippedItem1 != null)
            {
                player.items.Add(player.equippedItem1);
            }
            player.equippedItem1 = lastOpenItem;
        }
        else if (lastOpenItem.type == ItemType.Genes1)
        {
            if (player.equippedItem2 != null)
            {
                player.items.Add(player.equippedItem2);
            }
            player.equippedItem2 = lastOpenItem;
        }
        else if (lastOpenItem.type == ItemType.Genes2)
        {
            if (player.equippedItem3 != null)
            {
                player.items.Add(player.equippedItem3);
            }
            player.equippedItem3 = lastOpenItem;
        }
        else if (lastOpenItem.type == ItemType.Ori)
        {
            if (player.equippedItem4 != null)
            {
                player.items.Add(player.equippedItem4);
            }
            player.equippedItem4 = lastOpenItem;
        }

        Item itemToRemove = player.items.FirstOrDefault(item => 
                item.type == lastOpenItem.type &&
                item.rarity == lastOpenItem.rarity
        );

        if (itemToRemove != null)
        {
            player.items.Remove(itemToRemove);
        }

        UpdateUI();
        CloseItemDescription();
    }
    
    public void UnequipItem()
    {
        player.items.Add(lastOpenItem);
        if (lastOpenItem.type == ItemType.Promoter)
        {
            player.equippedItem1 = null;
        }
        else if (lastOpenItem.type == ItemType.Genes1)
        {
            player.equippedItem2 = null;
        }
        else if (lastOpenItem.type == ItemType.Genes2)
        {
            player.equippedItem3 = null;
        }
        else if (lastOpenItem.type == ItemType.Ori)
        {
            player.equippedItem4 = null;
        }
        UpdateUI();
        CloseItemDescription();
    }
}