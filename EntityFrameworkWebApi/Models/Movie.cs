using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EntityFrameWorkWebApi.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }

    }
}