using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveTools.DAO;

namespace EveTools.DataModels
{
    public class Invention
    {

        public List<Other> reqs = new List<Other>();
        public List<Skill> reqSkills = new List<Skill>();
        int count = 0;
        public string product;
        public string in_bp;

        public Invention(int outputId, int count)
        {
            this.count = count;
            Queries.getInstance().getInventionDetails(outputId, this);
            foreach(Other o in reqs)
            {
                o.count = o.count * count;
            }
        }
    }
}
