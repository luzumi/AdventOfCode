using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    public class Day4
    {
        readonly List<string> _fieldContent = new List<string>();
        private string _row;

        public void Day4Part1()
        {
            #region Desription

            //
            // The automatic passport scanners are slow because they're having trouble detecting which passports have all required fields.
            // The expected fields are as follows:
            ////////////////////////////
            //  byr (Birth Year)      //
            //  iyr (Issue Year)      //
            //  eyr (Expiration Year) //
            //  hgt (Height)          //
            //  hcl (Hair Color)      //
            //  ecl (Eye Color)       //
            //  pid (Passport ID)     //
            //  cid (Country ID)      //
            ///////////////////////////
            //Passport data is validated in batch files (your puzzle input). Each passport is represented as a sequence of key:value pairs separated by spaces or newlines.
            //
            //Passports are separated by blank lines.
            //
            //Here is an example batch file containing four passports:
            /////////////////////////////////////////////////////////// 
            //  ecl:gry pid:860033327 eyr:2020 hcl:#fffffd           //  The first passport is valid - all eight fields are present. The second passport is invalid - it is missing hgt (the Height field).
            //  byr:1937 iyr:2017 cid:147 hgt:183cm                  //
            //                                                       //  
            //  iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884      //
            //  hcl:#cfa07d byr:1929                                 //  
            //                                                       //
            //  hcl:#ae17e1 iyr:2013                                 //  The third passport is interesting; the only missing field is cid, so it looks like data from North Pole Credentials,
            //  eyr:2024                                             //  not a passport at all! Surely, nobody would mind if you made the system temporarily ignore missing cid fields.
            //  ecl:brn pid:760753108 byr:1931                       //  Treat this "passport" as valid.
            //  hgt:179cm                                            //
            //                                                       //
            //  hcl:#cfa07d eyr:2025 pid:166559648                   //  The fourth passport is missing two fields, cid and byr. Missing cid is fine, but missing any other field is not, so this passport is invalid.
            //  iyr:2011 ecl:brn hgt:59in                            //
            //////////////////////////////////////////////////////////
            // 
            // According to the above rules, your improved system would report 2 valid passports.
            // 
            // Count the number of valid passports - those that have all required fields. Treat cid as optional. In your batch file(Day4.txt), how many passports are valid?

            #endregion

            string text = File.ReadAllText(@"txtFiles\Day4.txt");

            string[] passportsText = text.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);
            int validPassports = 0;

            using (var reader = new StreamReader(@"txtFiles\Day4.txt"))
            {
                while ((_row = (reader.ReadLine())) != null)
                {
                    _fieldContent.AddRange(new[] {_row});
                }
            }

            foreach (string passport in passportsText)
            {
                if (passport.Contains("ecl") && passport.Contains("pid") &&
                    passport.Contains("eyr") && passport.Contains("hcl") &&
                    passport.Contains("byr") && passport.Contains("iyr") &&
                    passport.Contains("hgt"))
                {
                    validPassports += 1;
                }
            }

            Console.WriteLine(validPassports + " valid passports total");
        }

        public void Day4Part2()
        {
            #region Description

            //--- Part Two ---
            // 
            // The line is moving more quickly now, but you overhear airport security talking about how passports with invalid data are getting through. Better add some data validation, quick!
            // 
            // You can continue to ignore the cid field, but each other field has strict rules about what values are valid for automatic validation:
            // 
            // byr (Birth Year) - four digits; at least 1920 and at most 2002.
            // iyr (Issue Year) - four digits; at least 2010 and at most 2020.
            // eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
            // hgt (Height) - a number followed by either cm or in:
            // If cm, the number must be at least 150 and at most 193.
            // If in, the number must be at least 59 and at most 76.
            // hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
            // ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
            // pid (Passport ID) - a nine-digit number, including leading zeroes.
            // cid (Country ID) - ignored, missing or not.
            // 
            // Your job is to count the passports where all required fields are both present and valid according to the above rules. Here are some example values:
            // 
            // byr valid:   2002
            // byr invalid: 2003
            // 
            // hgt valid:   60in
            // hgt valid:   190cm
            // hgt invalid: 190in
            // hgt invalid: 190
            // 
            // hcl valid:   #123abc
            // hcl invalid: #123abz
            // hcl invalid: 123abc
            // 
            // ecl valid:   brn
            // ecl invalid: wat
            // 
            // pid valid:   000000001
            // pid invalid: 0123456789
            // 
            // Here are some invalid passports:
            // 
            // eyr:1972 cid:100
            // hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926
            // 
            // iyr:2019
            // hcl:#602927 eyr:1967 hgt:170cm
            // ecl:grn pid:012533040 byr:1946
            // 
            // hcl:dab227 iyr:2012
            // ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277
            // 
            // hgt:59cm ecl:zzz
            // eyr:2038 hcl:74454a iyr:2023
            // pid:3556412378 byr:2007
            // 
            // Here are some valid passports:
            // 
            // pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980
            // hcl:#623a2f
            // 
            // eyr:2029 ecl:blu cid:129 byr:1989
            // iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm
            // 
            // hcl:#888785
            // hgt:164cm byr:2001 iyr:2015 cid:88
            // pid:545766238 ecl:hzl
            // eyr:2022
            // 
            // iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719
            // 
            // Count the number of valid passports - those that have all required fields and valid values. Continue to treat cid as optional. In your batch file, how many passports are valid?
            // 

            #endregion

            string text = File.ReadAllText(@"txtFiles\Day4.txt");
            string[] passportsText = text.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);
            int validPassports = 0;

            foreach (string passport in passportsText)
            {
                if (passport.Contains("ecl") && passport.Contains("pid") && passport.Contains("eyr") &&
                    passport.Contains("hcl") && passport.Contains("byr") && passport.Contains("iyr") &&
                    passport.Contains("hgt"))
                {
                    string[] passportFields = passport.Replace("\r\n", " ").Split(" ");
                    int validFields = 0;

                    for (int i = 0; i < passportFields.Length; i++)
                    {
                        string field = passportFields[i];

                        if (field.Contains("hgt"))
                        {
                            if (field.EndsWith("cm") || field.EndsWith("in"))
                            {
                                field = field.Substring(4);
                                int height = int.Parse(field.Substring(0, field.Length - 2));
                                if (field.EndsWith("cm") && height >= 150 && height <= 193) validFields++;
                                else if (field.EndsWith("in") && height >= 59 && height <= 76) validFields++;
                            }
                        }
                        else if (field.Contains("byr"))
                        {
                            int birthYear = int.Parse(field.Substring(4));
                            if (birthYear >= 1920 && birthYear <= 2002) validFields++;
                        }
                        else if (field.Contains("iyr"))
                        {
                            int issueYear = int.Parse(field.Substring(4));
                            if (issueYear >= 2010 && issueYear <= 2020) validFields++;
                        }
                        else if (field.Contains("eyr"))
                        {
                            int expirationYear = int.Parse(field.Substring(4));
                            if (expirationYear >= 2020 && expirationYear <= 2030) validFields++;
                        }
                        else if (field.Contains("hcl"))
                        {
                            field = field.Substring(4);
                            if (field.StartsWith("#"))
                            {
                                field = field.Replace("#", "");
                                if (Int32.TryParse(field, System.Globalization.NumberStyles.HexNumber, null, out _))
                                    validFields++;
                            }
                        }
                        else if (field.Contains("ecl"))
                        {
                            string eyeColor = field.Substring(4);
                            if (eyeColor == "amb" || eyeColor == "blu" || eyeColor == "brn" || eyeColor == "gry" ||
                                eyeColor == "grn" || eyeColor == "hzl" || eyeColor == "oth")
                            {
                                validFields++;
                            }
                        }
                        else if (field.Contains("pid"))
                        {
                            string passportId = field.Substring(4);
                            if (int.TryParse(passportId, out _) && passportId.Length == 9) validFields++;
                        }
                    }

                    if (validFields == 7)
                    {
                        validPassports++;
                    }
                }
            }

            Console.WriteLine(validPassports + " valid passports total");
        }
    }
}