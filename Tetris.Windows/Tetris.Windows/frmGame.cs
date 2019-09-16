using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris.Windows
{
    public partial class frmGame : Form
    {
        private GameBoard _board;
        private List<Tile> _tiles;
        private Shapes.Shape _currentShape;
        private int _score = 0;

        public frmGame()
        {
            InitializeComponent();

            _board = new GameBoard(300);
            _board.Position = new Point(25, 25);

            _tiles = new List<Tile>();

            _currentShape = Shapes.ShapeFactory.GenerateShape(Shapes.ShapeTypeEnum.I);
            _currentShape.Position = new Point(5, 12);

            tickTimer.Enabled = true;
            //tickTimer.Interval = 5;
        }

        private void frmGame_Paint(object sender, PaintEventArgs e)
        {
            _board.DrawBackground(e.Graphics);

            _board.DrawPieces(e.Graphics, _tiles.ToArray(), new Point());

            _board.DrawShape(e.Graphics, _currentShape);

            _board.DrawGrid(e.Graphics);
        }

        private void tickTimer_Tick(object sender, EventArgs e)
        {
            _board.LightHerUpBoyo = false;
            // Can we move down?
            bool isItFucked = false;
            foreach (var tile in _currentShape.Tiles)
            {
                Point newTilePos = new Point(tile.X + _currentShape.Position.X, tile.Y + _currentShape.Position.Y + 1);

                if (newTilePos.Y >= Settings.BOARD_TILE_HEIGHT)
                {
                    isItFucked = true;
                }

                var existingTile = _tiles.Where(x => x.X == newTilePos.X && x.Y == newTilePos.Y).FirstOrDefault();

                if (existingTile != null)
                    isItFucked = true;
            }

            if (isItFucked)
            {
                foreach (var tile in _currentShape.Tiles)
                {
                    tile.X += _currentShape.Position.X;
                    tile.Y += _currentShape.Position.Y;

                    _tiles.Add(tile);
                }

                _currentShape = Shapes.ShapeFactory.GenerateShape(Shapes.ShapeTypeEnum.I);
            }

            // move the urrent chsape the so dasd mdfgalkdjg lkasdg lkasjdglk dielfield
            this._currentShape.Position = new Point(this._currentShape.Position.X, this._currentShape.Position.Y + 1);


            // Check for lines that have been completed
            for(int y = 0; y < Settings.BOARD_TILE_HEIGHT; y++)
            {
                var tiles = _tiles.Where(x => x.Y == y).ToArray();

                if(tiles.Length == Settings.BOARD_TILE_WIDTH)
                {
                    foreach(var tile in tiles)
                    {
                        _tiles.Remove(tile);
                    }

                    var subTiles = _tiles.Where(x => x.Y <= y).ToArray();
                    foreach(var subTile in subTiles)
                    {
                        subTile.Y += 1;
                    }

                    _score++;
                    _board.LightHerUpBoyo = true;
                }
            }

            // Find a tile too far above
            var northFaceTile = _tiles.Where(x => x.Y <= 1).FirstOrDefault();
            if(northFaceTile != null)
            {
                tickTimer.Enabled = false;
                _board.GameOver = true;
            }


            this.Text = _score.ToString();

            this.Invalidate();
        }

        private bool AmIAllUpInsideATileBuddy(Point newTilePos)
        {
            var existingTile = _tiles.Where(x => x.X == newTilePos.X && x.Y == newTilePos.Y).FirstOrDefault();

            return existingTile != null;
        }

        private void frmGame_KeyUp(object sender, KeyEventArgs e)
        {
            int xDir = 0;
            int yDir = 0;

            if (e.KeyCode == Keys.D)
            {
                xDir = 1;
            }

            if (e.KeyCode == Keys.A)
            {
                xDir = -1;
            }

            if (e.KeyCode == Keys.S)
            {
                yDir = 1;
            }

            if(e.KeyCode == Keys.Space)
            {
                _currentShape.Rotate(_tiles);
                this.Invalidate();
            }


            foreach (var tile in _currentShape.Tiles)
            {
                Point newTilePos = new Point(tile.X + _currentShape.Position.X + xDir, tile.Y + _currentShape.Position.Y);

                if (AmIAllUpInsideATileBuddy(newTilePos))
                {
                    xDir = 0;
                }
                else if (newTilePos.X < 0 || newTilePos.X >= Settings.BOARD_TILE_WIDTH)
                {
                    xDir = 0;
                }





                Point newTilePosY = new Point(tile.X + _currentShape.Position.X, tile.Y + _currentShape.Position.Y + yDir);

                if (AmIAllUpInsideATileBuddy(newTilePosY))
                {
                    yDir = 0;
                }
                else if (newTilePosY.Y < 0 || newTilePosY.Y >= Settings.BOARD_TILE_HEIGHT)
                {
                    yDir = 0;
                }

            }

            _currentShape.Position = new Point(_currentShape.Position.X + xDir, _currentShape.Position.Y + yDir);

            if (xDir != 0 || yDir != 0)
            {
                this.Invalidate();
            }
        }
    }
}
