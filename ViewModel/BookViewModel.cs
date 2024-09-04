namespace ViewModel
{
    public class BookViewModel
    {
        public int ID { get; set; }
        public string Isbn { get; set; }
        public string Notes { get; set; }
        public int PageCount { get; set; }
        public decimal Price { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public ICollection<string> Authers { get; set; }
        public ICollection<string> Attachments { get; set; }
    }
}
