using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Engine
{
    public class TileObject : ICloneable
    {
        public string Name { get; set; }
        public Position Position => CurrentTile.Position;
        public Actor Owner { get; }
        public Movement Movement { get; private set; }
        public Tile CurrentTile { get; set; }

        public event Action? OnMove;
        protected TileObject(Tile currentTile, List<MovePattern> movePatterns, Actor owner)
        {
            Owner = owner;
            CurrentTile = currentTile;
            Movement = new Movement(this, movePatterns);
        }

        //public bool TryMove(Tile newTile)
        //{
        //    if (newTile == null || !Movement.GetPossibleMoves().Contains(newTile.Position)) return false;

        //    if (!CheckPossibleMoveTileCallback(newTile)) return false;

        //    OnMoveCallback(newTile);
        //    OnMove?.Invoke();
        //    newTile.PlaceTileObject(this);
        //    CurrentTile = newTile;

        //    return true;

        //}

        public TileObject(string name) 
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
        public virtual object Clone()
        {
            return new TileObject(Name);
        }
    }
}
