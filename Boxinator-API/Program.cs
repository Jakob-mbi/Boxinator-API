using Boxinator_API.Models;
using Boxinator_API.Services;
using Boxinator_API.Services.ShipmentDataAccess.Admin;
using Boxinator_API.Services.ShipmentDataAccess.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Boxinator_API.Services.CountriesDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Reflection;
using Microsoft.Extensions.Hosting;
using System;

namespace Boxinator_API
{
    public class Program
    {
      /*  private static WebApplicationBuilder _builder;*/
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            string myCorsPolicy = builder.Configuration["JWT:policy"];;

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: myCorsPolicy,
                    policy =>
                    {
                        policy.WithOrigins(builder.Configuration["JWT:cors1"]).AllowAnyHeader().AllowAnyMethod();
                        policy.WithOrigins(builder.Configuration["JWT:cors2"]).AllowAnyHeader().AllowAnyMethod();
                    });
            });

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


            //SQLConnectionString
            builder.Services.AddDbContext<BoxinatorDbContext>(
                options =>
                {
                    options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"));
                });


            //AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddTransient<IShipmentAdminService, ShipmentAdminService>();
            builder.Services.AddTransient<IShipmentUserService, ShipmentUserService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<ICountryService, CountryService>();

            //LowercaseUrls for RouteOptions
            builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:audience"],
                        ValidIssuer = builder.Configuration["JWT:issuer"],
                        IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                        {
                            var client = new HttpClient();
                            var keyuri = builder.Configuration["JWT:key-uri"];
                            //Retrieves the keys from keycloak instance to verify token
                            var response = client.GetAsync(keyuri).Result;
                            var responseString = response.Content.ReadAsStringAsync().Result;
                            var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(responseString);
                            return keys.Keys;
                        }

                    };
                });
            var app = builder.Build();

            //using (var scope = app.Services.CreateScope())
            //{
            //    var dbContext = scope.ServiceProvider
            //        .GetRequiredService<BoxinatorDbContext>();

            //    // Here is the migration executed
            //    dbContext.Database.Migrate();
            //}

            app.UseCors(myCorsPolicy);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
       


    }
}