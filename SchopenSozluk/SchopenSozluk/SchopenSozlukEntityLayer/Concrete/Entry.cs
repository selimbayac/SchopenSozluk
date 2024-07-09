using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukEntityLayer.Concrete
{
        public class Entry
        {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public int BaslikId { get; set; }
        public Baslik Baslik { get; set; }
        public ICollection<Like> Likes { get; set; }  // Entry'yi beğenen kullanıcılar
        public ICollection<Dislike> Dislikes { get; set; }
        public int? ParentEntryId { get; set; }
        public Entry ParentEntry { get; set; }
        public ICollection<Entry> Replies { get; set; }

        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public ICollection<Comment> Comments { get; set; }  // Yorumlar
    }
}
