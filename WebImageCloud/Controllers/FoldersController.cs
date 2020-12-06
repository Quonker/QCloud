using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebImageCloud.Data;
using WebImageCloud.Models;
using WebImageCloud.Models.TypesFiles;
using WebImageCloud.ViewModel;
using File = WebImageCloud.Models.File;

namespace WebImageCloud.Controllers
{
    [Authorize]
    public class FoldersController : Controller
    {
        private readonly WebImageCloudContext _context;
        private readonly IMapper _mapper;
        public FoldersController(WebImageCloudContext context, IMapper mapper)
        {

            _mapper = mapper;
            _context = context;
        }

        // GET: Folders
        public async Task<IActionResult> Index()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Folder, FolderViewModel>());
            // Настройка AutoMapper
            var mapper = new Mapper(config);
            var folders = mapper.Map<IEnumerable<FolderViewModel>>(await _context.Folder.ToListAsync());
            return View(folders );
        }

        // GET: Folders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

          
            var folder = _mapper.Map<FolderViewModel>(await _context.Folder
                .FirstOrDefaultAsync(m => m.Id == id));

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId != folder.UserId)
            {
                return LocalRedirect("~/Home/Library");
            }

            if (folder == null)
            {
                return NotFound();
            }
            var files = _context.Files.Where(f => f.FolderId == folder.Id);

            var filesVM = new List<FileViewModel>();

            foreach (var item in files)
            {
                filesVM.Add(_mapper.Map<FileViewModel>(item));
            }


            ViewBag.Files = _mapper.Map<IEnumerable<FileViewModel>>(filesVM);



            return View(folder);
        }

        // GET: Folders/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: Folders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateOfCreate,DateOfChange,Path,Size,UserId")] Folder folder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(folder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(folder);
        }

        // GET: Folders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var folder = await _context.Folder.FindAsync(id);
            if (folder == null)
            {
                return NotFound();
            }
            return View(folder);
        }

        // POST: Folders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,DateOfCreate,DateOfChange,Path,Size,UserId")] Folder folder)
        {
            if (id != folder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(folder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FolderExists(folder.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(folder);
        }

        // GET: Folders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var folder = await _context.Folder
                .FirstOrDefaultAsync(m => m.Id == id);
            if (folder == null)
            {
                return NotFound();
            }

            return View(folder);
        }

        // POST: Folders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var folder = await _context.Folder.FindAsync(id);
            _context.Folder.Remove(folder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FolderExists(int? id)
        {
            return _context.Folder.Any(e => e.Id == id);
        }

        public ActionResult FolderToolsPartial(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var foldersid = _context.Folder.Where(f => f.UserId == userid).Select(f => f.Id);
                var files = _context.Files.Where(f => f.FolderId == foldersid.Where(id => id == f.FolderId).FirstOrDefault());
                var filesVM = new List<FileViewModel>();


                foreach (var item in files)
                {
                    filesVM.Add(_mapper.Map<FileViewModel>(item));
                }
                
                ViewBag.Files = filesVM;
            }
            else
            {
                ViewBag.Files = new List<FileViewModel>();
            }
            return PartialView();
        }

    }
}
