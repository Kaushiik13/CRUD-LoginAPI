using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using WebApplication1.Data; // Assuming this is where LoginDbContext is located

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController1 : ControllerBase
    {
        private readonly LoginDbContext _context;

        public ValuesController1(LoginDbContext context)
        {
            _context = context;
        }

        // POST: api/ValuesController1/UploadImage
        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using (var memoryStream = new MemoryStream())
            {
                // Copy the file data into memory
                await file.CopyToAsync(memoryStream);
                byte[] fileData = memoryStream.ToArray();

                // Store the file in the database
                var uploadedFile = new UploadedFile
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    FileSize = file.Length,
                    FileData = fileData
                };

                // Save the file information to the database
                _context.UploadedFiles.Add(uploadedFile);
                await _context.SaveChangesAsync();

                // Generate the file preview URL
                var fileUrl = $"{Request.Scheme}://{Request.Host}/api/ValuesController1/PreviewFile/{uploadedFile.Id}";

                // Return the file preview URL
                return Ok(new { message = "File uploaded successfully.", url = fileUrl });
            }
        }

        // GET: api/ValuesController1/PreviewFile/{id}
        [HttpGet("PreviewFile/{id}")]
        public IActionResult PreviewFile(int id)
        {
            // Retrieve the file from the database by its ID
            var file = _context.UploadedFiles.Find(id);
            if (file == null)
                return NotFound("File not found.");

            // Set content disposition to inline for preview
            Response.Headers.Add("Content-Disposition", $"inline; filename={file.FileName}");

            // Return the file content with proper content type to allow inline preview
            return File(file.FileData, file.ContentType);
        }
    }
}
