using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMvc5.Models
{
    public class UserRoleViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public List<RoleViewModel> Roles { get; set; }
    }
}