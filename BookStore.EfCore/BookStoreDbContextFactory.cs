using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookStore.EfCore;
public class BookStoreDbContextFactory : IDesignTimeDbContextFactory<BookStoreDbContext>
{
    public BookStoreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BookStoreDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=55432;Database=lecture;User ID=postgres;Password=1;");
        return new BookStoreDbContext(optionsBuilder.Options);
    }
}
