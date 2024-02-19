using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Engine
{
    internal class Tile : IPosition
    {
        public Position Position { get; }
        public TileObject TileObject { get; set; } = null;
        public bool IsEmpty { get {  return TileObject == null; } }
        //public Actor Actor { get; set; }

        public override string ToString()
        {
            return Position.ToString();
        }

        public Tile(Position position)
        {
            Position = position;
        }
    }
}
