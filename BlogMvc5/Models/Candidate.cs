using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMvc5.Models
{
    public class Candidate
    {
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public HttpPostedFileBase Cv { get; set; }
    }
}