namespace BookLoansBackend.Controllers;

[Route("[controller]")]
[ApiController]
public class DiagnosticsController : ControllerBase
{
  [HttpGet("fail")]
  public InternalServerError<string> Fail()
    => TypedResults.InternalServerError("The diagnostics endpoint deliberately returns HTTP 500.");
}
