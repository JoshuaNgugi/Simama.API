using System.ComponentModel.DataAnnotations;

public class CreatePrescriptionDto
{
    [Required]
    public int PatientId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    public int DrugId { get; set; }

    public int? PharmacistId { get; set; }

    public string? Dosage { get; set; }

    public DateTime? PrescribedOn { get; set; }
}
