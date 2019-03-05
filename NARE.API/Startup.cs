using System;
using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NARE.Application.Interfaces;
using NARE.Application.User.Command.RegisterUser;
using NARE.Application.Utilities;
using NARE.API.Filters;
using NARE.Domain.Entities;
using NARE.Infrastructure.Notification;
using NARE.Persistence;
using Swashbuckle.AspNetCore.Swagger;

namespace NARE.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            services.AddApiVersioning();

            services.AddSingleton(Configuration);
            services.AddSingleton(mappingConfig.CreateMapper());
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddScoped<ApplicationDbContext, ApplicationDbContext>();

            services
                .AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySQL(Configuration["ConnectionStrings:MySQL"],
                    optionsBuilder => { optionsBuilder.MigrationsAssembly("NARE.Persistence"); }));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SigningKey"])),
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["JWT:Audience"]
                    };
                })
                .AddCookie();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministratorOnly", policy => policy.RequireClaim("Administrator"));

            });
            
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddCors();
            
            services.AddMvc(
                    options =>
                    {
                        options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                    })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<RegisterUserCommandValidator>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "New Age Real Estate API", Version = "v1" });
            });

            services.AddMediatR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            }
            else
            {
                app.UseHsts();
                app.UseCors(builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            }
            
            UpdateDatabase(app);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "New Age Real Estate API V1");
                c.RoutePrefix = string.Empty;
            });


            logger.AddFile("Logs/NARE.txt");

            app.UseHealthChecks("/health");

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.EnsureCreated();
                    try
                    {
                        context.Database.Migrate();
                    }
                    catch (Exception)
                    {
                        // Database is already migrated. No issues here
                    }
                    
                }
            }
        }
    }
}
