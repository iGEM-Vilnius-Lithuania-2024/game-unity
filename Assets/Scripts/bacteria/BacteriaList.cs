using System.Collections.Generic;
using UnityEngine;

namespace bacteria
{
    public class BacteriaList : MonoBehaviour
    {
        public static List<BacteriaInfo> bacterias = new List<BacteriaInfo>
        {
            new BacteriaInfo(1, "Actinomyces israelii", "I am Actinomyces israelii, found in the human mouth. I cause infections leading to painful abscesses and swelling.", new List<Surface>{ Surface.Human }),
            new BacteriaInfo(2, "Bacillus anthracis", "I am Bacillus anthracis, found in soil and animals. I form resilient spores that can survive in harsh environments.", new List<Surface>{ Surface.Soil, Surface.Animal }),
            new BacteriaInfo(3, "Bacillus cereus", "I am Bacillus cereus, found in soil and animals. I produce toxins that cause food poisoning.", new List<Surface>{ Surface.Soil, Surface.Plant, Surface.Food }),
            new BacteriaInfo(4, "Borrelia burgdorferi", "I am Borrelia burgdorferi - I live in ticks and cause Lyme disease, leading to red rashes on the skin, neurological and cardiac issues.", new List<Surface> { Surface.Animal }),
            new BacteriaInfo(5, "Campylobacter jejuni", "I am Campylobacter jejuni, found in uncooked poultry. I cause fever and abdominal pain after consumption.", new List<Surface>{ Surface.Food, Surface.Animal }),
            new BacteriaInfo(6, "Campylobacter coli", "I am Campylobacter coli, found in contaminated food or water. I cause fever and abdominal pain after ingestion.", new List<Surface>{ Surface.Food, Surface.Animal }),
            new BacteriaInfo(7, "Campylobacter fetus", "I am Campylobacter fetus - I infect livestock, causing reproductive issues.", new List<Surface>{ Surface.Animal }),
            new BacteriaInfo(8, "Enterococcus faecalis", "I am Enterococcus faecalis - I am naturally found in intestines. Although usually harmless, I have the potential to cause a serious infection if I spread to other areas of the body.", new List<Surface>{ Surface.Human }),
            new BacteriaInfo(9, "Enterococcus faecium", "I am Enterococcus faecium - naturally found in intestines but I can cause infections. My high antibiotic resistance makes me a challenge in clinical environmets.", new List<Surface>{ Surface.Human }),
            new BacteriaInfo(10, "Leptospira interrogans", "I am Leptospira interrogans - I make animals sick. I am transmitted through contact with water contaminated by the urine of infected animals, highlighting the need for good sanitation practices.", new List<Surface>{ Surface.Water, Surface.Animal }),
            new BacteriaInfo(11, "Neisseria meningitidis", "I am Neisseria meningitidis - the main cause of meningitis and septicemia. I pose a serious threat to children and young adults.", new List<Surface>{ Surface.Human }),
            new BacteriaInfo(12, "Rickettsia rickettsii", "I am Rickettsia rickettsii - I cause Rocky Mountain spotted fever, a serious tick-borne disease. My infection leads to fever and rash.", new List<Surface>{ Surface.Animal }),
            new BacteriaInfo(13, "Rickettsia prowazekii", "I am Rickettsia prowazekii - I cause epidemic typhus, a serious disease transmitted by lice. I can lead to high fever, rash, and severe complications if untreated.", new List<Surface>{ Surface.Animal }),
            new BacteriaInfo(14, "Staphylococcus aureus", "I am Staphylococcus aureus - a common cause of skin infections, pneumonia, and food poisoning. I am notorious for my resistance to antibiotics, which pose significant challenges in healthcare.", new List<Surface>{ Surface.Human, Surface.Animal }),
            new BacteriaInfo(15, "Staphylococcus epidermidis", "I am Staphylococcus epidermidis - I am usually harmless and normally found on the skin, but I can cause infections in people with weakened immune systems, particularly those with medical devices like catheters.", new List<Surface>{ Surface.Human }),
            new BacteriaInfo(16, "Staphylococcus saprophyticus", "I am Staphylococcus saprophyticus - a common cause of urinary tract infections, especially in young women.", new List<Surface>{ Surface.Food, Surface.Human }),
            new BacteriaInfo(17, "Streptococcus pyogenes", "I am Streptococcus pyogenes - I cause a range of infections, from strep throat to life-threatening conditions like necrotizing fasciitis and rheumatic fever.", new List<Surface>{ Surface.Human }),
            new BacteriaInfo(18, "Streptococcus pneumoniae", "I am Streptococcus pneumoniae - I am a leading cause of pneumonia, meningitis, and ear infections, particularly in children and the elderly.", new List<Surface>{ Surface.Human }),
            new BacteriaInfo(19, "Streptococcus agalactiae", "I am Streptococcus agalactiae - also known as Group B Streptococcus, I can cause serious infections in newborns, pregnant women, and adults with underlying health conditions.", new List<Surface>{ Surface.Human }),
            new BacteriaInfo(20, "Streptococcus mutans", "I am Streptococcus mutans - I am found in the human mouth and play a key role in tooth decay by converting sugars into acid that erodes tooth enamel.", new List<Surface>{ Surface.Human }),
            new BacteriaInfo(21, "Vibrio cholerae", "I am Vibrio cholerae - I cause cholera, a severe diarrheal disease. I spread through contaminated water and can lead to rapid dehydration.", new List<Surface> { Surface.Water }),
            new BacteriaInfo(22, "Vibrio parahaemolyticus", "I am Vibrio parahaemolyticus - I cause stomach ache and diarrhea, often linked to eating raw or undercooked seafood. I am common in coastal waters.", new List<Surface> { Surface.Water, Surface.Animal }),
            new BacteriaInfo(23, "Vibrio vulnificus", "I am Vibrio vulnificus - I can cause severe infections, especially through open wounds or by consuming contaminated seafood. I am particularly dangerous to people with weakened immune systems.", new List<Surface> { Surface.Water }),
        };
    }
}