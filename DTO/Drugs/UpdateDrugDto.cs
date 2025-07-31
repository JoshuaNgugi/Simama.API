using System.ComponentModel.DataAnnotations;

public class UpdateDrugDto
{

    [Required]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}