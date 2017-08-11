using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyGameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            IGrid myGrid = new Grid(50, 25);

            //Gosper Glider Gun
            myGrid.Place(new Cell() { Type=CellType.Alive}, 10, 6);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 10, 7);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 11, 6);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 11, 7);

            myGrid.Place(new Cell() { Type = CellType.Alive }, 18, 7);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 18, 8);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 19, 6);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 19, 8);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 20, 6);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 20, 7);

            myGrid.Place(new Cell() { Type = CellType.Alive }, 26, 8);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 26, 9);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 26, 10);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 27, 8);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 28, 9);

            myGrid.Place(new Cell() { Type = CellType.Alive }, 32, 5);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 32, 6);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 33, 4);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 33, 6);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 34, 4);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 34, 5);

            myGrid.Place(new Cell() { Type = CellType.Alive }, 34, 16);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 34, 17);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 35, 16);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 35, 18);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 36, 16);

            myGrid.Place(new Cell() { Type = CellType.Alive }, 44, 4);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 44, 5);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 45, 4);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 45, 5);

            myGrid.Place(new Cell() { Type = CellType.Alive }, 45, 11);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 45, 12);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 45, 13);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 46, 11);
            myGrid.Place(new Cell() { Type = CellType.Alive }, 47, 12);

            World myWorld = new World(myGrid);

            myWorld.Start(1000, 20);

        }
    }
}
