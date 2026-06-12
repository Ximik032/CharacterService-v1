
using CharacterService.Application.Interfaces;
using CharacterService.Application.Services;
using CharacterService.Application.Validators;
using CharacterService.Infrastructure.Data;
using CharacterService.Infrastructure.Repositories;
using CharacterService.WebApi.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace CharacterService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<CharacterDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("Postgress"));
            });

            builder.Services.AddControllers(); 
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            InitService(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<DomainExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void InitService(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
            builder.Services.AddScoped<ICharacterService, CharacterService.Infrastructure.Services.CharacterService>();

            

        }
    }
}
