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
            if (tObject == null
                || tObject.IsLocked
                || !GetAllPossibleMoves().Item1.Contains(targetPosition)
                || !FullfilsConditionIfPossible(targetPosition))
                return false;
            TriggerMove.Invoke(targetPosition);
            return true;
        }

        private bool FullfilsConditionIfPossible(Position targetPosition)
        {
            foreach (var movePattern in _movePatterns)
            {
                if (movePattern.Conditions.HasValue)
                {
                    var conditions = movePattern.Conditions.Value;
                    if (!conditions.FullfilsCondition(_owner, targetPosition))
                        return false;
                    return true;
                };
            }
            return true;
        }

        public (IEnumerable<Position>, IEnumerable<Position>) GetAllPossibleMoves()
        {
            var viableMoves = new List<Position>();
            var nonViableMoves = new List<Position>();
            foreach (var movePattern in _movePatterns)
            {
                foreach (var movement in movePattern.Movements)
                {
                    Position newPosition = MovePositionByMovement(movement, _owner.Position);
                    for (int i = 0; i < movePattern.RepeatedMoves; i++)
                    {
                        if (TileMap.Map.IsWithinBounds(newPosition.X, newPosition.Y) && TileMap.IsTileEmpty(newPosition))
                        {
                            if (_owner.CanMoveIntoEmpty())
                            {
                                viableMoves.Add(newPosition);
                            }
                            else
                            {
                                nonViableMoves.Add(newPosition);
                                break;
                            }
                        }
                        else if (TileMap.Map.IsWithinBounds(newPosition.X, newPosition.Y))
                        {
                            if (TileMap.Map[newPosition.X, newPosition.Y].TileObject.Owner == _owner.Owner)
                            {
                                if (_owner.CanMoveIntoAlly()) viableMoves.Add(newPosition);
                                else nonViableMoves.Add(newPosition);
                            }
                            else
                            {
                                if (_owner.CanMoveIntoEnemy())
                                {
                                    Console.WriteLine("Eaten");
                                    TileMap.Map[newPosition.X, newPosition.Y].TileObject.Destroy();
                                    viableMoves.Add(newPosition);
                                }
                                else
                                {
                                    nonViableMoves.Add(newPosition);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (_owner.CanMoveOOB())
                            {
                                viableMoves.Add(newPosition);
                                continue;
                            }
                            nonViableMoves.Add(newPosition);
                            break;
                        }
                        newPosition = MovePositionByMovement(movement, _owner.Position);
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
                    newPosition += new Position(0, -1);
                    break;
                case MovementDirections.Right:
                    newPosition += new Position(1, 0);
                    break;
                case MovementDirections.Down:
                    newPosition += new Position(0, 1);
                    break;
                case MovementDirections.UpLeft:
                    newPosition += new Position(-1, -1);
                    break;
                case MovementDirections.UpRight:
                    newPosition += new Position(1, -1);
                    break;
                case MovementDirections.DownLeft:
                    newPosition += new Position(-1, 1);
                    break;
                case MovementDirections.DownRight:
                    newPosition += new Position(1, 1);
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
        }
    }

    public readonly struct MovePattern
    {
        public MovementDirections[] Movements { get; }
        public int RepeatedMoves { get; }
        public MoveConditions? Conditions { get; }
        public MovePattern(MovementDirections[] movements, MoveConditions? conditions = null, int repeatedMoves = 1)
        {
            Movements = movements;
            Conditions = conditions;
            RepeatedMoves = repeatedMoves;
        }
    }

    public readonly struct MoveConditions
    {
        private readonly Position _targetPosition;
        private readonly bool _canMoveIfTargetIsEmpty;
        private readonly bool _canMoveIfTargetIsAlly;
        private readonly bool _canMoveIfTargetIsEnemy;

        public MoveConditions(Position targetPosition, bool canMoveIfTargetIsEmpty, bool canMoveIfTargetIsAlly, bool canMoveIfTargetIsEnemy)
        {
            _targetPosition = targetPosition;
            _canMoveIfTargetIsEmpty = canMoveIfTargetIsEmpty;
            _canMoveIfTargetIsAlly = canMoveIfTargetIsAlly;
            _canMoveIfTargetIsEnemy = canMoveIfTargetIsEnemy;
        }

        public bool FullfilsCondition(TileObject to, Position attempttedPosition)
        {
            if(!attempttedPosition.Equals(to.Position + _targetPosition))
            {
                return false;
            }
            else if (TileMap.Map[attempttedPosition.X, attempttedPosition.Y].IsEmpty)
            {
                return _canMoveIfTargetIsEmpty;
            }
            else if (TileMap.Map[attempttedPosition.X, attempttedPosition.Y].TileObject.Owner == to.Owner)
            {
                return _canMoveIfTargetIsAlly;
            }
            else if (TileMap.Map[attempttedPosition.X, attempttedPosition.Y].TileObject.Owner != to.Owner)
            {
                return _canMoveIfTargetIsEnemy;
            }
            return false;
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