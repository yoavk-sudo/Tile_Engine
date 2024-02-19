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
        public Tile[,] Map { get; set; }
        
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
            if (Index[0, 1] >= Map.GetLength(1) - 1)
            {
                if (Index[0, 0] >= Map.GetLength(0) - 1)
                {
                    Reset();
                    return false;
                }
                Index[0, 1] = 0;
                Index[0, 0]++;
                return true;
            }
            Index[0, 1]++;
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
    }
}
