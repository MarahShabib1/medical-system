using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data
{
    public class DataContext : DbContext
    {


        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>()
  .HasOne(b => b.user)
  .WithMany().HasForeignKey(b => b.Userid)
  .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Medicine>()
        .HasOne(b => b.company)
        .WithMany().HasForeignKey(b => b.Companyid)
        .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Employee>()
.HasOne(b => b.doctor)
.WithMany().HasForeignKey(b => b.Doctorid)
.OnDelete(DeleteBehavior.Restrict);


            builder.Entity<UserRole>()
     .HasKey(bc => new { bc.Userid, bc.Roleid });
           builder.Entity<UserRole>()
                .HasOne(bc => bc.user)
                .WithMany(b => b.user_role)
                .HasForeignKey(bc => bc.Userid);
            builder.Entity<UserRole>()
                .HasOne(bc => bc.role)
                .WithMany(c => c.user_role)
                .HasForeignKey(bc => bc.Roleid);
            base.OnModelCreating(builder);  


        }
        public DbSet<User> user1 { get; set; }
        public DbSet<Doctor> doctor { get; set; }
        public DbSet<Role> roles { get; set; }
      public DbSet<UserRole> user_role{ get; set; }
        public DbSet<Employee> employee { get; set; }
      public DbSet<Records> records { get; set; }
      public DbSet<Prescription> prescription { get; set; }
      public DbSet<Medicine> medicine { get; set; }
        public DbSet<Company> company { get; set; }
    }
}
