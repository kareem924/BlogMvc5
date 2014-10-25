using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMvc5.Models
{
    public class Tag
    {
        public Tag ()
        {
            this.Post = new HashSet<Posts>();
        }
        [Key]
        public int TagId { get; set; }
        public string TagName { get; set; }
        public int PostId { get; set; }
        public ICollection< Posts> Post { get; set; }
    }
}