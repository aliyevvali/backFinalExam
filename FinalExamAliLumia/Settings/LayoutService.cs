using FinalExamAliLumia.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamAliLumia.Settings
{
    public class LayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext contex)
        {
            _context = contex;
        }
        public Dictionary<string,string> GetSetting()
        {
            return _context.Settings.ToDictionary(x=>x.Key,x=>x.Value);
        }
    }
}
