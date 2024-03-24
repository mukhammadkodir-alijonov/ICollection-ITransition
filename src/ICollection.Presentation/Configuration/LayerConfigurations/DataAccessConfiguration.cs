using ICollection.DataAccess.DbContexts;
using ICollection.DataAccess.Interfaces.Common;
using ICollection.DataAccess.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace ICollection.Presentation.Configuration.LayerConfigurations
{
    public static class DataAccessConfiguration
    {
        public static void ConfigureDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            string connectionString = configuration.GetConnectionString("DatabaseConnection");
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
