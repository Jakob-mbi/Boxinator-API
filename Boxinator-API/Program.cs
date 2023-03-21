using Boxinator_API.Models;
using Boxinator_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace Boxinator_API
{
    public class Program
    {
        private static WebApplicationBuilder _builder;
        public static void Main(string[] args)
        {
            var _builder = WebApplication.CreateBuilder(args);


            string myCorsPolicy = "_myAllowSpecificOrigins";

            _builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: myCorsPolicy,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
                    });
            });

            // Add services to the container.
            _builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            _builder.Services.AddEndpointsApiExplorer();

            //Swagger Documentaion 
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            _builder.Services.AddSwaggerGen(options =>
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
            _builder.Services.AddDbContext<BoxinatorDbContext>(
                options =>
                {
                    options.UseSqlServer(
                    _builder.Configuration.GetConnectionString("DefaultConnection"));
                });
            //AutoMapper
            _builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            _builder.Services.AddTransient<IUserService, UserService>();

            //LowercaseUrls for RouteOptions
            _builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            _builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = _builder.Configuration["JWT:audience"],
                        ValidIssuer = _builder.Configuration["JWT:issuer"],
                        IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                        {
                            var client = new HttpClient();
                            var keyuri = _builder.Configuration["JWT:key-uri"];
                            //Retrieves the keys from keycloak instance to verify token
                            var response = client.GetAsync(keyuri).Result;
                            var responseString = response.Content.ReadAsStringAsync().Result;
                            var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(responseString);
                            return keys.Keys;
                        }

                    };
                });
            var app = _builder.Build();

            app.UseCors(myCorsPolicy);

            app.MapGet("/", () => "Hello World!");

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