using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebImageCloud.Views.Files
{
    public class FilesViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
           
            Task.Delay(1000).Wait();
            return View();
        }
    }
}
