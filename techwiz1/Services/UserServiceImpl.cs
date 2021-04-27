using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.Services
{
    public class UserServiceImpl : IUserService
    {
        private DatabaseContext db;
        public UserServiceImpl(DatabaseContext databaseContext)
        {
            db = databaseContext;
        }
        public async Task Create([Bind("Id,Email,FbId,GgId,Password,Salt,LastName,FirstName,Phone,Role,Avatar,Status,CreatedAt,UpdatedAt")] User user)
        {
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        public async Task<User> EmailExists(string email)
        {
            var user = await db.Users.SingleOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<User> Find(int? id) => await db.Users.FindAsync(id);

        public async Task Update([Bind(new[] { "Id,Email,FbId,GgId,Password,Salt,LastName,FirstName,Phone,Role,Avatar,Status,CreatedAt,UpdatedAt" })] User user)
        {
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

    }
}
