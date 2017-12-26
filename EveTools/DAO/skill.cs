namespace EveTools.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eve.skills")]
    public partial class skill
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? skill_id { get; set; }

        public int? reqSkill_id { get; set; }

        public int? level { get; set; }
    }
}
