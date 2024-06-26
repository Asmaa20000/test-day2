
using CarAPI.Entities;
using CarAPI.Payment;
using CarAPI.Repositories_DAL;
using CarAPI.Services_BLL;
using CarFactoryAPI.Entities;
using CarFactoryAPI.Repositories_DAL;
using Microsoft.EntityFrameworkCore;

namespace CarFactoryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddNewtonsoftJson(x => 
            x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ICarsService, CarsService>();
            builder.Services.AddScoped<IOwnersService, OwnersService>();
            builder.Services.AddScoped<ICashService, CashService>();

            builder.Services.AddScoped<ICarsRepository, CarRepository>();
            builder.Services.AddScoped<IOwnersRepository, OwnerRepository>();

            builder.Services.AddSingleton<IInMemoryContext, InMemoryContext>();

            builder.Services.AddDbContext<FactoryContext>(options =>
            {
                options.UseSqlServer("Server=.;Database=UnitTest_Intake44_MNF;Trusted_Connection=True;TrustServerCertificate=True");
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