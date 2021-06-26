using FlashcardsApp.Data;
using FlashcardsApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FlashcardsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            string currentUserId = _userManager.GetUserId(User);
            return View(_db.albums.Where(c=>c.userid==currentUserId).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FlashcardAlbum album)
        {
            string currentUserId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                album.userid = currentUserId;
                _db.Add(album);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(album);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var album = _db.albums.Find(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var album = _db.albums.Find(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FlashcardAlbum album)
        {
            if (ModelState.IsValid)
            {
                _db.Update(album);
                await _db.SaveChangesAsync();
                RedirectToAction(actionName: nameof(Index));
            }
            return View(album);
        }

        public IActionResult DisplayEntries(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            FlashcardAlbum album = _db.albums.Find(id);
            ViewData["Tittle"] = album.Tittle;
            if (album == null)
            {
                return NotFound();
            }

            var entries = _db.flashcards.Where(c => c.Album == album).ToList();
            return View(entries);
        }
        //Delete action method 
        public IActionResult Delete (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var album = _db.albums.Find(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            var album = _db.albums.FirstOrDefault(c=>c.Id==id);
            if (ModelState.IsValid)
            {
                _db.Remove(album);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(album);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
