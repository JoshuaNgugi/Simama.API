using System.ComponentModel.DataAnnotations;

public enum PrescriptionStatus
{
    Pending = 1,
    Dispensed = 2,
    Rejected = 3
}

public class Prescription
{
    public int Id { get; set; }

    [Required]
    public int PatientId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    public int DrugId { get; set; }

    public int? PharmacistId { get; set; }

    public string? Dosage { get; set; }

    public DateTime? PrescribedOn { get; set; }

    public DateTime? FulfilledAt { get; set; }

    public PrescriptionStatus Status { get; set; } = PrescriptionStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    // Navigation
    public Patient Patient { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
    public Drug Drug { get; set; } = null!;
    public Pharmacist? Pharmacist { get; set; }
}
