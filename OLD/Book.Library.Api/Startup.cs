﻿using Book.Library.Api.Services;
using Book.Library.Api.Utils;
using Book.Library.Data;
using Microsoft.EntityFrameworkCore;

namespace Book.Library.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions => {
                    sqlOptions.EnableRetryOnFailure();
                }));

            services.AddAutoMapper(typeof(AutoMapperProfiles));

            services.AddScoped<JsonFileHelper>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            ApplicationServices.AddServices(services);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                if (!dbContext.Books.Any())
                {
                    var jsonFileHelper = serviceScope.ServiceProvider.GetService<JsonFileHelper>();
                    jsonFileHelper.SeedDatabase().Wait();
                }
            }
        }
    }
}
