using Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Engine
{
    public abstract class ChessPiece
    {
        protected List<MovePattern> MovePatterns = new();
        public TileObject CreateTileObject(Tile tile, Actor owner, ISprite sprite)
        {
            TileObject to = new(tile, MovePatterns, owner, false, true);
            CreateMovePatterns();
            foreach (var mp in MovePatterns)
            {
                to.TileObjectMovement.AddMovePattern(mp);
            }
            return to;
        }
        protected abstract void CreateMovePatterns();

    }
}
