using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandSimulator
{
    abstract class Cell
    {
        public abstract byte[] GetColour();
    }

    class EmptyCell : Cell
    {
        public override byte[] GetColour()
        {
            return new byte[3] { 0, 0, 0};
        }
    }
    class SandCell : Cell
    {
        public override byte[] GetColour()
        {
            return new byte[3] { 125, 125, 125 };
        }
    }

    class Watercell : Cell
    {
        public override byte[] GetColour()
        {
            return new byte[3] { 0, 0, 125 };
        }
    }

    internal class WorldGrid
    {
        Cell[] grid;
        public uint Width { get; }
        public uint Height { get; }

        public WorldGrid(uint width, uint height)
        {
            this.Width = width;
            this.Height = height;
            grid = new Cell[width * height];
            InitGrid();
        }

        private void InitGrid()
        {
            for (uint i = 0; i < Height; i++)
            {
                for (uint j = 0; j < Width; j++)
                {
                    if (j == 0 || j == 1)
                    {
                        if (i > 3 && i < 6)
                        {
                            SetCell(i, j, new SandCell());
                        } else
                        {
                            SetCell(i, j, new EmptyCell());

                        }
                    } else
                    {
                        SetCell(i, j, new EmptyCell());

                    }
                }
            }
        }
        public Cell GetCell(uint x, uint y)
        {
            if( x >= Width || y >= Height)
            {
                return new EmptyCell();
            } 
            return grid[x * y];
        }

        public void SetCell(uint x, uint y, Cell cell)
        {
            if (x >= Width || y >= Height)
            {
                return;
            }
            grid[x * y] = cell;
        }

        public Cell[] GetNeighbours(uint x, uint y)
        {
            return new Cell[] {
                GetCell(x - 1, y - 1),
                GetCell(x - 1, y),
                GetCell(x - 1, y + 1),
                GetCell(x, y - 1),
                GetCell(x, y + 1),
                GetCell(x + 1, y - 1),
                GetCell(x + 1, y),
                GetCell(x + 1, y + 1),
            };
        }

        public Cell[] GetGrid()
        {
            return grid;
        }

        public void Step()
        {
            for (uint i = 0; i < Height; i++)
            {
                for (uint j = 0; j < Width; j++)
                {
                    var cell = GetCell(i, j);
                    
                }
            }
        }
    }
}
