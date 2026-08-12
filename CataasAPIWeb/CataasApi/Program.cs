using CataasApi.Data;
using CataasApi.Interfaces;
using CataasApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Scalar.AspNetCore;

namespace CataasApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            
            builder.Services.AddControllers();
           
            builder.Services.AddOpenApi();
            builder.Services.AddHttpClient<ICataasApi, CataasApiClient>();
            builder.Services.AddScoped<ISearchService, SearchService>();
            
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    policy =>
                    {
                        policy.WithOrigins(
                                "http://127.0.0.1:5500",
								"https://guilhermebt1.github.io")
                            .WithMethods("GET")
                            .AllowAnyHeader();
                    }
                );
            });
            

            var app = builder.Build();
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsJsonAsync(new {erro500 ="Ocorreu um erro inesperado ao processar sua requisição."});
                });
            });
            
            
            
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();


            app.MapControllers();

            app.Run();
        }
    }
}
