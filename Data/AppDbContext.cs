using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    // public DbSet<Patient> Patients { get; set; }
    // public DbSet<Prescription> Prescriptions { get; set; }
    // public DbSet<User> Users { get; set; }
    // public DbSet<Drug> Drugs { get; set; }
}
