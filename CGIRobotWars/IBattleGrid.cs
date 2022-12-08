namespace CGIRobotWars
{
    public interface IBattleGrid
    {
        void InitializeBattleGrid(string battleGridSize);

        bool ValidateWarRobotPosition(CurrentWarRobotPosition warRobotPosition);
    }
}
