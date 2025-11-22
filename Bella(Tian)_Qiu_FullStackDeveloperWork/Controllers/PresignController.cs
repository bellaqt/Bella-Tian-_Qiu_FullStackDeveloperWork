using Bella_Tian__Qiu_FullStackDeveloperWork.Models;
using Bella_Tian__Qiu_FullStackDeveloperWork.Services;
using Microsoft.AspNetCore.Mvc;
using Amazon.S3;
using Amazon.S3.Model;

namespace Bella_Tian__Qiu_FullStackDeveloperWork.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PresignController : ControllerBase
    {
        private readonly IAmazonS3 _s3;

        public PresignController(IAmazonS3 s3)
        {
            _s3 = s3;
        }

        [HttpGet]
        //[HttpGet("file")] '/presign/file' // Alternative route
        public IActionResult GetPresignUrl([FromQuery] string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return BadRequest("Missing key");
            }

            var request = new GetPreSignedUrlRequest
            {
                BucketName = "tianber-demo-bucket",
                Key = key,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddMinutes(1)
            };

            string url = _s3.GetPreSignedURL(request);

            return Ok(new { url });
        }
    }
}
