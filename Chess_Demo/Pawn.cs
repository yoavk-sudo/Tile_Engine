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
            MovementDirections[] moves = { MovementDirections.Up, MovementDirections.UpLeft, MovementDirections.UpRight };
            MovePattern movePattern = new MovePattern(moves); 
            _movePatterns.Add(movePattern);
            return new TileObject(tile, _movePatterns, owner, false, true);
        }
    }
}
