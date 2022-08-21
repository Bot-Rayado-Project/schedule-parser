using Microsoft.EntityFrameworkCore;
using ParserDAL.Models;

namespace ParserDAL.DataAccess;

public class ScheduleContext : DbContext
{
    public ScheduleContext(DbContextOptions options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SharedSchedule>()
            .HasKey(nameof(SharedSchedule.stream_group), nameof(SharedSchedule.day), nameof(SharedSchedule.parity));
        modelBuilder.Entity<HeadmanSchedule>()
            .HasKey(nameof(HeadmanSchedule.stream_group), nameof(HeadmanSchedule.day), nameof(HeadmanSchedule.parity));
        modelBuilder.Entity<PersonalSchedule>()
            .HasKey(nameof(PersonalSchedule.id), nameof(PersonalSchedule.stream_group), nameof(PersonalSchedule.day), nameof(PersonalSchedule.parity));
        modelBuilder.Entity<HeadmanAnnotation>()
            .HasKey(nameof(HeadmanAnnotation.stream_group), nameof(HeadmanAnnotation.day), nameof(HeadmanAnnotation.parity));
        modelBuilder.Entity<PersonalAnnotation>()
            .HasKey(nameof(PersonalAnnotation.id), nameof(PersonalAnnotation.stream_group), nameof(PersonalAnnotation.day), nameof(PersonalAnnotation.parity));
        modelBuilder.Entity<HeadmanChange>()
            .HasKey(nameof(HeadmanChange.stream_group), nameof(HeadmanChange.day), nameof(HeadmanChange.parity));
        modelBuilder.Entity<PersonalChange>()
            .HasKey(nameof(PersonalChange.id), nameof(PersonalChange.stream_group), nameof(PersonalChange.day), nameof(PersonalChange.parity));
    }
    public DbSet<SharedSchedule> SharedSchedules { get; set; }
    public DbSet<HeadmanSchedule> HeadmanSchedules { get; set; }
    public DbSet<PersonalSchedule> PersonalSchedules { get; set; }
    public DbSet<HeadmanAnnotation> HeadmanAnnotations { get; set; }
    public DbSet<PersonalAnnotation> PersonalAnnotations { get; set; }
    public DbSet<HeadmanChange> HeadmanChanges { get; set; }
    public DbSet<PersonalChange> PersonalChanges { get; set; }
}