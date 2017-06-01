using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeDomain
{
    public interface IRule
    {
        Boolean DoesCellLive(IEnumerable<Cell> cells, Cell cell);
    }
}
