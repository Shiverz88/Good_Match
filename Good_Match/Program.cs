using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Text;


namespace Good_Match
{
    class Program
    {

        static string NumberReduce(string numberString)
        {
            bool isEven = true;
            double first;
            double last;
            double tempAdd;
            string sum = "";

            if (numberString.Length % 2 != 0)
            {
                isEven = false;
            }

            for (int i = 0; i < numberString.Length / 2; i++)
            {
                first = Char.GetNumericValue(numberString, i);
                last = Char.GetNumericValue(numberString, numberString.Length - 1 - i);
                tempAdd = first + last;
                sum += tempAdd.ToString();
            }
            if (isEven == false)
            {
                sum += numberString[numberString.Length / 2];
            }

            return sum;
        }
        static string MatchingAlgorithm(string name_one, string name_two)
        {
            string joined = name_one.ToLower() + "matches" + name_two.ToLower();//this is so capitals do not mess up the calc as well as the fact that matches needs to be run through as well
            string CheckedChars = "";
            string StrCount = "";
            int TempCount;

            foreach (char c in joined)
            {
                if (CheckedChars.Contains(c) == false)
                {
                    CheckedChars += c;
                    TempCount = joined.Split(c).Length - 1;
                    StrCount += TempCount.ToString();
                }
            }//this is where the count starts to be reduced

            string reduced;
            if (StrCount.Length <= 2)
            {
                reduced = StrCount;
            }
            else
            {
                reduced = NumberReduce(StrCount);
                while (reduced.Length > 2)
                {
                    reduced = NumberReduce(reduced);
                }
            }

            int percentage = Int32.Parse(reduced);
            string result = percentage > 80 ? (name_one + " matches " + name_two + " " + reduced + "%, good match") : name_one + " matches " + name_two + " " + reduced + "%";// ternary operator same as if...else


            return (result);

        }


        static Tuple<string, int> MatchingAlgorithm2(string name_one, string name_two)//there are two algorithims so it can work on the manually input data and the file input
        {
            string joined = name_one.ToLower() + "matches" + name_two.ToLower();
            string CheckedChars = "";
            string StrCount = "";
            int TempCount;

            foreach (char c in joined)
            {
                if (CheckedChars.Contains(c) == false)
                {
                    CheckedChars += c;

                    TempCount = joined.Split(c).Length - 1;
                    StrCount += TempCount.ToString();
                }
            }

            string reduced;
            if (StrCount.Length <= 2)
            {
                reduced = StrCount;
            }
            else
            {
                reduced = NumberReduce(StrCount);
                while (reduced.Length > 2)
                {
                    reduced = NumberReduce(reduced);
                }
            }

            int percentage = Int32.Parse(reduced);
            string result = percentage > 80 ? (name_one + " matches " + name_two + " " + reduced + "%, good match!") : name_one + " matches " + name_two + " " + reduced + "%";


            var myTuple = Tuple.Create(result, percentage);
            return (myTuple);

        }





        static Tuple<string, int>[,] MatchCSV(List<string> males, List<string> females)
        {

            Tuple<string, int>[,] ResultsArray = new Tuple<string, int>[males.Count, females.Count]; // this array will store all the results
            int i = 0;
            int j;

            foreach (string boy in males)
            {
                j = 0;

                foreach (string girl in females)
                {
                    Tuple<string, int> result = MatchingAlgorithm2(boy, girl);
                    ResultsArray[i, j] = result;

                    j = j + 1;

                }
                i = i + 1;

            }

            return (ResultsArray);

        }
        static void Main(string[] args)
        {
            //intro and instruction
            Console.WriteLine("Good Match \nPlease follow prompts \n");
            // user inputs info, it's read and checked against the condition to see that it is only letters
            Console.WriteLine("Enter your name: \n");
            string name1 = Console.ReadLine();
            if (name1.All(char.IsLetter))// if the input contains anything other than a letter it will not enter, even a blank space is excluded
            {
                Console.WriteLine("Your name is: " + name1 + "\n");
            }
            else
            {
                while (name1.All(char.IsLetter) != true)
                {
                    Console.WriteLine("Letters only!");
                    Console.WriteLine("Enter your name: \n"); //returns you back to the input promp
                    name1 = Console.ReadLine();
                }

            }

            Console.WriteLine("Your partners name: \n");
            string name2 = Console.ReadLine();
            if (name2.All(char.IsLetter))
            {
                Console.WriteLine("Your partners name is: " + name2 + "\n");
            }
            else
            {
                while (name2.All(char.IsLetter) != true)
                {
                    Console.WriteLine("Letters only!");
                    Console.WriteLine("Your partners name: \n");//same as above
                    name2 = Console.ReadLine();
                }

            }


            string OutputString = MatchingAlgorithm(name1, name2);

            //this is a display of the results of the manually input values 
            Console.WriteLine("Result: \n");
            Console.WriteLine(OutputString);

            List<string> listMales = new List<string>();
            List<string> listFemales = new List<string>();


            string path = @"C:\Users\shriv\Documents\DerivcoAssessment\Good_Match\names.csv";
            try
            {
                using (var reader = new StreamReader(path))
                {
                    //List<string> listMales = new List<string>();
                    //List<string> listFemales = new List<string>();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        listMales.Add(values[0]);
                        listFemales.Add(values[1]);
                        //this if statement compares the value of the gender column before adding 

                        if (values[1] == "m")
                        {
                            listMales.Add(values[0]);

                        }
                        else
                        {
                            listFemales.Add(values[0]);

                        }

                    }
                }
            }
            catch (FileNotFoundException e)

            {
                Console.WriteLine("File location or name is incorrect. Please check the file name and location are correct");
                Console.WriteLine(e.ToString());
            }
            //this will return the new collection of the files contents
            listMales = listMales.Distinct().ToList();
            listFemales = listFemales.Distinct().ToList();

            //this will inverse the data set
            listMales.Sort();
            listMales.Reverse();

            //same as above
            listFemales.Sort();
            listFemales.Reverse();


            Tuple<string, int>[,] MyResultsArr = new Tuple<string, int>[listMales.Count, listFemales.Count];
            MyResultsArr = MatchCSV(listMales, listFemales);


            List<string> ResultString = new List<string>();
            List<int> ResultInt = new List<int>();

            foreach (Tuple<string, int> res in MyResultsArr)
            {
                ResultString.Add(res.Item1);
                ResultInt.Add(res.Item2);

            }

            //numerical arrangement
            var newOrdering = ResultInt
            .Select((Int32, index) => new { Int32, index })
            .OrderBy(item => item.Int32)
            .ToArray();

            //restructure
            ResultString = newOrdering.Select(item => ResultString[item.index]).ToList();

            //inverse
            ResultString.Reverse();

            string filePath = @"C:\Users\shriv\Documents\DerivcoAssessment\Good_Match\output.txt";

            File.WriteAllLines(filePath, ResultString.Select(x => string.Join(",", x)));

            Console.WriteLine("Results will be printed to output.txt \n");//the output.txt file is located in the Debug folder

            Console.WriteLine(String.Join(",\n", ResultString));

        }
    }
}
/*Having only used C# in limited capacity before this has project absolutely battered me,
 I felt like I got into the ring with Mike Tyson. However with the help of My coaches
W3Schools, C-Sharpcorner, Stackoverflow and others i just about managed to produce something that seems
like it works, at least with what I'm currently capable of, but there are some things I could not figure out in time.*/
            
        
    
