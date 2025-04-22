using CVOIS.Models.Viewers;
using Microsoft.EntityFrameworkCore;

namespace CVOIS.Models
{
    public class cvois_context : DbContext
    {
        public DbSet<LoginModel> loginModel { get; set; }
        //public virtual DbSet<Appointing_Authority_Model> appointingAuthorities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginModel>().ToTable("tbl_MasterUser");
            //modelBuilder.Entity<Appointing_Authority_Model>().ToTable("tbl_masterAppointingAuthority");
            
            base.OnModelCreating(modelBuilder);
        }
        public cvois_context(DbContextOptions<cvois_context> options)
      : base(options)
        {


        }
    }
}
