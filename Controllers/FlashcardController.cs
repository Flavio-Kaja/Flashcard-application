using FlashcardsApp.Data;
using FlashcardsApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashcardsApp.Controllers
{
    public class FlashcardController :Controller
    {
        private ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public FlashcardController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            string currentUserId = _userManager.GetUserId(User);
            return View(_db.flashcards.Include(c => c.Album).Where(c=>c.Album.userid==currentUserId).ToList());
        }
        public IActionResult Create()
        {
            string currentUserId = _userManager.GetUserId(User);
            ViewData["AlbumId"] = new SelectList(_db.albums.Where(c=>c.userid==currentUserId).ToList(), "Id", "Tittle");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Flashcard flashcard)
        {
            if (ModelState.IsValid)
            {
                //FlashcardAlbum album = _db.albums.Single(c => c.Id == flashcard.AlbumId);
                //Flashcard newflashcard = new Flashcard()
                //{
                //    Question = flashcard.Question,
                //    Answer = flashcard.Answer,
                //    Album=album
                //};
                _db.Add(flashcard);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Create));
            }
            return View(flashcard);
        }
        public IActionResult Edit(int? id)
        {
            ViewData["AlbumId"] = new SelectList(_db.albums.ToList(), "Id", "Tittle");
            if (id == null)
            {
                return NotFound();
            }
            var flashcard = _db.flashcards.Find(id);
            if (flashcard == null)
            {
                return NotFound();
            }
            return View(flashcard);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Flashcard flashcard)
        {
            if (ModelState.IsValid)
            {
                _db.Update(flashcard);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(flashcard);
        }
      
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var flashcard = _db.flashcards.Include(c => c.Album).Where(c => c.Id == id).FirstOrDefault();
            if (flashcard == null)
            {
                return NotFound();
            }
            return View(flashcard);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            var flashcard = _db.flashcards.FirstOrDefault(c => c.Id == id);
            if (ModelState.IsValid)
            {
                _db.Remove(flashcard);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flashcard);
        }
    }
}
