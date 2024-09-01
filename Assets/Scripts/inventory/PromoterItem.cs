
public class PromoterItem : Item
{
    public int slotsUnlocked { get; private set; }
    public string boostIconPath { get; private set; }
    
    public PromoterItem(string name, string description, string iconPath, int slotsUnlocked, string boostIconPath) : base(ItemType.Promoter, name, description, iconPath)
    {
        this.slotsUnlocked = slotsUnlocked;
        this.boostIconPath = boostIconPath;
    }
}