using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Tests.Money.Unit
{
    public class AlternativeUnitTests
    {
        [Theory]
        [InlineData("kg")]
        [InlineData("szt")]
        public void AlternativeUnit_ShouldReturnAltUnit(string mainUnitCode)
        {
            // Arrange
            var mainUnit = Domain.Money.Unit.FromCode(mainUnitCode);

            // Act
            var altUnit = mainUnit.AlternativeUnit();

            // Assert
            if (mainUnit.Code == "kg")
            {
                Assert.Equal("szt", altUnit.Code);
            }

            if (mainUnit.Code == "szt")
            {
                Assert.Equal("kg", altUnit.Code);
            }
        }
    }
}
