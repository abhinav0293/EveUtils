using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveTools.DataModels
{
    public class Component
    {

        public int compId;
        public string compName;
        public long compCount;
        public bool hasBlueprint;
        public Blueprint bp;

        public Component(int id, string name, long count, bool hasBlueprint)
        {
            compId = id;
            compName = name;
            compCount = count;
            this.hasBlueprint = hasBlueprint;
        }

        public Component()
        {

        }
    }
}
