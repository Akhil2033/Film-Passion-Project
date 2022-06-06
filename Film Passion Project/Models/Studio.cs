using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Film_Passion_Project.Models
{
    public class Studio
    {
        [Key]
        public int StudioId { get; set; }
        public string StudioName { get; set; }
    }
}