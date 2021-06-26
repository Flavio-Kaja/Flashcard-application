using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FlashcardsApp.Models;
namespace FlashcardsApp
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Flashcard> flashcards { get; set; }
        public DbSet<FlashcardAlbum> albums { get; set; }
        public  DbSet<AppUser>users { get; set; }
    }
}
