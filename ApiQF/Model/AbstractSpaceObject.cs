using ApiQF.Interfaces;

namespace ApiQF.Model
{
    public abstract class AbstractSpaceObject : ISpaceLocation
    {
        public Location Location { get; set; }

        public Location GetLocation()
        {
            return Location;
        }

        public void SetLocation(Location location)
        {
            Location = location;
        }

        public bool IsValidLocation()
        {
            return Location != null;
        }
    }
}