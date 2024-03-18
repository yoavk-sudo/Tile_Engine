using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using Tile_Engine;

namespace Chess_Demo
{
    internal class Program
    {
        private const int PAWN_AMOUNT = 16;

        public static List<MovableTileObject> Pawns { get; private set; } = new List<MovableTileObject>();

        static void Main(string[] args)
        {
            MovableTileObject pawn = new("Pawn");

            for (int i = 0; i < PAWN_AMOUNT; i++)
            {
                //Pawns.Add((MovableTileObject)pawn.Clone());
            }
            
        }
    }
}
