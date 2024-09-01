public class OriItem : Item
{
    public float multiplier { get; private set; }
    
    public OriItem(string name, string description, string iconPath, float multiplier) : base(ItemType.Ori, name, description, iconPath)
    {
        this.multiplier = multiplier;
    }
}