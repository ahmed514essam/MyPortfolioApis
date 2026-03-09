using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
namespace MyPortfolioApis.Services
{
    public class CloudinaryService
    {
        private readonly Cloudinary cloudinary;
        public CloudinaryService(IConfiguration config)
        {
            var account = new Account(
                config["CloudinarySettings:CloudName"],
                config["CloudinarySettings:ApiKey"],
                config["CloudinarySettings:ApiSecret"]
            );

            cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> UploadImage(IFormFile file)
        {
            using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream)
            };

            var result = await cloudinary.UploadAsync(uploadParams);

            return result;
        }

        public async Task<bool> DeleteImage(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await cloudinary.DestroyAsync(deleteParams);

            return result.Result == "ok";
        }

    }
}
