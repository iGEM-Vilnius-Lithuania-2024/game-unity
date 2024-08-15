public class Item
{
    public ItemType type { get; private set; }
    public string name { get; private set; }
    public string description { get; private set; }
    public string iconPath { get; private set; }
    public bool isEquipped { get; set; } = false;

    public Item(ItemType type, string name, string description, string iconPath)
    {
        this.type = type;
        this.name = name;
        this.description = description;
        this.iconPath = iconPath;
    }
}