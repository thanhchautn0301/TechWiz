using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Areas.Admin.Models;

namespace TechWizProject.Areas.Admin.Services
{
    public interface IUserServices
    {
        Task<List<User>> FindAll();
        Task Create(User user);
        Task<User> EmailExists(string email);
    }
}
