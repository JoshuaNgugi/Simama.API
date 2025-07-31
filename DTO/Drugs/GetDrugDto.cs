using System.ComponentModel.DataAnnotations;

public class GetDrugDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}