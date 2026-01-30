using Microsoft.EntityFrameworkCore;
using GatePassAPI.Entities;

namespace GatePassAPI.Data;

public class AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : DbContext(options)
{
	public DbSet<Venue> Venues { get; set; }
	public DbSet<Event> Events { get; set; }
	public DbSet<User> Users { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Venue>(entity =>
		{
			entity.ToTable("Venues");
			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.Name).HasColumnName("name");
			entity.Property(e => e.Sport).HasColumnName("sport");
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
			entity.Property(e => e.Date).HasColumnName("date");
			entity.Property(e => e.ParticipantCapacity).HasColumnName("participant_capacity");
			entity.Property(e => e.Status).HasColumnName("status");
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
		});
	}
}