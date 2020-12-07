using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebImageCloud.Data;
using WebImageCloud.Models;
using WebImageCloud.Models.FileExtensions;
using WebImageCloud.Models.TypesFiles;
using WebImageCloud.Views.Files;

namespace WebImageCloud.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        private readonly WebImageCloudContext _context;

        public FilesController(WebImageCloudContext context)
        {
            _context = context;
        }

        // GET: Files
        public async Task<IActionResult> Index()
        {
            var webImageCloudContext = _context.Files.Include(f => f.Folder);
            return View(await webImageCloudContext.ToListAsync());
        }

        // GET: Files/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.Files
                .Include(f => f.Folder)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        // GET: Files/Create
        public IActionResult Create()
        {

            ViewData["FolderId"] = new SelectList(_context.Set<Folder>(), "Id", "Id");
            return View();
        }

        // POST: Files/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [RequestSizeLimit(2147483648)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("Id,Name,Path,Size,FolderId")] Models.File file,*/ IFormFile content)
        {
            var file = new Models.File();
            if (ModelState.IsValid)
            {
                if (content != null)
                {
                    if (content.Length > 0)
                    {
                        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        var user = _context.Users.FirstOrDefault(u => u.Id == id);
                        if (content.Length + user.UseStorage <= user.Storage)
                        {
                            int? folderId;
                            using (var memoryStream = new MemoryStream())
                            {
                                file.Name = content.FileName;
                                //file.Size = (int)content.Length;
                                file.Extension = Path.GetExtension(content.FileName).Remove(0, 1);
                                var fileType = FileType(ref file);

                                folderId = _context.Folder.Where(f => f.UserId == id).FirstOrDefault(f => f.Name == fileType).Id;
                                file.FolderId = folderId;
                                _context.Folder.Where(f => f.UserId == id).FirstOrDefault(f => f.Name == fileType).DateOfChange = DateTime.Now;
                               
                                await content.CopyToAsync(memoryStream);
                                file.ExtualyFile = memoryStream.ToArray();
                                _context.Folder.Where(f => f.UserId == id).FirstOrDefault(f => f.Name == fileType).Size += file.ExtualyFile.Length;

                            }
                            _context.Add(file);
                            await _context.SaveChangesAsync();
                            return LocalRedirect("~/Folders/Details/" + folderId);
                        }
                        else
                        {
                            ViewBag.Modal = "size";
                            return View();
                            //  return PartialView("FilesOutOfMemory");

                        }
                       
                    }

                }
                else
                {
                    ViewBag.Modal = "null";
                    return View();
                }
            }
            ViewData["FolderId"] = new SelectList(_context.Set<Folder>(), "Id", "Id", file.FolderId);
            return View(file);
        }

        private string FileType(ref Models.File File)
        {
            foreach (var t in (EnumFileIcons[])Enum.GetValues(typeof(EnumFileIcons)))
            {
                if (File.Extension == t.ToString())
                {
                    File.Icon = t.ToString();
                    break;
                }
                File.Icon = "file-8";
            }
                foreach (var t in (EnumAudioTypes[])Enum.GetValues(typeof(EnumAudioTypes)))
            {
                if (File.Extension == t.ToString())
                {
                    return "Sounds";
                }
            }
            foreach (var t in (EnumImageTypes[])Enum.GetValues(typeof(EnumImageTypes)))
            {
                if (File.Extension == t.ToString())
                {
                    return "Pictures";
                }
            }
            foreach (var t in (EnumVideoTypes[])Enum.GetValues(typeof(EnumVideoTypes)))
            {
                if (File.Extension == t.ToString())
                {
                    return "Videos";
                }
            }
            return "Other";
        }


        // GET: Files/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.Files.FindAsync(id);
            if (file == null)
            {
                return NotFound();
            }
            ViewData["FolderId"] = new SelectList(_context.Set<Folder>(), "Id", "Id", file.FolderId);
            return View(file);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Path,Size,FolderId")] Models.File file)
        {
            if (id != file.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(file);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileExists(file.Id))
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
            ViewData["FolderId"] = new SelectList(_context.Set<Folder>(), "Id", "Id", file.FolderId);
            return View(file);
        }

        // GET: Files/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.Files
                .Include(f => f.Folder)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var file = await _context.Files.FindAsync(id);
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileExists(int? id)
        {
            return _context.Files.Any(e => e.Id == id);
        }

        public ActionResult Download(int? id)
        {
            if (id != null)
            {
                var file = _context.Files.FirstOrDefault(f => f.Id == id);
                if(file != null)
                {
                    return File(file.ExtualyFile, System.Net.Mime.MediaTypeNames.Application.Octet, file.Name);
                }
            }
            return NotFound();
        }
    }
}
