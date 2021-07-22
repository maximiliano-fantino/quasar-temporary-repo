namespace ApiQF.Model
{
    public class Location
    {
        public double PositionX { get; set; }

        public double PositionY { get; set; }

        public Location(double x, double y)
        {
            PositionX = x;
            PositionY = y;
        }
    }
}