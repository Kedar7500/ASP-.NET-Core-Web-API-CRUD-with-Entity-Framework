using ContactsAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ContactsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add logging
            builder.Logging.AddConsole();
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<Program>();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add DbContext
            builder.Services.AddDbContext<ContactsAPIDBContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("Connections:ContactsApiConnectionString")
                ?? "Server=KEDARLAPTOP\\SQLEXPRESS;Database=ContactsDb1;Trusted_Connection=True";

                if (string.IsNullOrEmpty(connectionString))
                {
                    logger.LogError("Connection string 'ContactsApiConnectionString' not found.");
                    throw new InvalidOperationException("Connection string 'ContactsApiConnectionString' not found.");
                }
                options.UseSqlServer(connectionString);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}