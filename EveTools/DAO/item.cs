namespace EveTools.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eve.item")]
    public partial class item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Group { get; set; }

        [StringLength(255)]
        public string Category { get; set; }

        public double? volume { get; set; }
    }
}
