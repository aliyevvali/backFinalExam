using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamAliLumia.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string Email{ get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
