using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Shared.Domain.Photo;

namespace OnlineShop.Catalog.Infrastructure.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly Cloudinary _cloudinary;

        public PhotoRepository(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<PhotoUploadResult?> UploadPhoto(IFormFile file, CancellationToken cancellationToken = default)
        {
            if (file.Length == 0)
            {
                return null;
            }

            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken);

            if (uploadResult.Error != null)
            {
                throw new ApplicationException(uploadResult.Error.Message);
            }

            return new PhotoUploadResult(
                uploadResult.SecureUrl.ToString(),
                uploadResult.PublicId);
        }

        public async Task<bool> DeletePhoto(string photoId)
        {
            var deleteParams = new DeletionParams(photoId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result.Result == "ok";
        }
    }
}