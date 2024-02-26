using System;
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

        private Action OnSelect;
        private Action OnDeselect;
        private Action OnMove;
        static Action OnHelp;

        private TileObject _selectedObject;

        public BasicCommands()
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
        }
        void Select()
        {
            int x = -1, y = -1;
            while(x < 0) //|| x > TileMap.Map.GetLength(0))
            {
                Console.WriteLine("Enter x coordinate:");
                int.TryParse(Console.ReadLine(), out x);
            }
            while(y < 0)
            {
                Console.WriteLine("Enter y coordinate:");
                int.TryParse(Console.ReadLine(), out y);
            }
            //_selectedObject = TileMap.Map[x,y].TileObject;
        }
        void Deselect()
        {
            _selectedObject = null;
        }
        void Move()
        {
            if(_selectedObject == null )
            {
                Console.WriteLine("Select a tile");
                return;
            }
        }
    }
}
