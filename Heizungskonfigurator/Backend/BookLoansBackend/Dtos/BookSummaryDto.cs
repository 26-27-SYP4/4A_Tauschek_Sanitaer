namespace BookLoansBackend.Dtos;

public class BookSummaryDto
{
 [Required] public int Id { get; set; }
 [Required] public string Title { get; set; } = string.Empty;
 [Required] public int AuthorId { get; set; }
 [Required] public required string AuthorName { get; set; } = string.Empty;
 [Required] public int Year { get; set; }
 [Required] public required int AvailableCopies { get; set; }
}
