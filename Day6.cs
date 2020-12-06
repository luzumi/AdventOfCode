using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day6
    {
        readonly List<string> _fieldContent;
        int[] occuringChar = new int[26];
        private int sum = 0;
        private IEnumerable<string[]> groups;

        public Day6()
        {
            _fieldContent = new List<string>();

            string text = File.ReadAllText(@"txtFiles\Day6.txt");
            groups = text.Split("\r\n\r\n").Select(group => group.Split("\r\n"));

            _fieldContent.AddRange(text.Split("\r\n\r\n", StringSplitOptions.None));

            for (int i = 0; i < occuringChar.Length; i++)
            {
                occuringChar[i] = 0;
            }
        }

        public void Day6Part1()
        {
            #region Question

            //As your flight approaches the regional airport where you'll switch to a much larger plane, customs declaration forms are distributed to the passengers.
            // 
            // The form asks a series of 26 yes-or-no questions marked a through z. All you need to do is identify the questions for which anyone in your group answers "yes". Since your group is just you, this doesn't take very long.
            // 
            // However, the person sitting next to you seems to be experiencing a language barrier and asks if you can help. For each of the people in their group, you write down the questions for which they answer "yes", one per line. For example:
            // 
            // abcx
            // abcy
            // abcz
            // 
            // In this group, there are 6 questions to which anyone answered "yes": a, b, c, x, y, and z. (Duplicate answers to the same question don't count extra; each question counts at most once.)
            // 
            // Another group asks for your help, then another, and eventually you've collected answers from every group on the plane (your puzzle input). Each group's answers are separated by a blank line, and within each group, each person's answers are on a single line. For example:
            // 
            // abc
            // 
            // a
            // b
            // c
            // 
            // ab
            // ac
            // 
            // a
            // a
            // a
            // a
            // 
            // b
            // 
            // This list represents answers from five groups:
            // 
            // The first group contains one person who answered "yes" to 3 questions: a, b, and c.
            // The second group contains three people; combined, they answered "yes" to 3 questions: a, b, and c.
            // The third group contains two people; combined, they answered "yes" to 3 questions: a, b, and c.
            // The fourth group contains four people; combined, they answered "yes" to only 1 question, a.
            // The last group contains one person who answered "yes" to only 1 question, b.
            // 
            // In this example, the sum of these counts is 3 + 3 + 3 + 1 + 1 = 11.
            // 
            // For each group, count the number of questions to which anyone answered "yes". What is the sum of those counts?

            #endregion

            for (int lineIndex = 0; lineIndex < _fieldContent.Count; lineIndex++)
            {
                _fieldContent[lineIndex] =
                    String.Concat(_fieldContent[lineIndex]
                            .Replace("\r\n", " ")
                            .OrderBy(c => c)
                            .Distinct())
                        .TrimStart();
            }

            foreach (var group in _fieldContent)
            {
                sum += group.Length;
            }

            Console.WriteLine($"YesCounter = {sum}");
        }

        public void Day6Part2()
        {
            #region Question

            //--- Part Two ---
            // 
            // As you finish the last group's customs declaration, you notice that you misread one word in the instructions:
            // 
            // You don't need to identify the questions to which anyone answered "yes"; you need to identify the questions to which everyone answered "yes"!
            // 
            // Using the same example as above:
            // 
            // abc
            // 
            // a
            // b
            // c
            // 
            // ab
            // ac
            // 
            // a
            // a
            // a
            // a
            // 
            // b
            // 
            // This list represents answers from five groups:
            // 
            // In the first group, everyone (all 1 person) answered "yes" to 3 questions: a, b, and c.
            // In the second group, there is no question to which everyone answered "yes".
            // In the third group, everyone answered yes to only 1 question, a. Since some people did not answer "yes" to b or c, they don't count.
            // In the fourth group, everyone answered yes to only 1 question, a.
            // In the fifth group, everyone (all 1 person) answered "yes" to 1 question, b.
            // 
            // In this example, the sum of these counts is 3 + 0 + 1 + 1 + 1 = 6.
            // 
            // For each group, count the number of questions to which everyone answered "yes". What is the sum of those counts?
            // 

            #endregion

            var everyOneAnsweredYes = new List<int>();

            foreach (var group in groups)
            {
                IEnumerable<char> answeredYesByEveryone = null;
                foreach (string personAnswers in group)
                {
                    answeredYesByEveryone = answeredYesByEveryone == null ? personAnswers.ToCharArray() : personAnswers.Intersect(answeredYesByEveryone);
                }

                everyOneAnsweredYes.Add(answeredYesByEveryone.Count());
            }

            Console.WriteLine($"YesCounter(2) = {everyOneAnsweredYes.Sum()}");
        }
    }
}