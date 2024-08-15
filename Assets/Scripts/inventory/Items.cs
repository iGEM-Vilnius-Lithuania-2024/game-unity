using System;
using System.Collections.Generic;
using Mapbox.Map;

public class Items
{
    public static Dictionary<Tuple<int, int>, Item> items = new Dictionary<Tuple<int, int>, Item>
    {
        { new Tuple<int, int>(0, 0), new OriItem("F1 ori", "Ori 1 description", "items/0_0", 1) },
        { new Tuple<int, int>(0, 1), new OriItem("RK2 ori", "Ori 2 description", "items/0_1", 2) },
        { new Tuple<int, int>(0, 2), new OriItem("p15A ori", "Ori 3 description", "items/0_2", 3) },
        { new Tuple<int, int>(0, 3), new OriItem("pMB1 ori", "Ori 4 description", "items/0_3", 4) },
        
        { new Tuple<int, int>(1, 0), new PromoterItem("Constitutive promoter", "Promoter description", "items/1_0", 1) },
        { new Tuple<int, int>(1, 1), new PromoterItem("T7 lac promoter", "Promoter description", "items/1_1", 2) },
        { new Tuple<int, int>(1, 2), new PromoterItem("araBAD promoter", "Promoter description", "items/1_2", 3) },
        { new Tuple<int, int>(1, 3), new PromoterItem("lac promoter", "Promoter description", "items/1_3", 4) },
        
        { new Tuple<int, int>(2, 0), new GeneItem("Attack gene", "Gene description", "items/2_0", ItemAttribute.Attack, 1) },
        { new Tuple<int, int>(2, 1), new GeneItem("Attack gene", "Gene description", "items/2_1", ItemAttribute.Attack, 2) },
        { new Tuple<int, int>(2, 2), new GeneItem("Attack gene", "Gene description", "items/2_2", ItemAttribute.Attack, 4) },
        { new Tuple<int, int>(2, 3), new GeneItem("Attack gene", "Gene description", "items/2_3", ItemAttribute.Attack, 6) },
        
        { new Tuple<int, int>(3, 0), new GeneItem("HP gene", "Gene description", "items/3_0", ItemAttribute.HP, 5) },
        { new Tuple<int, int>(3, 1), new GeneItem("HP gene", "Gene description", "items/3_1", ItemAttribute.HP, 10) },
        { new Tuple<int, int>(3, 2), new GeneItem("HP gene", "Gene description", "items/3_2", ItemAttribute.HP, 15) },
        { new Tuple<int, int>(3, 3), new GeneItem("HP gene", "Gene description", "items/3_3", ItemAttribute.HP, 20) },
        
        { new Tuple<int, int>(4, 0), new GeneItem("Regen gene", "Gene description", "items/4_0", ItemAttribute.Regen, 1) },
        { new Tuple<int, int>(4, 1), new GeneItem("Regen gene", "Gene description", "items/4_1", ItemAttribute.Regen, 2) },
        { new Tuple<int, int>(4, 2), new GeneItem("Regen gene", "Gene description", "items/4_2", ItemAttribute.Regen, 3) },
        { new Tuple<int, int>(4, 3), new GeneItem("Regen gene", "Gene description", "items/4_3", ItemAttribute.Regen, 4) },
    };
}