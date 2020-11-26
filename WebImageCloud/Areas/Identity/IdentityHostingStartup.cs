using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebImageCloud.Data;

[assembly: HostingStartup(typeof(WebImageCloud.Areas.Identity.IdentityHostingStartup))]
namespace WebImageCloud.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                //services.AddDbContext<WebImageCloudContext>(options =>
                //    options.UseSqlServer(
                //        context.Configuration.GetConnectionString("WebImageCloudContextConnection")));

                //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<WebImageCloudContext>();
            });
        }
    }
}