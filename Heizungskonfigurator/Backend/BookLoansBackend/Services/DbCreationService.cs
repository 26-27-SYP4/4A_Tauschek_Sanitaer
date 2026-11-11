namespace BookLoansBackend.Services;

public class DbCreationService(IServiceProvider serviceProvider) : BackgroundService
{
  protected override Task ExecuteAsync(CancellationToken stoppingToken)
  {
    using var scope = serviceProvider.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<BookLoansContext>();
    //Console.WriteLine("db.Database.EnsureDeleted"); db.Database.EnsureDeleted();
    Console.WriteLine("db.Database.EnsureCreated"); db.Database.EnsureCreated();
    if (db.Books.Any()) return Task.CompletedTask;

    SeedAuthors(db);
    SeedBooks(db);
    SeedLoans(db);
    return Task.CompletedTask;
  }

  private static void SeedLoans(BookLoansContext db)
  {
    db.Loans.AddRange(
      new Loan { BookId = 1, BorrowerName = "Mia Berger", BorrowedAt = DateTime.UtcNow.AddDays(-12), ReturnedAt = DateTime.UtcNow.AddDays(-2) },
      new Loan { BookId = 3, BorrowerName = "Noah Wagner", BorrowedAt = DateTime.UtcNow.AddDays(-7) },
      new Loan { BookId = 3, BorrowerName = "Lea Huber", BorrowedAt = DateTime.UtcNow.AddDays(-3) },
      new Loan { BookId = 5, BorrowerName = "Elias Gruber", BorrowedAt = DateTime.UtcNow.AddDays(-9) },
      new Loan { BookId = 7, BorrowerName = "Anna Maier", BorrowedAt = DateTime.UtcNow.AddDays(-5) });
    db.SaveChanges();
    Console.WriteLine($"Nr loans in Db: {db.Loans.Count()}");
  }

  private static void SeedBooks(BookLoansContext db)
  {
    db.Books.AddRange(
      new Book { Title = "Pride and Prejudice", AuthorId = 1, Year = 1813, Isbn = "978-0-141-43951-8", Description = "A witty novel about love, family and first impressions.", TotalCopies = 4 },
      new Book { Title = "Emma", AuthorId = 1, Year = 1815, Isbn = "978-0-141-43958-7", Description = "A spirited matchmaker learns about empathy and affection.", TotalCopies = 3 },
      new Book { Title = "1984", AuthorId = 2, Year = 1949, Isbn = "978-0-452-28423-4", Description = "A chilling vision of surveillance and individual freedom.", TotalCopies = 5 },
      new Book { Title = "Animal Farm", AuthorId = 2, Year = 1945, Isbn = "978-0-452-28424-1", Description = "A political fable set on an English farm.", TotalCopies = 3 },
      new Book { Title = "Kindred", AuthorId = 3, Year = 1979, Isbn = "978-0-8070-8369-7", Description = "A woman is repeatedly pulled from 1970s California to antebellum Maryland.", TotalCopies = 4 },
      new Book { Title = "Parable of the Sower", AuthorId = 3, Year = 1993, Isbn = "978-1-58322-804-7", Description = "A young woman builds a community amid a collapsing society.", TotalCopies = 2 },
      new Book { Title = "The Colour of Magic", AuthorId = 4, Year = 1983, Isbn = "978-0-552-12584-1", Description = "The first humorous journey through Discworld.", TotalCopies = 4 },
      new Book { Title = "Mort", AuthorId = 4, Year = 1987, Isbn = "978-0-552-13319-8", Description = "An apprentice discovers the surprising realities of working for Death.", TotalCopies = 3 },
      new Book { Title = "Small Gods", AuthorId = 4, Year = 1992, Isbn = "978-0-552-13555-0", Description = "A satire about belief, philosophy and a very small god.", TotalCopies = 2 },
      new Book { Title = "Sense and Sensibility", AuthorId = 1, Year = 1811, Isbn = "978-0-141-43966-2", Description = "Two sisters navigate love and financial uncertainty.", TotalCopies = 3 },
      new Book { Title = "Homage to Catalonia", AuthorId = 2, Year = 1938, Isbn = "978-0-141-18435-1", Description = "Orwell's account of the Spanish Civil War.", TotalCopies = 2 },
      new Book { Title = "Fledgling", AuthorId = 3, Year = 2005, Isbn = "978-0-446-69151-9", Description = "A coming-of-age story with a speculative twist.", TotalCopies = 2 });
    db.SaveChanges();
    Console.WriteLine($"Nr books in Db: {db.Books.Count()}");
  }

  private static void SeedAuthors(BookLoansContext db)
  {
    var authors = new[]
    {
      new Author { Name = "Jane Austen" },
      new Author { Name = "George Orwell" },
      new Author { Name = "Octavia E. Butler" },
      new Author { Name = "Terry Pratchett" }
    };
    db.Authors.AddRange(authors);
    db.SaveChanges();
    Console.WriteLine($"Nr authors in Db: {db.Authors.Count()}");
  }
}
