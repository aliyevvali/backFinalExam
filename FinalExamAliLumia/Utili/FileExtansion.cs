using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamAliLumia.Utili
{
    public static class FileExtansion
    {
        public static bool CheckSize(this IFormFile file,int kb)
        {
            if (file.Length / 1024 > kb) return true;
            return false;
        }
        public static bool CheckType(this IFormFile file, string type)
        {
            if (file.ContentType.Contains(type)) return true;
            return false;
        }
        public static async Task<string> SaveFile(this IFormFile file,string path)
        {
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string savepath = Path.Combine(path,fileName);
            using (FileStream stream = new FileStream(savepath,FileMode.Create))
            {
               await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
