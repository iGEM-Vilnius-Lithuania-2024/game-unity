using System.Collections.Generic;

namespace bacteria
{
    public class BacteriaInfo
    {
        public int id;
        public string name;
        public string description;
        public List<Surface> surfaces;

        public BacteriaInfo(int id, string name, string description, List<Surface> surfaces)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.surfaces = surfaces;
        }
    }
}