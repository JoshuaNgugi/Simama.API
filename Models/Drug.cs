using System.ComponentModel.DataAnnotations;

public class Drug
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    // Navigation
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
