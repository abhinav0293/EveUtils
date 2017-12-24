namespace EveTools.DAO
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EveModel : DbContext
    {
        public EveModel()
            : base("name=EveMod")
        {
        }

        public virtual DbSet<blueprint> blueprints { get; set; }
        public virtual DbSet<blueprint_activity> blueprint_activity { get; set; }
        public virtual DbSet<blueprint_skills> blueprint_skills { get; set; }
        public virtual DbSet<bp_components> bp_components { get; set; }
        public virtual DbSet<item> items { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<blueprint>()
                .Property(e => e.blueprintName)
                .IsUnicode(false);

            modelBuilder.Entity<item>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<item>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<item>()
                .Property(e => e.Group)
                .IsUnicode(false);

            modelBuilder.Entity<item>()
                .Property(e => e.Category)
                .IsUnicode(false);
        }
    }
}
