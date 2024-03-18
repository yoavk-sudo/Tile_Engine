using System.Text.RegularExpressions;

namespace Tile_Engine
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            TileMap map = new(3, 5);
            Console.WriteLine(map.Current);
            map.MoveNext();Console.WriteLine(map.Current);
            map.MoveNext();Console.WriteLine(map.Current);
            map.MoveNext();Console.WriteLine(map.Current);
            map.MoveNext();Console.WriteLine(map.Current);
            map.MoveNext();Console.WriteLine(map.Current);
            map.MoveNext();
            Commands.ExecuteCommand(2.ToString());
            Commands.ExecuteCommand("SE LECT     ".TrimAndDecapitalize());
            TileObject obj = new("AAAA");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    map.AddNewObject((TileObject)obj.Clone(), new Position(i, j));
                }
            }
            TileObject obj2 = new("AssA");
            map.AddNewObject(obj, new Position(0, 0));
            map.AddNewObject(obj2, new Position(0, 1));
            Commands.ExecuteCommand("select");
        }
    }
}

