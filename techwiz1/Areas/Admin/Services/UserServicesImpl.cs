using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Areas.Admin.Models;

namespace TechWizProject.Areas.Admin.Services
{
    public class UserServicesImpl : IUserServices
    {
        private DatabaseContext db;
        public UserServicesImpl(DatabaseContext databaseContext)
        {
            db = databaseContext;
        }
        public async Task Create(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
        }

        public async Task<User> EmailExists(string email)
        {
            var user = await db.Users.SingleOrDefaultAsync(u => u.Email == email);
            return user != null ? user : null;
        }

        public async Task<List<User>> FindAll() => await db.Users.ToListAsync();
    }
}
