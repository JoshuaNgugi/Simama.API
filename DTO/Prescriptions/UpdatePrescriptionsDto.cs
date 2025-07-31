public class UpdatePrescriptionDto
{
    public int? PatientId { get; set; }

    public int? DoctorId { get; set; }

    public int? DrugId { get; set; }

    public int? PharmacistId { get; set; }

    public string? Dosage { get; set; }

    public DateTime? PrescribedOn { get; set; }

    public DateTime? FulfilledAt { get; set; }

    public PrescriptionStatus? Status { get; set; }
}
