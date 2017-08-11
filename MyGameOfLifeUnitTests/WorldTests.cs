using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyGameOfLife;
namespace MyGameOfLifeUnitTests
{
    [TestClass]
    public class WorldTests
    {
        [TestMethod]
        public void AWorldShouldHoldAGrid()
        {
            var mockedGrid = new Mock<IGrid>();
            World testWorld = new World(mockedGrid.Object);
            IGrid testGrid = testWorld.GetGrid();

            var expectedGrid = mockedGrid.Object;
            Assert.AreEqual(expectedGrid, testGrid);
        }
    }
}
