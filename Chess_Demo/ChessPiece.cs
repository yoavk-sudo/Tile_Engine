using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Engine
{
    internal abstract class ChessPiece
    {
        protected List<MovePattern> MovePatterns = new();
        public TileObject CreateTileObject(Tile tile, Actor owner, ISprite sprite)
        {
            CreateMovePatterns();
            TileObject to = new(tile, MovePatterns, owner, sprite, false, true);
            var patternsCopy = new List<MovePattern>(MovePatterns);
            foreach (var mp in patternsCopy)
            {
                to.TileObjectMovement.AddMovePattern(mp);
            }
            return to;
        }
        protected abstract void CreateMovePatterns();

    }
}
