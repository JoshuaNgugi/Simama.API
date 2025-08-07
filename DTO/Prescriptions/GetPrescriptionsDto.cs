public class GetPrescriptionDto
{
    public int Id { get; set; }

    public string PatientName { get; set; } = null!;

    public string DoctorName { get; set; } = null!;

    public string DrugName { get; set; } = null!;

    public string? PharmacistName { get; set; }

    public string? Dosage { get; set; }

    public DateTime? PrescribedOn { get; set; }

    public DateTime? FulfilledAt { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
