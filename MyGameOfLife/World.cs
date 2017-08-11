using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyGameOfLife
{
    public class World
    {
        public IGrid _grid;

        public World(IGrid grid)
        {
            _grid = grid;
        }

        public IGrid GetGrid()
        {
            return _grid;
        }

        public void Start(int steps, int speed)
        {
            for (int i = 1; i <= steps; i++)
            {
                int cellIndex = 1;
                IGrid grid = GetGrid();
                List<CellType> currentGrid = grid.GetCurrentGrid();

                grid.Fields.ForEach(cell =>
                {
                    PrintCell(grid.Width, cell, cellIndex);

                    cellIndex++;
                });
                GetGrid().InitiateStep();
                Thread.Sleep(speed);
                Console.Clear();
            }
        }

        private void PrintCell(int gridWidth, ICell cell, int cellIndex)
        {
            string nextLine = cellIndex % gridWidth == 0 ? "\n" : "";
            if (cell.Type == CellType.Alive)
            {
                Console.Write("O" + nextLine);
            }
            else
            {
                Console.Write(" " + nextLine);
            }
        }

    }
}
