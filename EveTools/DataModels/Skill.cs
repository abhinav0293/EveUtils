using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveTools.DAO;

namespace EveTools.DataModels
{
    public class Skill
    {
        public int skillId;
        public string skillName;
        public int level;
        public List<Skill> reqSkills;
        public bool hasReqSkills = false;

        public Skill(int id, string name, int level)
        {
            skillId = id;
            skillName = name;
            this.level = level;
            getReqSkills();
        }

        private void getReqSkills()
        {
            reqSkills = Queries.getInstance().getSkillReq(skillId);
            if (reqSkills != null)
                hasReqSkills = true;
        }
    }
}
