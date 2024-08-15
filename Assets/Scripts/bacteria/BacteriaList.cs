using System.Collections.Generic;
using UnityEngine;

namespace bacteria
{
    public class BacteriaList : MonoBehaviour
    {
        public static List<BacteriaInfo> bacterias = new List<BacteriaInfo>
        {
            new BacteriaInfo(1, "Vibrio cholerae", "I am Vibrio cholerae - I cause cholera, a severe diarrheal disease. I spread through contaminated water and can lead to rapid dehydration.", new List<Surface> { Surface.Water }),
            new BacteriaInfo(2, "Streptococcus pyogenes", "I am Streptococcus pyogenes - I cause a range of infections, from strep throat to life-threatening conditions like necrotizing fasciitis and rheumatic fever.", new List<Surface>{ Surface.Human }),
            new BacteriaInfo(3, "Bacillus cereus", "I am Bacillus cereus, found in soil and animals. I produce toxins that cause food poisoning.", new List<Surface>{ Surface.Soil, Surface.Plant, Surface.Food }),
            new BacteriaInfo(4, "Enterococcus faecalis", "I am Enterococcus faecalis - I am naturally found in intestines. Although usually harmless, I have the potential to cause a serious infection if I spread to other areas of the body.", new List<Surface>{ Surface.Human }),
            new BacteriaInfo(5, "Leptospira interrogans", "I am Leptospira interrogans - I make animals sick. I am transmitted through contact with water contaminated by the urine of infected animals, highlighting the need for good sanitation practices.", new List<Surface>{ Surface.Water, Surface.Animal }),
        };
    }
}