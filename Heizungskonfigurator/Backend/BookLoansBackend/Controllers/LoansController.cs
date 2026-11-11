namespace BookLoansBackend.Controllers;

[Route("[controller]")]
[ApiController]
public class LoansController(DbService dbService) : ControllerBase
{
  [HttpGet]
  public ActionResult<List<LoanDto>> GetLoans(int bookId)
  {
    var book = dbService.GetBook(bookId);

    if (book is null)
      return NotFound(new
      {
        message = $"No book exists with ID {bookId}."
      });

    return dbService.GetLoansByBookId(bookId).Select(x => new LoanDto
    {
      BorrowedAt = x.BorrowedAt,
      BorrowerName = x.BorrowerName,
      Id = x.Id,
      ReturnedAt = x.ReturnedAt,
    }).ToList();
  }


  [HttpPost]
  public ActionResult<Loan> CreateLoan(NewLoanDto dto)
  {
    if (string.IsNullOrWhiteSpace(dto.BorrowerName))
      return BadRequest(new
      {
        message = "Borrower name must not be empty."
      });

    var result = dbService.CreateLoan(dto);

    return result.Status switch
    {
      LoanOperationStatus.Success =>
          Ok(result.Loan),

      LoanOperationStatus.BookNotFound =>
          NotFound(new
          {
            message = $"No book exists with ID {dto.BookId}."
          }),

      LoanOperationStatus.NoCopiesAvailable =>
          Conflict(new
          {
            message = "No copies of this book are available."
          }),

      _ => BadRequest()
    };
  }

  [HttpPut("{id}/return")]
  public ActionResult<Loan> ReturnLoan(int id)
  { 
    var result = dbService.ReturnLoan(id);

    return result.Status switch
    {
      LoanOperationStatus.Success =>
          Ok(result.Loan),

      LoanOperationStatus.LoanNotFound =>
          NotFound(new
          {
            message = $"No loan exists with ID {id}."
          }),

      LoanOperationStatus.AlreadyReturned =>
          BadRequest(new
          {
            message = $"Loan with ID {id} was already returned."
          }),

      _ => BadRequest()
    };
  }
}
