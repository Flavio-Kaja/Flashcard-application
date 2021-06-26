using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlashcardsApp.Models
{
    public class Flashcard
    {
        public int Id { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public string Answer { get; set; }
        public int AlbumId { get; set; }
        public FlashcardAlbum Album { get; set; }
    }
}
