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
        public Car Red { get; private set; }
        public bool[,] Occupancy { get; set; }
        public int Dimension { get; set; }

        public TrafficGrid()
        {
            this.Cars = new List<Car>();
        }

        public TrafficGrid(int dim)
        {
            this.Cars = new List<Car>();
            this.Dimension = dim;
            this.Occupancy = new bool[dim, dim];
        }

        public TrafficGrid(List<Car> cars, int dim)
        {
            this.Cars = new List<Car>();
            this.Dimension = dim;
            this.Occupancy = new bool[dim, dim];
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
            int i = 1;
            switch (car.Orientation)
            {
                case Orientation.Vertical:
                    switch (dir)
                    {
                        case Direction.Up:
                            while (((car.Y - i) >= 0) && !this.Occupancy[car.X, car.Y - i])
                                ++i;
                            return i - 1;

                        case Direction.Down:
                            while (((car.Y + i + car.Length) < this.Dimension) && !this.Occupancy[car.X, car.Y + i + car.Length])
                                ++i;
                            return i - 1;

                        default:
                            string msg = String.Format("Orientation {0} doesn't allow moving {1}", car.Orientation.ToString(), dir.ToString());
                            throw new ArgumentException(msg);
                    }
                case Orientation.Horizontal:
                    switch (dir)
                    {
                        case Direction.Left:
                            while (((car.X - i) >= 0) && !this.Occupancy[car.X - i, car.Y])
                                ++i;
                            return i - 1;

                        case Direction.Right:
                            while (((car.X + i + car.Length) < this.Dimension) && !this.Occupancy[car.X + i + car.Length, car.Y])
                            {
                                ++i;
                            }
                            return i - 1;

                        default:
                            string msg = String.Format("Orientation {0} doesn't allow moving {1}", car.Orientation.ToString(), dir.ToString());
                            throw new ArgumentException(msg);
                    }
                default:
                    throw new ArgumentException("Unrecognised car direction");
            }
        }

        public void Move(Car car, Direction direction, int distance)
        {
            //maybe add CanMove check if distance <= canmove
            // if(CanMove(car)<distance) throw Exception
            int newX = 0, newY = 0;

            switch (car.Orientation)
            {
                case Orientation.Vertical:
                    switch (direction)
                    {
                        case Direction.Up:
                            for (int i = 0; i < car.Length; i++)
                            {
                                this.Occupancy[car.X, car.Y - distance + i] = true;
                                this.Occupancy[car.X, car.Y] = false;
                            }
                            newY = car.Y - distance;
                            break;
                        case Direction.Down:
                            for (int i = 0; i < car.Length; i++)
                            {
                                this.Occupancy[car.X, car.Y + distance + i] = true;
                                this.Occupancy[car.X, car.Y] = false;
                            }
                            newY = car.Y + distance;
                            break;
                        default:
                            break;
                    }
                    newX = car.X;
                    break;
                case Orientation.Horizontal:
                    switch (direction)
                    {
                        case Direction.Left:
                            for (int i = 0; i < car.Length; i++)
                            {
                                this.Occupancy[car.X -distance, car.Y] = true;
                                this.Occupancy[car.X, car.Y] = false;
                            }
                            newX = car.X - distance;
                            break;
                        case Direction.Right:
                            for (int i = 0; i < car.Length; i++)
                            {
                                this.Occupancy[car.X + distance, car.Y] = true;
                                this.Occupancy[car.X, car.Y] = false;
                            }
                            newX = car.X + distance;
                            break;
                        default:
                            break;
                    }
                    newY = car.Y;
                    break;
                default:
                    break;
            }

            car.X = newX;
            car.Y = newY;
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

