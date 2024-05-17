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

        public Movement(TileObject owner, List<MovePattern> movePatterns)
        {
            _owner = owner;
            _movePatterns = movePatterns;
        }

        /// <summary>
        /// Gets the possible moves for the TileObject.
        /// </summary>
        /// <returns>
        /// An enumerable collection of Position2D representing the possible moves.
        /// </returns>
        public IEnumerable<Position> GetPossibleMoves()
        {
            List<Position> possibleMoves = new List<Position>();
            foreach (var movePattern in _movePatterns)
            {
                if (movePattern.IsDirection)
                    possibleMoves.AddRange(GetMovesByDirection(movePattern, _owner.Position));
                else
                    possibleMoves.Add(GetMovesByMovePattern(movePattern, _owner.Position));
            }

            return possibleMoves.ToArray();
        }

        /// <summary>
        /// Gets the available moves by a specified direction and position.
        /// </summary>
        /// <param name="movePattern">The move pattern representing the direction.</param>
        /// <param name="position">The starting position.</param>
        /// <returns>A list of available positions based on the specified direction and starting position.</returns>
        public static List<Position> GetMovesByDirection(MovePattern movePattern, Position position)
        {
            var newPosition = position;
            List<Position> availablePositions = new List<Position>();
            foreach (var movement in movePattern.Movements)
            {
                do
                {
                    newPosition = MovePositionByMovement(movement, newPosition);
                    availablePositions.Add(newPosition);
                    if (!TileMap.Map.IsWithinBounds(newPosition.X, newPosition.Y)) return availablePositions;
                } while (TileMap.IsTileEmpty(newPosition));
            }
            return availablePositions;
        }

        /// <summary>
        /// Gets the moves for the TileObject based on the given MovePattern and position.
        /// </summary>
        /// <param name="movePattern">The MovePattern to be used.</param>
        /// <param name="position">The position of the TileObject.</param>
        /// <returns>
        /// A Position2D representing the moves based on the MovePattern and position.
        /// </returns>
        public static Position GetMovesByMovePattern(MovePattern movePattern, Position position)
        {
            return movePattern.Movements.Aggregate(position,
                (current, movement) => MovePositionByMovement(movement, current));
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
                MovementType.BackLeft => new Position(-1, 1),
                _ => throw new ArgumentOutOfRangeException(nameof(movementType), movementType, null)
            };

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