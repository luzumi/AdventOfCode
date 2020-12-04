using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    class Day3
    {
        private static readonly List<string> FileContent = new List<string>();
        private readonly int _rowCount;
        int _counter;

        public Day3()
        {
            using (var reader = new StreamReader(@"txtFiles\Day3.txt"))
            {
                string row;
                while ((row = (reader.ReadLine())) != null)
                {
                    FileContent.Add(row);
                }
            }

            _rowCount = FileContent.Count;
        }

        public void Day3Lösung1()
        {


            for (int zeileIndex = 0; zeileIndex < FileContent.Count; zeileIndex++)
            {
                string row = FileContent[zeileIndex];
                for (int i = 0; i < _rowCount; i++)
                {
                    FileContent[zeileIndex] += row;
                }
            }

            int try1;
            int try2;
            int try3;
            int try4;
            int try5;

            //Right 1, down 1.
            _counter = 0;
            int stepDown = 0;
            for (int stepRight = 0; stepRight < FileContent[stepDown].Length;)
            {
                if (stepDown == 322) break;
                if (FileContent[++stepDown][++stepRight] == '#')
                {
                    _counter++;
                }
            }

            try1 = _counter;


            // Right 3, down 1. (This is the slope you already checked.)
            _counter = 0;
            stepDown = 0;
            for (int stepRight = 0;
                stepRight < FileContent[stepDown].Length -
                3;)
            {
                if (FileContent[++stepDown][stepRight += 3] == '#')
                {
                    _counter++;
                }

                if (stepDown == 322) break;
            }

            Console.WriteLine($"trees Multiplied = {_counter}");
            try2 = _counter;

            // Right 5, down 1.
            _counter = 0;
            stepDown = 0;
            for (int stepRight = 0; stepRight < FileContent[stepDown].Length - 5;)
            {
                if (FileContent[++stepDown][stepRight += 5] == '#')
                {
                    _counter++;
                }

                if (stepDown == 322) break;
            }

            try3 = _counter;

            // Right 7, down 1.
            _counter = 0;
            stepDown = 0;
            for (int stepRight = 0; stepRight < FileContent[stepDown].Length - 7;)
            {
                if (FileContent[++stepDown][stepRight += 7] == '#')
                {
                    _counter++;
                }

                if (stepDown == 322) break;
            }

            try4 = _counter;
            
            //Right 1, down 2.
            _counter = 0;
            stepDown = 0;
            for (int stepRight = 0; stepRight < FileContent[stepDown].Length - 1;)
            {
                if (FileContent[stepDown += 2][++stepRight] == '#')
                {
                    _counter++;
                }

                if (stepDown == 320) break;
            }

            try5 = _counter;

            Console.WriteLine("" + (long) try1 * try2 * try3 * try4 * try5);
        }


        public void Day3Part1()
        {
            // Right 3, down 1. (This is the slope you already checked.)
            _counter = 0;
            int stepDown = 0;
            for (int stepRight = 0;
                stepRight < FileContent[stepDown].Length -
                3;)
            {
                if (FileContent[++stepDown][stepRight += 3] == '#')
                {
                    _counter++;
                }

                if (stepDown == 322) break;
            }

            Console.WriteLine($"Trees = {_counter}");
        }
    }
}