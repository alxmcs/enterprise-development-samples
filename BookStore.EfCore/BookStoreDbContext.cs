using BookStore.Domain.Data;
using BookStore.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStore.EfCore;

public class BookStoreDbContext : DbContext
{
    public DbSet<Book>? Books { get; set; }
    public DbSet<Author>? Authors { get; set; }
    public DbSet<BookAuthor>? BookAuthors { get; set; }

    public BookStoreDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(DataSeeder.Books);
        modelBuilder.Entity<Author>().HasData(DataSeeder.Authors);
        modelBuilder.Entity<BookAuthor>().HasData(DataSeeder.BookAuthors);
    }
}
