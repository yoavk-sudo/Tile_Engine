using System.Collections.Generic;

namespace Tile_Engine
{
    public class Movement
    {
        private TileObject _owner;
        private List<MovePattern> _movePatterns;
        public MovePattern[] MovePatterns => _movePatterns.ToArray();

        public event Action TriggerMove;

        public Movement(TileObject owner, List<MovePattern> movePatterns)
        {
            _owner = owner;
            _movePatterns = movePatterns;
            TriggerMove += _owner.Move;
        }

        public bool TryMoveTileObject(TileObject tObject, Position targetPosition)
        {
            if(tObject == null 
                || !GetAllPossibleMoves().Contains(targetPosition)) 
                return false;
            TriggerMove.Invoke();
            return true;
        }

        private IEnumerable<Position> GetAllPossibleMoves()
        {
            var possibleMoves = new List<Position>();
            foreach (var movePattern in _movePatterns)
            {
                foreach (var movement in movePattern.Movements)
                {
                    Position newPosition = MovePositionByMovement(movement, _owner.Position);
                    if (TileMap.IsTileEmpty(newPosition))
                    {
                        // show as empty, let client decide what to do with this
                    }
                    else if (TileMap.Map.IsWithinBounds(newPosition.X, newPosition.Y))
                    {
                        // show as OOB, let client decide what to do with this
                    }
                    else
                    {
                        // show as occupied, let client decide what to do with this
                    }
                }
            }
            return null;
        }

        private static Position MovePositionByMovement(MovementType movementType, Position newPosition)
        {
            switch (movementType)
            {
                case MovementType.Left:
                    newPosition += new Position(-1, 0);
                    break;
                case MovementType.Up:
                    newPosition += new Position(1, 0);
                    break;
                case MovementType.Right:
                    newPosition += new Position(0, 1);
                    break;
                case MovementType.Down:
                    newPosition += new Position(0, -1);
                    break;
                case MovementType.UpLeft:
                    newPosition += new Position(-1, 1);
                    break;
                case MovementType.UpRight:
                    newPosition += new Position(1, 1);
                    break;
                case MovementType.DownLeft:
                    newPosition += new Position(-1, -1);
                    break;
                case MovementType.DownRight:
                    newPosition += new Position(1, -1);
                    break;
                default:
                    break;
            }
            return newPosition;
        }

        public void AddMovePattern(MovePattern movePattern)
        {
            _movePatterns.Add(movePattern);
        }

        public void RemoveMovePattern(MovePattern movePattern)
        {
            _movePatterns.Remove(movePattern);
            MovementType m1 = MovementType.Up;
            MovementType m2 = MovementType.Up;
            MovementType[] ms = { m1, m2 };
            MovePattern mp = new(ms,true);
        }
    }

    public readonly struct MovePattern
    {
        public MovementType[] Movements { get; }
        public bool IsDirectional { get; }

        public MovePattern(MovementType[] movements, bool isDirectional = false)
        {
            Movements = movements;
            IsDirectional = isDirectional;
        }
    }

    public enum MovementType
    {
        Left,
        Up,
        Right,
        Down,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight
    }
}