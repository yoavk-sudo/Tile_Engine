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
            static TileMap map = new(10, 10, sprite);

        static void Main(string[] args)
        {
            TileGame game = new TileGame(map);
            game.MousePressedOnTile += Tint;
            game.Run();
            

            Console.WriteLine(map.Current);
            map.MoveNext(); 
            Console.WriteLine(map.Current);
            //TileObject pawn = new("Pawn");

            //for (int i = 0; i < PAWN_AMOUNT; i++)
            //{
            //    //Pawns.Add((MovableTileObject)pawn.Clone());
            //}
            var keyInfo = Console.ReadKey();
            while (keyInfo.Key != ConsoleKey.W)
            {
                keyInfo = Console.ReadKey();
            }
        }


        public static void Tint(Tile tile)
        {
            tile.Texture.Tint(new Object());
        }

    }
}
