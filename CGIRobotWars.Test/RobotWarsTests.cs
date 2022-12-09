using System.Collections.Generic;
using CGIRobotWars;
using Moq;
using NUnit.Framework;

namespace CGIRobotWars.Test
{
    public class RobotWarsTests
    {
        private Mock<IBattleGrid>? _mockBattleGrid;
        private IDictionary<char, CurrentWarRobotOrientation>? _orientationDictionary;

        [SetUp]
        public void Setup()
        {
            _mockBattleGrid = new Mock<IBattleGrid>();
        }

        [TestCase("5 5", "1 2 N", "LMLMLMLMM", 1, 3, CurrentWarRobotOrientation.North)]
        [TestCase("5 5", "3 3 E", "MMRMMRMRRM", 5, 1, CurrentWarRobotOrientation.East)]
        public void AreWarRobotsFinalCoordinatesAsExpected(string battleGridSize, string warRobotCoordinate,
            string movesCommands, int expectedXResult, int expectedYResult, CurrentWarRobotOrientation orientationResult)
        {
            // arrange
            _mockBattleGrid.Setup(x => x.ValidateWarRobotPosition(It.IsAny<CurrentWarRobotPosition>())).Returns(true);
            var warRobot = new WarRobot();

            // act
            warRobot.Launch(_mockBattleGrid.Object, warRobotCoordinate);
            warRobot.Move(movesCommands);

            // assert
            Assert.NotNull(warRobot);
            Assert.NotNull(warRobot.WarRobotPosition);
            Assert.AreEqual(expectedXResult, warRobot.WarRobotPosition.X);
            Assert.AreEqual(expectedYResult, warRobot.WarRobotPosition.Y);
            Assert.AreEqual(orientationResult, warRobot.WarRobotOrientation);
        }

        [TestCase("5 5")]
        public void DoesBattleGridInitializeAsExpected(string battleGridSize)
        {
            // arrange
            _mockBattleGrid.Setup(x => x.ValidateWarRobotPosition(It.IsAny<CurrentWarRobotPosition>())).Returns(true);
            var battleGrid = new BattleGrid();

            // act
            battleGrid.InitializeBattleGrid(battleGridSize);

            var inputs = battleGridSize.Split(' ');
            int.TryParse(inputs[0], out var gridWidth);
            int.TryParse(inputs[1], out var gridHeight);

            // assert
            Assert.AreEqual(battleGrid.GridWidth, gridWidth);
            Assert.AreEqual(battleGrid.GridHeight, gridHeight);
        }

        [TestCase("1 2 N")]
        [TestCase("5 3 W")]
        [TestCase("3 1 S")]
        [TestCase("2 2 E")]
        public void DoesWarRobotLaunchAsExpected(string warRobotCoordinate)
        {
            // arrange
            _mockBattleGrid.Setup(x => x.ValidateWarRobotPosition(It.IsAny<CurrentWarRobotPosition>())).Returns(true);
            var battleGrid = new BattleGrid();
            var warRobot = new WarRobot();

            // act
            var battleGridSize = "5 5";
            battleGrid.InitializeBattleGrid(battleGridSize);

            warRobot.Launch(_mockBattleGrid.Object, warRobotCoordinate);

            var warRobotPosition = warRobotCoordinate.Split(' ');
            int.TryParse(warRobotPosition[0], out int xPosition);
            int.TryParse(warRobotPosition[1], out int yPosition);
            var roverDirection = warRobotPosition[2][0];

            _orientationDictionary = new Dictionary<char, CurrentWarRobotOrientation>
            {
                {'N', CurrentWarRobotOrientation.North},
                {'E', CurrentWarRobotOrientation.East},
                {'S', CurrentWarRobotOrientation.South},
                {'W', CurrentWarRobotOrientation.West}
            };

            // assert
            Assert.AreEqual(warRobot.WarRobotPosition.X, xPosition);
            Assert.AreEqual(warRobot.WarRobotPosition.Y, yPosition);
            Assert.AreEqual(warRobot.WarRobotOrientation, _orientationDictionary[roverDirection]);
        }
    }
}
