﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveTools.DataModels
{
    public class PI
    {
        public string name;
        public int id;
        public long count;

        public PI(string name, int id, long count)
        {
            this.id = id;
            this.name = name;
            this.count = count;
        }
    }
}
