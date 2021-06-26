using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlashcardsApp.Models
{
    public class FlashcardAlbum
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tittle")]
        public string Tittle { get; set; }
        [Display(Name = "Flashcards")]
        public List<Flashcard> Flashcards { get; set; }
        public string userid { get; set; }
    }
}
