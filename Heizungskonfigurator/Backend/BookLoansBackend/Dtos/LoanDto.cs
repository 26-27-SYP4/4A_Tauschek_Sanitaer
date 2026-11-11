namespace BookLoansBackend.Dtos;

public class LoanDto
{
  [Required] public int Id { get; set; }
  [Required] public string BorrowerName { get; set; } = string.Empty;
  [Required] public DateTime BorrowedAt { get; set; }
  [Required] public DateTime? ReturnedAt { get; set; }
}
