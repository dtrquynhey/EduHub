using EduHubLibrary.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHubLibrary.DataAccess
{
    public class EduHubDbContext : DbContext
    {
        public EduHubDbContext(DbContextOptions<EduHubDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<CampaignMember> CampaignMembers { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Engagement> Engagements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
