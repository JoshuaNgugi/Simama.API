using System.ComponentModel.DataAnnotations;

// Will be used in POST /api/patients
public class CreatePatientDto
{
    [Required, MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(50)]
    public string LastName { get; set; } = null!;

    [Required, EmailAddress, MaxLength(100)]
    public string Email { get; set; } = null!;

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    public DateTime? DateOfBirth { get; set; }
}
