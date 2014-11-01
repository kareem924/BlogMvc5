using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMvc5.Models
{
    public class Candidate
    {
          [Required]
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public HttpPostedFileBase Cv { get; set; }
    }
}