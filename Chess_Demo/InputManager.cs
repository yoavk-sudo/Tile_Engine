﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoTileGame;
using Tile_Engine;

namespace Chess_Demo
{
    internal class InputManager
    {
        private ChessTurnHandler turnHandler { get; set; }

        public InputManager(TileGame game, ChessTurnHandler turnHandler)
        {
            game.CommandLineTrigger += HandleTextInput;
            game.MousePressedOnTile += HandleTileInput;
            this.turnHandler = turnHandler; 
        }


        private void HandleTextInput(string input)
        {
            Commands.ExecuteCommand(input);
        }


        private void HandleTileInput(Tile tile)
        {
            if (TileObject.SelectedTileObject == null && tile.TileObject != null && tile.TileObject.Owner.Name == ChessTurnHandler.GetCurrentPlayer().Name)
            {
                Console.WriteLine("select");
                Commands.ExecuteCommand("Select", tile.Position);
            }
            else if (TileObject.SelectedTileObject != null && tile.Position == TileObject.SelectedTileObject.Position)
            {
                Console.WriteLine("deselect");
                Commands.ExecuteCommand("Deselect");
            }
            else if (TileObject.SelectedTileObject != null && TileObject.SelectedTileObject.TileObjectMovement.GetAllPossibleMoves().Item1.Contains(tile.Position))
            {
                Console.WriteLine("move");
                Commands.ExecuteCommand("Move", tile.Position);
                Commands.ExecuteCommand("Deselect");
                turnHandler.EndTurn();
            }
            else
            {
                Commands.ExecuteCommand("Deselect");
            }
        }



    }
}
