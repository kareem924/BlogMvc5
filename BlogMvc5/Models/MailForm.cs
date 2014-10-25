using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMvc5.Models
{
    public class MailForm
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Mail { get; set; }
        public string Body { get; set; }
    }
}