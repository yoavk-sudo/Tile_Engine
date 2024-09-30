using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using Tile_Engine;

using MonoTileGame;

namespace Chess_Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameSetup gs = new GameSetup();
            gs.CreateChessPieces();
            gs.RunGame();
        }
    }
}
