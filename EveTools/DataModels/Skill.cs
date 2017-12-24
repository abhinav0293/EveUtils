using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveTools.DataModels
{
    public class Skill
    {
        public int skillId;
        public string skillName;
        public int level;

        public Skill(int id, string name, int level)
        {
            skillId = id;
            skillName = name;
            this.level = level;
        }
    }
}
