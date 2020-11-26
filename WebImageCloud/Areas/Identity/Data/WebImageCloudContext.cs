using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebImageCloud.Models;

namespace WebImageCloud.Data
{
    public class WebImageCloudContext : IdentityDbContext<User>
    {
        public DbSet<File> Files { get; set; }
        public WebImageCloudContext(DbContextOptions<WebImageCloudContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<WebImageCloud.Models.Folder> Folder { get; set; }
    }
}
