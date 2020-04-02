using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace QuickRedirectsForAzureWebsites
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
            services.AddRazorPages();
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            #region quick-redirects-for-azure-websites

            app.Run(async (context) =>
            {
                //read the website from env. variable which is automatically available when app is deployed on azure.

                var websiteName = Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME");

                //define your routes and urls here. actually you can add as many urls as you wish.
                Dictionary<string, string> urls = new Dictionary<string, string>();
                urls.Add("dev", $"https://{websiteName}.scm.azurewebsites.net/dev");
                urls.Add("kudu", $"https://{websiteName}.scm.azurewebsites.net");
                urls.Add("portal", $"https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Web%2Fsites");

                //last part of the url will be used to find the redirect url
                var path = context.Request.Path.Value.Split('/').LastOrDefault().ToLower();

                if (urls.ContainsKey(path))
                {
                    context.Response.Redirect(urls[path]);
                }
            });
            #endregion

        }
    }
}
