namespace EveTools.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eve.blueprint_skills")]
    public partial class blueprint_skills
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? bp_act_id { get; set; }

        public int? skill_id { get; set; }

        public int? level { get; set; }
    }
}
