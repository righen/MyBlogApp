using Microsoft.EntityFrameworkCore;
using MyBlogApp.Domain.Entities;

namespace MyBlogApp.Infrastructure.Data;

public class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>(builder =>
        {
            builder.HasKey(p => p.Id);

            // Configure the Title value object
            builder.OwnsOne(p => p.Title, titleBuilder =>
            {
                titleBuilder.Property(t => t.Value)
                    .HasMaxLength(200)
                    .HasColumnName("Title")
                    .IsRequired();
            });

            // Configure the Content value object
            builder.OwnsOne(p => p.Content, contentBuilder =>
            {
                contentBuilder.Property(c => c.Value)
                    .HasMaxLength(1000)
                    .HasColumnName("Content")
                    .IsRequired();
            });

            // Configure the CreatedAt property
            builder.Property(p => p.CreatedAt)
                .IsRequired();
        });
    }
}