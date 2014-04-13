using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            int explored_states = 0;
            //if(entry.Parent!=null)
            //    throw new Exception()
            sw.Start();
            stateQueue.Enqueue(entry);

            while (stateQueue.Count > 0)
            {
                State tested = stateQueue.Dequeue();
                ++explored_states;
                if (tested.IsFinal())
                {
                    sw.Stop();
                    System.Console.WriteLine("WE found a solution! YES! it goes like this");
                    int steps = State.PrintRoute(tested);
                    System.Console.WriteLine("actually in {0} steps, explored {1} states", steps, explored_states);
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
            int explored_states = 0;
            //if(entry.Parent!=null)
            //    throw new Exception()
            sw.Start();
            stateStack.Push(entry);

            while (stateStack.Count > 0)
            {
                State tested = stateStack.Pop();
                ++explored_states;
                if (tested.IsFinal())
                {
                    sw.Stop();
                    System.Console.WriteLine("WE found a solution! YES! it goes like this");
                    int steps = State.PrintRoute(tested);
                    System.Console.WriteLine("Elapsed milis: {0}", sw.ElapsedMilliseconds);
                    System.Console.WriteLine("actually in {0} steps, explored {1} states", steps,explored_states);
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

        public State RandomWalk(State entry)
        {
            Random r = new Random();
            int willGo = 0;
            int rand = 0;
            int explored = 0;
            Direction direct;
            StreamWriter sw = new StreamWriter("./randomwalk.out");
            Stopwatch watch = new Stopwatch();
            Queue<State> queueOfStates = new Queue<State>();
            queueOfStates.Enqueue(entry);

            watch.Start();
            while (queueOfStates.Count > 0)
            {
                State s = queueOfStates.Dequeue();
                
                if (s.IsFinal())
                {
                    watch.Stop();
                    sw.WriteLine("Solution found in: {0} millis\n Explored: {1} states\n", watch.ElapsedMilliseconds, explored);
                    Console.WriteLine("Solution found in: {0} millis\n Explored: {1} states\n", watch.ElapsedMilliseconds, explored);
                    sw.Close();
                    return s;
                }
                
                rand = r.Next(s.Traffic.Cars.Count);
                Car car = s.Traffic.Cars.ElementAt(rand);
                

                //100%
                rand = r.Next(10);
                
                switch (car.Orientation)
                {
                    case Orientation.Vertical:
                        if(rand<5)
                            direct = Direction.Up;
                        else
                            direct = Direction.Down;
                        break;
                    case Orientation.Horizontal:
                        if(rand<5)
                            direct = Direction.Right;
                        else
                            direct = Direction.Left;
                        break;
                    default:
                        throw new Exception("Unsupported orientation");
                        break;
                }

                willGo = s.Traffic.CanMove(car,direct);

                if (willGo == 0)
                {
                    queueOfStates.Enqueue(s);
                    continue;
                }
                ++explored;
                int distance = r.Next(1,willGo+1);
                var t = s.Traffic.Move(car, direct, distance);
                t.Message = String.Format("Moved car: {0} => {1} to {2}", car.Color, distance, direct.ToString());

                var newState = new State();
                newState.Parent = s;
                newState.Traffic = t;

                sw.WriteLine(newState.Traffic.PrintGrid());
                queueOfStates.Enqueue(newState);
                //Console.ReadLine();
            }
            return null;
        }
    }
}
