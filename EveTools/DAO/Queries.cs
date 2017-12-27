using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveTools.DataModels;

namespace EveTools.DAO
{
    public class Queries
    {

        #region singleton_crap
        private Queries()
        {

        }

        private static Queries instance=null;
        private EveModel eve = new EveModel();

        public static Queries getInstance()
        {
            if (instance == null)
                instance = new Queries();
            return instance;
        }
        #endregion

        #region app_init_methods
        public List<string> getManuList()
        {
            List<string> ret = new List<string>();
            var query = from ct
                        in eve.blueprint_activity
                        where ct.activity_id == 1
                        select ct;
            foreach(blueprint_activity bpa in query.ToList())
            {
                var bq = from ct
                         in eve.blueprints
                         where bpa.bp_id == ct.id
                         select ct.blueprintName;
                try
                {
                    string st = bq.Single();
                    st = st.Replace(" Blueprint", "");
                    ret.Add(st);
                }
                catch
                {
                    continue;
                }
            }
            ret.Sort();
            return ret;
        }

        public List<string> getInvList()
        {
            List<string> ret = new List<string>();
            var query = from ct
                        in eve.blueprint_activity
                        where ct.activity_id == 8
                        select ct;
            foreach (blueprint_activity bpa in query.ToList())
            {
                var bq = from ct
                         in eve.items
                         where bpa.product_id == ct.id
                         select ct.name;
                try
                {
                    ret.Add(bq.Single());
                }
                catch
                {
                    continue;
                }
            }
            ret.Sort();
            return ret;
        }
        #endregion

        #region job_requests
        public List<Component> getCompList(String name, int activity)
        {
            var bpq = from ct
                      in eve.blueprints
                      where ct.blueprintName.StartsWith(name)
                      select ct;
            blueprint bp = bpq.ToList()[0];

            var bpaq = from ct
                       in eve.blueprint_activity
                       where ct.bp_id == bp.id && ct.activity_id == activity
                       select ct;
            blueprint_activity bpa = bpaq.Single();

            var bpcq = from ct
                       in eve.bp_components
                       where ct.bp_act_id == bpa.id
                       select ct;
            List<bp_components> bpcl = bpcq.ToList();

            List<Component> comps = new List<Component>();
            foreach(bp_components bpc in bpcl)
            {
                string compName = getItemName((int)bpc.item_id);
                Component comp = new Component((int)bpc.item_id, compName, (long)bpc.count, checkBlueprint(compName));
                comps.Add(comp);
            }
            return comps;
        }

        public void getInventionDetails(int id, Invention i)
        {
            var query = from ct
                        in eve.blueprint_activity
                        where ct.product_id == id && ct.activity_id == 8
                        select ct;
            blueprint_activity ba = query.Single();

            var cquery = from ct
                         in eve.bp_components
                         where ct.bp_act_id == ba.id
                         select ct;
            List<bp_components> comps = cquery.ToList();

            i.in_bp = getItemName((int)ba.bp_id);
            i.product = getItemName(id);

            foreach (bp_components comp in comps)
            {
                Other o = new Other(getItemName((int)comp.item_id), (int)comp.item_id, (long)comp.count);
                i.reqs.Add(o);
            }

            var squery = from ct
                         in eve.blueprint_skills
                         where ct.bp_act_id == ba.id
                         select ct;
            List<blueprint_skills> bs = squery.ToList();

            foreach (blueprint_skills b in bs)
            {
                Skill s = new Skill((int)b.skill_id, getItemName((int)b.skill_id), (int)b.level);
                i.reqSkills.Add(s);
            }
        }
        #endregion

        #region item_handling
        public string getItemName(int id)
        {
            return getItem(id).name;
        }

        public item getItem(int id)
        {
            var query = from ct
                        in eve.items
                        where ct.id == id
                        select ct;
            return query.Single();
        }

        public int getItemId(string name)
        {
            var query = from ct
                        in eve.items
                        where ct.name.Equals(name)
                        select ct.id;
            return (int)query.Single();
        }
        #endregion

        #region checks
        public bool checkBlueprint(string name)
        {
            name += " Blueprint";
            try
            {
                var query = from ct
                            in eve.blueprints
                            where ct.blueprintName.Equals(name)
                            select ct;
                query.Single();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public string checkType(int id)
        {
            item i = getItem(id);
            if (i.Group.Equals("Mineral"))
                return "Mineral";
            if (i.Category.Equals("Planetary Commodities"))
                return "PI";
            if (i.Group.Equals("Moon Materials") || i.Group.Equals("Intermediate Materials")
                || i.Group.Equals("Composite"))
                return "Moon";
            return "Other";
        }
        #endregion

        #region skills
        public List<Skill> getBPSkills(string name, int activity)
        {
            var bpq = from ct
                      in eve.blueprints
                      where ct.blueprintName.StartsWith(name)
                      select ct;
            blueprint bp = bpq.ToList()[0];

            var bpaq = from ct
                       in eve.blueprint_activity
                       where ct.bp_id == bp.id && ct.activity_id == activity
                       select ct;
            blueprint_activity bpa = bpaq.Single();

            var bpsq = from ct
                       in eve.blueprint_skills
                       where ct.bp_act_id == bpa.id
                       select ct;
            List<blueprint_skills> bsl = bpsq.ToList();

            List<Skill> skillList = new List<Skill>();
            foreach(blueprint_skills bs in bsl)
            {
                string sname = getItemName((int)bs.skill_id);
                Skill s = new Skill((int)bs.skill_id,sname,(int)bs.level);
                skillList.Add(s);
            }
            return skillList;
        }

        public List<Skill> getSkillReq(int id)
        {
            var query = from ct
                        in eve.skills
                        where ct.skill_id == id
                        select ct;
            try
            {
                List<Skill> ls = new List<Skill>();
                List<skill> s = query.ToList();
                if (s.Count > 0)
                {
                    foreach (skill st in s)
                    {

                        Skill sk = new Skill((int)st.reqSkill_id, getItemName((int)st.reqSkill_id), (int)st.level);
                        ls.Add(sk);
                    }
                    return ls;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
