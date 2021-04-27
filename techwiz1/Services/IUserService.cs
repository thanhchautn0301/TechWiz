using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.Services
{
    public interface IUserService
    {
        Task<User> Find(int? id);
        Task<User> EmailExists(string email);
        Task Create([Bind("Id,Email,FbId,GgId,Password,Salt,LastName,FirstName,Phone,Role,Avatar,Status,CreatedAt,UpdatedAt")] User user);
        Task Update([Bind("Id,Email,FbId,GgId,Password,Salt,LastName,FirstName,Phone,Role,Avatar,Status,CreatedAt,UpdatedAt")] User user);
    }
}
