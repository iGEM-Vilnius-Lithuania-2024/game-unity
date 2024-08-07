using System;
using System.Collections.Generic;
using System.Linq;
using Mapbox.Json;

public class ItemType
{
    public static readonly ItemType Type1 = new ItemType("Type1Name", "Type1 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque egestas laoreet erat, vel ornare elit dictum at. Donec lacinia ipsum vel arcu posuere, ac vulputate arcu tristique. Aliquam id sapien vehicula, ultrices dui et, efficitur sapien. Maecenas felis nisl, facilisis id leo nec, porttitor convallis");
    public static readonly ItemType Type2 = new ItemType("Type2Name", "Type2 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque egestas laoreet erat, vel ornare elit dictum at. Donec lacinia ipsum vel arcu posuere, ac vulputate arcu tristique. Aliquam id sapien vehicula, ultrices dui et, efficitur sapien. Maecenas felis nisl, facilisis id leo nec, porttitor convallis");
    public static readonly ItemType Type3 = new ItemType("Type3Name", "Type3 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque egestas laoreet erat, vel ornare elit dictum at. Donec lacinia ipsum vel arcu posuere, ac vulputate arcu tristique. Aliquam id sapien vehicula, ultrices dui et, efficitur sapien. Maecenas felis nisl, facilisis id leo nec, porttitor convallis");
    public static readonly ItemType Type4 = new ItemType("Type4Name", "Type4 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque egestas laoreet erat, vel ornare elit dictum at. Donec lacinia ipsum vel arcu posuere, ac vulputate arcu tristique. Aliquam id sapien vehicula, ultrices dui et, efficitur sapien. Maecenas felis nisl, facilisis id leo nec, porttitor convallis");

    public string Name { get; private set; }
    public string Description { get; private set; }

    [JsonConstructor]
    private ItemType(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static IEnumerable<ItemType> List()
    {
        return new[] { Type1, Type2, Type3, Type4 };
    }
    
    public override string ToString()
    {
        return Name;
    }
    
    public static ItemType GetByName(string name)
    {
        return List().FirstOrDefault(item => item.Name == name);
    }
}