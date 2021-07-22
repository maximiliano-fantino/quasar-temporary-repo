using System;

namespace ApiQF.Model
{
    public class Satellite : AbstractSpaceObject
    {
        public String Name { get; set; }
        private bool IsActive { get; set; } = true;
        public double Distance { get; set; }
        public String[] Message { get; set; }

        public Satellite(Location l, String name)
        {
            Name = name;
            Location = l;
        }

        public bool Active()
        {
            return IsActive;
        }
    }
}