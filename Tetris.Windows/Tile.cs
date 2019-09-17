using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Windows
{
    internal class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }

        public System.Drawing.Color Colour { get; set; }

        public Tile()
        {

        }

        public Tile(int x, int y, System.Drawing.Color colour)
        {
            this.X = x;
            this.Y = y;
            this.Colour = colour;
        }
    }
}
