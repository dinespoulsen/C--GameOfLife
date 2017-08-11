using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameOfLife
{
    public enum CellType
    {
       Air,
       Alive,
       Dead,
    }

    public class Cell : ICell
    {
        public Dictionary<string, ICell> Neighbours { get; }
        public int X { get; set; }

        public int Y { get; set; }

        public CellType Type { get; set; }

        public void Place(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Dictionary<string, CellType> GetNeighbouringCells(IGrid grid, List<CellType> currentGrid)
        {
            var neighbouringCells = new Dictionary<string, CellType>();
            neighbouringCells["N"] = grid.IsInGrid(X, (Y + 1))
                ? currentGrid[X + grid.Width * (Y + 1)]
                : CellType.Air;

            neighbouringCells["NE"] = grid.IsInGrid(X + 1, (Y + 1))
                ? currentGrid[(X + 1) + grid.Width * (Y + 1)]
                : CellType.Air;

            neighbouringCells["E"] = grid.IsInGrid(X + 1, (Y))
                ? currentGrid[(X + 1) + grid.Width * (Y)]
                : CellType.Air;

            neighbouringCells["SE"] = grid.IsInGrid(X + 1, (Y - 1))
                ? currentGrid[(X + 1) + grid.Width * (Y - 1)]
                : CellType.Air;

            neighbouringCells["S"] = grid.IsInGrid(X, (Y - 1))
                ? currentGrid[(X) + grid.Width * (Y - 1)]
                : CellType.Air;

            neighbouringCells["SW"] = grid.IsInGrid(X - 1, (Y - 1))
                ? currentGrid[(X - 1) + grid.Width * (Y - 1)]
                : CellType.Air;

            neighbouringCells["W"] = grid.IsInGrid(X - 1, (Y))
                ? currentGrid[(X - 1) + grid.Width * (Y)]
                : CellType.Air;

            neighbouringCells["NW"] = grid.IsInGrid(X - 1, (Y + 1))
                ? currentGrid[(X - 1) + grid.Width * (Y + 1)]
                : CellType.Air;

            return neighbouringCells;
        }

        public Dictionary<CellType, int> GetNeighbouringCellsStatus(Dictionary<string, CellType> neighbouringCells)
        {
            int aliveCells = 0;
            int deadCells = 0;

            foreach (KeyValuePair<string, CellType> pair in neighbouringCells)
            {
                if (pair.Value == CellType.Alive)
                {
                    aliveCells++;
                }

                if (pair.Value == CellType.Dead || pair.Value == CellType.Air)
                {
                    deadCells++;
                }
            }

            Dictionary<CellType, int> status = new Dictionary<CellType, int>();
            status[CellType.Alive] = aliveCells;
            status[CellType.Dead] = deadCells;
            return status;
        }

        public void Act(IGrid grid, List<CellType> currentGrid)
        {
            Dictionary<string, CellType> neighbourgs = GetNeighbouringCells(grid, currentGrid);
            var status = GetNeighbouringCellsStatus(neighbourgs);
            if (Type == CellType.Alive && (status[CellType.Alive] == 2 && status[CellType.Alive] == 3))
            {
                Type = CellType.Alive;
                return;
            }
            if (Type == CellType.Alive && status[CellType.Alive] < 2)
            {
                Type = CellType.Dead;
                return;
            }

            if (Type == CellType.Alive && status[CellType.Alive] > 3)
            {
                Type = CellType.Dead;
                return;
            }
            if (Type == CellType.Dead && status[CellType.Alive] == 3)
            {
                Type = CellType.Alive;
                return;
            }

        }

    }

    public interface ICell
   {
        int X { get; set; }
        int Y { get; set; }
        CellType Type { get; set; }

       void Place(int x, int y);

       Dictionary<string, CellType> GetNeighbouringCells(IGrid grid, List<CellType> currentGrid);

       Dictionary<CellType, int> GetNeighbouringCellsStatus(Dictionary<string, CellType> neighbouringCells);

       void Act(IGrid grid, List<CellType> currentGrid);

   }
}
