namespace BookLoansBackend.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthorsController(DbService dbService) : ControllerBase
{
  [HttpGet]
  public List<AuthorDto> GetAuthors()
  {
    this.Log();
    return dbService.GetAuthors()
    .Select(x => new AuthorDto().CopyFrom(x))
    .ToList();
  }
}
