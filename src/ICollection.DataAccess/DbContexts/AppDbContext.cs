using ICollection.Domain.Entities.Admins;
using ICollection.Domain.Entities.Collections;
using ICollection.Domain.Entities.Comments;
using ICollection.Domain.Entities.CustomFields;
using ICollection.Domain.Entities.Items;
using ICollection.Domain.Entities.Likes;
using ICollection.Domain.Entities.Tags;
using ICollection.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace ICollection.DataAccess.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = default!;
        public virtual DbSet<Collection> Collections { get; set; } = default!;
        public virtual DbSet<Comment> Comments { get; set; } = default!;
        public virtual DbSet<CustomField> CustomFields { get; set; } = default!;
        public virtual DbSet<Item> Items { get; set; } = default!;
        public virtual DbSet<Like> Likes { get; set; } = default!;
        public virtual DbSet<LikeItem> LikeItems { get; set; } = default!;
        public virtual DbSet<Tag> Tags { get; set; } = default!;
        public virtual DbSet<User> Users { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new SuperAdminConfiguration());
        }
    }
}
