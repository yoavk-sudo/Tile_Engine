using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        private static Position MovePositionByMovement(MovementType movementType, Position newPosition)
        {
            newPosition += movementType switch
            {
                MovementType.Left => new Position(-1, 0),
                MovementType.Right => new Position(1, 0),
                MovementType.Forward => new Position(0, -1),
                MovementType.Back => new Position(0, 1),
                MovementType.ForwardRight => new Position(1, -1),
                MovementType.ForwardLeft => new Position(-1, -1),
                MovementType.BackRight => new Position(1, 1),
                MovementType.BackLeft => new Position(-1, 1)
            };;

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
        public MovementType[] Movements { get; }
        public bool IsDirection { get; }

        public MovePattern(MovementType[] movements, bool isDirection)
        {
            Movements = movements;
            IsDirection = isDirection;
        }
    }

    public enum MovementType
    {
        Left,
        Right,
        Forward,
        Back,
        ForwardRight,
        ForwardLeft,
        BackRight,
        BackLeft
    }
}