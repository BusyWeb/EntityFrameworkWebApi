using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EntityFrameWorkWebApi.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; } 
        [Required]
        public string Name { get; set; }
    }
}