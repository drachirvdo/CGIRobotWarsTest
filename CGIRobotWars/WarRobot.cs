using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CGIRobotWars
{
    public class WarRobot : IWarRobot
    {
        public CurrentWarRobotPosition WarRobotPosition { get; set; }
        public CurrentWarRobotOrientation WarRobotOrientation { get; set; }

        private readonly IDictionary<char, CurrentWarRobotOrientation> _orientationDictionary;
        private readonly IDictionary<char, WarRobotMovement> _movementDictionary;

        public WarRobot()
        {
            _orientationDictionary = new Dictionary<char, CurrentWarRobotOrientation>
            {
                {'N', CurrentWarRobotOrientation.North},
                {'E', CurrentWarRobotOrientation.East},
                {'S', CurrentWarRobotOrientation.South},
                {'W', CurrentWarRobotOrientation.West}
            };

            _movementDictionary = new Dictionary<char, WarRobotMovement>
            {
                {'L', WarRobotMovement.Left},
                {'R', WarRobotMovement.Right},
                {'M', WarRobotMovement.Movement}
            };
        }

        public void Launch(IBattleGrid battleGrid, string currentWarRobotPosition)
        {
            var warRobotPosition = currentWarRobotPosition.Split(' ');

            int.TryParse(warRobotPosition[0], out int xCoordinate);
            int.TryParse(warRobotPosition[1], out int yCoordinate);
            WarRobotPosition = new CurrentWarRobotPosition(xCoordinate, yCoordinate);

            if (!battleGrid.ValidateWarRobotPosition(WarRobotPosition))
            {
                throw new InvalidDataException(
                    $"War Robot position {xCoordinate}:{yCoordinate} is out of bounds of the Battle Grid");
            }

            var warRobotOrientation = warRobotPosition[2][0];
            WarRobotOrientation = _orientationDictionary[warRobotOrientation];
        }

        public void Move(string movesCommands)
        {
            var input = movesCommands.ToCharArray();
            var commands = input.Select(x => _movementDictionary[x]).ToList();

            foreach (var movement in commands)
            {
                switch (movement)
                {
                    case WarRobotMovement.Left:
                        MoveWarRobotLeft(WarRobotOrientation);
                        break;
                    case WarRobotMovement.Right:
                        MoveWarRobotRight(WarRobotOrientation);
                        break;
                    case WarRobotMovement.Movement:
                        MoveWarRobotForward(WarRobotOrientation);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        #region Utilities

        private CurrentWarRobotOrientation MoveWarRobotLeft(CurrentWarRobotOrientation orientation) =>
            orientation switch
            {
                CurrentWarRobotOrientation.North => WarRobotOrientation = CurrentWarRobotOrientation.West,
                CurrentWarRobotOrientation.East => WarRobotOrientation = CurrentWarRobotOrientation.North,
                CurrentWarRobotOrientation.South => WarRobotOrientation = CurrentWarRobotOrientation.East,
                CurrentWarRobotOrientation.West => WarRobotOrientation = CurrentWarRobotOrientation.South,
                _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null)
            };

        private CurrentWarRobotOrientation MoveWarRobotRight(CurrentWarRobotOrientation orientation) =>
            orientation switch
            {
                CurrentWarRobotOrientation.North => WarRobotOrientation = CurrentWarRobotOrientation.East,
                CurrentWarRobotOrientation.East => WarRobotOrientation = CurrentWarRobotOrientation.South,
                CurrentWarRobotOrientation.South => WarRobotOrientation = CurrentWarRobotOrientation.West,
                CurrentWarRobotOrientation.West => WarRobotOrientation = CurrentWarRobotOrientation.North,
                _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null)
            };

        private CurrentWarRobotPosition MoveWarRobotForward(CurrentWarRobotOrientation direction) =>
            direction switch
            {
                CurrentWarRobotOrientation.North => WarRobotPosition = new CurrentWarRobotPosition(WarRobotPosition.X, WarRobotPosition.Y + 1),
                CurrentWarRobotOrientation.East => WarRobotPosition = new CurrentWarRobotPosition(WarRobotPosition.X + 1, WarRobotPosition.Y),
                CurrentWarRobotOrientation.South => WarRobotPosition = new CurrentWarRobotPosition(WarRobotPosition.X, WarRobotPosition.Y - 1),
                CurrentWarRobotOrientation.West => WarRobotPosition = new CurrentWarRobotPosition(WarRobotPosition.X - 1, WarRobotPosition.Y),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };

        #endregion
    }
}