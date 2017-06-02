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
        private IRule rule;

        public GameControllerTests()
        {
            rule = new DefaultRuleSet();
            gameController = new GameController(new List<Cell>(), rule);
        }

        [TestMethod]
        public void CellCollectionInitializedForTenByTenGrid()
        {
            IEnumerable<Cell> cells = gameController.GetCells();
            Assert.AreEqual(100, cells.Count());

            for (var x = 0; x < 10; x++)
                for (var y = 0; y < 10; y++)
                    Assert.AreEqual(1, cells.Count(c => c.XCoordinate == x && c.YCoordinate == y));
        }

        [TestMethod]
        public void CellSeededAsAlive()
        {
            var x = 2;
            var y = 3;
            var seededCell = new Cell(x, y);

            var seededController = new GameController(new List<Cell>() { seededCell }, rule);
            
            var seededGameCell = seededController.GetCells().First(c => c.XCoordinate == x && c.YCoordinate == y);
            Assert.IsTrue(seededGameCell.IsAlive);
        }

        [TestMethod]
        public void GenerationWithAllDeadCells()
        {
            var newGeneration = gameController.GetNextGeneration();

            Assert.AreEqual(0, newGeneration.Count(g => g.IsAlive));
        }

        [TestMethod]
        public void GenerationWithLShapedSeedBecomesSquare()
        {
            var topLeft = new Cell(0, 1);
            var bottomLeft = new Cell(0, 0);
            var bottomRight = new Cell(1, 0);
            var lShapedSeed = new List<Cell>()
            {
                bottomLeft,
                bottomRight,
                topLeft
            };
            var seededController = new GameController(lShapedSeed, rule);
            var topRight = new Cell(1, 1);

            var nextGeneration = seededController.GetNextGeneration();

            Assert.AreEqual(4, nextGeneration.Count(g => g.IsAlive));
            Assert.IsTrue(nextGeneration.First(g => g.XCoordinate == bottomLeft.XCoordinate && g.YCoordinate == bottomLeft.YCoordinate).IsAlive);
            Assert.IsTrue(nextGeneration.First(g => g.XCoordinate == topLeft.XCoordinate && g.YCoordinate == topLeft.YCoordinate).IsAlive);
            Assert.IsTrue(nextGeneration.First(g => g.XCoordinate == bottomRight.XCoordinate && g.YCoordinate == bottomRight.YCoordinate).IsAlive);
            Assert.IsTrue(nextGeneration.First(g => g.XCoordinate == topRight.XCoordinate && g.YCoordinate == topRight.YCoordinate).IsAlive);
        }
    }
}
