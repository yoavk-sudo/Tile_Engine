using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Engine
{
    public struct Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }

        public override bool Equals([NotNullWhen(true)] object obj)
        {
            if(obj == null) throw new ArgumentNullException(nameof(obj));
            if (obj is not Position pos) throw new ArgumentException("object must be struct Position");
            return X == pos.X && Y == pos.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static Position operator +(Position posOne, Position posTwo) => new Position(posOne.X + posTwo.X, posOne.Y + posTwo.Y);
        public static Position operator -(Position posOne, Position posTwo) => new Position(posOne.X - posTwo.X, posOne.Y - posTwo.Y);
    }
}
