using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RushHour
{
    public enum Direction
    {
        Up, Down, Left, Right
    }

    public class TrafficGrid
    {
        //fields
        public List<Car> Cars { get; set; }
        //public Car Red { get; private set; }
        public bool[,] Occupancy { get; set; }
        public int Dimension { get; set; }

        public TrafficGrid()
        {
            this.Cars = new List<Car>();
        }

        public TrafficGrid(List<Car> cars, int dim)
        {
            this.Cars = new List<Car>();
            foreach (var car in cars)
            {
                var c = new Car();
                c.Orientation = car.Orientation;
                c.Length = car.Length;
                c.X = car.X;
                c.Y = car.Y;
                c.Color = car.Color;
                this.Cars.Add(c);
            }

            this.Dimension = dim;
            this.Occupancy = new bool[dim, dim];
            this.genOccupancy();
        }

        public TrafficGrid(TrafficGrid g)
        {
            this.Cars = new List<Car>();
            foreach (var car in g.Cars)
            {
                var c = new Car();
                c.Orientation = car.Orientation;
                c.Length = car.Length;
                c.X = car.X;
                c.Y = car.Y;
                c.Color = car.Color;
                this.Cars.Add(c);
            }
            this.Dimension = g.Dimension;
            this.Occupancy = this.Occupancy = new bool[g.Dimension, g.Dimension];
            this.genOccupancy();
        }

        public void LoadGridFromFile(string filename)
        {
            try
            {
                string[] lines = File.ReadAllLines(filename);

                this.Dimension = int.Parse(lines[0]);
                this.Occupancy = new bool[this.Dimension, this.Dimension];

                for (int i = 1; i < lines.Length; i++)
                {
                    var car = new Car();
                    var c = lines[i].Split(' ');
                    //tak just first letter of first string as color-identifier of th car
                    car.Color = c[0][0];
                    car.X = int.Parse(c[1]);
                    car.Y = int.Parse(c[2]);
                    car.Orientation = c[3] == "h" ? Orientation.Horizontal : Orientation.Vertical;
                    car.Length = int.Parse(c[4]);

                    this.Cars.Add(car);
                }
                genOccupancy();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //this method returns longest possible distance to travel
        //if car orientation doesnt permit moving in dir an exception is thrown
        public int CanMove(Car car, Direction dir)
        {
            int i = 0;
            switch (car.Orientation)
            {
                case Orientation.Vertical:
                    switch (dir)
                    {
                        case Direction.Up:
                            while (((car.Y - i - 1) >= 0) && !this.Occupancy[car.X, car.Y - i -1])
                                ++i;
                            return i;

                        case Direction.Down:
                            while (((car.Y + i + car.Length) < this.Dimension) && !this.Occupancy[car.X, car.Y + i + car.Length ])
                                ++i;
                            
                            return i;

                        default:
                            string msg = String.Format("Orientation {0} doesn't allow moving {1}", car.Orientation.ToString(), dir.ToString());
                            throw new ArgumentException(msg);
                    }
                case Orientation.Horizontal:
                    switch (dir)
                    {
                        case Direction.Left:
                            while (((car.X - i - 1) >= 0) && !this.Occupancy[car.X - i - 1, car.Y])
                                ++i;
                            return i;

                        case Direction.Right:
                            while (((car.X + i + car.Length) < this.Dimension) && !this.Occupancy[car.X + i + car.Length  , car.Y])
                            {
                                ++i;
                            }
                            return i;

                        default:
                            string msg = String.Format("Orientation {0} doesn't allow moving {1}", car.Orientation.ToString(), dir.ToString());
                            throw new ArgumentException(msg);
                    }
                default:
                    throw new ArgumentException("Unrecognised car direction");
            }
        }

        public TrafficGrid Move(Car car, Direction direction, int distance)
        {
            TrafficGrid result = new TrafficGrid(this);
            Car actualCar = result.Cars.Find(c => c.Color == car.Color);

            //if (CanMove(actualCar,direction) < distance) throw new Exception("Cant move that far");
            
            int newX = actualCar.X, newY = actualCar.Y;

            switch (actualCar.Orientation)
            {
                case Orientation.Vertical:
                    switch (direction)
                    {
                        case Direction.Up:
                            for (int i = 0; i < actualCar.Length; i++)
                            {
                                result.Occupancy[actualCar.X, actualCar.Y - distance + i] = true;
                                result.Occupancy[actualCar.X, actualCar.Y + i] = false;
                            }
                            newY -= distance;
                            break;
                        case Direction.Down:
                            for (int i = 0; i < actualCar.Length; i++)
                            {
                                result.Occupancy[actualCar.X, actualCar.Y + distance + i] = true;
                                result.Occupancy[actualCar.X, actualCar.Y + i] = false;
                            }
                            newY += distance;
                            break;
                        default:
                            break;
                    }
                    break;
                case Orientation.Horizontal:
                    switch (direction)
                    {
                        case Direction.Left:
                            for (int i = 0; i < actualCar.Length; i++)
                            {
                                result.Occupancy[actualCar.X -distance + i, actualCar.Y] = true;
                                result.Occupancy[actualCar.X + i, actualCar.Y] = false;
                            }
                            newX -= distance;
                            break;
                        case Direction.Right:
                            for (int i = 0; i < actualCar.Length; i++)
                            {
                                result.Occupancy[actualCar.X + distance + i, actualCar.Y] = true;
                                result.Occupancy[actualCar.X+i, actualCar.Y] = false;
                            }
                            newX +=distance;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            actualCar.X = newX;
            actualCar.Y = newY;

            return result;
        }

        public void PrintGrid()
        {
            char[,] map = new char[this.Dimension, this.Dimension];

            for (int i = 0; i < this.Dimension; i++)
            {
                for (int j = 0; j < this.Dimension; j++)
                {
                    map[j, i] = '0';
                }
            }

            foreach (var car in this.Cars)
            {
                switch (car.Orientation)
                {
                    case Orientation.Vertical:
                        for (int i = 0; i < car.Length; i++)
                        {
                            map[car.X, car.Y + i] = car.Color;
                        }
                        break;
                    case Orientation.Horizontal:
                        for (int i = 0; i < car.Length; i++)
                        {
                            map[car.X + i, car.Y] = car.Color;
                        }
                        break;
                    default:
                        break;
                }
            }

            for (int i = 0; i < this.Dimension; ++i)
            {
                for (int j = 0; j < this.Dimension; ++j)
                {
                    Console.Write(map[j, i]);
                }
                Console.WriteLine();
            }
        }

        public List<TrafficGrid> GeneratePossibleMoves()
        {

            List<TrafficGrid> result = new List<TrafficGrid>();
            Direction[] vert = { Direction.Up, Direction.Down };
            Direction[] horiz = { Direction.Left, Direction.Right };

            foreach(var car in this.Cars)
            {
                switch (car.Orientation)
	            {
		            case Orientation.Vertical:
                        foreach (var dir in vert)
                        {
                            int canGo = CanMove(car, dir);;
                            for (int i = 1; i <= canGo; i++)
                            {                              
                                result.Add(Move(car, dir, i));
                            }
                        }
                        break;
                    case Orientation.Horizontal:
                        foreach (var dir in horiz)
                        {
                            int canGo = this.CanMove(car, dir);
                            for (int i = 1; i <= canGo; i++)
                            {
                                result.Add(Move(car, dir, i));
                            }
                        }
                        break;
                    default:
                        break;
	            }
            }
            return result;
        }

        private void genOccupancy()
        {
            foreach (var car in this.Cars)
            {
                switch (car.Orientation)
                {
                    case Orientation.Vertical:
                        for (int i = 0; (i < car.Length) && ((car.Y + i) < this.Dimension); ++i)
                        {
                            this.Occupancy[car.X, car.Y + i] = true;
                        }
                        break;
                    case Orientation.Horizontal:
                        for (int i = 0; (i < car.Length) && ((car.X + i) < this.Dimension); ++i)
                        {
                            this.Occupancy[car.X + i, car.Y] = true;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

