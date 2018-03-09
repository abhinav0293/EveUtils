using System.Collections.Generic;
using System.Linq;
using EveTools.Utils;
using Newtonsoft.Json;

namespace EveTools.DataModels
{
    public class Project
    {
        public Blueprint original;
        public Blueprint current;
        public string name;

        [JsonIgnore]
        public List<Blueprint> rollback = null;
        [JsonIgnore]
        public List<Blueprint> redo = null;

        public Project(Blueprint bp, string name)
        {
            original = bp;
            current = ObjectCloning.CloneJson(bp);
            this.name = name;
        }

        public Project()
        {

        }

        public void addToRollbackQueue()
        {
            Blueprint bpc = ObjectCloning.CloneJson(current);
            if (rollback == null)
            {
                rollback = new List<Blueprint>();
            }
            rollback.Add(bpc);
        }

        public Blueprint getLastChange()
        {
            if (rollback == null)
            {
                return null;
            }
            if (redo == null)
            {
                redo = new List<Blueprint>();
            }
            redo.Add(ObjectCloning.CloneJson(current));
            Blueprint bpc = rollback.Last();
            rollback.Remove(bpc);
            if (rollback.Count == 0)
            {
                rollback = null;
            }
            return bpc;
        }

        public Blueprint discardChange()
        {
            Blueprint bpc = rollback.Last();
            rollback.Remove(bpc);
            return bpc;
        }

        public Blueprint getLastRollback()
        {
            if (redo == null)
            {
                return null;
            }
            if (rollback == null)
            {
                rollback = new List<Blueprint>();
            }
            rollback.Add(ObjectCloning.CloneJson(current));
            Blueprint bpo = redo.Last();
            redo.Remove(bpo);
            if (redo.Count == 0)
            {
                redo = null;
            }
            return bpo;
        }

        public void reset() => current = ObjectCloning.CloneJson(original);
    }
}
