using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeDomain
{
    public class DefaultRuleSet : IRule
    {
        public Boolean DoesCellLive(IEnumerable<Cell> cells, Cell targetCell)
        {
            var sumOfLiveNeighbors = 0;
            var cellsCopy = cells.ToList();
            cellsCopy.Remove(cells.First(c => c.XCoordinate == targetCell.XCoordinate && c.YCoordinate == targetCell.YCoordinate));

            sumOfLiveNeighbors = cellsCopy.Count(c => c.IsAlive);

            if (targetCell.IsAlive)
            {
                if (sumOfLiveNeighbors == 2 || sumOfLiveNeighbors == 3)
                {
                    return true;
                }
            }
            else
            {
                if (sumOfLiveNeighbors == 3)
                    return true;
            }
            return false;
        }
    }
}
