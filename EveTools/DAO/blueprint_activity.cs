namespace EveTools.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eve.blueprint_activity")]
    public partial class blueprint_activity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? bp_id { get; set; }

        public int? activity_id { get; set; }

        public int? product_id { get; set; }

        public int? time { get; set; }
    }
}
