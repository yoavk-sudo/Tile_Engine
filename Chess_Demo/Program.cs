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
            Sprite pSprite = spriteManager.WhiteUnits[0]; 
            Actor white = new("White");
            Actor black = new("Black");
            spriteManager.SetMapBackground(map);
            //pawnObject.Destroy();

            WhitePawn p = new();
            TileObject pawnObject = p.CreateTileObject(map.GetMap()[0, 0], white, pSprite);
            for (int i = 0; i < map.Count(); i++)
            {
                pawnObject.Clone(new Position(i, 1));
            }
            //pawnObject.Destroy();
            game.MousePressedOnTile += Tint;
            game.Run();
            

            Console.WriteLine(map.Current);
            map.MoveNext(); 
            Console.WriteLine(map.Current);
            
        }


        public static void Tint(Tile tile)
        {
            tile.Texture.Tint(new Object());
        }

    }
}
