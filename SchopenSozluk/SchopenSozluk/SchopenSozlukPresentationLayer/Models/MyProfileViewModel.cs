namespace SchopenSozlukPresentationLayer.Models
{
    public class MyProfileViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Bio { get; set; }
        public string ProfileImage { get; set; }
        public List<BaslikViewModel> Basliklar { get; set; }
        public List<EntryViewModel> Entries { get; set; }
    }
}
