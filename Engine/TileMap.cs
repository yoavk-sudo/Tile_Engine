using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renderer;

namespace Tile_Engine
{
    public class TileMap : IEnumerable<Tile>, IEnumerator<Tile>, IIntIndexer
    {
        public static Tile[,] Map { get; private set; }
        
        public TileMap(int x, int y, ISprite DefaultSprite) 
        {
            Map = new Tile[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Map[i, j] = new Tile(new Position(i, j), DefaultSprite);
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
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    yield return Map[i, j];
                }
            }
        }

        public bool MoveNext()
        {
            if (Index[0,1] < Map.GetLength(1) - 1)
            {
                return TryMoveHorizontal();
            }
            else
            {
                return TryMoveVertical();
            }
            //if (Index[0, 1] >= Map.GetLength(1) - 1) //x >= X
            //{
            //    if (Index[0, 0] >= Map.GetLength(0) - 1) //y >= Y
            //    {
            //        Index[0, 0] = 0;
            //        Index[0, 1] = 0;
            //        return false;
            //    }
            //    Index[0, 1] = 0; //x=0
            //    Index[0, 0]++; //y++
            //    return true;
            //}
            //Index[0, 1]++; //x++
            //return true;
        }

        private bool TryMoveVertical()
        {
            if (--Index[0, 0] >= 0) //y--
            {
                return true;
            }
            if ((Index[0, 0] += 2) >= Map.GetLength(0) - 1) //y >= Y
            {
                return false;
            }
            return true;
        }

        private bool TryMoveHorizontal()
        {
            if (--Index[0, 1] >= 0) //x--
            {
                return true;
            }
            if ((Index[0, 1] += 2) >= Map.GetLength(1) - 1) //x >= X
            {
                return TryMoveVertical();
            }
            return true;
        }

        public void Reset()
        {}

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion

        public Tile[,] GetMap()
        {
            return Map;
        }

        public static bool IsTileEmpty(Position pos)
        {
            if (Map == null)
                throw new ArgumentNullException("Map is null");
            if(!Map.IsWithinBounds(pos.X, pos.Y)) 
                return false;
            return Map[pos.X, pos.Y].IsEmpty;
        }

        public void AddNewObject(TileObject obj, Position pos)
        {
            if (obj == null) 
                throw new ArgumentNullException("object was null");
            if (Map == null)
                throw new ArgumentNullException("Map is null"); 
            if (!Map.IsWithinBounds(pos.X, pos.Y))
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
