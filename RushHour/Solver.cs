using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHour
{
    class Solver
    {
        public void BFSolve(State entry)
        {
            Queue<State> stateQueue = new Queue<State>();
            //if(entry.Parent!=null)
            //    throw new Exception()
            stateQueue.Enqueue(entry);

            while (stateQueue.Count > 0)
            {
                State tested = stateQueue.Dequeue();
                if (tested.IsFinal())
                {
                    System.Console.WriteLine("WE found a solution! YES! it goes like this");
                    State.PrintRoute(tested);
                    return;
                }
                else
                {
                    foreach (var state in tested.GenerateChildStates())
                    {
                        stateQueue.Enqueue(state);
                    }
                }
            }

            System.Console.WriteLine("Dang...unsolvable");
        }

        public void DFSolve(State entry)
        {

        }
    }
}
