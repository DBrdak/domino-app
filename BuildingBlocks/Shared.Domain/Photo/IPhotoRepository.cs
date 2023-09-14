using Microsoft.AspNetCore.Http;

namespace Shared.Domain.Photo
{
    public interface IPhotoRepository
    {
        public Task<PhotoUploadResult?> UploadPhoto(IFormFile file, CancellationToken cancellationToken = default);

        public Task<bool> DeletePhoto(string photoUrl);
    }
}