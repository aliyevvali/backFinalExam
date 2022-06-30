using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamAliLumia.Models
{
    public class Team
    {
       
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Posission { get; set; }
        [Required]
        public string FeedBack { get; set; }
        public string  SocialIcon { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
