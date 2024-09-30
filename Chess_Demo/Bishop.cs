using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tile_Engine;

namespace Chess_Demo
{
    internal class Bishop : ChessPiece
    {
        protected override void CreateMovePatterns()
        {
            MovementDirections[] moves = { MovementDirections.UpLeft };
            MovementDirections[] moves2 = { MovementDirections.DownLeft };
            MovementDirections[] moves3 = { MovementDirections.DownRight };
            MovementDirections[] moves4 = { MovementDirections.UpRight };

            MovePattern movePattern = new MovePattern(moves, null, TileMap.Map.Length - 1);
            MovePattern movePattern2 = new MovePattern(moves2, null, TileMap.Map.Length - 1);
            MovePattern movePattern3 = new MovePattern(moves3, null, TileMap.Map.Length - 1);
            MovePattern movePattern4 = new MovePattern(moves4, null, TileMap.Map.Length - 1);

            MovePatterns.Add(movePattern);
            MovePatterns.Add(movePattern2);
            MovePatterns.Add(movePattern3);
            MovePatterns.Add(movePattern4);
        }
    }
}
