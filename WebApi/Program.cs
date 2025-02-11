
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Application;

namespace WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);            

            var app = builder.Build();

            // Database Initializer
            await app.Services.AddDatabaseInitializerAsync();

            // Configure the HTTP request pipeline.
            
            app.UseHttpsRedirection();

            //app.UseAuthorization();
         
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.MapControllers();

            app.UserInfrastructure();

            app.Run();
        }
    }
}
