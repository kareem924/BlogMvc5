using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace BlogMvc5.Models
{
    public class Posts
    {
        public Posts()
        {
            this.Comments=new HashSet<Comment>();
            this.Tags=new HashSet<Tag>();
        }
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}