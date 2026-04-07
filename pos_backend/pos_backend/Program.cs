using Application.Extension;
using Domain.DTOs;
using FluentValidation;
using Infrastructure.Configuration;
using Infrastructure.Context;
using Infrastructure.InfrastructureExtension;
using Infrastructure.JwtService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace pos_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddValidatorsFromAssemblyContaining<SaleDto>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("POSConnection");

            builder.Services.AddDbContext<POSDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddInfrastructure(connectionString!);
            builder.Services.AddJwtAuthentication(builder.Configuration);
            builder.Services.AddScoped<JwtTokenService>();
            builder.Services.AddInfrastructureRepository();
            builder.Services.AddApplicationService();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("POS",
                    builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithExposedHeaders("Set-Cookie"));
            });
            var portString = Environment.GetEnvironmentVariable("PORT");
            int port = string.IsNullOrEmpty(portString) ? 5000 : int.Parse(portString);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("POS");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
