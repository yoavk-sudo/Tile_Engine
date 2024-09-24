using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using Tile_Engine;

using MonoTileGame;
using Renderer;

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
            foreach (var tile in map)
            {
                
            }
            game.MousePressedOnTile += Tint;
            //game.Run();
            

            //Console.WriteLine(map.Current);
            //map.MoveNext(); 
            //Console.WriteLine(map.Current);
            //TileObject pawn = new("Pawn");

            //for (int i = 0; i < PAWN_AMOUNT; i++)
            //{
            //    //Pawns.Add((MovableTileObject)pawn.Clone());
            //}
            //var keyInfo = Console.ReadKey();
            //while (keyInfo.Key != ConsoleKey.W)
            //{
            //    keyInfo = Console.ReadKey();
            //}
            Actor white = new("white");
            List<MovePattern> patterns = new List<MovePattern>();
            MovementDirections[] moves = { MovementDirections.Up, MovementDirections.Right, MovementDirections.UpRight };
            MovePattern movePattern = new MovePattern(moves);
            patterns.Add(movePattern);
            TileObject knight = new(TileMap.Map[0,0], patterns, white, false, true);
            TileObject pawn = new(TileMap.Map[1,1], patterns, white, true, true);
            map.AddNewObject(knight, knight.Position);
            map.AddNewObject(pawn, pawn.Position);

            Console.WriteLine("Knight's current position is: " + knight.CurrentTile);
            knight.TileObjectMovement.TryMoveTileObject(knight, new Position(1, 1));
            Console.WriteLine("Knight's new position is: " + knight.CurrentTile);
            knight.TileObjectMovement.TryMoveTileObject(knight, new Position(3, 1));
            Console.WriteLine("Knight's new position is: " + knight.CurrentTile);
        }

        public static void Tint(Tile tile)
        {
            tile.Texture.Tint(new Object());
        }

    }
}
