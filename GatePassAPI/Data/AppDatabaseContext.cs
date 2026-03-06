using Microsoft.EntityFrameworkCore;
using GatePassAPI.Entities;

namespace GatePassAPI.Data;

public class AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : DbContext(options)
{
	public DbSet<Venue> Venues { get; set; }
	public DbSet<Event> Events { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Participant> Participants { get; set; }
	public DbSet<EventRegistration> EventRegistrations { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Venue>(entity =>
		{
			entity.ToTable("Venues");
			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.Name).HasColumnName("name");
			entity.Property(e => e.LogoImageURL).HasColumnName("logo_image_url");
			entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
			entity.Property(e => e.AddressLine1).HasColumnName("address_line_1");
			entity.Property(e => e.AddressLine2).HasColumnName("address_line_2");
			entity.Property(e => e.City).HasColumnName("city");
			entity.Property(e => e.State).HasColumnName("state");
			entity.Property(e => e.Country).HasColumnName("country");
		});

		modelBuilder.Entity<Event>(entity =>
		{
			entity.ToTable("Events");
			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.VenueId).HasColumnName("venue_id");
			entity.Property(e => e.Name).HasColumnName("name");
			entity.Property(e => e.StartDateTime).HasColumnName("start_date_time");
			entity.Property(e => e.ParticipantCapacity).HasColumnName("participant_capacity");
			entity.Property(e => e.Status).HasConversion<string>().HasColumnName("status");

			entity.HasIndex(e => e.VenueId);
			entity.HasIndex(e => e.StartDateTime);
		});

		modelBuilder.Entity<User>(entity =>
		{
			entity.ToTable("Users");
			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.Auth0Id).HasColumnName("auth0_id");
			entity.Property(e => e.VenueId).HasColumnName("venue_id");
			entity.Property(e => e.Email).HasColumnName("email");
			entity.Property(e => e.FirstName).HasColumnName("first_name");
			entity.Property(e => e.LastName).HasColumnName("last_name");
			entity.Property(e => e.CreatedAt).HasColumnName("created_at");

			entity.HasIndex(e => e.Auth0Id).IsUnique();
			entity.HasIndex(e => e.VenueId);
		});

		modelBuilder.Entity<Participant>(entity =>
		{
			entity.ToTable("Participants");
			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.VenueId).HasColumnName("venue_id");
			entity.Property(e => e.FirstName).HasColumnName("first_name");
			entity.Property(e => e.LastName).HasColumnName("last_name");

			entity.HasIndex(e => e.VenueId);
		});

		modelBuilder.Entity<EventRegistration>(entity =>
		{
			entity.ToTable("EventRegistrations");
			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.EventId).HasColumnName("event_id");
			entity.Property(e => e.ParticipantId).HasColumnName("participant_id");
			entity.Property(e => e.Class).HasColumnName("class");
			entity.Property(e => e.EventNumber).HasColumnName("event_number");
			entity.Property(e => e.CheckedIn).HasColumnName("checked_in");
			entity.Property(e => e.CreatedAt).HasColumnName("created_at");
			entity.Property(e => e.CheckedInAt).HasColumnName("checked_in_at");

			entity.HasOne(e => e.Event)
				.WithMany(e => e.Registrations)
				.HasForeignKey(e => e.EventId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasOne(e => e.Participant)
				.WithMany(e => e.Registrations)
				.HasForeignKey(e => e.ParticipantId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasIndex(e => new { e.EventId, e.ParticipantId })
				.IsUnique();

			entity.HasIndex(e => e.EventId);
			entity.HasIndex(e => e.ParticipantId);
			entity.HasIndex(e => new { e.EventId, e.CheckedIn });
			entity.HasIndex(e => new { e.EventId, e.Id });

		});
	}
}