using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Engine
{
    public class TileObject : ICloneable
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
        public virtual object Clone()
        {
            return new TileObject(Name);
        }
    }
}
