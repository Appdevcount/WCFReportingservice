namespace WCFReportingservice
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WCFEmployee_CFEXDB_DBContext : DbContext
    {
        public WCFEmployee_CFEXDB_DBContext()
            : base("name=WCFEmployee_CFEXDB_DBContext")
        {
        }

        public virtual DbSet<WCFEmployee> WCFEmployees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
