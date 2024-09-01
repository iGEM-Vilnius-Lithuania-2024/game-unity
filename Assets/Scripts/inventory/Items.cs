using System;
using System.Collections.Generic;
using Mapbox.Map;

public class Items
{
    public static Dictionary<Tuple<int, int>, Item> items = new Dictionary<Tuple<int, int>, Item>
    {
        { new Tuple<int, int>(0, 0), new OriItem("F1 ori", "Ori 1 description", "items/0_0", 1.5f) },
        { new Tuple<int, int>(0, 1), new OriItem("RK2 ori", "Ori 2 description", "items/0_1", 2) },
        { new Tuple<int, int>(0, 2), new OriItem("p15A ori", "Ori 3 description", "items/0_2", 3) },
        { new Tuple<int, int>(0, 3), new OriItem("pMB1 ori", "Ori 4 description", "items/0_3", 4) },
        
        { new Tuple<int, int>(1, 0), new PromoterItem("Constitutive promoter", "Promoter description", "items/1_0", 1, "items/lock") },
        { new Tuple<int, int>(1, 1), new PromoterItem("T7 lac promoter", "Promoter description", "items/1_1", 2, "items/lock") },
        { new Tuple<int, int>(1, 2), new PromoterItem("araBAD promoter", "Promoter description", "items/1_2", 3, "items/lock") },
        { new Tuple<int, int>(1, 3), new PromoterItem("lac promoter", "Promoter description", "items/1_3", 4, "items/lock") },
        
        { new Tuple<int, int>(2, 0), new GeneItem("VirA", "The gyrB gene codes for a key part of DNA gyrase, a superpowered enzyme in bacteria that twists their DNA for replication and transcription.", "items/2_0", ItemAttribute.Attack, 1, "items/attack") },
        { new Tuple<int, int>(2, 1), new GeneItem("Cry gene", "The cry gene produces Cry toxins, which are like tiny poison crystals for pests.", "items/2_1", ItemAttribute.Attack, 2, "items/attack") },
        { new Tuple<int, int>(2, 2), new GeneItem("GluE gene", "The gluE gene produces glutamate synthase, the enzyme that creates glutamate. This essential amino acid acts as a key player in various metabolic processes, helping the cell in good shape!", "items/2_2", ItemAttribute.Attack, 4, "items/attack") },
        { new Tuple<int, int>(2, 3), new GeneItem("BceA gene", "The bceA gene makes a protein that helps build the polysaccharide matrix of bacterial biofilms. This biofilm boosts Burkholderia cepacia's ability to persist and cause chronic infections, especially in cystic fibrosis patients.", "items/2_3", ItemAttribute.Attack, 6, "items/attack") },
        
        { new Tuple<int, int>(3, 0), new GeneItem("OmpA protein", "OmpA gene codes a gatekeeper protein found in cell membrane. It controls the passage of molecules into and out of the bacterium while ensuring its safety.", "items/3_0", ItemAttribute.HP, 5, "items/hp") },
        { new Tuple<int, int>(3, 1), new GeneItem("NifH", "The nifH gene makes a hardworking iron protein found in the cytoplasm of nitrogen-fixing bacteria. It fuels nitrogenase with energy to convert nitrogen gas into usable ammonia for growth!", "items/3_1", ItemAttribute.HP, 10, "items/hp") },
        { new Tuple<int, int>(3, 2), new GeneItem("FusA", "The fusA gene makes elongation factor G (EF-G), which helps the ribosome slide along mRNA. This keeps protein production flowing smoothly by adding amino acids to the growing chain!", "items/3_2", ItemAttribute.HP, 15, "items/hp") },
        { new Tuple<int, int>(3, 3), new GeneItem("PtxA gene", "The ptxA gene creates pertussis toxin, which is key to the bacterium's ability to cause disease. It acts as an ADP-ribosylating enzyme, modifying host cell signaling pathways and contributing to the disease symptoms.", "items/3_3", ItemAttribute.HP, 20, "items/hp") },
        
        { new Tuple<int, int>(4, 0), new GeneItem("GyrB", "The gyrB gene codes for a key part of DNA gyrase, a superpowered enzyme in bacteria that twists their DNA for replication and transcription. ", "items/4_0", ItemAttribute.Regen, 1, "items/hp_regen") },
        { new Tuple<int, int>(4, 1), new GeneItem("Spo0A", "The Spo0A gene encodes a protein that is the key player in starting sporulation when the food runs out. It helps bacteria to survive harsh conditions.", "items/4_1", ItemAttribute.Regen, 2, "items/hp_regen") },
        { new Tuple<int, int>(4, 2), new GeneItem("Hsp20 gene", "The hsp20 gene produces the small heat shock protein Hsp20, the ultimate troubleshooter. It keeps proteins in shape and functional, even when temperatures rise and things get heated!", "items/4_2", ItemAttribute.Regen, 3, "items/hp_regen") },
        { new Tuple<int, int>(4, 3), new GeneItem("CtrA gene", "The ctrA gene makes a protein that controls how Caulobacter crescentus moves through its different life stages. It helps manage DNA replication and cell division, making sure the bacterium develops correctly.", "items/4_3", ItemAttribute.Regen, 4, "items/hp_regen") },
    };
}