using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Shared.Domain.Photo;

namespace OnlineShop.Catalog.Infrastructure.Repositories
{
    public sealed class PhotoRepository : IPhotoRepository
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
                Transformation = new Transformation().Height(500).Width(500).Crop("fill"),
                Tags = $"{file.FileName}"
            };

            var resourceInCloud = await CheckIsAlreadyUploaded(uploadParams.Tags);

            if (resourceInCloud is not null)
            {
                return resourceInCloud;
            }

            var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken);

            if (uploadResult.Error != null)
            {
                return null;
            }

            return new PhotoUploadResult(
                uploadResult.SecureUrl.ToString(),
                uploadResult.PublicId);
        }

        public async Task<bool> DeletePhoto(string photoUrl)
        {
            var photosInCloud = await _cloudinary.ListResourcesAsync();

            var photoInCloud = photosInCloud
                .Resources
                .ToList()
                .FirstOrDefault(p => p.SecureUrl.AbsoluteUri == photoUrl);

            if (photoInCloud is null)
            {
                return true;
            }

            var deleteParams = new DeletionParams(photoInCloud.PublicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result.Result == "ok";
        }

        private async Task<PhotoUploadResult?> CheckIsAlreadyUploaded(string tag)
        {
            var resourceList = await _cloudinary.ListResourcesByTagAsync(tag);
            var resource = resourceList.Resources.FirstOrDefault();

            if (resource is null)
            {
                return null;
            }

            return new PhotoUploadResult(
                resource.SecureUrl.ToString(),
                resource.PublicId);
        }
    }
}