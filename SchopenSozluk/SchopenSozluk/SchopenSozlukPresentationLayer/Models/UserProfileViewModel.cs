using SchopenSozlukEntityLayer.Concrete;

namespace SchopenSozlukPresentationLayer.Models
{
    public class UserProfileViewModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImage { get; set; }    
        public List<Baslik> Basliklar { get; set; }
        public List<Entry> Entries { get; set; }
    }
}
