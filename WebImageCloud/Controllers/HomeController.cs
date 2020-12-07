using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebImageCloud.Data;
using WebImageCloud.Models;

namespace WebImageCloud.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private WebImageCloudContext _context;
        private UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, WebImageCloudContext context, UserManager<User> userManager)
        {
            _logger = logger;
           _context = context;
            _userManager = userManager;

        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Library()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            var folders = _context.Folder.Where(f => f.UserId == id);
            ViewBag.FoldersCount = folders.Select(fol => (_context.Files.Where(fil => fil.FolderId == fol.Id).Count())).ToArray();
            ViewBag.Storage = folders.Sum(f => f.Size);
            ViewBag.StorageDiv = (Convert.ToDouble(folders.Sum(f => f.Size)) / user.Storage) * 100;
            ViewBag.MaxStorage = user.Storage;

            return View(folders);
            
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
