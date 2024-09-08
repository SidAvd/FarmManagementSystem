using FarmManagementSystem.Data;
using FarmManagementSystem.Services.Interfaces;
using FarmManagementSystem.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace FarmManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddDbContext<FarmDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ApiDbConnectionString")));

            // Register services
            builder.Services.AddScoped<ICropService, CropService>();
            builder.Services.AddScoped<IFieldService, FieldService>();
            builder.Services.AddScoped<IHarvestService, HarvestService>();
            builder.Services.AddScoped<IWorkerAssignmentService, WorkerAssignmentService>();
            builder.Services.AddScoped<IWorkerService, WorkerService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
