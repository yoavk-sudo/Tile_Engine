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
        public static Actor WhitePlayer { get; private set; }

        public static Actor BlackPlayer { get; private set; }


        private bool isTurnActive;

        public ChessTurnHandler(Actor whitePlayer,Actor blackPlayer) 
        {
            CurrentPlayer = Player.White;
            WhitePlayer = whitePlayer;
            BlackPlayer = blackPlayer;
            NewTurn = StartTurn;
            EndOfTurn = BetweenTurns;
        }

        public static Actor GetCurrentPlayer()
        {
            if (CurrentPlayer == Player.White) return WhitePlayer;
            else return BlackPlayer;
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
