using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameOfLife
{
    public class Grid : IGrid
    {
        public int Width { get; }
        public int Height { get; }
        public List<ICell> Fields { get; set; }
        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
            Fields = new List<ICell>();
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Fields.Add(new Cell() { X=j, Y=i, Type = CellType.Dead });
                }
            }
        }

        public void Place(ICell cell, int x, int y)
        {
            var index = GetGridIndex(x, y);
            cell.Place(x, y);
            Fields[index] = cell;
        }

        public List<CellType> GetCurrentGrid()
        {
            List<CellType> currentGrid = new List<CellType>();
            Fields.ForEach(f =>
            {
                if (f.Type == CellType.Air)
                {
                     currentGrid.Add(CellType.Air);
                }

                if (f.Type == CellType.Alive)
                {
                    currentGrid.Add(CellType.Alive);
                }

                if (f.Type == CellType.Dead)
                {
                    currentGrid.Add(CellType.Dead);
                }
            });

            return currentGrid;
        }

        public bool IsInGrid(int x, int y)
        {
            bool isWithinWidth = false;
            bool isWithinHeight = false;
            if (x >= 0 && x < Width)
            {
                isWithinWidth = true;
            }
            if (y >= 0 && y < Height)
            {
                isWithinHeight = true;
            }

            return isWithinHeight && isWithinWidth;
        }

        public int GetGridIndex(int x, int y)
        {
            return x + Width * y;
        }

        public void InitiateStep()
        {
            List<CellType> currentGrid = GetCurrentGrid();
            Fields.ForEach(c => c.Act(this, currentGrid));
        }
    }

    public interface IGrid
    {
        List<ICell> Fields { get; }
        int Width { get; }
        int Height { get; }

        bool IsInGrid(int x, int y);

        void Place(ICell cell, int x, int y);

        int GetGridIndex(int x, int y);

        List<CellType> GetCurrentGrid();

        void InitiateStep();

    }
}
