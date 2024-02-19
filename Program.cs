namespace Tile_Engine
{
    internal class Program
    {
        public static int[,] Index { get; set; } = new int[1, 1];
        
        static void Main(string[] args)
        {
            Index = new int[1, 2]
            {
                {1, 3 }
            };

            TileMap m = new(4, 5);
            Console.WriteLine(m.Current);
            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);
            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);
            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);
            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);
            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);
            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);
            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);
            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);
            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);
            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);
            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);
            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);
            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);
            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);
            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);
            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);
            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);
            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);
            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);            Console.WriteLine(m.MoveNext());
            Console.WriteLine(m.Current);


        }

    }
}

