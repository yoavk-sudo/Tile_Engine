using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tile_Engine;

namespace Chess_Demo
{
    internal class Knight : ChessPiece
    {
        protected override void CreateMovePatterns()
        {
            MovementDirections[] moves = { MovementDirections.Up, MovementDirections.Up, MovementDirections.Left };
            MovementDirections[] moves2 = { MovementDirections.Up, MovementDirections.Up, MovementDirections.Right };
            MovementDirections[] moves3 = { MovementDirections.Down, MovementDirections.Down, MovementDirections.Left };
            MovementDirections[] moves4 = { MovementDirections.Down, MovementDirections.Down, MovementDirections.Right };

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
