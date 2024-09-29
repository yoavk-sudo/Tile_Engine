using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tile_Engine;

namespace Chess_Demo
{
    internal class King : ChessPiece
    {
        protected override void CreateMovePatterns()
        {
            MovementDirections[] moves = { MovementDirections.Up };
            MovementDirections[] moves2 = { MovementDirections.UpLeft };
            MovementDirections[] moves3 = { MovementDirections.Left };
            MovementDirections[] moves4 = { MovementDirections.DownLeft };
            MovementDirections[] moves5 = { MovementDirections.Down };
            MovementDirections[] moves6 = { MovementDirections.DownRight };
            MovementDirections[] moves7 = { MovementDirections.Right };
            MovementDirections[] moves8 = { MovementDirections.UpRight };

            MovePattern movePattern = new MovePattern(moves);
            MovePattern movePattern2 = new MovePattern(moves2);
            MovePattern movePattern3 = new MovePattern(moves3);
            MovePattern movePattern4 = new MovePattern(moves4);
            MovePattern movePattern5 = new MovePattern(moves5);
            MovePattern movePattern6 = new MovePattern(moves6);
            MovePattern movePattern7 = new MovePattern(moves7);
            MovePattern movePattern8 = new MovePattern(moves8);
            
            MovePatterns.Add(movePattern);
            MovePatterns.Add(movePattern2);
            MovePatterns.Add(movePattern3);
            MovePatterns.Add(movePattern4);
            MovePatterns.Add(movePattern5);
            MovePatterns.Add(movePattern6);
            MovePatterns.Add(movePattern7);
            MovePatterns.Add(movePattern8);
        }
    }
}
