namespace BookLoansBackend.Dtos;

public class BookDetailDto : BookSummaryDto
{
  [Required] public string Isbn { get; set; } = string.Empty;
  [Required] public string Description { get; set; } = string.Empty;
  [Required] public int TotalCopies { get; set; }
}
