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
            //bool isSameParent = true;
            State entry = new State();
            entry.Traffic = new TrafficGrid();

            try 
	        {	        
		        entry.Traffic.LoadGridFromFile("../../Resources/1.txt");
	        }
	        catch (Exception ex)
	        {
		        System.Console.WriteLine(ex.Message);
                return;
		        //throw;
	        }

            entry.Parent = null;
            Console.WriteLine("STARTING FROM");
            entry.Traffic.PrintGrid();
            Console.WriteLine("_______________________________\n");
            Solver solver = new Solver();
            solver.BFSolve(entry);
            solver.BFSolve(entry, "./output.txt");

            //if (isSameParent)
            //    Console.WriteLine("\n the paret works");
           // System.Console.WriteLine( "Traffiiiiic \n" + entry.Traffic.ToString());
           

            System.Console.ReadLine();

            
        }

        

        
    }
}
