using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukEntityLayer.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImage { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ICollection<Baslik> Basliklar { get; set; } // Baslik'lar koleksiyonu
        public ICollection<Entry> Entries { get; set; }
        public int ConfirmCode { get; set; } // eposta

    }
}
