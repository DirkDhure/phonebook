using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PhoneBook.Abstractions;
using PhoneBook.Abstractions.Repositories.Read;
using PhoneBook.Abstractions.Repositories.Write;
using PhoneBook.Abstractions.Services;
using PhoneBook.API.Services;
using PhoneBook.Core.Services;
using PhoneBook.Infrastructure.Data.MongoDB.Read;
using PhoneBook.Infrastructure.Data.MongoDB.Write;
using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace PhoneBook.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public ILifetimeScope AutofacContainer { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });
            services.AddControllers();
            // Set the comments path for the Swagger JSON and UI.
            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Phone Book Manager API", Version = "v1" });
                c.IncludeXmlComments(xmlPath);

            });
            // 1. Add Authentication Services
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://veneka.auth0.com/";
                options.Audience = "https://agencybanking/agents/api";
            });

            services.Configure<ApplicationSettings>(this.Configuration.GetSection("ApplicationSettings"));
            services.AddTransient<IPhoneBookApplication, PhoneBookApplication>();
            services.AddTransient<IPhoneBookQueryRepository, PhoneBookQueryRepository>();
            services.AddTransient<IPhoneBookRepository, PhoneBookRepository>();
            services.AddTransient<IPhoneBookQueryHandler, PhoneBookQueryHandler>();
    
            //add httpclient
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            string virtualDirectory = Configuration.GetValue<string>("ApplicationSettings:VirtualDirectory");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(virtualDirectory + "/swagger/v1/swagger.json", "Phone Book Manager API");
            });

            //app.UseHttpsRedirection();
            loggerFactory.AddSerilog();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //configure auto fac here
            builder.RegisterModule(new EventsModule());
        }

    }
}
