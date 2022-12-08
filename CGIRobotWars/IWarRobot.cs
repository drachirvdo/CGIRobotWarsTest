namespace CGIRobotWars
{
    public interface IWarRobot
    {
        CurrentWarRobotPosition WarRobotPosition { get; set; }

        CurrentWarRobotOrientation WarRobotOrientation { get; set; }
    }
}
