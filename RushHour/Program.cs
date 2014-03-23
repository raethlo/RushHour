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

            Console.WriteLine("Which board should i load? (0,1,2,3,4,5)");
            var response = Console.ReadLine();
            int board = -1;
            try
            {
                board = int.Parse(response);
                if (board > 5 || board < 0)
                    throw new Exception();
            }
            catch (Exception)
            {

                Console.WriteLine("invalid board number");
                return;
            }

            //nacitanie boardu
            try 
	        {	     
                
		        entry.Traffic.LoadGridFromFile(String.Format("../../Resources/{0}.txt",board));
	        }
	        catch (Exception ex)
	        {
		        System.Console.WriteLine(ex.Message);
                return;
	        }

            entry.Parent = null;
            Console.WriteLine("Breadth first ('B') or depth first ('D')?");
            Solver solver = new Solver();
            switch (Console.ReadLine().ToUpper())
	        {
                case "D":
                    Console.WriteLine("STARTING FROM");
                    Console.WriteLine(entry.Traffic.PrintGrid());
                    Console.WriteLine("_______________________________\n");                   
                    solver.DFSolve(entry);
                    //solver.DFSolve(entry, "./output.txt");
                    break;
                case "B":
                    Console.WriteLine("STARTING FROM");
                    Console.WriteLine(entry.Traffic.PrintGrid());
                    Console.WriteLine("_______________________________\n");
                    solver.BFSolve(entry);
                    //solver.BFSolve(entry, "./output.txt");
                    break;
		        default:
                    break;
	        }

            System.Console.ReadLine();

            
        }      
    }
}
