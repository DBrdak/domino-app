using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Http;
using OnlineShop.Catalog.Domain.Products;
using System.Drawing;
using System.Drawing.Imaging;
using Rectangle = System.Drawing.Rectangle;
using System.IO;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.Products.TestData
{
    internal class ProductTestData
    {
        internal static async Task<FormFile> CreateImageFile()
        {
            var width = 500;
            var height = 300;
            using var image = new Bitmap(width, height);

            var graphics = Graphics.FromImage(image);

            var pen = new Pen(Color.Red);
            var rectangle = new Rectangle(50, 50, 200, 100);
            graphics.DrawRectangle(pen, rectangle);

            var stream = new MemoryStream();
            image.Save(stream,  ImageFormat.Jpeg);
            byte[] imageData = stream.ToArray();
            // Optionally, you can save the memory stream to a file
            await File.WriteAllBytesAsync("image.jpg", imageData);
            stream.Position = 0;
            var file = new FormFile(stream, 0, stream.Length, "exampleFile.jpg", "exampleFile.jpg");
            
            return file;
        }

        internal class UpdateProductValidTestData : TheoryData<int, string?, bool, decimal?, bool>
        {
            public UpdateProductValidTestData()
            {
                Add(0, "new description", true, 15.3m, false);
                Add(1, "", true, 15.3m, true);
                Add(2, "", false, null, true);
                Add(3, "new description", false, null, true);
            }
        }

        internal class UpdateProductInvalidTestData : TheoryData<int, string, bool, decimal?, bool>
        {
            public UpdateProductInvalidTestData()
            {
                Add(0, "new description", false, 15.3m, false);
                Add(2, "", true, null, true);
            }
        }

        internal class AddProductValidTestData : TheoryData<int, string, bool, decimal?>
        {
            public AddProductValidTestData()
            {
                Add(0, "description 1", false, null);
                Add(1, "description 2", true, 15.4m);
            }
        }

        internal class AddProductInvalidTestData : TheoryData<int, string, bool, decimal?>
        {
            public AddProductInvalidTestData()
            {
                Add(0, "description 1", true, null);
                Add(1, "description 2", false, 15.4m);
                Add(0, "", true, 15.4m);
            }
        }
    }
}
