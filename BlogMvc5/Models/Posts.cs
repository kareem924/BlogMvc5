using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace BlogMvc5.Models
{
    public class Posts
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}