using System;
using System.Collections.Generic;
using System.Linq;
using Mapbox.Json;

public class ItemType
{
    public static readonly ItemType Promoter = new ItemType(
        "Promoter", 
        "Promoter Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque egestas laoreet erat, vel ornare elit dictum at. Donec lacinia ipsum vel arcu posuere, ac vulputate arcu tristique. Aliquam id sapien vehicula, ultrices dui et, efficitur sapien. Maecenas felis nisl, facilisis id leo nec, porttitor convallis",
        "Increases attack damage");
        
    public static readonly ItemType Genes1 = new ItemType(
        "Genes 1", 
        "Genes 1 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque egestas laoreet erat, vel ornare elit dictum at. Donec lacinia ipsum vel arcu posuere, ac vulputate arcu tristique. Aliquam id sapien vehicula, ultrices dui et, efficitur sapien. Maecenas felis nisl, facilisis id leo nec, porttitor convallis",
        "Increases health points");

    public static readonly ItemType Genes2 = new ItemType(
        "Genes 2", 
        "Genes 2 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque egestas laoreet erat, vel ornare elit dictum at. Donec lacinia ipsum vel arcu posuere, ac vulputate arcu tristique. Aliquam id sapien vehicula, ultrices dui et, efficitur sapien. Maecenas felis nisl, facilisis id leo nec, porttitor convallis",
        "Increases health regeneration rate");
        
    public static readonly ItemType Ori = new ItemType(
        "Ori", 
        "Ori Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque egestas laoreet erat, vel ornare elit dictum at. Donec lacinia ipsum vel arcu posuere, ac vulputate arcu tristique. Aliquam id sapien vehicula, ultrices dui et, efficitur sapien. Maecenas felis nisl, facilisis id leo nec, porttitor convallis",
        "Decreases battle cooldown time");

    public string Name { get; private set; }
    public string Description { get; private set; }
    public string PerkDescription { get; private set; }

    [JsonConstructor]
    private ItemType(string name, string description, string perkDescription)
    {
        Name = name;
        Description = description;
        PerkDescription = perkDescription;
    }

    public static IEnumerable<ItemType> List()
    {
        return new[] { Promoter, Genes1, Genes2, Ori };
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