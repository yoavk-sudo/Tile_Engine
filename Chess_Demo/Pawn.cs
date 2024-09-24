using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tile_Engine;

namespace Chess_Demo
{
    internal class Pawn : ChessPiece
    {       
        protected override void CreateMovePatterns()
        {
            MovementDirections[] moves = { MovementDirections.Up };
            MovementDirections[] moves2 = { MovementDirections.Up, MovementDirections.Up };
            MoveConditions doubleUpConditon = new MoveConditions(new Position(0, 2), true, false, false);
            MovementDirections[] moves3 = { MovementDirections.UpLeft };
            MoveConditions leftUpConditon = new MoveConditions(new Position(-1, 1), false, false, true);
            MovementDirections[] moves4 = { MovementDirections.UpRight };
            MoveConditions rightUpConditon = new MoveConditions(new Position(1, 1), false, false, true);
            MovePattern movePattern = new MovePattern(moves, null);
            MovePattern movePattern2 = new MovePattern(moves2, doubleUpConditon);
            MovePattern movePattern3 = new MovePattern(moves3, leftUpConditon);
            MovePattern movePattern4 = new MovePattern(moves4, rightUpConditon);
            MovePatterns.Add(movePattern);
            MovePatterns.Add(movePattern2);
            MovePatterns.Add(movePattern3);
            MovePatterns.Add(movePattern4);

        }
    }
}
