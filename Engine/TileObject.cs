using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Renderer;

namespace Tile_Engine
{
    public abstract class TileObject : ICloneable, IDestroyable
    {
        private ISprite _sprite;
        public string Name { get; set; }
        public Position Position => CurrentTile.Position;
        public Actor Owner { get; }
        public Movement TileObjectMovement { get; private set; }
        public Tile CurrentTile { get; set; }
        public IRenderable Texture { get; set; }

        public event Action OnMove;

        protected TileObject(Tile currentTile, List<MovePattern> movePatterns, Actor owner, ISprite sprite)
        {
            Owner = owner;
            CurrentTile = currentTile;
            TileObjectMovement = new Movement(this, movePatterns);
            _sprite = sprite;
        }

        public bool TryMove(Tile newTile)
        {
            if (newTile == null || !TileObjectMovement.GetPossibleMoves().Contains(newTile.Position)) 
                return false;

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
        {
            
        }
        protected abstract bool CanMoveToTile(Tile newTile);

        public TileObject(string name, Actor owner) 
        {
            Name = name;
            Owner = owner;
        }

        public void InitTexture(IRenderable Renderer)
        {
            Texture = Renderer;
            Texture.Init(_sprite);
        }

        public override string ToString()
        {
            return Name;
        }
        public abstract object Clone();

        public void Destroy()
        {
            _sprite = null;
            Name = null;
            TileObjectMovement = null;
            CurrentTile.TileObject = null;
            Texture = null;
        }

        public void DestroyChildren()
        {
            Owner.Destroy();
        }
    }
}
