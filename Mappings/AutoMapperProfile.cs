using AutoMapper;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Patient mappings
        CreateMap<Patient, GetPatientDto>();
        CreateMap<CreatePatientDto, Patient>();
        CreateMap<UpdatePatientDto, Patient>();

        // Doctor mappings
        CreateMap<Doctor, GetDoctorDto>();
        CreateMap<CreateDoctorDto, Doctor>();
        CreateMap<UpdateDoctorDto, Doctor>();

        // Pharmacist mappings
        CreateMap<Pharmacist, GetPharmacistDto>();
        CreateMap<CreatePharmacistDto, Pharmacist>();
        CreateMap<UpdatePharmacistDto, Pharmacist>();

        // Drug mappings
        CreateMap<Drug, GetDrugDto>();
        CreateMap<CreateDrugDto, Drug>();
        CreateMap<UpdateDrugDto, Drug>();

        // Prescription mappings
        CreateMap<Prescription, GetPrescriptionDto>()
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.FullName))
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FullName))
            .ForMember(dest => dest.DrugName, opt => opt.MapFrom(src => src.Drug.Name))
            .ForMember(dest => dest.PharmacistName, opt => opt.MapFrom(src => src.Pharmacist != null ? src.Pharmacist.FullName : null));

        CreateMap<CreatePrescriptionDto, Prescription>();
        CreateMap<UpdatePrescriptionDto, Prescription>();
    }
}