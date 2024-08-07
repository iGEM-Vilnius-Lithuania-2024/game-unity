using System;
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
    
    public Image slot1;
    public Image slot2;
    public Image slot3;
    public Image slot4;
    
    public Sprite itemIcon1;
    public Sprite itemIcon2;
    public Sprite itemIcon3;
    public Sprite itemIcon4;

    public Item lastOpenItem;

    void Start()
    {
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        if (player != null)
        {
            if (player.equippedItem1 != null)
            {
                slot1.sprite = itemIcon1;
            }

            if (player.equippedItem2 != null)
            {
                slot2.sprite = itemIcon2;
            }

            if (player.equippedItem3 != null)
            {
                slot3.sprite = itemIcon3;
            }

            if (player.equippedItem4 != null)
            {
                slot4.sprite = itemIcon4;
            }
        }
        foreach (Item _item in player.items)
        {
            GameObject newItem = Instantiate(item, panel.transform);
            newItem.name = _item.type + "_" + _item.rarity;
            
            // TODO: Set the item's icon
            // newItem.GetComponent<ItemUI>().Setup(_item);
        }
    }
    
    public void OpenItemDescription(GameObject item)
    {
        string itemType = item.name.Split('_')[0]; 
        string itemRarity = item.name.Split('_')[1];
        ItemType _itemType = ItemType.GetByName(itemType);
        ItemRarity _itemRarity = (ItemRarity)Enum.Parse(typeof(ItemRarity), itemRarity);
        lastOpenItem = new Item(_itemType, _itemRarity);
        string itemDescription = _itemType.Description;
        equipPopup.SetActive(true);

        itemTypeText.text = itemType;
        //EquipPopup.transform.Find("Item icon").GetComponent<Image>().image = TODO: CHANGE ITEM ICON ACCORDING TO ITEM TYPE AND RARITY
        itemDescriptionText.text = itemDescription;
        //TODO: CHANGE PERK ACCORDING TO ITEM TYPE AND RARITY
    }
    
    public void CloseItemDescription()
    {
        equipPopup.SetActive(false);
    }
    
    public void EquipItem()
    {
        if (lastOpenItem.type == ItemType.Type1)
        {
            if (player.equippedItem1 != null)
            {
                player.items.Add(player.equippedItem1);
            }
            player.equippedItem1 = lastOpenItem;
        }
        else if (lastOpenItem.type == ItemType.Type2)
        {
            if (player.equippedItem2 != null)
            {
                player.items.Add(player.equippedItem2);
            }
            player.equippedItem2 = lastOpenItem;
        }
        else if (lastOpenItem.type == ItemType.Type3)
        {
            if (player.equippedItem3 != null)
            {
                player.items.Add(player.equippedItem3);
            }
            player.equippedItem3 = lastOpenItem;
        }
        else if (lastOpenItem.type == ItemType.Type4)
        {
            if (player.equippedItem4 != null)
            {
                player.items.Add(player.equippedItem4);
            }
            player.equippedItem4 = lastOpenItem;
        }
        
        player.items.Remove(lastOpenItem);
        UpdateUI();
        CloseItemDescription();
    }
}