using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris.Windows.Shapes
{
    abstract internal class Shape
    {
        public Tile[] Tiles { get; set; }
        public Point Position { get; set; }
        public bool Rotated { get; set; }

        public abstract Color ShapeColour { get; }

        public void Rotate(List<Tile> worldTiles)
        {
            var rotatedTiles = RotateDemTilesYo();

            // Do some collision checking
            foreach(var tile in rotatedTiles)
            {
                Point worldTilePosition = new Point(tile.X + Position.X, tile.Y + Position.Y);
                // Check the world for collisions
                var collidedTile = worldTiles.Where(x => x.X == worldTilePosition.X && x.Y == worldTilePosition.Y).FirstOrDefault();

                if(collidedTile != null)
                {
                    return;
                }

                if(worldTilePosition.X < 0 || worldTilePosition.X >= Settings.BOARD_TILE_WIDTH || worldTilePosition.Y < 0 || worldTilePosition.Y >= Settings.BOARD_TILE_HEIGHT)
                {
                    return;
                }
            }

            // Are we good in the teh good?

            // Setth tiles to the the tiles

            // Set rotated to tre
            this.Tiles = rotatedTiles;

            Rotated = !Rotated;
        }

        protected abstract Tile[] RotateDemTilesYo();
    }

    internal static class ShapeFactory
    {

        
        public static Shape GenerateShape(ShapeTypeEnum shapeType)
        {
            switch (shapeType)
            {
                case ShapeTypeEnum.I:
                    return new ShapeI();

            }

            return null;
        }
    }

    internal enum ShapeTypeEnum
    {
        I, O, T, J, L, S, Z
    }

    internal class ShapeI : Shape
    {
        public ShapeI()
        {
            this.Tiles = new Tile[4];
            this.Tiles[0] = new Tile(0, 0, ShapeColour);
            this.Tiles[1] = new Tile(0, 1, ShapeColour);
            this.Tiles[2] = new Tile(0, 2, ShapeColour);
            this.Tiles[3] = new Tile(0, 3, ShapeColour);
        }

        public override Color ShapeColour => Color.ForestGreen;

        protected override Tile[] RotateDemTilesYo()
        {
            var results = new Tile[4];
            if (!Rotated)
            {
                results[0] = new Tile(0, 0, ShapeColour);
                results[1] = new Tile(1, 0, ShapeColour);
                results[2] = new Tile(2, 0, ShapeColour);
                results[3] = new Tile(3, 0, ShapeColour);
            }
            else
            {
                results[0] = new Tile(0, 0, ShapeColour);
                results[1] = new Tile(0, 1, ShapeColour);
                results[2] = new Tile(0, 2, ShapeColour);
                results[3] = new Tile(0, 3, ShapeColour);
            }

            return results;
        }
    }

    internal class ShapeO : Shape
    {
        protected override Tile[] RotateDemTilesYo()
        {
            throw new NotImplementedException();
        }

        public override Color ShapeColour => Color.Green;
    }

    internal class ShapeT : Shape
    {
        protected override Tile[] RotateDemTilesYo()
        {
            throw new NotImplementedException();
        }

        public override Color ShapeColour => Color.Green;
    }

    internal class ShapeJ : Shape
    {
        protected override Tile[] RotateDemTilesYo()
        {
            throw new NotImplementedException();
        }

        public override Color ShapeColour => Color.Green;
    }

    internal class ShapeL : Shape
    {
        protected override Tile[] RotateDemTilesYo()
        {
            throw new NotImplementedException();
        }

        public override Color ShapeColour => Color.Green;
    }

    internal class ShapeS : Shape
    {
        protected override Tile[] RotateDemTilesYo()
        {
            throw new NotImplementedException();
        }

        public override Color ShapeColour => Color.Green;
    }

    internal class ShapeZ : Shape
    {
        protected override Tile[] RotateDemTilesYo()
        {
            throw new NotImplementedException();
        }

        public override Color ShapeColour => Color.Green;
    }
}
