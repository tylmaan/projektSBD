using Microsoft.EntityFrameworkCore;
using projektSBD.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<CarOwner> CarOwners { get; set; }
    public DbSet<Accidents> Accidents { get; set; }
    public DbSet<ServiceHistory> ServiceHistories { get; set; }
    public DbSet<InsurancePolicy> InsurancePolicies { get; set; }
    public DbSet<Claim> Claims { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ServiceHistory>()
            .HasKey(sh => sh.SERVICEID);

        modelBuilder.Entity<CarOwner>()
            .HasKey(co => new { co.CARID, co.OWNERID });

        modelBuilder.Entity<Accidents>()
            .HasKey(ai => ai.ACCIDENTID);

        modelBuilder.Entity<CarOwner>()
            .HasOne(co => co.Car)
            .WithMany(c => c.CarOwners)
            .HasForeignKey(co => co.CARID);

        modelBuilder.Entity<CarOwner>()
            .HasOne(co => co.Owner)
            .WithMany(o => o.CarOwners)
            .HasForeignKey(co => co.OWNERID);

        modelBuilder.Entity<InsurancePolicy>()
            .HasKey(ip => ip.POLICYID);

        modelBuilder.Entity<InsurancePolicy>()
            .HasOne(ip => ip.Car)
            .WithMany(c => c.InsurancePolicies)
            .HasForeignKey(ip => ip.CARID);

        modelBuilder.Entity<Claim>()
            .HasKey(cl => cl.CLAIMID);

        modelBuilder.Entity<Claim>()
            .HasOne(cl => cl.Accident)
            .WithMany()
            .HasForeignKey(cl => cl.ACCIDENTID);

        modelBuilder.Entity<Claim>()
            .HasOne(cl => cl.InsurancePolicy)
            .WithMany(ip => ip.Claims)
            .HasForeignKey(cl => cl.POLICYID);
    }
}
