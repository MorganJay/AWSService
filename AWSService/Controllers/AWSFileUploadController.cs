using Data.RequestDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;

namespace AWSService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AWSFileUploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;

        public AWSFileUploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] UploadRequestDto requestDto)
        {
            var result = await _uploadService.UploadImageToS3BucketAsync(requestDto);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost("create-bucket")]
        public async Task<IActionResult> CreateS3BucketAsync(string bucketName)
        {
            await _uploadService.CreateBucketAsync(bucketName);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpGet("bucket")]
        public async Task<IActionResult> GetBucketContent(string bucketName, string folder)
        {
            var result = await _uploadService.ListBucketContent(bucketName, folder);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("file")]
        public async Task<IActionResult> GetFile(string bucketName, string path)
        {
            var (success, result) = await _uploadService.GetS3Object(bucketName, path);

            return success ? StatusCode(StatusCodes.Status200OK, result) : StatusCode(StatusCodes.Status400BadRequest, result);
        }
    }
}