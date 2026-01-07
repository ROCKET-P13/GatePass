using Microsoft.EntityFrameworkCore;
using GatePassAPI.Entities;

namespace GatePassAPI.Data;

public class AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : DbContext(options)
{
	public DbSet<Venue> Venues { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Venue>(entity =>
		{
			entity.ToTable("Venues");
			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.Name).HasColumnName("name");
			entity.Property(e => e.LogoImageURL).HasColumnName("logo_image_url");
		});
	}
}