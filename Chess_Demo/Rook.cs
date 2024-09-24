using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tile_Engine;

namespace Chess_Demo
{
    internal class Rook : ChessPiece
    {
        protected override void CreateMovePatterns()
        {
            MovementDirections[] moves = { MovementDirections.Up };
            MovementDirections[] moves2 = { MovementDirections.Left };
            MovementDirections[] moves3 = { MovementDirections.Down };
            MovementDirections[] moves4 = { MovementDirections.Right };

            MovePattern movePattern = new MovePattern(moves, null, TileMap.Map.Length);
            MovePattern movePattern2 = new MovePattern(moves2, null, TileMap.Map.Length);
            MovePattern movePattern3 = new MovePattern(moves3, null, TileMap.Map.Length);
            MovePattern movePattern4 = new MovePattern(moves4, null, TileMap.Map.Length);

            MovePatterns.Add(movePattern);
            MovePatterns.Add(movePattern2);
            MovePatterns.Add(movePattern3);
            MovePatterns.Add(movePattern4);
        }
    }
}
