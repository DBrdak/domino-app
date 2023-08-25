using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using Shared.Domain.Photo;

namespace OnlineShop.Catalog.Infrastructure.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        //private readonly Cloudinary _cloudinary;

        public PhotoRepository()
        {
        }

        //public async Task<PhotoUploadResult> AddPhoto(IFormFile file)
        //{
        //    if (file.Length == 0)
        //    {
        //        return null;
        //    }

        //    await using var stream = file.OpenReadStream();
        //    var uploadParams = new ImageUploadParams
        //    {
        //        File = new FileDescription(file.FileName, stream),
        //        Transformation = new Transformation().Height(500).Width(500).Crop("fill")
        //    };

        //    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        //    if (uploadResult.Error != null)
        //    {
        //        throw new Exception(uploadResult.Error.Message);
        //    }

        //    return new PhotoUploadResult
        //    {
        //        PublicId = uploadResult.PublicId,
        //        Url = uploadResult.SecureUrl.ToString(),
        //    };
        //}

        //public async Task<string> DeletePhoto(string publicId)
        //{
        //    var deleteParams = new DeletionParams(publicId);
        //    var result = await _cloudinary.DestroyAsync(deleteParams);
        //    return result.Result == "ok" ? result.Result : null;
        //}
    }
}