using ApiQF.Model;

namespace ApiQF.Interfaces
{
    public interface ISpaceLocation
    {
        Location GetLocation();

        void SetLocation(Location location);
    }
}