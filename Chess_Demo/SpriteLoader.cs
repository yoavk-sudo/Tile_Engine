using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tile_Engine;
using MonoTileGame;

namespace Chess_Demo
{
    public class SpriteLoader
    {
        public Sprite[] WhiteTiles = { new Sprite("WhiteTile1","WhiteTile1"),
                new Sprite("WhiteTile2", "WhiteTile2"),
                new Sprite("WhiteTile3", "WhiteTile3"),
                new Sprite("WhiteTile4", "WhiteTile4") };

        public Sprite[] BlackTiles = { new Sprite("BlackTile1","BlackTile1"),
                new Sprite("BlackTile2", "BlackTile2"),
                new Sprite("BlackTile3", "BlackTile3"),
                new Sprite("BlackTile4", "BlackTile4") };

        public Sprite[] WhiteUnits =
        {
            new Sprite("Pawn W","Pawn W"),
            new Sprite("Bishop W","Bishop W"),
            new Sprite("Knight W","Knight W"),
            new Sprite("Rook W","Rook W"),
            new Sprite("Queen W","Queen W"),
            new Sprite("King W","King W")
        };

        public Sprite[] BlackUnits =
        {
            new Sprite("Pawn B","Pawn B"),
            new Sprite("Bishop B","Bishop B"),
            new Sprite("Knight B","Knight B"),
            new Sprite("Rook B","Rook B"),
            new Sprite("Queen B","Queen B"),
            new Sprite("King B","King B")
        };

        public SpriteLoader()
        {
            LoadData(WhiteTiles);
            LoadData(BlackTiles);
            LoadData(WhiteUnits);
            LoadData(BlackUnits);
        }

        public void SetMapBackground(TileMap map)
        {
            Random rnd = new Random();

            foreach (var tile in map)
            {
                if ((tile.Position.X + tile.Position.Y) % 2 == 0)
                {
                    tile.Texture.UpdateSprite(WhiteTiles[rnd.Next(0, 4)]);
                }
                else
                {
                    tile.Texture.UpdateSprite(BlackTiles[rnd.Next(0, 4)]);
                }

            }
        }

        public void LoadData(Sprite[] SpriteData)
        {
            foreach (Sprite sprite in SpriteData)
            {
                LoadLibrary.Instance.LoadData(sprite);
            }
        }
    }
}
