using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogMvc5.Models.Repositories;

namespace BlogMvc5.Models.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Posts> Posts { get; }

        IGenericRepository<Tag> Tags { get; }

        void Save(); //Commit

    }
}
