using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Renderer;

namespace Tile_Engine
{
    public class TileObject : ICloneable, IDestroyable
    {
        private ISprite _sprite;
        private Dictionary<string, bool> _specialRules = new Dictionary<string, bool>();

        public string Name { get; set; }
        public Position Position => CurrentTile.Position;
        public Actor Owner { get; }
        public Movement TileObjectMovement { get; private set; }
        public Tile CurrentTile { get; set; }
        public IRenderable Texture { get; set; }

        public event Action OnMove;

        public TileObject(Tile currentTile, List<MovePattern> movePatterns, Actor owner, ISprite sprite, 
            bool canMoveIntoAlly, bool canMoveIntoEnemy, bool canMoveIntoEmpty = true, bool canMoveOutOfBounds = false)
        {
            Owner = owner;
            CurrentTile = currentTile;
            TileObjectMovement = new Movement(this, movePatterns);
            _sprite = sprite;
            InitializeSpecialRules(canMoveIntoEmpty, canMoveIntoAlly, canMoveIntoEnemy, canMoveOutOfBounds);
        }

        private void InitializeSpecialRules(bool canMoveIntoEmpty, bool canMoveIntoAlly, bool canMoveIntoEnemy, bool canMoveOutOfBounds)
        {
            _specialRules.Add("CanMoveIntoEmpty", canMoveIntoEmpty);
            _specialRules.Add("CanMoveIntoAlly", canMoveIntoAlly);
            _specialRules.Add("CanMoveIntoEnemy", canMoveIntoEnemy);
            _specialRules.Add("CanMoveOutOfBounds", canMoveOutOfBounds);
        }

        public bool CanMoveIntoEmpty()
        {
            bool able = false;
            _specialRules.TryGetValue("canMoveIntoEmpty", out able);
            return able;
        }
        public bool CanMoveIntoAlly()
        {
            bool able = false;
            _specialRules.TryGetValue("CanMoveIntoAlly", out able);
            return able;
        }
        public bool CanMoveIntoEnemy()
        {
            bool able = false;
            _specialRules.TryGetValue("CanMoveIntoEnemy", out able);
            return able;
        }
        public bool CanMoveOOB()
        {
            bool able = false;
            _specialRules.TryGetValue("CanMoveOutOfBounds", out able);
            return able;
        }

        public  void OnMoveCallback(Tile newTile)
        {

        }
        public void Move(Position newPosition)
        {
            CurrentTile = TileMap.Map[newPosition.X, newPosition.Y];
            OnMove.Invoke(); // update renderer, let client code decide what to do on new tile and its object
        }
        //protected abstract bool CanMoveToTile(Tile newTile);

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
        public object Clone()
        {
            return new TileObject(Name, Owner);
        }

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
