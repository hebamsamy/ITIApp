using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class RoleViewModel
    {
        [Required]
        public string Name { get; set; }
        public string ID { get; set; } = string.Empty;
    }
}
