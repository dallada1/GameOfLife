using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLifeDomain;

namespace GameOfLifeDomainTests
{
    [TestClass]
    public class CellTests
    {
        [TestMethod]
        public void CoordinatesSetWhenConstructed()
        {
            var x = 2;
            var y = 3;

            var newCell = new Cell(x, y);

            Assert.AreEqual(x, newCell.XCoordinate);
            Assert.AreEqual(y, newCell.YCoordinate);
        }

        [TestMethod]
        public void IsAliveIsFalseAfterTurnOff()
        {
            var cell = new Cell(0, 0);

            cell.TurnOff();

            Assert.IsFalse(cell.IsAlive);
        }

        [TestMethod]
        public void IsAliveIsFalseAfterTurnOn()
        {
            var cell = new Cell(0, 0);

            cell.TurnOn();

            Assert.IsTrue(cell.IsAlive);
        }
    }
}
