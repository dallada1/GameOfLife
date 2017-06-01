using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeDomain
{
    public class GameController
    {
        private const Int32 DefaultCellCount = 100;
        private List<Cell> cells;
        public GameController(List<Cell> liveCellSeed)
        {
            InitilizeCellCollection();
            SeedCells(liveCellSeed);
        }

        private void SeedCells(List<Cell> liveCellSeed)
        {
            foreach (var seedCell in liveCellSeed)
            {
                var matchingCell = GetCellWithCoordinates(seedCell.XCoordinate, seedCell.YCoordinate);
                matchingCell.TurnOn();
            }
        }

        private void InitilizeCellCollection()
        {
            cells = new List<Cell>();

            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 10; y++)
                {
                    cells.Add(new Cell(x, y));
                }
            }
        }

        public List<Cell> GetCells()
        {
            return cells;
        }

        private Cell GetCellWithCoordinates(Int32 xCoordinate, Int32 yCoordinate)
        {
            return cells.First(c => c.XCoordinate == xCoordinate && c.YCoordinate == yCoordinate);
        }
    }
}
