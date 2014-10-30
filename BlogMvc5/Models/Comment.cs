using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMvc5.Models
{
    public class Comment
    {
          [Key]
        public int Id { get; set; }
        public string Body { get; set; }
        //public  Posts post { get; set; }
        public int PostId { get; set; }
    }
}