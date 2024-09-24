using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tile_Engine;

namespace Chess_Demo
{
    internal class Pawn
    {
        List<MovePattern> _movePatterns = new();
        
        public TileObject CreateTileObjectPawn(Tile tile, Actor owner, ISprite sprite)
        {
            MovementDirections[] moves = { MovementDirections.Up };
            MovementDirections[] moves2 = { MovementDirections.Up, MovementDirections.Up };
            MovementDirections[] moves3 = { MovementDirections.UpLeft };
            MovementDirections[] moves4 = { MovementDirections.UpRight };
            MovePattern movePattern = new MovePattern(moves); 
            MovePattern movePattern2 = new MovePattern(moves2); 
            MovePattern movePattern3 = new MovePattern(moves3); 
            MovePattern movePattern4 = new MovePattern(moves4); 
            _movePatterns.Add(movePattern);
            _movePatterns.Add(movePattern2);
            _movePatterns.Add(movePattern3);
            _movePatterns.Add(movePattern4);
            return new TileObject(tile, _movePatterns, owner, false, true);
        }
    }
}
