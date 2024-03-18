using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Engine
{
    public abstract class AbstractTurnHandler
    {
        public event Action NewTurn;

        public void EndTurn();
    }
}
