using System.ComponentModel.DataAnnotations;

public class Patient
{
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(50)]
    public string LastName { get; set; } = null!;

    [Required, EmailAddress, MaxLength(100)]
    public string Email { get; set; } = null!;

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    public string FullName => $"{FirstName} {LastName}";

    // Navigation
    // It means "One Patient can have many Prescriptions"
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
