using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyGameOfLife;
namespace MyGameOfLifeUnitTests
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void WhenAGridIsInstantiated_WithAHeightAndAWidth_ExpectAGridShouldHaveFields()
        {
            Grid testGrid = new Grid(5, 5);

            var actualFields = testGrid.Fields;

            List<ICell> expectedFields = new List<ICell>();
            for (int i = 0; i < 25; i ++)
            {
                expectedFields.Add(new Cell());
            }
            Assert.AreEqual(expectedFields.Count, actualFields.Count);
        }

        [TestMethod]
        public void WhenGivenTwoCoordinates_WhenTheCoordinatesAreOutsideOfTheGrid_ExpectIsInGridToReturnFalse()
        {
            Grid testGrid = new Grid(5, 5);
            Assert.IsFalse(testGrid.IsInGrid(-1, 5));
        }

        [TestMethod]
        public void WhenGivenTwoCoordinates_WhenTheCoordinatesAreInsideTheGrid_ExpectIsInGridToReturnTrue()
        {
            Grid testGrid = new Grid(5, 5);
            Assert.IsTrue(testGrid.IsInGrid(0, 4));
        }
    }
}
