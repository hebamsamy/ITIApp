using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class UserLoginViewModel
    {
        [Required, StringLength(50, MinimumLength = 8)]
        [Display(Name ="Enter UserName Or Email")]
        public string LoginMethod { get; set; }

        [Required, StringLength(10, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RemeberMe {  get; set; } = false;
        public string ReturnUrl {  get; set; } = "/";
    }
}
