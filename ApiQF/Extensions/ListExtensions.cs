using ApiQF.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiQF.Extensions
{
    public static class ListExtensions
    {
        public static Double GetLocationX(this List<Satellite> s, int element)
        {
            return s.Skip(element).First().Location.PositionX;
        }

        public static Double GetLocationY(this List<Satellite> s, int element)
        {
            return s.Skip(element).First().Location.PositionY;
        }

        public static Double GetFirstTwoXDifference(this List<Satellite> s)
        {
            return s.GetLocationX(1) - s.GetLocationX(0);
        }

        public static Double GetFirstTwoYDifference(this List<Satellite> s)
        {
            return s.GetLocationY(1) - s.GetLocationY(0);
        }
    }
}