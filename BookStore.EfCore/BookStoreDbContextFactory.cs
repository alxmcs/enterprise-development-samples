using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookStore.EfCore;
public class BookStoreDbContextFactory : IDesignTimeDbContextFactory<BookStoreDbContext>
{
    public BookStoreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BookStoreDbContext>();
        optionsBuilder.UseSqlite("Data Source=BookStore.db;");
        return new BookStoreDbContext(optionsBuilder.Options);
    }
}
