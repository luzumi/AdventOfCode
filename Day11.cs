using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day11
    {
        List<char[]> _FileContent = new List<char[]>();
        List<char[]> seatStates = new List<char[]>();
        private readonly int maxSeats;
        private readonly int maxRows;
        private List<bool[]> equal = new List<bool[]>();
        private bool run = true;


        public Day11()
        {
            using (var reader = new StreamReader(@"txtFiles\Day11.txt"))
            {
                string row;

                while ((row = (reader.ReadLine())) != null)
                {
                    _FileContent.Add(row.ToCharArray());
                }
            }

            for (int row = 0; row < _FileContent.Count; row++)
            {
                equal.Add(new bool[_FileContent[0].Length]);
                seatStates.Add(new char[_FileContent[0].Length]);

                for (int column = 0; column < _FileContent[0].Length; column++)
                {
                    equal[row][column] = _FileContent[row][column] == '.';
                    seatStates[row][column] = '.';
                }
            }

            maxSeats = _FileContent[0].Length;
            maxRows = _FileContent.Count;
        }


        public void Day11Part1()
        {
            int rounds = 0;

            Console.WriteLine(countOccupied());

            for (int i = 0; run; i++)
            {
                rounds++;
                CalculateSeats();
            Console.WriteLine(countOccupied());

                GetRun();

                CopySeatsToContent();
            }

            ConsoleOutput();

            Console.WriteLine($"rounds {rounds} und count {countOccupied()}");
        }

        private int countOccupied()
        {
            int count = 0;

            for (int row = 0; row < _FileContent.Count; row++)
            {
                for (int column = 0; column < _FileContent[row].Length; column++)
                {
                    if (_FileContent[row][column] == '#')
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private void CalculateSeats()
        {
            for (int row = 0; row < _FileContent.Count; row++)
            {
                for (int seatNumber = 0; seatNumber < _FileContent[0].Length; seatNumber++)
                {
                    char entry = _FileContent[row][seatNumber] == '.' ? '.' : SeatsAreEmpty(row, seatNumber);

                    seatStates[row][seatNumber] = entry;

                    if (seatStates[row][seatNumber] == _FileContent[row][seatNumber])
                    {
                        equal[row][seatNumber] = true;
                    }
                    else
                    {
                        equal[row][seatNumber] = false;
                    }
                }
            }
        }

        private void CopySeatsToContent()
        {
            for (int row = 0; row < _FileContent.Count; row++)
            {
                for (int column = 0; column < _FileContent[0].Length; column++)
                {
                    _FileContent[row][column] = seatStates[row][column];
                }
            }
        }

        private void GetRun()
        {
            for (int row = 0; row < equal.Count; row++)
            {
                for (int column = 0; column < equal[0].Length; column++)
                {
                    if (equal[row][column])
                    {
                        run = false;
                    }
                    else
                    {
                        run = true;
                        return;
                    }
                }
            }
        }

        private void ConsoleOutput()
        {
            for (int row = 0; row < _FileContent.Count; row++)
            {
                for (int column = 0; column < _FileContent[0].Length; column++)
                {
                    Console.Write(_FileContent[row][column]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public void Day11Part2()
        {
            //throw new System.NotImplementedException();
        }


        private char SeatsAreEmpty(int row, int seatNumber)
        {
            int counter;

            switch (_FileContent[row][seatNumber])
            {
                case 'L':
                    counter = CheckNeighboors(row, seatNumber, 'L');
                    counter += CheckNeighboors(row, seatNumber, '.');

                    if (counter == 8)
                    {
                        seatStates[row][seatNumber] = '#';
                        return '#';
                    }

                    else if (counter == 3 &&
                    (row == 0 && seatNumber == 0 ||
                    row == 0 && seatNumber == maxSeats - 1) ||
                    row == maxRows - 1 && seatNumber == 0 ||
                    row == maxRows - 1 && seatNumber == maxSeats - 1)
                    {
                    seatStates[row][seatNumber] = '#';

                    return '#';
                    }

                    else if (counter == 5 &&
                    (row == 0 ||
                    row == maxRows - 1 ||
                    seatNumber == 0 ||
                    seatNumber == maxSeats - 1))

                    {
                    seatStates[row][seatNumber] = '#';

                    return '#';
                    }

                    else return 'L';

                case '#':
                    counter = CheckNeighboors(row, seatNumber, '#');
                    if (counter >= 4)
                    {
                        seatStates[row][seatNumber] = 'L';

                        return 'L';
                    }

                    return '#';

                default:
                    seatStates[row][seatNumber] = '.';
                    return '.';
            }
        }


        private int CheckNeighboors(int row, int seatNumber, char sign)
        {
            int counter = 0;

            if (row > 0 && row < maxSeats - 1 && seatNumber > 0 && seatNumber < maxSeats - 1)
            {
                //vorn links
                if (_FileContent[row - 1][seatNumber - 1] == sign)
                    counter++;
                //vorn mitte
                if (_FileContent[row - 1][seatNumber] == sign)
                    counter++;
                //vorn rechts
                if (_FileContent[row - 1][seatNumber + 1] == sign)
                    counter++;

                //nach rechts
                if (_FileContent[row][seatNumber + 1] == sign)
                    counter++;
                //nach links            
                if (_FileContent[row][seatNumber - 1] == sign)
                    counter++;

                //hinten rechts
                if (_FileContent[row + 1][seatNumber + 1] == sign)
                    counter++;
                //hinten mitte
                if (_FileContent[row + 1][seatNumber] == sign)
                    counter++;
                //hinten links       
                if (_FileContent[row + 1][seatNumber - 1] == sign)
                    counter++;
            }

            else if (row == 0 && seatNumber == 0)
            {
                //hinten rechts
                if (_FileContent[row + 1][seatNumber + 1] == sign)
                    counter++;
                //hinten mitte
                if (_FileContent[row + 1][seatNumber] == sign)
                    counter++;
                //nach rechts            
                if (_FileContent[row][seatNumber + 1] == sign)
                    counter++;
            }
            else if (row == 0 && seatNumber == maxSeats - 1)
            {
                //hinten mitte
                if (_FileContent[row + 1][seatNumber] == sign)
                    counter++;
                //hinten links
                if (_FileContent[row + 1][seatNumber - 1] == sign)
                    counter++;
                //nach links            
                if (_FileContent[row][seatNumber - 1] == sign)
                    counter++;
            }
            else if (row == maxRows - 1 && seatNumber == 0)
            {
                //vorn mitte
                if (_FileContent[row - 1][seatNumber] == sign)
                    counter++;
                //vorn rechts
                if (_FileContent[row - 1][seatNumber + 1] == sign)
                    counter++;
                //nach rechts
                if (_FileContent[row][seatNumber + 1] == sign)
                    counter++;
            }
            else if (row == maxRows - 1 && seatNumber == maxSeats - 1)
            {
                //vorn links
                if (_FileContent[row - 1][seatNumber - 1] == sign)
                    counter++;
                //vorn mitte
                if (_FileContent[row - 1][seatNumber] == sign)
                    counter++;
                //nach links            
                if (_FileContent[row][seatNumber - 1] == sign)
                    counter++;
            }
            else if (seatNumber == 0)
            {
                //vorn rechts
                if (_FileContent[row - 1][seatNumber + 1] == sign)
                    counter++;
                //vorn mitte
                if (_FileContent[row - 1][seatNumber] == sign)
                    counter++;
                //nach rechts
                if (_FileContent[row][seatNumber + 1] == sign)
                    counter++;
                //hinten rechts
                if (_FileContent[row + 1][seatNumber + 1] == sign)
                    counter++;
                //hinten mitte
                if (_FileContent[row + 1][seatNumber] == sign)
                    counter++;
            }
            else if (seatNumber == maxSeats - 1)
            {
                //vorn links
                if (_FileContent[row - 1][seatNumber - 1] == sign)
                    counter++;
                //vorn mitte
                if (_FileContent[row - 1][seatNumber] == sign)
                    counter++;
                //nach links            
                if (_FileContent[row][seatNumber - 1] == sign)
                    counter++;
                //hinten mitte
                if (_FileContent[row + 1][seatNumber] == sign)
                    counter++;
                //hinten links       
                if (_FileContent[row + 1][seatNumber - 1] == sign)
                    counter++;
            }
            else if (row == 0)
            {
                //nach rechts
                if (_FileContent[row][seatNumber + 1] == sign)
                    counter++;
                //nach links            
                if (_FileContent[row][seatNumber - 1] == sign)
                    counter++;

                //hinten rechts
                if (_FileContent[row + 1][seatNumber + 1] == sign)
                    counter++;
                //hinten mitte
                if (_FileContent[row + 1][seatNumber] == sign)
                    counter++;
                //hinten links       
                if (_FileContent[row + 1][seatNumber - 1] == sign)
                    counter++;
            }
            else if (row == maxRows - 1)
            {
                //vorn links
                if (_FileContent[row - 1][seatNumber - 1] == sign)
                    counter++;
                //vorn mitte
                if (_FileContent[row - 1][seatNumber] == sign)
                    counter++;
                //vorn rechts
                if (_FileContent[row - 1][seatNumber + 1] == sign)
                    counter++;

                //nach rechts
                if (_FileContent[row][seatNumber + 1] == sign)
                    counter++;
                //nach links            
                if (_FileContent[row][seatNumber - 1] == sign)
                    counter++;
            }

            return counter;
        }


        public static void SolveDay11(string filename, int maxNeighbors, int maxSteps)
        {
            var seatGrid = File.ReadAllLines(filename);
            bool isEqual;
            do
            {
                var newGrid = IterateStep(seatGrid, maxNeighbors, maxSteps);
                isEqual = seatGrid.SequenceEqual(newGrid);
                seatGrid = newGrid;
            } while (!isEqual);

            Console.WriteLine(seatGrid.Sum(l => l.Count(x => x == '#')));
        }

        private static string[] IterateStep(string[] inGrid, int maxNeighbors, int maxSteps)
        {
            var width = inGrid[0].Length;
            var height = inGrid.Length;

            var outGrid = new string[height];

            for (var y = 0; y < height; y++)
            {
                outGrid[y] = "";
                for (var x = 0; x < width; x++)
                {
                    var occupiedNeighbors = 0;
                    for (var dy = -1; dy <= 1; dy++)
                    {
                        for (var dx = -1; dx <= 1; dx++)
                        {
                            if (dx == 0 && dy == 0) continue;

                            var seatFound = false;

                            var tx = x + dx;
                            var ty = y + dy;
                            var steps = maxSteps;

                            while (ty >= 0 && ty < height && tx >= 0 && tx < width && !seatFound && steps-- != 0)
                            {
                                if (inGrid[ty][tx] != '.') seatFound = true;
                                else
                                {
                                    tx += dx;
                                    ty += dy;
                                }
                            }

                            if (seatFound && inGrid[ty][tx] == '#') occupiedNeighbors++;
                        }
                    }

                    outGrid[y] += inGrid[y][x] switch
                    {
                        'L' when occupiedNeighbors == 0 => "#",
                        '#' when occupiedNeighbors >= maxNeighbors => "L",
                        _ => inGrid[y][x]
                    };
                }
            }

            return outGrid;
        }
    }
}