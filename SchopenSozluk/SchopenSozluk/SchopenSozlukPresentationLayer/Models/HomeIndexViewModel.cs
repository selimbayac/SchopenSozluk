using SchopenSozlukEntityLayer.Concrete;

namespace SchopenSozlukPresentationLayer.Models
{
    public class HomeIndexViewModel
    {
        public List<Baslik> Basliklar { get; set; }
        public List<Entry> Entries { get; set; }
        public Dictionary<int, string> UserNames { get; set; }
    }
}
