using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHour
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid g = new Grid();
            g.LoadGridFromFile("../../Resources/0.txt");

            TimeSpan t = new TimeSpan(0, 0, 0, 0, int.MaxValue);
            Console.WriteLine(t.TotalDays);

            foreach(var car in g.Cars)
            {
                System.Console.WriteLine("{0} {1} {2}",car.X,car.Y,car.Color);
            }

            System.Console.WriteLine();

            for (int i = 0; i < g.Dimension; i++)
            {
                for (int j = 0; j < g.Dimension; j++)
                {
                    Console.Write(g.Occupancy[j,i]); 
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            foreach (var car in g.Cars)
            {
                g.CanMove(car, Direction.Right);   
            }


            System.Console.ReadLine();

           
        }
    }
}
