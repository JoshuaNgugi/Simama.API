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
            var patient1 = new Patient
            {
                FirstName = "Sona",
                LastName = "Namona",
                Email = "patient1@simama.com",
                PhoneNumber = "0710100100",
                DateOfBirth = new DateTime(2000, 10, 11, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = DateTime.UtcNow
            };
            context.Patients.Add(patient1);

            var patient2 = new Patient
            {
                FirstName = "Venus",
                LastName = "Warrior",
                Email = "venus@gmail.com",
                PhoneNumber = "0710100101",
                DateOfBirth = new DateTime(1997, 3, 3, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = DateTime.UtcNow
            };
            context.Patients.Add(patient2);

            var patient3 = new Patient
            {
                FirstName = "Arusha",
                LastName = "Kijogoo",
                Email = "kijogoo@outlook.com",
                PhoneNumber = "0710100102",
                DateOfBirth = new DateTime(1975, 2, 4, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = DateTime.UtcNow
            };
            context.Patients.Add(patient3);

            var patient4 = new Patient
            {
                FirstName = "Elon",
                LastName = "Tusk",
                Email = "elon@x.com",
                PhoneNumber = "0710100103",
                DateOfBirth = new DateTime(1970, 9, 11, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = DateTime.UtcNow
            };
            context.Patients.Add(patient4);

            var patient5 = new Patient
            {
                FirstName = "Plutipus",
                LastName = "Renolds",
                Email = "plat@gmail.com",
                PhoneNumber = "0710100104",
                DateOfBirth = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = DateTime.UtcNow
            };
            context.Patients.Add(patient5);
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
