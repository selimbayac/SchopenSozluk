using SchopenSozlukEntityLayer.Concrete;

namespace SchopenSozlukPresentationLayer.Models
{
    public class BaslikDetayViewModel
    {
        public Baslik Baslik { get; set; }
        public List<Entry> Entry { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
