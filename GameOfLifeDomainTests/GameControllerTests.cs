using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLifeDomain;
using System.Collections.Generic;

namespace GameOfLifeDomainTests
{
    [TestClass]
    public class GameControllerTests
    {
        private GameController gameController;

        public GameControllerTests()
        {
            gameController = new GameController(new List<Cell>());
        }

        [TestMethod]
        public void CellCollectionInitializedForTenByTenGrid()
        {
            IEnumerable<Cell> cells = gameController.GetCells();
            Assert.AreEqual(100, cells.Count());

            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 10; y++)
                {
                    Assert.AreEqual(1, cells.Count(c => c.XCoordinate == x && c.YCoordinate == y));
                }
            }
        }

        [TestMethod]
        public void CellSeededAsAlive()
        {
            var x = 2;
            var y = 3;
            var seededCell = new Cell(x, y);

            var seededController = new GameController(new List<Cell>() { seededCell });

            var cells = seededController.GetCells();
            var seededGameCell = cells.First(c => c.XCoordinate == x && c.YCoordinate == y);
            Assert.IsTrue(seededGameCell.IsAlive);
        }
    }
}
