using Microsoft.EntityFrameworkCore;
using BooksApp.Models;

namespace BooksApp.Data;

public class BooksContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TitleTag> TitleTags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=data/books.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Cambiar nombre de tabla TitleTag a TitlesTags
        modelBuilder.Entity<TitleTag>().ToTable("TitlesTags");
        
        // Definir orden de columnas en Title
        modelBuilder.Entity<Title>(entity =>
        {
            entity.Property(e => e.TitleId).HasColumnOrder(0);
            entity.Property(e => e.AuthorId).HasColumnOrder(1);
            entity.Property(e => e.TitleName).HasColumnOrder(2);
        });
    }
}