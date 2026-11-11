namespace BookLoansBackend.Dtos;

public class NewLoanDto
{
 [Required] public int BookId { get; set; }
 [Required] public string BorrowerName { get; set; } = string.Empty;
}
