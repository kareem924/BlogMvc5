using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogMvc5.Models.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BlogMvc5.Models.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        BlogDbContext _context;
        public UnitOfWork()
        {
            _context = new BlogDbContext();
        }
        private IGenericRepository<Posts> _posts;
        public Repositories.IGenericRepository<Posts> Posts
        {
            get
            {
                if (_posts == null)
                {
                    return new EfGenericRepository<Posts>(_context);
                }
                return _posts;
            }
        }
        private IGenericRepository<IdentityRole> _Roles;
        public Repositories.IGenericRepository<IdentityRole> Roles
        {
            get
            {
                if (_Roles == null)
                {
                    return new EfGenericRepository<IdentityRole>(_context);
                }
                return _Roles;
            }
        }
        private IGenericRepository<ApplicationUser> _users;
        public Repositories.IGenericRepository<ApplicationUser> Users
        {
            get
            {
                if (_users == null)
                {
                    return new EfGenericRepository<ApplicationUser>(_context);
                }
                return _users;
            }
        }
        private IGenericRepository<Tag> _tags;
        public Repositories.IGenericRepository<Tag> Tags
        {
            get
            {
                if (_tags == null)
                {
                    return new EfGenericRepository<Tag>(_context);
                }
                return _tags;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}