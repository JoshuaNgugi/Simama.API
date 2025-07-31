using System.ComponentModel.DataAnnotations;

public class CreateDrugDto
{
    [Required]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}
