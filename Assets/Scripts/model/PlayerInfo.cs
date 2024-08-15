using System;
using System.Collections.Generic;

[Serializable]
public class PlayerInfo
{
    public DateTime saveTime;
    public float currentHealth;
    public List<ItemKey> items;
    
    public PlayerInfo(Player player)
    {
        saveTime = DateTime.Now;
        currentHealth = player.currentHealth;
        items = player.items;
    }

    public PlayerInfo Initialize()
    {
        saveTime = DateTime.Now;
        currentHealth = 100;
        items = new List<ItemKey>
        {
            new ItemKey(new Tuple<int, int>(0, 0), true, 1), 
            new ItemKey(new Tuple<int, int>(1, 0), true, 2)
        };
        
        return this;
    }
    
    public PlayerInfo()
    {
        
    }
}