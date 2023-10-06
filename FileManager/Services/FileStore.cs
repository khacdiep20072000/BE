
using FileManager.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Services
{
    public class FileStore : IFileStore
    {
        public FileStore()
        {

        }

        public bool deleteFile(string fileName)
        {
            string filePath = Path.Combine(IFileStore._uploadsPath, fileName);
            if (File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                return true;
            }
            else
            {
                throw new NotFoundException("Không tìm thấy tệp tin.");
            }
        }

        public byte[] readFileByName(string fileName)
        {
         
            string filePath = Path.Combine(IFileStore._uploadsPath, fileName);
            if (File.Exists(filePath))
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                return fileBytes;
            }
            else
            {
                throw new NotFoundException("Hình ảnh không tồn tại.");
            }
        }

        public bool saveFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }
            var filePath = Path.Combine(IFileStore._uploadsPath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }
            return true;
        }
    }
}
