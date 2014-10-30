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
           [Required]
        public string Content { get; set; }
           [Display(Name = "Post Title")]
           [MaxLength(200)]
           [MinLength(10)]
           [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public  ICollection<Comment> Comments { get; set; }
    }
}