using FileManager.Data;
using FileManager.Dto;
using FileManager.Exceptions;
using FileManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Services
{
    public class FileService : IFileService
    {
        private DataContext _dbContext;
        private IFileStore _fileStore;
        public FileService(DataContext dbContext, IFileStore fileStore)
        {
            this._dbContext = dbContext;
            this._fileStore = fileStore;
        }
        public bool deleteFileById(int id)
        {
            var fileEntity = this._dbContext.FileEntity.Find(id);

            if (fileEntity != null)
            {
                this._dbContext.FileEntity.Remove(fileEntity);
                bool check =  this._dbContext.SaveChanges() == 1;
                if (check)
                {
                    this._fileStore.deleteFile(fileEntity.FileName);
                    return true;
                }
            }
            throw new NotFoundException("Không tìm thấy file chỉ định");
        }

        public ICollection<FileEntity> getAllFile()
        {
            ICollection<FileEntity> files = this._dbContext.FileEntity.ToList();
            if (files.Count() > 0)
                return files;
            else throw new NotFoundException("Danh sách rỗng");
        }

        public FileEntity getFileById(int id)
        {
            FileEntity files = this._dbContext.FileEntity.Find(id);
            if (files != null)
                return files;
            else throw new NotFoundException("Không tìm thấy file");
        }
        public FileRespon viewFileById(int id)
        {
            FileEntity fileEntity = this._dbContext.FileEntity.Find(id);
            if (fileEntity != null)
            {
                String contentType = this.getContentType(fileEntity.FileName);

                byte[] fileBytes = this._fileStore.readFileByName(fileEntity.FileName);

                if (fileBytes != null && fileBytes.Length > 0)
                {
                    FileRespon fileRespon = new FileRespon();
                    fileRespon.FileBytes = fileBytes;
                    fileRespon.ContentType = contentType;
                    return fileRespon;
                }
                throw new NotFoundException("Không tim thấy file");
            }
            throw new NotFoundException("Không tim thấy file");

        }
        public bool saveFile(string author, IFormFile file)
        {
            if (_fileStore.saveFile(file) == true){
                Console.WriteLine(author);
                FileEntity fileEntity = new FileEntity();
                fileEntity.FileName = file.FileName;
                fileEntity.Author = author;
                fileEntity.UploadDate = DateTime.Now;
                this._dbContext.Add(fileEntity);
                return _dbContext.SaveChanges() > 0;
            }
            return false;
        }


        public String getContentType(String fileName)
        {
            fileName = fileName.Trim().ToLower();
            
            if (fileName.EndsWith(".jpg") || fileName.EndsWith(".jpeg"))
            {
                return "image/jpeg";
            }
            else if (fileName.EndsWith(".png"))
            {
                return "image/png";
            }
            else if (fileName.EndsWith(".gif"))
            {
                return "image/gif";
            }
            else if (fileName.EndsWith(".pdf"))
            {
                return "application/pdf";
            }
            else if (fileName.EndsWith(".doc")|| fileName.EndsWith(".docx"))
            {
                return "application/msword";
            }
            else if (fileName.EndsWith(".xls")  || fileName.EndsWith(".xlsx"))
            {
                return "application/vnd.ms-excel";
            }
            else
                return "application/octet-stream";
        }
    }
}
