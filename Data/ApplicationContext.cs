using Microsoft.EntityFrameworkCore;
using Models.DBTables;

namespace AnalysisService.Data;

public class ApplicationContext : DbContext
{
    private IConfiguration _configuration;

    public ApplicationContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<EventModel> Events { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("MainDB");
        optionsBuilder.UseNpgsql(connectionString);
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}