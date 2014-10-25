using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMvc5.Models
{
    public class BlogDbInitializer:System.Data.Entity.DropCreateDatabaseIfModelChanges<BlogDbContext>
    {
        protected override void Seed(BlogDbContext context)
        {
            context.Posts.Add(new Posts
            {
                Title = "Change Layout and runtime in MVC",
                Content = "You typically want to maintain a consistent look and feel across all of the pages within your web-site/application.  ASP.NET 2.0 introduced the concept of “master pages” which helps enable this when using .aspx based pages or templates.  Razor also supports this concept with a feature called “layouts” – which allow you to define a common site template, and then inherit its look and feel across all the views/pages on your site.",
                Tags = new List<Tag>
                {
                    new Tag{TagName = "ASP.NET"},
                    new Tag{TagName = "MVC"},
                    new Tag{TagName = "Razor"}
                }
            });
            context.SaveChanges();
            base.Seed(context);
        }
    }
}