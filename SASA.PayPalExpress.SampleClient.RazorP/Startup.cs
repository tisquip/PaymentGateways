using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateways.PayPal;
using SASA.PayPalExpress.SampleClient.RazorP.Data;
using SASA.PayPalExpress.SampleClient.RazorP.Models;

namespace SASA.PayPalExpress.SampleClient.RazorP
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
            services.AddMvc();

            services.AddDbContext<SASAPayPalExpressSampleClientRazorPContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SASAPayPalExpressSampleClientRazorPContext")));

            services.AddScoped<IPayPalSettings, PayPalSettingsObject>();
            services.AddScoped<IRepositoryPayPalBearersToken, RepositoryPayPalBearersToken>();
            services.AddScoped<IPayPalSASA, PayPalSASA>();
            services.AddScoped<IPayPalSASAScript, PayPalSASAScript>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
