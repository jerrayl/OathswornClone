using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oathsworn.Database;
using Oathsworn.Business;
using Oathsworn.Repositories;
using Oathsworn.SignalR;
using Oathsworn.Business.Services;
using Oathsworn.Business.Bosses;
using Oathsworn.Infrastructure;

namespace Oathsworn
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<DatabaseContext>();

            services.AddTransient<GoogleJwtBearerHandler>();
            services.AddScoped<IUserContextFactory, UserContextFactory>();
            services.AddScoped(sp => sp.GetRequiredService<IUserContextFactory>().UserContext);
            services.AddScoped<IUserManagement, UserManagement>();
            services.AddScoped<IGame, Game>();
            services.AddScoped<IMightCardsService, MightCardsService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IBossFactory, BossFactory>();
            services.AddScoped<IBossDependencies, BossDependencies>();
            services.AddScoped(typeof(IDatabaseRepository<>), typeof(DatabaseRepository<>));

            // Add Swagger
            services.AddSwaggerDocument();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });

            services.AddCors();

            services.AddSignalR(options => { options.EnableDetailedErrors = Environment.IsDevelopment(); });

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Startup>());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddScheme<JwtBearerOptions, GoogleJwtBearerHandler>(JwtBearerDefaults.AuthenticationScheme, options => { });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseOpenApi();
                app.UseSwaggerUi();
            }

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseUserContext();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapHub<SignalRHub>("signalr");
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            using (var dbContext = serviceScope.ServiceProvider.GetService<DatabaseContext>())
            {
                DatabaseSeed.Seed(dbContext);
            }
        }
    }
}
