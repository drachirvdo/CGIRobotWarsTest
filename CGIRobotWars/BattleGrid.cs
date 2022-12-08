using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGIRobotWars
{
    public class BattleGrid
    {
        public int GridWidth { get; private set; }
        public int GridHeight { get; private set; }

        public void InitializeBattleGrid(string battleGridSize)
        {
            var inputs = battleGridSize.Split(' ');
            _ = int.TryParse(inputs[0], out var gridWidth);
            _ = int.TryParse(inputs[1], out var gridHeight);

            GridWidth = gridWidth;
            GridHeight = gridHeight;
        }

        public bool ValidateWarRobotPositionOnTheBattleGrid(CurrentWarRobotPosition warRobotPosition)
        {
            var xCoordinate = warRobotPosition.X >= 0 && warRobotPosition.X <= GridWidth;
            var yCoordinate = warRobotPosition.Y >= 0 && warRobotPosition.Y <= GridHeight;
            return xCoordinate && yCoordinate;
        }
    }
}
