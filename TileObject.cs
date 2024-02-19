using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Engine
{
    internal class TileObject : ICloneable
    {
        public string Name { get; set; }

        public TileObject(string name) 
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
        public object Clone()
        {
            return new TileObject(Name);
        }
    }
}
