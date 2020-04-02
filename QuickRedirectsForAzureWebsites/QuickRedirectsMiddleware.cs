using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace QuickRedirectsForAzureWebsites
{
    public class QuickRedirectsMiddleware
    {
        private readonly RequestDelegate _next;
        private Dictionary<string, string> _redirectUrls;

        public QuickRedirectsMiddleware(RequestDelegate next)
        {
            _next = next;
            InitRedirectUrls();
        }

        private void InitRedirectUrls()
        {
            if (_redirectUrls == null)
            {
                //read the website from env. variable which is automatically available when app is deployed on azure.
                //define this parameter in project settings to test in local
                _redirectUrls = new Dictionary<string, string>();
                var websiteName = Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME");

                //define your routes and urls here. actually you can add as many urls as you wish.
                _redirectUrls.Add("dev", $"https://{websiteName}.scm.azurewebsites.net/dev");
                _redirectUrls.Add("kudu", $"https://{websiteName}.scm.azurewebsites.net");
                _redirectUrls.Add("portal", $"https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Web%2Fsites");

                //TODO : Add your custom urls here.
            }
        }

        public Task Invoke(HttpContext httpContext)
        {

            //last part of the url will be used to find the redirect url
            var path = httpContext.Request.Path.Value.Split('/').LastOrDefault().ToLower();

            if (_redirectUrls.ContainsKey(path))
            {
                httpContext.Response.Redirect(_redirectUrls[path]);
                return Task.CompletedTask;
            }

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class QuickRedirectsMiddlewareExtensions
    {
        public static IApplicationBuilder UseQuickRedirectsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<QuickRedirectsMiddleware>();
        }
    }
}
