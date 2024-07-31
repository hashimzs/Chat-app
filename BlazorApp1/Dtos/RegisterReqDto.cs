using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.Dtos
{
    public class RegisterReqDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(8,ErrorMessage = "The password must have alteast a length of 8 ")]
        [RegularExpression("^(?=.*[A-Z]).*$", ErrorMessage ="The password must have atleast have one uppercase letter")]
        public string Password { get; set; } = string.Empty;
    }
}
