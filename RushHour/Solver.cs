using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHour
{
    class Solver
    {
        public void BFSolve(State entry)
        {
            Stopwatch sw = new Stopwatch();
            Queue<State> stateQueue = new Queue<State>();
            Dictionary<string, State> setOfStates = new Dictionary<string, State>();
            //if(entry.Parent!=null)
            //    throw new Exception()
            sw.Start();
            stateQueue.Enqueue(entry);

            while (stateQueue.Count > 0)
            {
                State tested = stateQueue.Dequeue();
                if (tested.IsFinal())
                {
                    sw.Stop();
                    System.Console.WriteLine("WE found a solution! YES! it goes like this");
                    int steps = State.PrintRoute(tested);
                    System.Console.WriteLine("actually in {0} steps",steps);
                    System.Console.WriteLine("Elapsed milis: {0}", sw.ElapsedMilliseconds);
                    return;
                }
                else
                {
                    foreach (var state in tested.GenerateChildStates())
                    {
                        if (!setOfStates.ContainsKey(state.Traffic.ToString()))
                        {
                            stateQueue.Enqueue(state);
                            setOfStates.Add(state.Traffic.ToString(), state);
                        }
                    }
                }
            }
            sw.Stop();
            System.Console.WriteLine("Dang...unsolvable");
            System.Console.WriteLine("Elapsed milis: {0}", sw.ElapsedMilliseconds);
        }

        public void BFSolve(State entry,string path)
        {
            Stopwatch sw = new Stopwatch();
            Queue<State> stateQueue = new Queue<State>();
            Dictionary<string, State> setOfStates = new Dictionary<string, State>();
            //if(entry.Parent!=null)
            //    throw new Exception()
            sw.Start();
            stateQueue.Enqueue(entry);

            while (stateQueue.Count > 0)
            {
                State tested = stateQueue.Dequeue();
                if (tested.IsFinal())
                {
                    sw.Stop();
                    System.Console.WriteLine("WE found a solution! YES! look to the otput file for more info");
                    System.Console.WriteLine("Elapsed milis: {0}", sw.ElapsedMilliseconds);
                    State.PrintReverseRouteToFile(tested,path);
                    return;
                }
                else
                {
                    foreach (var state in tested.GenerateChildStates())
                    {
                        if (!setOfStates.ContainsKey(state.Traffic.ToString()))
                        {
                            stateQueue.Enqueue(state);
                            setOfStates.Add(state.Traffic.ToString(), state);
                        }
                    }
                }
            }

            System.Console.WriteLine("Dang...unsolvable");
        }

        //add set
        public void DFSolve(State entry)
        {
            Stopwatch sw = new Stopwatch();
            Stack<State> stateStack = new Stack<State>();
            Dictionary<string,State> setOfStates = new Dictionary<string,State>();
            //if(entry.Parent!=null)
            //    throw new Exception()
            sw.Start();
            stateStack.Push(entry);

            while (stateStack.Count > 0)
            {
                State tested = stateStack.Pop();
                if (tested.IsFinal())
                {
                    sw.Stop();
                    System.Console.WriteLine("WE found a solution! YES! it goes like this");
                    int steps = State.PrintRoute(tested);
                    System.Console.WriteLine("Elapsed milis: {0}", sw.ElapsedMilliseconds);
                    System.Console.WriteLine("actually in {0} steps", steps);
                    return;
                }
                else
                {
                    foreach (var state in tested.GenerateChildStates())
                    {
                        if (!setOfStates.ContainsKey(state.Traffic.ToString()))
                        {
                            stateStack.Push(state);
                            setOfStates.Add(state.Traffic.ToString(), state);
                        }
                            
                    }
                }
            }
            sw.Stop();
            System.Console.WriteLine("Dang...unsolvable");
            System.Console.WriteLine("Elapsed milis: {0}", sw.ElapsedMilliseconds);
            System.Console.WriteLine("Dang...unsolvable");
        }

        public void DFSolve(State entry,string path)
        {
            Stack<State> stateStack = new Stack<State>();
            Dictionary<string, State> setOfStates = new Dictionary<string, State>();
            //if(entry.Parent!=null)
            //    throw new Exception()
            stateStack.Push(entry);

            while (stateStack.Count > 0)
            {
                State tested = stateStack.Pop();
                if (tested.IsFinal())
                {
                    System.Console.WriteLine("WE found a solution! YES! look to the otput file for more info");
                    State.PrintReverseRouteToFile(tested, path);
                    return;
                }
                else
                {
                    foreach (var state in tested.GenerateChildStates())
                    {
                        if (!setOfStates.ContainsKey(state.Traffic.ToString()))
                        {
                            stateStack.Push(state);
                            setOfStates.Add(state.Traffic.ToString(), state);
                        }

                    }
                }
            }

            System.Console.WriteLine("Dang...unsolvable");
        }
    }
}
