﻿namespace Tile_Engine
{
    public class Tile : IPosition
    {
        public Position Position { get; }
        public TileObject TileObject { get; set; } = null;
        public bool IsEmpty { get {  return TileObject == null; } }
        //public Actor Actor { get; set; }

        public override string ToString()
        {
            //return TileObject.ToString() + ", " + Position.ToString();
            return Position.ToString();
        }

        public Tile(Position position)
        {
            Position = position;
        }

        public virtual void NewTileObject(TileObject tObject)
        {
            TileObject = tObject;
        }
    }
}
