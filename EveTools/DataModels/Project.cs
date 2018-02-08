using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveTools.DataModels
{
    public class Project
    {
        public Blueprint original;
        public Blueprint current;
        public string name;

        public Project(Blueprint bp, string name)
        {
            original = bp;
            current = bp;
            this.name = name;
        }

        public Project()
        {

        }
    }
}
