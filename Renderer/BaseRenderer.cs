using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer
{
    internal abstract class BaseRenderer
    {
        public abstract char LeftTile { get; set; }
        public abstract char RightTile { get; set; }

        public abstract Color TileForegroundColor { get; set; }
        public abstract Color TileBackgroundColor { get; set; }
        public abstract Color ObjectForegroundColor { get; set; }
        public abstract Color ObjectBackgroundColor { get; set; }
    }
}
