using GameOfLifeDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameOfLife.Models
{
    public class CellsModel
    {
        public IEnumerable<Cell> Cells { get; set; }
    }
}