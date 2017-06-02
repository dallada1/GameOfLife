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
            var sumOfLiveNeighbors = cells.Count(c => c.IsAlive && (c.XCoordinate != targetCell.XCoordinate || c.YCoordinate != targetCell.YCoordinate));

            if (sumOfLiveNeighbors == 3 || (targetCell.IsAlive && sumOfLiveNeighbors == 2))
                return true;

            return false;
        }
    }
}
