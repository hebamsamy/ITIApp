using System.ComponentModel.DataAnnotations;

namespace ViewModel
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
        [Display(Name = "Number of Pages")]

        public int PageCount { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Publication Date")]

        public DateTime PublicationDate { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Display(Name ="Choose Supject")]
        public int SubjectId { get; set; }
        [Required]
        [Display(Name = "Choose Puplisher")]
        public int PublisherId { get; set; }
            

        //imgaes

    }
}
