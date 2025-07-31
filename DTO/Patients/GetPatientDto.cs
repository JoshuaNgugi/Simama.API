// Will be used in GET /api/patients or /api/patients/{id}
public class GetPatientDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
