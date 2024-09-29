using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using Tile_Engine;

using MonoTileGame;

namespace Chess_Demo
{
    internal class Program
    {
        static Sprite sprite = new("a", "ball");
        private const int PAWN_AMOUNT = 16;


       // public static List<MovableTileObject> Pawns { get; private set; } = new List<MovableTileObject>();
            static TileMap map = new(8, 8, sprite);

        static void Main(string[] args)
        {
            TileGame game = new TileGame(map);
            SpriteLoader spriteManager = new();
            spriteManager.SetMapBackground(map);
            Actor white = new("White");
            Actor black = new("Black");

            game.MousePressedOnTile += Tint;
            game.Run();
            

            Console.WriteLine(map.Current);
            map.MoveNext(); 
            Console.WriteLine(map.Current);
            WhitePawn p = new();
            TileObject pawnObject = p.CreateTileObject(map.Map[0, 0], white, sprite);
            for (int i = 0; i < map.Count(); i++)
            {
                pawnObject.Clone(new Position(i, 1));
            }
            pawnObject.Destroy();
        }


        public static void Tint(Tile tile)
        {
            tile.Texture.Tint(new Object());
        }

    }
}
