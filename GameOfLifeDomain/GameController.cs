using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeDomain
{
    public class GameController
    {
        private List<Cell> cells;
        private IRule rule;
        private const Int32 MaxBound = 19;

        public GameController(List<Cell> liveCellSeed, IRule rule)
        {
            this.rule = rule;
            InitilizeCellCollection();
            SeedCells(liveCellSeed);
        }

        private void SeedCells(List<Cell> liveCellSeed)
        {
            foreach (var seedCell in liveCellSeed)
                GetCellWithCoordinates(seedCell.XCoordinate, seedCell.YCoordinate).TurnOn();
        }

        private void InitilizeCellCollection()
        {
            cells = new List<Cell>();

            for (var x = 0; x < MaxBound + 1; x++)
                for (var y = 0; y < MaxBound + 1; y++)
                    cells.Add(new Cell(x, y));
        }

        public List<Cell> GetCells()
        {
            return cells;
        }

        private Cell GetCellWithCoordinates(Int32 xCoordinate, Int32 yCoordinate)
        {
           return cells.First(c => c.XCoordinate == xCoordinate && c.YCoordinate == yCoordinate);
        }

        public IEnumerable<Cell> GetNextGeneration()
        {
            var newCellList = new List<Cell>();

            foreach(var cell in cells)
            {
                var newCell = new Cell(cell.XCoordinate, cell.YCoordinate);
                var neighbors = GetNeighbors(cell);

                if (rule.DoesCellLive(neighbors, cell))
                    newCell.IsAlive = true;

                newCellList.Add(newCell);
            }

            cells = newCellList;

            return GetCells();
        }

        private IEnumerable<Cell> GetNeighbors(Cell cell)
        {
            var neighbors = new List<Cell>();

            for(int x = -1; x < 2; x++)
            {
                for(int y = -1; y < 2; y++)
                {
                    var xToLookFor = cell.XCoordinate + x;
                    var yToLookFor = cell.YCoordinate + y;
                    if (xToLookFor >= 0 && xToLookFor <= MaxBound && yToLookFor >= 0 && yToLookFor <= MaxBound)
                        neighbors.Add(GetCellWithCoordinates(xToLookFor, yToLookFor));
                }
            }

            return neighbors;
        }
    }
}
