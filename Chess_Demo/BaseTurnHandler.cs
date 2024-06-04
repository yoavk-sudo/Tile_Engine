using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tile_Engine;
using System.Threading.Tasks;

namespace Chess_Demo
{
    internal class BaseTurnHandler : AbstractTurnHandler
    {
        public event Action NewTurn;

        private bool isTurnActive;

        public BaseTurnHandler() 
        {
            NewTurn = StartTurn;
        }
        public override void EndTurn()
        {

            isTurnActive = false;
        }

        private void StartTurn()
        {
            isTurnActive=true;
        }


    }
}
