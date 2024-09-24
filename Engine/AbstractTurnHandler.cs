using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Engine
{
    public abstract class AbstractTurnHandler
    {
        public abstract event Action NewTurn;

        public abstract event Action EndOfTurn;

        public abstract void EndTurn();
    }
}
