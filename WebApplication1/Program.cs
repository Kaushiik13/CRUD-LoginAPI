using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<LoginDbContext>(options =>
                options.UseMySQL(builder.Configuration.GetConnectionString("login")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost3000",
                    policyBuilder => policyBuilder
                        .WithOrigins("http://localhost:3000") // Front-end URL
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
