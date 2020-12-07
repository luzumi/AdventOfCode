using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day7
    {
        private List<string> _fieldContent = new List<string>();
        private List<string> bags;
        private Dictionary<string, List<BagContent>> _bags;

        public Day7()
        {
            _bags = new Dictionary<string, List<BagContent>>();

            _fieldContent = File.ReadAllText(@"txtFiles\Day7.txt").Split("\r\n").Where(x => !string.IsNullOrEmpty(x))
                .ToList();
            _fieldContent.ForEach(Parse);
        }

        public void Day7Part1()
        {
            #region Question

            //--- Day 7: Handy Haversacks ---

            //You land at the regional airport in time for your next flight. In fact, it looks like you'll even have time to grab some food: all flights are currently delayed due to issues in luggage processing.

            //Due to recent aviation regulations, many rules (your puzzle input) are being enforced about bags and their contents; bags must be color-coded and must contain specific quantities of other color-coded bags. Apparently, nobody responsible for these regulations considered how long they would take to enforce!

            //For example, consider the following rules:

            //light red bags contain 1 bright white bag, 2 muted yellow bags.
            //dark orange bags contain 3 bright white bags, 4 muted yellow bags.
            //bright white bags contain 1 shiny gold bag.
            //muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
            //shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
            //dark olive bags contain 3 faded blue bags, 4 dotted black bags.
            //vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
            //faded blue bags contain no other bags.
            //dotted black bags contain no other bags.

            //These rules specify the required contents for 9 bag types. In this example, every faded blue bag is empty, every vibrant plum bag contains 11 bags (5 faded blue and 6 dotted black), and so on.

            //You have a shiny gold bag. If you wanted to carry it in at least one other bag, how many different bag colors would be valid for the outermost bag? (In other words: how many colors can, eventually, contain at least one shiny gold bag?)

            //In the above rules, the following options would be available to you:

            //    A bright white bag, which can hold your shiny gold bag directly.
            //    A muted yellow bag, which can hold your shiny gold bag directly, plus some other bags.
            //    A dark orange bag, which can hold bright white and muted yellow bags, either of which could then hold your shiny gold bag.
            //    A light red bag, which can hold bright white and muted yellow bags, either of which could then hold your shiny gold bag.

            //So, in this example, the number of bag colors that can eventually contain at least one shiny gold bag is 4.

            //How many bag colors can eventually contain at least one shiny gold bag? 

            #endregion

            bags = _bags.Where(pX => pX.Value.Select(pX => pX.Colour)
                    .Contains("shiny gold bag"))
                .Select(pX => pX.Key)
                .ToList();

            var total = bags;

            while (bags.Any())
            {
                var nextBags = _bags.Where(pX => pX.Value
                        .Select(pX => pX.Colour) //Search content in bag for content and its contents
                        .Intersect(bags).Any())
                    .ToList(); //searches for intersections as long as there are still bags in the entry

                bags = nextBags.Select(pX => pX.Key).ToList();
                total = total.Concat(bags).ToList(); //concatenate all found bags
            }

            Console.WriteLine($"Part 1: {total.Distinct().Count()}");
        }


        public void Day7Part2()
        {
            #region Question

            //--- Part Two ---
            // 
            // It's getting pretty expensive to fly these days - not because of ticket prices, but because of the ridiculous number of bags you need to buy!
            // 
            // Consider again your shiny gold bag and the rules from the above example:
            // 
            // faded blue bags contain 0 other bags.
            // dotted black bags contain 0 other bags.
            // vibrant plum bags contain 11 other bags: 5 faded blue bags and 6 dotted black bags.
            // dark olive bags contain 7 other bags: 3 faded blue bags and 4 dotted black bags.
            // 
            // So, a single shiny gold bag must contain 1 dark olive bag (and the 7 bags within it) plus 2 vibrant plum bags (and the 11 bags within each of those): 1 + 1*7 + 2 + 2*11 = 32 bags!
            // 
            // Of course, the actual rules have a small chance of going several levels deeper than this example; be sure to count all of the bags, even if the nesting becomes topologically impractical!
            // 
            // Here's another example:
            // 
            // shiny gold bags contain 2 dark red bags.
            // dark red bags contain 2 dark orange bags.
            // dark orange bags contain 2 dark yellow bags.
            // dark yellow bags contain 2 dark green bags.
            // dark green bags contain 2 dark blue bags.
            // dark blue bags contain 2 dark violet bags.
            // dark violet bags contain no other bags.
            // 
            // In this example, a single shiny gold bag must contain 126 other bags.
            // 
            // How many individual bags are required inside your single shiny gold bag?
            // 

            #endregion

            var goldBag = _bags["shiny gold bag"];

            Console.WriteLine($"Part 2: {CalculateContentsCount(goldBag)}");
        }

        private int CalculateContentsCount(List<BagContent> pContents)
        {
            int bagCount = pContents.Sum(pBagContent => pBagContent.Count);

            bagCount += pContents.Sum(pBagContent =>  
                pBagContent.Colour == "no other bag" ? 0 : CalculateContentsCount(_bags[pBagContent.Colour]) * pBagContent.Count);

            return bagCount;
        }


        private void Parse(string pLine)
        {
            pLine = pLine.Replace("bags", "bag").Replace(".", "");

            var bagColour = pLine.Split(" contain ")[0]; //ColorName
            var bagContents = pLine.Split(" contain ")[1].Split(","); //included bags

            _bags.Add(bagColour, bagContents.Select(ParseContent).ToList()); //DictEntry
        }


        private BagContent ParseContent(string pContentString)
        {
            pContentString = pContentString.Trim();

            var allNumbers = Regex.Matches(pContentString, @"\d+") //finds the number of the current bag 
                .Select(pX => int.Parse(pX.Value));

            var count = allNumbers.Count() == 0 ? 0 : allNumbers.First();

            var result = new BagContent
            {
                Count = count,
                Colour = Regex.Replace(pContentString, @"\d", "").Trim()
            };
            return result;
        }


        public class BagContent
        {
            public int Count { get; set; }
            public string Colour { get; set; }
        }
    }
}
//[‎07.‎12.‎2020 08:34]  Oliver Mahmoudi:  
// https://www.gesetze-im-internet.de/bbig_2005 
// 
// [‎07.‎12.‎2020 08:35]  Oliver Mahmoudi:  
// https://www.duesseldorf.ihk.de/produktmarken/ausbildung/ausbildung-von-a-z/zwischen-und-abschlusspruefung-2596774 
// 
// [‎07.‎12.‎2020 08:38]  Oliver Mahmoudi:  
// https://www.ihk-aka.de/ 
// 