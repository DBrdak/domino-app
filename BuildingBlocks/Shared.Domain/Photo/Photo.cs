using System.Text.RegularExpressions;
using Shared.Domain.Exceptions;

namespace Shared.Domain.Photo
{
    public record Photo
    {
        private const string urlRegex = "https://res\\.cloudinary\\.com/\\S+";

        public Photo(string url)
        {
            if (!Regex.IsMatch(url, urlRegex))
            {
                throw new DomainException<Photo>("Wrong url format, only Cloudinary urls are accepted");
            }
            
            this.Url = url;
        }

        public string Url { get; init; }

        public void Deconstruct(out string url)
        {
            url = this.Url;
        }
    }
}