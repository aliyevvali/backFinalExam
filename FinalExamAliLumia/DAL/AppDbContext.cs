using FinalExamAliLumia.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamAliLumia.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions opt):base(opt)
        {

        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Setting> Settings{ get; set; }
    }
}
