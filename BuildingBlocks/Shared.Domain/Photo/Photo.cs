using Shared.Domain.Exceptions;

namespace Shared.Domain.Photo
{
    public record Photo
    {
        public Photo(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new DomainException<Photo>("Url is required");
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