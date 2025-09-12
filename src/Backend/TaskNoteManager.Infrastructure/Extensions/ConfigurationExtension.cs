using Microsoft.Extensions.Configuration;
using TaskNoteManager.Domain.Enums;

namespace TaskNoteManager.Infrastructure.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="IConfiguration"/> interface to retrieve application-specific
    /// configuration values, such as environment settings, database type,  and connection strings.
    /// </summary>
    /// <remarks>These extension methods simplify access to configuration values by encapsulating common 
    /// patterns for retrieving and interpreting configuration data. They are intended for internal  use and assume
    /// specific configuration keys are present in the application's configuration source.</remarks>
    public static class ConfigurationExtension
    {
        /// <summary>
        /// Determines whether the current environment is configured as a unit test environment.
        /// </summary>
        /// <param name="configuration">The configuration instance to check for the "InMemoryTest" setting.</param>
        /// <returns><see langword="true"/> if the "InMemoryTest" setting is present and set to <see langword="true"/>;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool IsUnitTestEnviroment(this IConfiguration configuration)
        {
            return configuration.GetValue<bool>("InMemoryTest");
        }

        /// <summary>
        /// Get the database type specified in the configuration.
        /// </summary>
        /// <param name="configuration">The configuration instance containing the connection string for the database type.</param>
        /// <returns>The <see cref="DatabaseType"/> value parsed from the "DatabaseType" connection string.</returns>
        public static DatabaseType DatabaseType(this IConfiguration configuration) 
        {
            var databaseType = configuration.GetConnectionString("DatabaseType");

            return (DatabaseType)Enum.Parse(typeof(DatabaseType), databaseType!);
        }

        /// <summary>
        /// Get the connection string for the configured database type.
        /// </summary>
        /// <param name="configuration">The configuration instance containing the database settings.</param>
        /// <returns>The connection string for the database if the database type is SQL Server; otherwise, an empty string.</returns>
        public static string ConnectionString(this IConfiguration configuration)
        {
            var databaseType = configuration.DatabaseType();

            if (databaseType == Domain.Enums.DatabaseType.SqlServer)
                return configuration.GetConnectionString("ConnectionSqlServer")!;
           
            return string.Empty;
         
        }
    }
}
