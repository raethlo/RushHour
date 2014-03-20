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
		        entry.Traffic.LoadGridFromFile("../../Resources/0.txt");
	        }
	        catch (Exception ex)
	        {
		        System.Console.WriteLine(ex.Message);
                return;
		        //throw;
	        }

            entry.Parent = null;

            Solver solver= new Solver();
            solver.BFSolve(entry);

            //foreach (var state in s.GenerateChildStates())
            //{
            //    if (state.Parent != s)
            //        isSameParent = false;
            //    Console.WriteLine("\nPARENT\n");
            //    state.Parent.Traffic.PrintGrid();
            //    Console.WriteLine("\nchild\n");
            //    state.Traffic.PrintGrid();
            //}

            //if (isSameParent)
            //    Console.WriteLine("\n the paret works");

            System.Console.ReadLine();
        }

        
    }
}
