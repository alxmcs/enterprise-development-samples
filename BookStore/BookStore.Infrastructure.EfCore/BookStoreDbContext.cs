using BookStore.Domain.Data;
using BookStore.Domain.Model.Authors;
using BookStore.Domain.Model.BookAuthors;
using BookStore.Domain.Model.Books;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.EfCore;
public class BookStoreDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(builder =>
        {
            builder.HasKey(b => b.Id);
            builder.HasMany(b => b.BookAuthors).WithOne(ba => ba.Book).IsRequired(false);
            builder.HasData(DataSeeder.Books);
        });

        modelBuilder.Entity<Author>(builder =>
        {
            builder.HasKey(a => a.Id);
            builder.HasMany(a => a.BookAuthors).WithOne(bs => bs.Author).IsRequired(false);
            builder.HasData(DataSeeder.Authors);
        });

        modelBuilder.Entity<BookAuthor>(builder =>
        {
            builder.HasKey(ba => ba.Id);
            builder.HasOne(ba => ba.Author).WithMany(b => b.BookAuthors).IsRequired();
            builder.HasOne(ba => ba.Book).WithMany(b => b.BookAuthors).IsRequired();
            builder.HasData(DataSeeder.BookAuthors);
        });

    }
}
