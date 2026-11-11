namespace BookLoansBackend.Services;

public class DbService(BookLoansContext db)
{
  public List<Author> GetAuthors() => db.Authors
    .OrderBy(x => x.Name)
    .ToList();

  public List<Book> GetBooks(string? search, int? authorId)
  {
    var books = db.Books
      .Include(x => x.Author)
      .Include(x => x.Loans)
      .Where(x => authorId == null || x.AuthorId == authorId.Value)
      .AsEnumerable()
      .Where(x => string.IsNullOrWhiteSpace(search) || x.Title.Contains(search))
      .OrderBy(x => x.Title)
      .ToList();
    return books;
  }

  public Book? GetBook(int id) => db.Books
    .Include(x => x.Author)
    .Include(x => x.Loans)
    .SingleOrDefault(x => x.Id == id);

   public int CountBooks() => db.Books.Count();

  public List<Loan> GetLoansByBookId(int bookId)
  {
    return db.Loans
        .Where(x => x.BookId == bookId)
        .OrderByDescending(x => x.BorrowedAt)
        .ToList();
  }

  public LoanOperationResult CreateLoan(NewLoanDto dto)
  {
    var book = GetBook(dto.BookId);

    if (book is null)
      return new LoanOperationResult
      {
        Status = LoanOperationStatus.BookNotFound
      };

    var availableCopies = book.TotalCopies -
        book.Loans.Count(x => x.ReturnedAt is null);

    if (availableCopies <= 0)
      return new LoanOperationResult
      {
        Status = LoanOperationStatus.NoCopiesAvailable
      };

    var loan = new Loan
    {
      BookId = dto.BookId,
      BorrowerName = dto.BorrowerName,
      BorrowedAt = DateTime.UtcNow
    };

    db.Loans.Add(loan);
    db.SaveChanges();

    return new LoanOperationResult
    {
      Status = LoanOperationStatus.Success,
      Loan = loan
    };
  }

  public LoanOperationResult ReturnLoan(int id)
  {
    var loan = db.Loans
        .SingleOrDefault(x => x.Id == id);

    if (loan is null)
      return new LoanOperationResult
      {
        Status = LoanOperationStatus.LoanNotFound
      };

    if (loan.ReturnedAt is not null)
      return new LoanOperationResult
      {
        Status = LoanOperationStatus.AlreadyReturned
      };

    loan.ReturnedAt = DateTime.UtcNow;

    db.SaveChanges();

    return new LoanOperationResult
    {
      Status = LoanOperationStatus.Success,
      Loan = loan
    };
  }
}
