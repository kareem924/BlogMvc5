using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMvc5.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public string TagName { get; set; }
        public int PostId { get; set; }
        public Posts Post { get; set; }
    }
}