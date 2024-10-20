using LinkShortener.Application;
using LinkShortener.Persistence.Database.ShortenLink;
using LinkShortener.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            ConfigurationManager config = builder.Configuration;
            
            string url = String.Format("http://localhost:{0}", 5002);
            builder.WebHost.UseUrls(url);

            builder.Services.AddDbContext<ShortenedLinkDBContext>(options => options.UseSqlServer(config.GetConnectionString("localDBConnection")));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            builder.Services.AddScoped<ILinkShortenerService, LinkShortenerService>();
            builder.Services.AddScoped<IShortenedLinkRepo, ShortenedLinkRepo>();
            RunApplication(builder);
        }

        private static void RunApplication(WebApplicationBuilder builder)
        {
            WebApplication app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsProduction())
            {
                app.UseSwagger(c => c.RouteTemplate = "/{documentName}/swagger.json");
                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = "swagger";
                    c.SwaggerEndpoint("../v1/swagger.json", "LinkShortener");
                });
            }
            app.UseCors(policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });
            app.MapControllers();
            app.Run();
        }
    }
}
