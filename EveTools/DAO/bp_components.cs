namespace EveTools.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eve.bp_components")]
    public partial class bp_components
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? bp_act_id { get; set; }

        public int? item_id { get; set; }

        public long? count { get; set; }
    }
}
