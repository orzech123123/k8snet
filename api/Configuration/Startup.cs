using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using react_app.BackgroundTasks;
using react_app.Utils;
using Serilog;

namespace react_app.Configuration
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.Configure<Settings>(Configuration.GetSection("settings"));

          
            //services.AddDbContext<WmprojackDbContext>(
            //    o => o. UseSqlServer(Configuration.GetConnectionString("wmprojack"),
            //    options => options.EnableRetryOnFailure())
            //);

            services.AddTransient<CommandExecutor>();

            //services.AddSingleton<IJobFactory, SingletonJobFactory>();
            //services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            //services.AddHostedService<QuartzHostedService>();

            //services.AddTransient<OrdersSyncBackgroundJob>();
            //services.AddSingleton(new JobSchedule(
            //    jobType: typeof(OrdersSyncBackgroundJob),
            //    cronExpression: "0 0/1 * * * ?"));

            //services.AddTransient<RefreshAllegroTokenBackgroundJob>();
            //services.AddSingleton(new JobSchedule(
            //    jobType: typeof(RefreshAllegroTokenBackgroundJob),
            //    cronExpression: "0 0/15 * * * ?"));

            //services.AddTransient<TruncateLogsBackgroundJob>();
            //services.AddSingleton(new JobSchedule(
            //    jobType: typeof(TruncateLogsBackgroundJob),
            //    cronExpression: "0 0 0/23 * * ?"));

            //services.AddTransient<EmailMinimumInventoryBackgroundJob>();
            //services.AddSingleton(new JobSchedule(
            //    jobType: typeof(EmailMinimumInventoryBackgroundJob),
            //    cronExpression: "0 0 0/23 * * ?"));

            //services.AddTransient<ActivateInactiveAllegroOffersJob>();
            //services.AddSingleton(new JobSchedule(
            //    jobType: typeof(ActivateInactiveAllegroOffersJob),
            //    cronExpression: "0 0/1 * * * ?"));

            //services.AddTransient<BackupAndSendDatabasesJob>();
            //services.AddSingleton(new JobSchedule(
            //    jobType: typeof(BackupAndSendDatabasesJob),
            //    cronExpression: "0 0 0/23 * * ?"));
        }

        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            //wmprojackDbContext.Database.EnsureCreated();
        }
    }
}
