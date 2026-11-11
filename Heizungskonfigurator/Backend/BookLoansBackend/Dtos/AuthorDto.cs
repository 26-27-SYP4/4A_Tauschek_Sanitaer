namespace BookLoansBackend.Dtos;

public class AuthorDto
{
 [Required] public int Id { get; set; }
 [Required] public string Name { get; set; } = string.Empty;
}
