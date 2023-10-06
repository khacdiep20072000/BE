using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Dto
{
    public class FileRequest
    {
        IFormFile file;
        String author;

        public FileRequest(IFormFile file, string author)
        {
            this.File = file;
            this.Author = author;
        }

        public IFormFile File { get => file; set => file = value; }
        public string Author { get => author; set => author = value; }
    }
}
