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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ServiceHistory>()
        .HasKey(sh => sh.SERVICEID);

        modelBuilder.Entity<CarOwner>()
            .HasKey(co => new { co.CarID, co.OwnerID });

        modelBuilder.Entity<Accidents>()
            .HasKey(ai => ai.ACCIDENTID);

        modelBuilder.Entity<CarOwner>()
            .HasOne(co => co.Car)
            .WithMany(c => c.CarOwners)
            .HasForeignKey(co => co.CarID);

        modelBuilder.Entity<CarOwner>()
            .HasOne(co => co.Owner)
            .WithMany(o => o.CarOwners)
            .HasForeignKey(co => co.OwnerID);
    }
}
