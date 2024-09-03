using System;
using System.Collections.Generic;
using Mapbox.Map;

public class Items
{
    public static Dictionary<Tuple<int, int>, Item> items = new Dictionary<Tuple<int, int>, Item>
    {
        { new Tuple<int, int>(0, 0), new OriItem("<i>F1</i> ori\n(level 1)", "Short DNA sequence that lets plasmids make 1 copies of themselves.", "items/0_0", 2) },
        { new Tuple<int, int>(0, 1), new OriItem("<i>RK2</i> ori\n(level 2)", "Short DNA sequence that lets plasmids make 5 copies of themselves.", "items/0_1", 3) },
        { new Tuple<int, int>(0, 2), new OriItem("<i>p15A</i> ori\n(level 3)", "Short DNA sequence that lets plasmids make 30 copies of themselves.", "items/0_2", 4) },
        { new Tuple<int, int>(0, 3), new OriItem("<i>pMB1</i> ori\n(level 4)", "Short DNA sequence that lets plasmids make more than 200 copies of themselves.", "items/0_3", 5) },
        
        { new Tuple<int, int>(1, 0), new PromoterItem("Constitutive promoter<\n(level 1)", "A constitutive promoter is an independent and never-resting part of the plasmid - it continuously turns on the genes that go after it.", "items/1_0", 1, "items/lock") },
        { new Tuple<int, int>(1, 1), new PromoterItem("<i>Lac</i> promoter\n(level 2)", "The <i>lac</i> promoter controls activity of genes needed to break down lactose - a sugar found in milk. Lactose sugar or IPTG (molecule imitating lactose) turns on <i>lac</i> promoter.", "items/1_1", 2, "items/lock") },
        { new Tuple<int, int>(1, 2), new PromoterItem("<i>AraBAD</i> promoter\n(level 3)", "The <i>araBAD</i> promoter controls activity of genes needed to break down L-arabinose - a sugar used to thicken sauces. Arabinose turns on <i>araBAD</i> promoter.", "items/1_2", 3, "items/lock") },
        { new Tuple<int, int>(1, 3), new PromoterItem("<i>T7</i> promoter\n(level 4)", "The <i>T7</i> promoter is a switch that tells a special helper (an enzyme) to make copies of instructions (RNA) needed to build proteins. Lactose sugar found in milk or IPTG (molecule imitating lactose) turns on <i>T7</i> promoter.", "items/1_3", 4, "items/lock") },
        
        { new Tuple<int, int>(2, 0), new GeneItem("<i>Cry</i> gene\n(level 1)", "The <i>cry</i> gene produces Cry toxins, which are like tiny poison crystals for pests.", "items/2_0", ItemAttribute.Attack, 2, "items/attack") },
        { new Tuple<int, int>(2, 1), new GeneItem("<i>Cry</i> gene\n(level 2)", "The <i>cry</i> gene produces Cry toxins, which are like tiny poison crystals for pests.", "items/2_1", ItemAttribute.Attack, 4, "items/attack") },
        { new Tuple<int, int>(2, 2), new GeneItem("<i>Cry</i> gene\n(level 3)", "The <i>cry</i> gene produces Cry toxins, which are like tiny poison crystals for pests.", "items/2_2", ItemAttribute.Attack, 8, "items/attack") },
        { new Tuple<int, int>(2, 3), new GeneItem("<i>Cry</i> gene\n(level 4)", "The <i>cry</i> gene produces Cry toxins, which are like tiny poison crystals for pests.", "items/2_3", ItemAttribute.Attack, 16, "items/attack") },
        
        { new Tuple<int, int>(3, 0), new GeneItem("<i>NifH</i> gene\n(level 1)", "The <i>nifH</i> gene makes a hardworking iron protein found in the cytoplasm of nitrogen-fixing bacteria. It fuels nitrogenase with energy to convert nitrogen gas into usable ammonia for growth!", "items/3_0", ItemAttribute.HP, 10, "items/hp") },
        { new Tuple<int, int>(3, 1), new GeneItem("<i>NifH</i> gene\n(level 2)", "The <i>nifH</i> gene makes a hardworking iron protein found in the cytoplasm of nitrogen-fixing bacteria. It fuels nitrogenase with energy to convert nitrogen gas into usable ammonia for growth!", "items/3_1", ItemAttribute.HP, 20, "items/hp") },
        { new Tuple<int, int>(3, 2), new GeneItem("<i>NifH</i> gene\n(level 3)", "The <i>nifH</i> gene makes a hardworking iron protein found in the cytoplasm of nitrogen-fixing bacteria. It fuels nitrogenase with energy to convert nitrogen gas into usable ammonia for growth!", "items/3_2", ItemAttribute.HP, 40, "items/hp") },
        { new Tuple<int, int>(3, 3), new GeneItem("<i>NifH</i> gene\n(level 4)", "The <i>nifH</i> gene makes a hardworking iron protein found in the cytoplasm of nitrogen-fixing bacteria. It fuels nitrogenase with energy to convert nitrogen gas into usable ammonia for growth!", "items/3_3", ItemAttribute.HP, 80, "items/hp") },
        
        { new Tuple<int, int>(4, 0), new GeneItem("<i>Spo0A</i> gene\n(level 1)", "The <i>spo0A</i> gene encodes a protein that is the key player in starting sporulation when the food runs out. It helps bacteria to survive harsh conditions.", "items/4_0", ItemAttribute.Regen, 1, "items/hp_regen") },
        { new Tuple<int, int>(4, 1), new GeneItem("<i>Spo0A</i> gene\n(level 2)", "The <i>spo0A</i> gene encodes a protein that is the key player in starting sporulation when the food runs out. It helps bacteria to survive harsh conditions.", "items/4_1", ItemAttribute.Regen, 2, "items/hp_regen") },
        { new Tuple<int, int>(4, 2), new GeneItem("<i>Spo0A</i> gene\n(level 3)", "The <i>spo0A</i> gene encodes a protein that is the key player in starting sporulation when the food runs out. It helps bacteria to survive harsh conditions.", "items/4_2", ItemAttribute.Regen, 3, "items/hp_regen") },
        { new Tuple<int, int>(4, 3), new GeneItem("<i>Spo0A</i> gene\n(level 4)", "The <i>spo0A</i> gene encodes a protein that is the key player in starting sporulation when the food runs out. It helps bacteria to survive harsh conditions.", "items/4_3", ItemAttribute.Regen, 4, "items/hp_regen") },
    };
}