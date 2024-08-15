public class OriItem : Item
{
    public int multiplier { get; private set; }
    
    public OriItem(string name, string description, string iconPath, int multiplier) : base(ItemType.Ori, name, description, iconPath)
    {
        this.multiplier = multiplier;
    }
}