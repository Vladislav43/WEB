using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluentify.Models.Database;

namespace Fluentify.Models.Frontend
{
    public class LoginUserRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter valid email address.")]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        // Google authentication properties
        public string? GoogleEmail { get; set; }
        public string? GoogleToken { get; set; }
    }
}
