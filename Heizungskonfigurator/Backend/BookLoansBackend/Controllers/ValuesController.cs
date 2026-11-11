namespace BookLoansBackend.Controllers;

public record struct OkStatus(bool IsOk, int Nr, string? Error = null);

[Route("[controller]")]
[ApiController]
public class ValuesController(DbService dbService) : ControllerBase
{

  [HttpGet("Books")]
  public OkStatus GetBooksToTest()
  {
    this.Log();
    try
    {
      int nr = dbService.CountBooks();
      return new OkStatus(true, nr);
    }
    catch (Exception exc)
    {
      return new OkStatus(false, -1, exc.Message);
    }
  }
}
