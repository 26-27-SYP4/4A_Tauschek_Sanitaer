namespace BookLoansBackend.Services;

public class LoanOperationResult
{
  public LoanOperationStatus Status { get; init; }
  public Loan? Loan { get; init; }
}

public enum LoanOperationStatus
{
  Success,
  BookNotFound,
  LoanNotFound,
  NoCopiesAvailable,
  AlreadyReturned,
}
