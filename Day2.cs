using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day2
    {
        private static string row;
        List<string> fileContent = new List<string>();
        List<int> zahl1 = new List<int>();
        List<int> zahl2 = new List<int>();
        List<string> letterList = new List<string>();
        List<string> passWordList = new List<string>();
        int treffer = 0;
        int treffer2 = 0;
        List<bool> hacked = new List<bool>();

        public Day2()
        {
            using (var reader = new StreamReader(@"txtFiles\Day2.txt"))
            {
                while ((row = (reader.ReadLine())) != null)
                {
                    fileContent.AddRange(new[] {row});
                }
            }
        }

        #region Day2Aufgabe1

        public void Day2Part1()
        {
            #region Description

            //Your flight departs in a few days from the coastal airport; the easiest way down to the coast from here is via toboggan.
            // 
            // The shopkeeper at the North Pole Toboggan Rental Shop is having a bad day. "Something's wrong with our computers; we can't log in!" You ask if you can take a look.
            // 
            // Their password database seems to be a little corrupted: some of the passwords wouldn't have been allowed by the Official Toboggan Corporate Policy that was in effect when they were chosen.
            // 
            // To try to debug the problem, they have created a list (your puzzle input) of passwords (according to the corrupted database) and the corporate policy when that password was set.
            // 
            // For example, suppose you have the following list:
            // 
            // 1-3 a: abcde
            // 1-3 b: cdefg
            // 2-9 c: ccccccccc
            // 
            // Each line gives the password policy and then the password. The password policy indicates the lowest and highest number of times a given letter must appear for the password to be valid. For example, 1-3 a means that the password must contain a at least 1 time and at most 3 times.
            // 
            // In the above example, 2 passwords are valid. The middle password, cdefg, is not; it contains no instances of b, but needs at least 1. The first and third passwords are valid: they contain one a or nine c, both within the limits of their respective policies.
            // 
            // How many passwords are valid according to their policies?

            #endregion

            for (int zeile = 0; zeile < fileContent.Count; zeile++)
            {
                readContent(zeile);

                for (int i = 0; i < passWordList[zeile].Length; i++)
                {
                    if (passWordList[zeile][i] == letterList[zeile][0])
                    {
                        treffer++;
                    }
                }

                if (treffer >= zahl1[zeile] && treffer <= zahl2[zeile])
                    hacked[zeile] = true;
                
                treffer = 0;
                foreach (var b in hacked)
                {
                    if (b) treffer++;
                }


                Console.WriteLine($"Treffer {treffer}");
                
                #endregion

                
            }

            
        }

        private void readContent(int zeile)
        {
            hacked.Add(new bool());
            string line = fileContent[zeile];
            fileContent[zeile] = line;
            string zahlA = "";

            string pattern = @"(\d+)-";

            Match match = Regex.Match(line, pattern);

            zahlA = match.Value;
            zahlA = zahlA.Replace('-', ' ');
            zahl1[zeile] = Int32.Parse(zahlA.TrimEnd());

            pattern = @"-(\d+)";
            match = Regex.Match(line, pattern);
            zahl2[zeile] =Int32.Parse(match.Value.Substring(1));

            pattern = @"([a-z])";
            match = Regex.Match(line, pattern);
            letterList[zeile] = match.Value;

            pattern = @"(: [a-z]+)";
            match = Regex.Match(line, pattern);
            passWordList[zeile] =match.Value.Substring(2);
        }

        #region Day2PartTwo

        public void Day2Part2()
        {
            for (int zeile = 0; zeile < passWordList.Count; zeile++)
            {
                readContent(zeile);

                if (passWordList[zeile][zahl1[zeile] - 1] == letterList[zeile][0] &&
                    passWordList[zeile][zahl2[zeile] - 1] != letterList[zeile][0])
                {
                    treffer2++;
                }
            }

            Console.WriteLine($"Treffer2 {treffer2}");
        }

        #endregion
    }
}