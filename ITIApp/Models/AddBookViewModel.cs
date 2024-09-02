using System.ComponentModel.DataAnnotations;

namespace ITIApp
{
    public class AddBookViewModel
    {
        public int? ID { get; set; }

        [MaxLength(15)]
        [Required(ErrorMessage ="Please provide this info!!")]
        public string Isbn { get; set; }
        [Required]
        public string Notes { get; set; }
        [Required]
        public int PageCount { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime PublicationDate { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int PublisherId { get; set; }
            



    }
}
