using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace DoaFacil.Infrastructure.Persistence.DesignTime;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("DOAFACIL_MYSQL_CONN") ?? "Server=localhost;Port=3307;Database=doafacil;Uid=doafacil;Pwd=doafacil123;";

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Variável de ambiente DOAFACIL_MYSQL_CONN não encontrada. " +
                "Exemplo: Server=localhost;Port=3307;Database=doafacil;Uid=doafacil;Pwd=doafacil123;");
        }
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql(connectionString, 
            serverVersion,
            mySqlOptions =>
            {
                mySqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null
                );
            });

        return new AppDbContext(optionsBuilder.Options);
    }
}
