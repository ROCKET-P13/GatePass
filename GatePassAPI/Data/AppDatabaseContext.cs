using Microsoft.EntityFrameworkCore;
using MotoPassAPI.Entities;

namespace MotoPassAPI.Data;

public class AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : DbContext(options)
{
	public DbSet<Track> Tracks { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Track>(entity =>
		{
			entity.ToTable("Tracks");
			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.Name).HasColumnName("name");
			entity.Property(e => e.LogoImageURL).HasColumnName("logo_image_url");
		});
	}
}