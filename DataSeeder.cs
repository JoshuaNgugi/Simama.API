using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

public static class DataSeeder
{
    public static void Seed(AppDbContext context)
    {
        // Ensure DB is created
        context.Database.Migrate();

        // Seed Patient
        if (!context.Patients.Any())
        {
            var patient = new Patient
            {
                FirstName = "Sona",
                LastName = "Namona",
                Email = "patient1@simama.com",
                PhoneNumber = "0710100100",
                DateOfBirth = new DateTime(2000, 10, 11),
                CreatedAt = DateTime.UtcNow
            };
            context.Patients.Add(patient);
        }
        // Seed Doctor
        if (!context.Doctors.Any())
        {
            var doctor = new Doctor
            {
                FirstName = "John",
                LastName = "Lamubule",
                Email = "doctor1@simama.com",
                PhoneNumber = "0720200200",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password1234"),
                CreatedAt = DateTime.UtcNow
            };
            context.Doctors.Add(doctor);
        }

        // Seed Pharmacists
        if (!context.Pharmacists.Any())
        {
            var pharmacist = new Pharmacist
            {
                FirstName = "Jane",
                LastName = "Shitiambi",
                Email = "pharmacist1@simama.com",
                PhoneNumber = "0730300300",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password1234"),
                CreatedAt = DateTime.UtcNow
            };
            context.Pharmacists.Add(pharmacist);
        }

        // Seed Drugs
        if (!context.Drugs.Any())
        {
            context.Drugs.AddRange(
                new Drug { Name = "Paracetamol", Description = "Pain reliever", CreatedAt = DateTime.UtcNow },
                new Drug { Name = "Amoxicillin", Description = "Antibiotic", CreatedAt = DateTime.UtcNow }
            );
        }

        context.SaveChanges();
    }
}
