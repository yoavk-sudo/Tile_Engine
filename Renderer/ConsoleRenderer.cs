using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer
{
    internal class ConsoleRenderer : BaseRenderer
    {
        public override char LeftTile { get; set; } = '[';
        public override char RightTile { get; set; } = ']';
        public override Color TileForegroundColor { get; set; }
        public override Color TileBackgroundColor { get; set; }
        public override Color ObjectForegroundColor { get; set; }
        public override Color ObjectBackgroundColor { get; set; }
    }
}
