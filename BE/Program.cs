using Microsoft.EntityFrameworkCore;
using BE.Model;
using BE.Data;
using Microsoft.AspNetCore.Builder;



namespace BE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<Contex>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });
            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors("AllowLocalhost");
            

            app.UseHttpsRedirection();
            app.UseRouting();

            app.MapControllers();
            app.Run();
        }
    }
}