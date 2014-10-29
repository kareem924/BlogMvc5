using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogMvc5.Models.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BlogMvc5.Models.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Posts> Posts { get; }

        IGenericRepository<Tag> Tags { get; }
        IGenericRepository<ApplicationUser> Users { get; }
        IGenericRepository<IdentityRole> Roles { get; }
        void Save(); //Commit

    }
}
