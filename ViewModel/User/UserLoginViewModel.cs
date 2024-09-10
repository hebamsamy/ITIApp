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
        [Required, StringLength(10, MinimumLength = 8)]
        public string UserName { get; set; }
        //[Required, EmailAddress]
        //public string Email { get; set; }

        [Required, StringLength(10, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RemeberMe {  get; set; } = false;
    }
}
