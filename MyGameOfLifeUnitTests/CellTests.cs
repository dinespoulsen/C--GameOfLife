using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyGameOfLife;
namespace MyGameOfLifeUnitTests
{
    [TestClass]
    public class CellTests
    {
        [TestMethod]
        public void WhenPlaced_AndGivenAnXAndYCoordinate_ExpectCellToHaveAnXAndYCoordinate()
        {
            ICell testCell = new Cell();

            testCell.Place(4, 6);

            Assert.AreEqual(4, testCell.X);
            Assert.AreEqual(6, testCell.Y);
        }

        [TestMethod]
        public void WhenLookingAtNeighbouringFields_WhenPlacedAt00Coordinate_Expect5AirCellAnd3DeadCells()
        {
            ICell testCell = new Cell() {X = 0, Y = 0, Type = CellType.Alive};
            Mock<IGrid> mockedGrid = new Mock<IGrid>();
            mockedGrid.Setup(m => m.IsInGrid(testCell.X, testCell.Y + 1)).Returns(true);
            mockedGrid.Setup(m => m.IsInGrid(testCell.X + 1, testCell.Y + 1)).Returns(true);
            mockedGrid.Setup(m => m.IsInGrid(testCell.X + 1, testCell.Y)).Returns(true);

            var northIndex = testCell.X + 5 * (testCell.Y + 1);
            var northEastIndex = (testCell.X + 1) + 5 * (testCell.Y + 1);
            var eastIndex = testCell.X + 5 * (testCell.Y + 1);

            List<CellType> mockedNeighbourgs = new List<CellType>()
            {
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead
            };
            mockedNeighbourgs[northIndex] = CellType.Dead;
            mockedNeighbourgs[northEastIndex] = CellType.Dead;
            mockedNeighbourgs[eastIndex] = CellType.Dead;

            Dictionary<string, CellType> expectedFields = testCell.GetNeighbouringCells(mockedGrid.Object, mockedNeighbourgs);
            Assert.AreEqual(CellType.Dead, expectedFields["N"]);
            Assert.AreEqual(CellType.Dead, expectedFields["NE"]);
            Assert.AreEqual(CellType.Dead, expectedFields["E"]);
            Assert.AreEqual(CellType.Air, expectedFields["SE"]);
            Assert.AreEqual(CellType.Air, expectedFields["S"]);
            Assert.AreEqual(CellType.Air, expectedFields["SW"]);
            Assert.AreEqual(CellType.Air, expectedFields["W"]);
            Assert.AreEqual(CellType.Air, expectedFields["NW"]);
        }

        [TestMethod]
        public void
            WhenACellChecksNeightbouringCells_WhenThereIsOneAliveAnd7Dead_ExpectGetNeighbouringCellsStatusToReturn1AliveAnd7Dead()
        {
            ICell testCell = new Cell();

            Dictionary<string, CellType> mockedNeighbourgs = new Dictionary<string, CellType>();
            mockedNeighbourgs["N"] = CellType.Alive;
            mockedNeighbourgs["NE"] = CellType.Alive;
            mockedNeighbourgs["E"] = CellType.Dead;
            mockedNeighbourgs["SE"] = CellType.Dead;
            mockedNeighbourgs["S"] = CellType.Dead;
            mockedNeighbourgs["SW"] = CellType.Dead;
            mockedNeighbourgs["W"] = CellType.Dead;
            mockedNeighbourgs["NW"] =CellType.Alive;

            int expectedAliveCells = testCell.GetNeighbouringCellsStatus(mockedNeighbourgs)[CellType.Alive];
            Assert.AreEqual(expectedAliveCells, 3);

            int expectedDeadCells = testCell.GetNeighbouringCellsStatus(mockedNeighbourgs)[CellType.Dead];
            Assert.AreEqual(expectedDeadCells, 5);
        }

        [TestMethod]
        public void WhenPassedAGrid_WhenOnlyOneCellAlive_ExpectLivingCellToDie()
        {
            Cell testCell = new Cell() {Type = CellType.Alive};

            Mock<IGrid> mockedGrid = new Mock<IGrid>();

            mockedGrid.Setup(m => m.IsInGrid(testCell.X, testCell.Y + 1)).Returns(true);

            List<CellType> mockedNeighbourgs = new List<CellType>()
            {
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead
            };

            var northIndex = testCell.X + 5 * (testCell.Y + 1);
            mockedNeighbourgs[northIndex] = CellType.Alive;


            testCell.Act(mockedGrid.Object, mockedNeighbourgs);
            Assert.AreEqual(testCell.Type, CellType.Dead);
        }

        [TestMethod]
        public void WhenPassedAGrid_WhenOnlyTwoCellsAreAlive_ExpectLivingCellToLive()
        {
            Cell testCell = new Cell() { X = 0, Y = 0, Type = CellType.Alive };

            Mock<IGrid> mockedGrid = new Mock<IGrid>();

            mockedGrid.Setup(m => m.IsInGrid(testCell.X, testCell.Y + 1)).Returns(true);
            mockedGrid.Setup(m => m.IsInGrid(testCell.X  + 1, testCell.Y)).Returns(true);

            List<CellType> mockedNeighbourgs = new List<CellType>()
            {
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead
            };

            var northIndex = testCell.X + 5 * (testCell.Y + 1);
            var eastIndex = (testCell.X + 1) + 5 * testCell.Y;
            mockedNeighbourgs[northIndex] = CellType.Alive;
            mockedNeighbourgs[eastIndex] = CellType.Alive;

            mockedGrid.SetupGet(m => m.Width).Returns(5);


            testCell.Act(mockedGrid.Object, mockedNeighbourgs);
            Assert.AreEqual(CellType.Alive, testCell.Type);
        }

        [TestMethod]
        public void WhenPassedAGrid_WhenThreeCellsAreAlive_ExpectLivingCellToLive()
        {
            Cell testCell = new Cell() { X = 0, Y = 0, Type = CellType.Alive };

            Mock<IGrid> mockedGrid = new Mock<IGrid>();

            mockedGrid.Setup(m => m.IsInGrid(testCell.X, testCell.Y + 1)).Returns(true);
            mockedGrid.Setup(m => m.IsInGrid(testCell.X + 1, testCell.Y)).Returns(true);
            mockedGrid.Setup(m => m.IsInGrid(testCell.X + 1, testCell.Y + 1)).Returns(true);

            List<CellType> mockedNeighbourgs = new List<CellType>()
            {
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead
            };

            var northIndex = testCell.X + 5 * (testCell.Y + 1);
            var northEastIndex = (testCell.X + 1) * (testCell.Y + 1);
            var eastIndex = (testCell.X + 1) + 5 * testCell.Y;
            mockedNeighbourgs[northIndex] = CellType.Alive;
            mockedNeighbourgs[eastIndex] = CellType.Alive;
            mockedNeighbourgs[northEastIndex] = CellType.Alive;

            mockedGrid.SetupGet(m => m.Width).Returns(5);


            testCell.Act(mockedGrid.Object, mockedNeighbourgs);
            Assert.AreEqual(CellType.Alive, testCell.Type);
        }

        [TestMethod]
        public void WhenPassedAGrid_WhenFourCellsAreAlive_ExpectLivingCellToDie()
        {
            Cell testCell = new Cell() { X = 1, Y = 1, Type = CellType.Alive };

            Mock<IGrid> mockedGrid = new Mock<IGrid>();

            mockedGrid.Setup(m => m.IsInGrid(testCell.X, testCell.Y + 1)).Returns(true);
            mockedGrid.Setup(m => m.IsInGrid(testCell.X + 1, testCell.Y)).Returns(true);
            mockedGrid.Setup(m => m.IsInGrid(testCell.X + 1, testCell.Y + 1)).Returns(true);
            mockedGrid.Setup(m => m.IsInGrid(testCell.X, testCell.Y - 1)).Returns(true);

            List<CellType> mockedNeighbourgs = new List<CellType>()
            {
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead
            };

            var northIndex = testCell.X + 5 * (testCell.Y + 1);
            var northEastIndex = (testCell.X + 1) + 5* (testCell.Y + 1);
            var eastIndex = (testCell.X + 1) + 5 * testCell.Y;
            var southIndex = (testCell.X) + 5 * (testCell.Y - 1);
            mockedNeighbourgs[northIndex] = CellType.Alive;
            mockedNeighbourgs[eastIndex] = CellType.Alive;
            mockedNeighbourgs[northEastIndex] = CellType.Alive;
            mockedNeighbourgs[southIndex] = CellType.Alive;

            mockedGrid.SetupGet(m => m.Width).Returns(5);


            testCell.Act(mockedGrid.Object, mockedNeighbourgs);
            Assert.AreEqual(CellType.Dead, testCell.Type);
        }

        [TestMethod]
        public void WhenPassedAGrid_WhenThreeCellsAreAlive_ExpectDeadCellToLive()
        {
            Cell testCell = new Cell() { X = 1, Y = 0, Type = CellType.Dead };
            Cell test2Cell = new Cell() { X = 1, Y = 2, Type = CellType.Dead };
            Mock<IGrid> mockedGrid = new Mock<IGrid>();

            mockedGrid.Setup(m => m.IsInGrid(testCell.X - 1, testCell.Y + 1)).Returns(true);
            mockedGrid.Setup(m => m.IsInGrid(testCell.X, testCell.Y + 1)).Returns(true);
            mockedGrid.Setup(m => m.IsInGrid(testCell.X + 1, testCell.Y + 1)).Returns(true);

            List<CellType> mockedNeighbourgs = new List<CellType>()
            {
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead,
                CellType.Dead
            };

            var northIndex = testCell.X + 5 * (testCell.Y + 1);
            var northEastIndex = (testCell.X + 1) + 5 * (testCell.Y + 1);
            var northWestIndex = (testCell.X - 1) + 5 * (testCell.Y + 1);
            mockedNeighbourgs[northIndex] = CellType.Alive;
            mockedNeighbourgs[northEastIndex] = CellType.Alive;
            mockedNeighbourgs[northWestIndex] = CellType.Alive;

            mockedGrid.SetupGet(m => m.Width).Returns(5);


            testCell.Act(mockedGrid.Object, mockedNeighbourgs);
            test2Cell.Act(mockedGrid.Object, mockedNeighbourgs);
            Assert.AreEqual(CellType.Alive, test2Cell.Type);
            Assert.AreEqual(CellType.Alive, testCell.Type);
        }

    }
}
