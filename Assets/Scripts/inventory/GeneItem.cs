public class GeneItem : Item
{
    public ItemAttribute attribute { get; private set; }
    public float boost { get; private set; }
    public string boostIconPath { get; private set; }
    
    public GeneItem(string name, string description, string iconPath, ItemAttribute attribute, float boost, string boostIconPath) : base(ItemType.Gene, name, description, iconPath)
    {
        this.attribute = attribute;
        this.boost = boost;
        this.boostIconPath = boostIconPath;
    }
}