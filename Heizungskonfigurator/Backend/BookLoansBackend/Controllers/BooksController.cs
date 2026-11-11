namespace BookLoansBackend.Controllers;

[Route("[controller]")]
[ApiController]
public class BooksController(DbService dbService) : ControllerBase
{
  [HttpGet]
  public List<BookSummaryDto> GetBooks(string? search, int? authorId)
  {
    this.Log($"search={search} authorId={authorId}");
    Thread.Sleep(750);
    return dbService.GetBooks(search, authorId)
      .Select(ToSummary)
      .ToList();
  }

  [HttpGet("{id}")]
  public ActionResult<BookDetailDto> GetBook(int id)
  {
    this.Log(id);
    var book = dbService.GetBook(id);
    return book is null
      ? NotFound(new { message = $"No book exists with ID {id}." })
      : ToDetail(book);
  }

  private static BookSummaryDto ToSummary(Book book)
  {
    var dto = new BookSummaryDto
    {
      AuthorName = book.Author.Name,
      AvailableCopies = book.TotalCopies - book.Loans.Count(x => x.ReturnedAt is null),
    }.CopyFrom(book);
    return dto;
  }

  private static BookDetailDto ToDetail(Book book)
  {
    var dto = new BookDetailDto
    {
      AuthorName = book.Author.Name,
      AvailableCopies = book.TotalCopies - book.Loans.Count(x => x.ReturnedAt is null),
    }.CopyFrom(book);
    return dto;
  }
}
