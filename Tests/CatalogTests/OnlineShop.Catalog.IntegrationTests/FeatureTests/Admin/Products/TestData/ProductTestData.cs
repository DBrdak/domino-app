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
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Color = SixLabors.ImageSharp.Color;
using RectangleF = SixLabors.ImageSharp.RectangleF;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.Products.TestData
{
    internal class ProductTestData
    {
        internal static async Task<FormFile> CreateImageFile()
        {
            using var image = new Image<Rgba32>(800, 600);

            image.Mutate(ctx => ctx.Draw(new DrawingOptions(), Color.Azure, 2, RectangleF.Empty));

            var stream = new MemoryStream();
            await image.SaveAsync(stream, new JpegEncoder());
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
                Add(0, "", true, 15.4m);
            }
        }
    }
}
