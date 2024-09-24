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
        private const string CAN_MOVE_INTO_EMPTY = "CanMoveIntoEmpty";
        private const string CAN_MOVE_INTO_ALLY = "CanMoveIntoAlly";
        private const string CAN_MOVE_INTO_ENEMY = "CanMoveIntoEnemy";
        private const string CAN_MOVE_OUT_OF_BOUNDS = "CanMoveOutOfBounds";
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
        public TileObject(Tile currentTile, List<MovePattern> movePatterns, Actor owner, 
            bool canMoveIntoAlly, bool canMoveIntoEnemy, bool canMoveIntoEmpty = true, bool canMoveOutOfBounds = false)
        {
            Owner = owner;
            CurrentTile = currentTile;
            TileObjectMovement = new Movement(this, movePatterns);
            InitializeSpecialRules(canMoveIntoEmpty, canMoveIntoAlly, canMoveIntoEnemy, canMoveOutOfBounds);
        }

        private void InitializeSpecialRules(bool canMoveIntoEmpty, bool canMoveIntoAlly, bool canMoveIntoEnemy, bool canMoveOutOfBounds)
        {
            _specialRules.Add(CAN_MOVE_INTO_EMPTY, canMoveIntoEmpty);
            _specialRules.Add(CAN_MOVE_INTO_ALLY, canMoveIntoAlly);
            _specialRules.Add(CAN_MOVE_INTO_ENEMY, canMoveIntoEnemy);
            _specialRules.Add(CAN_MOVE_OUT_OF_BOUNDS, canMoveOutOfBounds);
        }

        public bool CanMoveIntoEmpty()
        {
            bool able = false;
            _specialRules.TryGetValue(CAN_MOVE_INTO_EMPTY, out able);
            return able;
        }
        public bool CanMoveIntoAlly()
        {
            bool able = false;
            _specialRules.TryGetValue(CAN_MOVE_INTO_ALLY, out able);
            return able;
        }
        public bool CanMoveIntoEnemy()
        {
            bool able = false;
            _specialRules.TryGetValue(CAN_MOVE_INTO_ENEMY, out able);
            return able;
        }
        public bool CanMoveOOB()
        {
            bool able = false;
            _specialRules.TryGetValue(CAN_MOVE_OUT_OF_BOUNDS, out able);
            return able;
        }

        public void OnMoveCallback(Tile newTile)
        {

        }
        public void Move(Position newPosition)
        {
            if(!TileMap.Map.IsWithinBounds(newPosition.X, newPosition.Y)) 
                return;
            CurrentTile = TileMap.Map[newPosition.X, newPosition.Y];
            OnMove?.Invoke(); // update renderer, let client code decide what to do on new tile and its object
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
