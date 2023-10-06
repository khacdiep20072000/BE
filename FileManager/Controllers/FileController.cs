using FileManager.Data;
using FileManager.Dto;
using FileManager.Models;
using FileManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace FileManager.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private IFileService _fileService;
        
    
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public ActionResult<ICollection<FileEntity>> GetAllFile()
        {
            try
            {
                var files = this._fileService.getAllFile();
                return Ok(files);
            }catch(Exception e)
            {
                return NotFound();
            }
          
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetById(int id)
        {
            try
            {
                var file = this._fileService.getFileById(id);
                return Ok(file);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
        [HttpGet("view/{id}")]
        public IActionResult ViewById(int id)
        {
            try
            {
                FileRespon file = this._fileService.viewFileById(id);

                string contentType = "image/jpeg";
                return File(file.FileBytes, file.ContentType);
            }
            catch(Exception e)
            {
                return NotFound("Hình ảnh không tồn tại.");
            }
        }
       
        [HttpPost]
        public IActionResult Post([FromForm] IFormFile file, [FromForm] string author)
        {

            try
            {
          
                bool isSave = this._fileService.saveFile(author, file);
                if (isSave == true)
                    return Ok();
                else
                    return StatusCode(500, "An internal server error occurred.");
            }
            catch (Exception e)
            {
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var check = this._fileService.deleteFileById(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}
