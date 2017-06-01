using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLifeDomain;
using System.Collections;
using System.Collections.Generic;

namespace GameOfLifeDomainTests
{
    [TestClass]
    public class DefaultRuleSetTests
    {
        private IRule ruleSet;

        public DefaultRuleSetTests()
        {
            ruleSet = new DefaultRuleSet();
        }

        [TestMethod]
        public void AnyLiveCellWithFewerThanTwoLiveNeighborsDies()
        {
            var cells = CreateNineCellGrid();
            var centerCell = cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 1);
            centerCell.IsAlive = true;

            Assert.IsFalse(ruleSet.DoesCellLive(cells, centerCell));

            cells.First(c => c.XCoordinate == 0 && c.YCoordinate == 0).IsAlive = true;

            Assert.IsFalse(ruleSet.DoesCellLive(cells, centerCell));
        }

        [TestMethod]
        public void AnyLiveCellWithTwoOrThreeLiveNeighborsLives()
        {
            var cells = CreateNineCellGrid();
            var centerCell = cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 1);
            centerCell.IsAlive = true;
            cells.First(c => c.XCoordinate == 0 && c.YCoordinate == 0).IsAlive = true;
            cells.First(c => c.XCoordinate == 0 && c.YCoordinate == 1).IsAlive = true;

            Assert.IsTrue(ruleSet.DoesCellLive(cells, centerCell));

            cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 0).IsAlive = true;

            Assert.IsTrue(ruleSet.DoesCellLive(cells, centerCell));
        }

        [TestMethod]
        public void AnyDeadCellWithTwoLiveNeighborsDies()
        {
            var cells = CreateNineCellGrid();
            var centerCell = cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 1);
            cells.First(c => c.XCoordinate == 0 && c.YCoordinate == 0).IsAlive = true;
            cells.First(c => c.XCoordinate == 0 && c.YCoordinate == 1).IsAlive = true;

            Assert.IsFalse(ruleSet.DoesCellLive(cells, centerCell));
        }

        [TestMethod]
        public void AnyLiveCellWithMoreThanThreeLiveNeighborsDies()
        {
            var cells = CreateNineCellGrid();
            var centerCell = cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 1);
            centerCell.IsAlive = true;
            cells.First(c => c.XCoordinate == 0 && c.YCoordinate == 0).IsAlive = true;
            cells.First(c => c.XCoordinate == 0 && c.YCoordinate == 1).IsAlive = true;
            cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 0).IsAlive = true;
            cells.First(c => c.XCoordinate == 2 && c.YCoordinate == 0).IsAlive = true;

            Assert.IsFalse(ruleSet.DoesCellLive(cells, centerCell));
        }

        [TestMethod]
        public void AnyDeadCellWithExactlyThreeLiveNeighborsLives()
        {
            var cells = CreateNineCellGrid();
            var centerCell = cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 1);
            centerCell.IsAlive = true;
            cells.First(c => c.XCoordinate == 0 && c.YCoordinate == 0).IsAlive = true;
            cells.First(c => c.XCoordinate == 0 && c.YCoordinate == 1).IsAlive = true;
            cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 0).IsAlive = true;

            Assert.IsTrue(ruleSet.DoesCellLive(cells, centerCell));
        }

        private IEnumerable<Cell> CreateNineCellGrid()
        {
            var cells = new List<Cell>();

            for (var x = 0; x < 3; x++)
                for (var y = 0; y < 3; y++)
                    cells.Add(new Cell(x, y));

            return cells;
        }
    }
}
