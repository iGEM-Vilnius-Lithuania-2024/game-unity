using System;
using System.Collections.Generic;

[Serializable]
public class PlayerInfo
{
    public DateTime saveTime;
    public float currentHealth;
    public Item equippedItem1;
    public Item equippedItem2;
    public Item equippedItem3;
    public Item equippedItem4;
    public List<Item> items;

    public PlayerInfo(Player player)
    {
        this.saveTime = DateTime.Now;
        this.currentHealth = player.currentHealth;
        this.equippedItem1 = player.equippedItem1;
        this.equippedItem2 = player.equippedItem2;
        this.equippedItem3 = player.equippedItem3;
        this.equippedItem4 = player.equippedItem4;
        this.items = player.items;
    }
    
    public PlayerInfo()
    {
        this.saveTime = DateTime.Now;
        this.currentHealth = 100;
        this.equippedItem1 = null;
        this.equippedItem2 = null;
        this.equippedItem3 = null;
        this.equippedItem4 = null;
        this.items = new List<Item>();
    }
}