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
            TrafficGrid g = new TrafficGrid();
            g.LoadGridFromFile("../../Resources/0.txt");
            
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
            System.Console.WriteLine("BEFORE \n");
            g.PrintGrid();

            TrafficGrid g2 = new TrafficGrid(g);
         
            g2.Move(g2.Cars.Find(c => c.Color == 'r'), Direction.Right, 2); 
            
            System.Console.WriteLine("\n AFTER parent grid\n");
            g.PrintGrid();

            System.Console.WriteLine("\n AFTER child grid\n");
            g2.PrintGrid();

            //System.Console.WriteLine();

            //g.Move(g.Cars.Find(c => c.Color == 'b'), Direction.Down, 2);
            //g.PrintGrid();

            //State s = new State();
            //s.Traffic = g;
            //s.Parent = null;

            //if (s.IsFinal())
            //    Console.WriteLine("this state is final");
            //else
            //    Console.WriteLine("not final");

            System.Console.ReadLine();

           
        }
    }
}
