using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RushHour
{
    

    public enum Orientation
    {
        Vertical,Horizontal
    }

    public class Car
    {
        public const char red = 'r';

        public Orientation Orientation { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Length { get; set; }

        public char Color { get; set; }

        public bool IsRed()
        {
            return this.Color.Equals(Car.red);
        }

    }
}
