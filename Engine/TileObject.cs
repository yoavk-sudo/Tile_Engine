using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Renderer;

namespace Tile_Engine
{
    public abstract class TileObject : ICloneable
    {
        public string Name { get; set; }
        public Position Position => CurrentTile.Position;
        public Actor Owner { get; }
        public Movement Movement { get; private set; }
        public Tile CurrentTile { get; set; }
        public IRenderable Texture { get; set; }

        private ISprite Sprite;


        public event Action OnMove;

        protected TileObject(Tile currentTile, List<MovePattern> movePatterns, Actor owner, IRenderable texture, ISprite sprite)
        {
            Owner = owner;
            CurrentTile = currentTile;
            Movement = new Movement(this, movePatterns);
            Texture = texture;
            Texture.Init(sprite);
        }

        public bool TryMove(Tile newTile)
        {
            //if (newTile == null || !Movement.GetPossibleMoves().Contains(newTile.Position)) return false;

            if (!CanMoveToTile(newTile)) 
                return false;
            CurrentTile.TileObject = null;
            OnMoveCallback(newTile);
            OnMove.Invoke();
            newTile.NewTileObject(this);
            CurrentTile = newTile;

            return true;
        }
        public  void OnMoveCallback(Tile newTile)
        {

        }
        public void Move()
        { }
        protected abstract bool CanMoveToTile(Tile newTile);

        public TileObject(string name, Actor owner) 
        {
            Name = name;
            Owner = owner;
        }

        public void InitTexture()
        {
            Texture.Init(Sprite);
        }

        public override string ToString()
        {
            return Name;
        }
        public abstract object Clone();
    }
}
