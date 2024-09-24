using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tile_Engine;
using System.Threading.Tasks;

namespace Chess_Demo
{
    internal class ChessTurnHandler : AbstractTurnHandler
    {
        public override event Action NewTurn;
        public override event Action EndOfTurn;

        public static Player CurrentPlayer {  get; private set; }

        private bool isTurnActive;

        public ChessTurnHandler() 
        {
            CurrentPlayer = Player.White;
            NewTurn = StartTurn;
            EndOfTurn = BetweenTurns;
        }
        public override void EndTurn()
        {
            EndOfTurn.Invoke();
            NextPlayer();
            NewTurn.Invoke();
        }

        private void BetweenTurns()
        {
            isTurnActive = false;
        }

        private void StartTurn()
        {

            isTurnActive=true;
        }

        private void NextPlayer()
        {
            if (CurrentPlayer == Player.White) CurrentPlayer = Player.Black;
            else CurrentPlayer = Player.White;
        }
    }

    public enum Player
    {
        White,Black
    }
}
