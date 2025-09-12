using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskNoteManager.Domain.DataAccess.UnitOfWork;
using TaskNoteManager.Infrastructure.DataAccess;
using TaskNoteManager.Infrastructure.DataAccess.UnitOfWork;
using TaskNoteManager.Infrastructure.Extensions;

namespace TaskNoteManager.Infrastructure
{
    /// <summary>
    /// Provides extension methods for configuring infrastructure services in a dependency injection.
    /// </summary>
    /// <remarks>This class contains methods to register infrastructure-related services, such as database
    /// contexts, into the dependency injection container.</remarks>
    public static class DependencyInjectionInfra
    {
        /// <summary>
        /// Configures and adds the necessary infrastructure services to the specified service collection.
        /// </summary>
        /// <remarks>This method sets up the required infrastructure components, such as database
        /// contexts,  by invoking internal configuration methods. Ensure that the provided <paramref name="services"/> 
        /// and <paramref name="configuration"/> are properly initialized before calling this method.</remarks>
        /// <param name="services">The <see cref="IServiceCollection"/> to which the infrastructure services will be added.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> instance containing the application's configuration settings.</param>
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDatabaseContext(services, configuration);
        }

        /// <summary>
        /// Configures and registers the database context and related services in the dependency injection container.
        /// </summary>
        /// <remarks>This method registers the unit of work and factory for SQL Server using the
        /// connection string provided in the configuration.</remarks>
        /// <param name="services">The <see cref="IServiceCollection"/> to which the database services will be added.</param>
        /// <param name="configuration">The application configuration used to retrieve the database connection string.</param>
        private static void AddDatabaseContext(IServiceCollection services, IConfiguration configuration)
        {
            //Get connectionString from ./TaskNoteManager.Infrastructure/Extensions/ConfigurationExtension.cs
            var connectionString = configuration.ConnectionString();
            
            services.AddScoped<IUnitOfWork,UnitOfWork<SqlConnection>>();
            services.AddScoped(c=> new SqlServerFactoryUnitOfWork(connectionString));

        }
    }
}
