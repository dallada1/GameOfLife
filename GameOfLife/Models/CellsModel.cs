using GameOfLifeDomain;
using System.Collections.Generic;

namespace GameOfLife.Models
{
    public class CellsModel
    {
        public IEnumerable<Cell> Cells { get; set; }
    }
}