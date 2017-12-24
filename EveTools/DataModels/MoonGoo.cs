using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveTools.DataModels
{
    public class MoonGoo
    {
        public string name;
        public int id;
        public long count;

        public MoonGoo(string name, int id, long count)
        {
            this.name = name;
            this.id = id;
            this.count = count;
        }
    }
}
