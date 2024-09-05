using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class AddBookViewModel
    {
        public int? ID { get; set; }

        [MaxLength(15)]
        [Required(ErrorMessage = "Please provide this info!!")]
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
        [Display(Name = "Choose Supject")]
        public int SubjectId { get; set; }
        [Required]
        [Display(Name = "Choose Puplisher")]
        public int PublisherId { get; set; }
        [Display (Name ="Choose Book Images")]
        [ImageLimit]
        public IFormFileCollection Images { get; set; }
        public List<string> ImagePaths { get; set; } = new List<string>();
        public bool KeepOldImages { get; set; } = true;
    }
}
