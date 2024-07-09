using Microsoft.AspNetCore.Identity;
using SchopenSozlukEntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukEntityLayer.Concrete
{

    public class Baslik
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UserId { get; set; } // Nullable olmalı
        public AppUser User { get; set; }
        public ICollection<Entry> Entries { get; set; }
    }
}
