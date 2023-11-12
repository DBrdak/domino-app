using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineShop.Catalog.Domain.Products;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.Products.TestData
{
    internal class ProductTestData
    {
        internal static async Task<FormFile> CreateImageFile()
        {
            FileStream sourceImg;

            try
            {
                sourceImg = File.OpenRead("../../../FeatureTests/Admin/Products/TestData/exampleImage.jpg");
            }
            catch (FileNotFoundException)
            {
                sourceImg = File.OpenRead("/home/runner/work/domino-app/domino-app/Services/OnlineShop/Catalog/Tests/OnlineShop.Catalog.IntegrationTests/FeatureTests/Admin/Products/TestData/exampleImage.jpg");
            }

            var stream = new MemoryStream();
            await sourceImg.CopyToAsync(stream);
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
