using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;


namespace BlogMvc5.Models
{
    public class ApplicationUser : IdentityUser
    {
    }
    public class BlogDbContext : IdentityDbContext<ApplicationUser>
    {
        public BlogDbContext() : base("BlogDbContext") { }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public  DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Page> Pages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new BlogDbInitializer());
          
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        
        }

    }
}