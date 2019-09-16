using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris.Windows
{
    internal class GameBoard
    {
        public Point Position { get; set; }

        private int _width, _height;
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
        public bool LightHerUpBoyo { get; set; }
        public bool GameOver { get; set; }

        private int _tileSize;

        public GameBoard(int width)
        {
            this._width = width;

            this._tileSize = _width / Settings.BOARD_TILE_WIDTH;
            this._height = this._tileSize * Settings.BOARD_TILE_HEIGHT;
        }

        public void DrawBackground(Graphics gobj)
        {
            var col = Color.CornflowerBlue;
            if (LightHerUpBoyo)
                col = Color.Goldenrod;
            else if (GameOver)
                col = Color.Firebrick;
            // Fill in background
            gobj.FillRectangle(new SolidBrush(col), new RectangleF(Position.X, Position.Y, Width, Height));

        }

        public void DrawGrid(Graphics gobj)
        {  
            // Draw a border
            gobj.DrawRectangle(new Pen(Color.Black, 2), new Rectangle(Position.X, Position.Y, Width, Height));
            
            // Draw lines
            for(int x = 0; x < Settings.BOARD_TILE_WIDTH; x++)
            {
                gobj.DrawLine(new Pen(Color.Black, 2), new Point((x * _tileSize) + Position.X, Position.Y), new Point((x * _tileSize) + Position.X, Position.Y + _height));
            }

            for(int y = 0; y < Settings.BOARD_TILE_HEIGHT; y++)
            {
                gobj.DrawLine(new Pen(Color.Black, 2), new Point(Position.X, (y * _tileSize) + Position.Y), new Point(Position.X + _width, (y * _tileSize) + Position.Y));
            }
        }

        public void DrawPieces(Graphics gobj, Tile[] tiles, Point extraTagAlongPointzies)
        {
            foreach(var tile in tiles)
            {
                gobj.FillRectangle(new SolidBrush(tile.Colour), GetTileArea(tile, extraTagAlongPointzies));
            }
        }

        public void DrawShape(Graphics gobj, Shapes.Shape shape)
        {
            DrawPieces(gobj, shape.Tiles, shape.Position);            
        }

        private Rectangle GetTileArea(Tile tile, Point extraTagAlongPointzies)
        {
            return new Rectangle(((tile.X + extraTagAlongPointzies.X) * _tileSize) + Position.X, ((tile.Y + extraTagAlongPointzies.Y) * _tileSize) + Position.Y, _tileSize, _tileSize);
        }
    }
}
