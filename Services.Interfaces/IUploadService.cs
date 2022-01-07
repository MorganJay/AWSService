using Amazon.S3.Model;
using Data;
using Data.RequestDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUploadService
    {
        Task<bool> CreateBucketAsync(string bucketName);

        AWSUploadResult<string> GenerateAwsFileUrl(string bucketName, string key, bool useRegion = true);

        Task<AWSUploadResult<string>> UploadImageToS3BucketAsync(UploadRequestDto requestDto);

        Task<(bool, List<S3Object>)> ListBucketContent(string bucketName, string folder = "");

        Task<(bool, GetObjectResponse)> GetS3Object(string bucketName, string key);
    }
}