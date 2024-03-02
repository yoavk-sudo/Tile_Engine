using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Engine
{
    internal class TileMap : IEnumerable<Tile>, IEnumerator<Tile>, IIntIndexer
    {
        internal static Tile[,] Map { get; private set; }
        
        public TileMap(int x, int y) 
        {
            Map = new Tile[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Map[i, j] = new Tile(new Position(i, j));
                }
            }
        }

        #region Enumartor
        public Tile Current => Map[Index[0, 0], Index[0, 1]];

        object IEnumerator.Current => Current;

        public int[,] Index { get; set; } = new int[1, 2]
        {
            {0, 0 }
        };

        public void Dispose()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    Map[i, j] = null;
                }
            }
        }

        public IEnumerator<Tile> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            if (Index[0, 1] >= Map.GetLength(1) - 1) //x >= X
            {
                if (Index[0, 0] >= Map.GetLength(0) - 1) //y >= Y
                {
                    Reset();
                    return false;
                }
                Index[0, 1] = 0; //x=0
                Index[0, 0]++; //y++
                return true;
            }
            Index[0, 1]++; //x++
            return true;
        }

        public void Reset()
        {
            Index[0,0] = 0;
            Index[0,1] = 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion

        public static bool IsTileEmpty(Position pos)
        {
            if(!IsPositionValid(pos)) 
                return false;
            return Map[pos.X, pos.Y].IsEmpty;
        }

        public static bool IsPositionValid(Position pos)
        {
            if (pos.X < 0 || pos.Y < 0)
                return false;
            return pos.X < Map.GetLength(0) && pos.Y < Map.GetLength(1);
        }

        public void AddNewObject(TileObject obj, Position pos)
        {
            if (obj == null) throw new ArgumentNullException("object was null");
            if (!IsPositionValid(pos))
            {
                Console.WriteLine("object position is out of bounds, invalid");
                return;
            }
            if(!IsTileEmpty(pos))
            {
                Console.WriteLine($"Tile is not empty, cannot place {obj} in it.");
                return;
            }
            Map[pos.X, pos.Y].TileObject = obj;
            Console.WriteLine($"{obj} is now in the Tile Map, position is: {pos}");
        }
    }
}
