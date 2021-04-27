using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.Services
{
    public interface TagService
    {
        Task<List<Tag>> List();
        Task<Tag> Find(int? id);
        Task Create(Tag tag);
        Task Edit(int id, [Bind("IdTag,NameTag,Status")] Tag tag);
        Task Delete(int id);
        Task<Tag> Details(int? id);
    }
}
