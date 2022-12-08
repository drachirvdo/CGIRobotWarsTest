namespace CGIRobotWars
{
    public class CurrentWarRobotPosition
    {
        public int X { get; }
        public int Y { get; }

        public CurrentWarRobotPosition(int xCoordinate, int yCoordinate)
        {
            X = xCoordinate;
            Y = yCoordinate;
        }
    }
}
