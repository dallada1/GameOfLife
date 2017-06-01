using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeDomain
{
    public class Cell
    {
        public Boolean IsAlive { get; set; }
        public Int32 XCoordinate { get; private set; }
        public Int32 YCoordinate { get; private set; }

        public Cell(Int32 xCoordinate, Int32 yCoordinate)
        {
            this.XCoordinate = xCoordinate;
            this.YCoordinate = yCoordinate;
        }

        public void TurnOn()
        {
            IsAlive = true;
        }

        public void TurnOff()
        {
            IsAlive = false;
        }
    }
}
