using System.Collections.Generic;

namespace Tile_Engine
{
    public class Movement
    {
        private TileObject _owner;
        private List<MovePattern> _movePatterns;
        public MovePattern[] MovePatterns => _movePatterns.ToArray();

        public event Action<Position> TriggerMove;

        public Movement(TileObject owner, List<MovePattern> movePatterns)
        {
            _owner = owner;
            _movePatterns = movePatterns;
            TriggerMove += _owner.Move;
        }

        public bool TryMoveTileObject(TileObject tObject, Position targetPosition)
        {
            if(tObject == null 
                || !GetAllPossibleMoves().Item1.Contains(targetPosition)) 
                return false;
            TriggerMove.Invoke(targetPosition);
            return true;
        }

        private (IEnumerable<Position>, IEnumerable<Position>) GetAllPossibleMoves()
        {
            var viableMoves = new List<Position>();
            var nonViableMoves = new List<Position>();
            foreach (var movePattern in _movePatterns)
            {
                foreach (var movement in movePattern.Movements)
                {
                    Position newPosition = MovePositionByMovement(movement, _owner.Position);
                    if (TileMap.IsTileEmpty(newPosition))
                    {
                        if (_owner.CanMoveIntoEmpty())
                        {
                            viableMoves.Add(newPosition);
                        }
                        else nonViableMoves.Add(newPosition);
                    }
                    else if (TileMap.Map.IsWithinBounds(newPosition.X, newPosition.Y))
                    {
                        if(TileMap.Map[newPosition.X, newPosition.Y].TileObject.Owner == _owner.Owner)
                        {
                            if(_owner.CanMoveIntoAlly()) viableMoves.Add(newPosition);
                            else nonViableMoves.Add(newPosition);
                        }
                        else
                        {
                            if(_owner.CanMoveIntoEnemy()) viableMoves.Add(newPosition);
                            else nonViableMoves.Add(newPosition);
                        }
                    }
                    else
                    {
                        if(_owner.CanMoveOOB())
                        {
                            viableMoves.Add(newPosition);
                            continue;
                        }
                        nonViableMoves.Add(newPosition);
                    }
                }
            }
            return (viableMoves, nonViableMoves);
        }

        private static Position MovePositionByMovement(MovementDirections movementType, Position newPosition)
        {
            switch (movementType)
            {
                case MovementDirections.Left:
                    newPosition += new Position(-1, 0);
                    break;
                case MovementDirections.Up:
                    newPosition += new Position(1, 0);
                    break;
                case MovementDirections.Right:
                    newPosition += new Position(0, 1);
                    break;
                case MovementDirections.Down:
                    newPosition += new Position(0, -1);
                    break;
                case MovementDirections.UpLeft:
                    newPosition += new Position(-1, 1);
                    break;
                case MovementDirections.UpRight:
                    newPosition += new Position(1, 1);
                    break;
                case MovementDirections.DownLeft:
                    newPosition += new Position(-1, -1);
                    break;
                case MovementDirections.DownRight:
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
            MovementDirections m1 = MovementDirections.Up;
            MovementDirections m2 = MovementDirections.Up;
            MovementDirections[] ms = { m1, m2 };
            MovePattern mp = new(ms,true);
        }
    }

    public readonly struct MovePattern
    {
        public MovementDirections[] Movements { get; }
        public bool IsDirectional { get; }

        public MovePattern(MovementDirections[] movements, bool isDirectional = false)
        {
            Movements = movements;
            IsDirectional = isDirectional;
        }
    }

    public enum MovementDirections
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