using Boxinator_API.Models;
using Boxinator_API.Services.CountriesDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Boxinator_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //Swagger Documentaion 
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            builder.Services.AddSwaggerGen(options =>
            {
               

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = " Boxinator-API",
                    Description = "A REST API with full CRUD functionality for Boxinator, built with ASP.NET and Entity Framework.",

                });
                options.IncludeXmlComments(xmlPath);
            });


            //AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            //SQLConnectionString
            builder.Services.AddDbContext<BoxinatorDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            //LowercaseUrls for RouteOptions
            builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            builder.Services.AddTransient<ICountryService, CountryService>();

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