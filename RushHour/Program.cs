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
                Console.Write("Car {0}: ", car.Color);
                switch (car.Orientation)
                {
                    case Orientation.Vertical:
                        Console.WriteLine("\t can move {0} to {1}", g.CanMove(car, Direction.Down), Direction.Down.ToString());
                        Console.WriteLine("\t can move {0} to {1}", g.CanMove(car, Direction.Up), Direction.Up.ToString());                                           
                        break;
                    case Orientation.Horizontal:
                        Console.WriteLine("\t can move {0} to {1}",g.CanMove(car, Direction.Right),Direction.Right.ToString());
                        Console.WriteLine("\t can move {0} to {1}", g.CanMove(car, Direction.Left), Direction.Left.ToString());
                        try
                        {
                            Console.WriteLine("\t can move {0} to {1}", g.CanMove(car, Direction.Up), Direction.Up.ToString());
                        }
                        catch (Exception ex)
                        {
                            
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    default:
                        break;
                }
                 
            }


            System.Console.WriteLine();
            
            g.Move(g.Cars.Find(c => c.Color == 'r'),Direction.Right,2);
            g.PrintGrid();
            System.Console.ReadLine();

           
        }
    }
}
