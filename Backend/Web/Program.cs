using Data;
using Data.Repositories;
using Data.Repositories.Users;
using Data.Interfaces;
using Data.Interfaces.Users;

namespace Web
{
    public class Program
    {
        private static readonly string connectionString = "Data Source=.;Database=SyncSphere;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<DataAccess>(x => new DataAccess(connectionString));
            builder.Services.AddScoped<IGenericRepository, GenericRepository>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
