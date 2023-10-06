using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Services
{
    public interface IFileStore
    {
        public static string _uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        public bool saveFile(IFormFile file);
        public byte[] readFileByName(String name);

        public bool deleteFile(String name);
    }

}
