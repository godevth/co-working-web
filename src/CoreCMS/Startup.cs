using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CoreCMS.Application;
using CoreCMS.Application.Office365.Models;
using CoreCMS.Data;
using CoreCMS.Domain;
using CoreCMS.Models.IdentityServer;
using CoreCMS.Models.LogMiddleware;
using CoreCMS.Persistence;
using IdentityServer4.Configuration;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Serilog;
using VueCliMiddleware;

namespace CoreCMS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string certPath = Configuration.GetSection("IdentityServer")["SigningCertPath"];
            var certificate = new X509Certificate2(certPath, "Sbpds@2020+");

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(@"App_Data"))
                .ProtectKeysWithCertificate(certificate)
                .SetApplicationName("CoreCMS")
                .DisableAutomaticKeyGeneration();

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly("CoreCMS.Persistence")));
            services.AddIdentity<ApplicationUser, ApplicationRole>(
                options => {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllers().AddNewtonsoftJson();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp";
            });

            #region [Get App Settings]
            services.Configure<IdentityServerConfig>(Configuration.GetSection("IdentityServer"));
            services.Configure<OpenIDConfig>(Configuration.GetSection("OpenID"));
            services.Configure<ApiConfig>(Configuration.GetSection("ApiConfig"));
            services.Configure<Office365Config>(Configuration.GetSection("Office365Config"));
            services.Configure<CowConfig>(Configuration.GetSection("CowConfig"));
            services.Configure<ConsentConfig>(Configuration.GetSection("ConsentConfig"));
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });
            #endregion

            #region [DIs]
            // MediatR DI
            services.AddMediatR(typeof(Class1).GetTypeInfo().Assembly);
            services.AddTransient<IAuthenticationProvider, GraphAuthenticationProvider>();
            services.AddTransient<IGraphServiceClient, GraphServiceClient>();
            services.AddTransient<IGraphProvider, MicrosoftGraphProvider>();
            #endregion

            #region [Identity Server Configuration]
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services
                .AddIdentityServer(options =>
                {
                    options.UserInteraction = new UserInteractionOptions()
                    {
                        ErrorUrl = "/Home/Error"
                    };
                })
                .AddSigningCredential(certificate)
                // this adds the config data from DB (clients, resources)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                })
                .AddAspNetIdentity<ApplicationUser>()
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddProfileService<ProfileService>();

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(Configuration.GetSection("IdentityServer")["Authority"])
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // Add JWTs Authen
            services.AddAuthentication()
                .AddJwtBearer(jwt =>
                {
                    jwt.Authority = Configuration.GetSection("IdentityServer")["Authority"];
                    jwt.RequireHttpsMetadata = bool.Parse(Configuration.GetSection("IdentityServer")["RequireHttpsMetadata"]);
                    jwt.Audience = Configuration.GetSection("IdentityServer")["Audience"];
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("backoffice", policy =>
                   {
                       policy.RequireScope(Configuration.GetSection("IdentityServer")["Audience"]);
                       policy.RequireScope(Configuration.GetSection("IdentityServer")["Audience"] + ".backoffice");
                       policy.RequireRole(new string[] {"super_admin", "system", "admin", "business_owner"});
                   });

                options.AddPolicy("automate", policy =>
                   {
                       policy.RequireScope(Configuration.GetSection("IdentityServer")["Audience"]);
                       policy.RequireScope(Configuration.GetSection("IdentityServer")["Audience"] + ".automate");
                   });

                options.AddPolicy("mobile", policy =>
                   {
                       policy.RequireScope(Configuration.GetSection("IdentityServer")["Audience"]);
                       policy.RequireScope(Configuration.GetSection("IdentityServer")["Audience"] + ".mobile");
                   });
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();
            app.UseCors("default");

            InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            
            app.UseSerilogRequestLogging();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<LogUserNameMiddleware>();
            app.UseMiddleware<LogRequestInformationMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                    spa.Options.SourcePath = "ClientApp";
                else
                    spa.Options.SourcePath = "dist";

                if (env.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "serve", forceKill: true);
                }
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            try
            {
                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
                    serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                    var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                    context.Database.Migrate();

                    foreach (var client in Config.GetClients())
                    {
                        if (!context.Clients.Where(o => o.ClientId == client.ClientId).Any())
                        {
                            context.Clients.Add(client.ToEntity());
                            context.SaveChanges();
                        }
                    }

                    if (context.ApiResources.Count() == 0)
                    {
                        foreach (var resource in Config.GetApis())
                        {
                            context.ApiResources.Add(resource.ToEntity());
                        }
                        context.SaveChanges();
                    }

                    if (context.IdentityResources.Count() == 0)
                    {
                        foreach (var resource in Config.GetIdentityResources())
                        {
                            context.IdentityResources.Add(resource.ToEntity());
                        }
                        context.SaveChanges();
                    }
                }
            }
            catch { }
        }
    }
}
