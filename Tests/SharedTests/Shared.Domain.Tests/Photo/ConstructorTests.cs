using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Tests.Photo
{
    public class ConstructorTests
    {
        [Theory]
        [InlineData("https://res.cloudinary.com/dsafadsfvv234324r34g")]
        [InlineData("https://res.cloudinary.com/uashdfuh398rfh39u4hd")]
        public void Contructor_ValidData_ShouldCreatePhotoInstance(string url)
        {
            // Arrange

            // Act
            var photo = new Domain.Photo.Photo(url);

            // Assert
            Assert.NotNull(photo);
        }
    }
}
