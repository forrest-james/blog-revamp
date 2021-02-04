using Domain.Entities;
using Domain.Objects;
using Domain.Persistence.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.Persistence
{
    public class ApplicationDbContext
        : IdentityDbContext<BlogUser, BlogRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Post>()
                .HasMany(post => post.Comments)
                .WithOne(comment => comment.Parent);
            builder.Entity<Post>()
                .HasMany(post => post.Reactions)
                .WithOne();
            builder.Entity<Reaction>()
                .Property(reaction => reaction.Parent)
                .HasConversion(
                    parent => parent.Id,
                    id => Posts.Find(id)
                );
            builder.Entity<Reaction>()
                .HasKey(reaction => new
                {
                    reaction.Parent,
                    reaction.IpAddress
                });

        }
    }
}