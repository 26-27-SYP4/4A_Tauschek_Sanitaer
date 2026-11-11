namespace BookLoansDb;

public class Book
{
  public int Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public int AuthorId { get; set; }
  public Author Author { get; set; } = null!;
  public int Year { get; set; }
  public string Isbn { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public int TotalCopies { get; set; }
  public List<Loan> Loans { get; set; } = [];
}
