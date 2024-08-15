using System;

public class ItemKey
{
    public Tuple<int, int> id { get; private set; }
    public bool isEquipped { get; set; }
    public int equipedSlot { get; set; }
    
    public ItemKey(Tuple<int, int> id, bool isEquipped, int equipedSlot)
    {
        this.id = id;
        this.isEquipped = isEquipped;
        this.equipedSlot = equipedSlot;
    }
}