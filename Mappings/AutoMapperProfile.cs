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
            .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.Doctor))
            .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient))
            .ForMember(dest => dest.Drug, opt => opt.MapFrom(src => src.Drug))
            .ForMember(dest => dest.Pharmacist, opt => opt.MapFrom(src => src.Pharmacist != null ? src.Pharmacist : null));

        CreateMap<CreatePrescriptionDto, Prescription>();
        CreateMap<UpdatePrescriptionDto, Prescription>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}