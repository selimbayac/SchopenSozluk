using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukEntityLayer.Concrete
{
    public class Comment
    {
        public int Id { get; set; } // Yorum için birincil anahtar
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public int EntryId { get; set; }  // Hangi entry'ye ait olduğunu belirten yabancı anahtar
        public Entry Entry { get; set; }
    }
}
