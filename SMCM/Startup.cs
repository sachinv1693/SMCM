using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.Repositories;
using SmartMeterConsumerManagement.ServiceContracts;
using SmartMeterConsumerManagement.BillGeneration;
using Microsoft.AspNetCore.Authorization;

namespace SmartMeterConsumerManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public const string CookieScheme = "myauthenticationschema";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SMCM_DBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DevConnection"));
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<SMCM_LoginContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVendorRepository, VendorRepository>();
            services.AddScoped<ISmartMeterRepository, SmartMeterRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IUserRequestRepository, UserRequestRepository>();
            services.AddScoped<IComplaintRepository, ComplaintRepository>();
            services.AddControllersWithViews(options => options.EnableEndpointRouting = false);
            services.AddAuthentication(CookieScheme)
                .AddCookie(CookieScheme, options =>
                {
                    options.AccessDeniedPath = "/authentication/AccessDenied";
                    options.LoginPath = "/authentication/login";
                });
            services.AddAuthorization(config =>
            {
                config.AddPolicy("UserPolicy", policyBuilder =>
                {
                    policyBuilder.UserRequireCustomClaim("UserEmail");
                    policyBuilder.UserRequireCustomClaim("UserRole");
                });
            });
            services.AddScoped<IAuthorizationHandler, PoliciesAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();
            services.AddHttpContextAccessor();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DevConnection")));
            services.AddScoped<IBillGenerator, BillGenerator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Authentication}/{action=Login}/{id?}");
            });

            // Using HangFireServer to process monthly bill generation in a background thread
            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate<IBillGenerator>(
                billGenerator => billGenerator.GenerateBillsForAllConsumers(), Cron.Monthly); // Runs at 6:52 on the 1st day of every month
        }
    }
}
