﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tile_Engine
{
    internal class BasicCommands
    {
        private const string HELP = "/help";
        private const string SELECT = "select";
        private const string DESELECT = "deselect";
        private const string MOVE = "move";

        private const string HELP_DESCRIPTION = "Displays list of available commands";
        private const string SELECT_DESCRIPTION = "Select tile object at position (x,y)";
        private const string DESELECT_DESCRIPTION = "Deselects the current tile object";
        private const string MOVE_DESCRIPTION = "Moves the selected tile object (if possible) to position (x,y)";

        private Action<object> OnSelect;
        private Action OnDeselect;
        private Action<object> OnMove;
        static Action OnHelp;
        

        internal BasicCommands()
        {
            OnHelp += HelpPrint;
            OnSelect += Select;
            OnDeselect += Deselect;
            OnMove += Move;
            AddBasicCommands();
        }

        void AddBasicCommands()
        {
            Commands.CreateNewCommand(HELP, HELP_DESCRIPTION, OnHelp);
            Commands.CreateNewCommand(SELECT, SELECT_DESCRIPTION, OnSelect);
            Commands.CreateNewCommand(DESELECT, DESELECT_DESCRIPTION, OnDeselect);
            Commands.CreateNewCommand(MOVE, MOVE_DESCRIPTION, OnMove);
        }
        void HelpPrint()
        {
            for (int i = 0; i < Commands.CommandDescriptions.Count; i++)
            {
                Console.Write(Commands.CommandDescriptions.Keys.ToArray()[i] + "\t");
                Console.Write(Commands.CommandDescriptions.Values.ToArray()[i] + "\n");
            }
            
            Console.WriteLine();
        }

        void Select(object input)
        {
            try
            {
                Position test = (Position)input;
            }
            catch
            {
                Console.WriteLine("input was not a position");
                return;
            }
            if (TileMap.Map == null)
            {
                Console.WriteLine("TileMap was not created");
                return;
            }

            Position pos = (Position)input;

            if (!TileMap.Map.IsWithinBounds(pos.X, pos.Y))
            {
                Console.WriteLine("Attempted to select outside of map bounds.");
                return;
            }


            TileObject.SelectedTileObject = TileMap.Map[pos.X, pos.Y].TileObject;
            Console.WriteLine($"Selected {TileObject.SelectedTileObject}");

        }


        /*
        void OldSelect()
        {
            if (TileMap.Map == null)
            {
                Console.WriteLine("TileMap was not created");
                return;
            }
            int x = -1, y = -1;

            Console.WriteLine("Enter a valid x coordinate:");
            int.TryParse(Console.ReadLine(), out x);

            Console.WriteLine("Enter a valid y coordinate:");
            int.TryParse(Console.ReadLine(), out y);
            
            if (!TileMap.Map.IsWithinBounds(x, y))
            {
                Console.WriteLine("Attempted to select outside of map bounds.");
                return;
            }
            _selectedObject = TileMap.Map[x,y].TileObject;
            Console.WriteLine($"Selected {_selectedObject}");
        }
        */
        
        void Deselect()
        {
            TileObject.SelectedTileObject = null;
        }
        void Move(object input)
        {
            try
            {
                Position newPosition = (Position) input;
                if (TileObject.SelectedTileObject == null)
                {
                    Console.WriteLine("Select a tile with an object in it");
                    return;
                }
                TileObject.SelectedTileObject.TileObjectMovement.TryMoveTileObject(TileObject.SelectedTileObject, newPosition);
            }
            catch{
                throw new InvalidCastException("move input was not a position");
            }
        }
    }
}
