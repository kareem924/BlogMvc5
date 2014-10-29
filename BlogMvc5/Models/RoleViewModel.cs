using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMvc5.Models
{
    public class RoleViewModel
    {
        public string RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
        //public string UserName { get; set; }
    }
}