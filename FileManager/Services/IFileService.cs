using FileManager.Dto;
using FileManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Services
{
    public interface IFileService
    {
        ICollection<FileEntity> getAllFile();
        FileEntity getFileById(int id);
        bool deleteFileById(int id);
        bool saveFile(String author, IFormFile file);
        public FileRespon viewFileById(int id);
    }
}
