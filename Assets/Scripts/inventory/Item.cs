public class Item
{
    public ItemType type { get; private set; }
    public ItemRarity rarity { get; private set; }

    public Item(ItemType type, ItemRarity rarity)
    {
        this.type = type;
        this.rarity = rarity;
    }
}