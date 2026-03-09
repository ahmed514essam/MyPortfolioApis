using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyPortfolioApis.Models;

namespace MyPortfolioApis.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<About> About { get; set; }
        public DbSet<Home> Home { get; set; }
        public DbSet<MySkills> MySkills { get; set; }
        public DbSet<MyCertificates> MyCertificates { get; set; }
        public DbSet<MyProjects> Projects { get; set; }
        public DbSet<MyImages> MyImages { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasSequence<int>("DeliveryOrder").StartsAt(100).IncrementsBy(1);
            //Fluent API configurations
            var about = builder.Entity<About>();
            about.HasKey(a => a.Id);
            var home = builder.Entity<Home>();
            home.HasKey(a => a.Id);
            var skill = builder.Entity<MySkills>();
            skill.HasKey(a => a.Id);
            var certificates = builder.Entity<MyCertificates>();
            certificates.HasKey(a => a.Id);
            var projects = builder.Entity<MyProjects>();
            projects.HasKey(a => a.Id);
            var images = builder.Entity<MyImages>();
            images.HasKey(a => a.Id);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            ;


        }
    }
}
