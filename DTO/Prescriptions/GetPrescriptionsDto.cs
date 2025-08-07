public class GetPrescriptionDto
{
    public int Id { get; set; }

    public Patient Patient { get; set; } = null!;

    public Doctor Doctor { get; set; } = null!;

    public Drug Drug { get; set; } = null!;

    public Pharmacist? Pharmacist { get; set; }

    public string? Dosage { get; set; }

    public DateTime? PrescribedOn { get; set; }

    public DateTime? FulfilledAt { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
