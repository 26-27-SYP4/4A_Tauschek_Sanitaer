using Microsoft.EntityFrameworkCore;

namespace BookLoansDb;

public class BookLoansContext(DbContextOptions<BookLoansContext> options) : DbContext(options)
{
  public DbSet<Author> Authors { get; set; }
  public DbSet<Book> Books { get; set; }
  public DbSet<Loan> Loans { get; set; }
}
