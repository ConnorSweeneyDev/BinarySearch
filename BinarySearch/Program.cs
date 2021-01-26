using System;
using System.IO;
using System.Text;
using System.Linq;

namespace BinarySearch
{
    class Program
    {
        static Array HalfArray(Array array, string input)
        {
            string[] words = File.ReadAllLines(@"c:\Users\connor\Documents\GitHub\BinarySearch\RandomWords.txt", Encoding.UTF8);
            Array.Sort(words);

            int mid = array.Length / 2;
            string[] first = array.Cast<string>().Take(mid).ToArray();
            string[] second = array.Cast<string>().Skip(mid).ToArray();

            int LexCompare = String.Compare(input, words[mid]);

            if (LexCompare == -1)
            {
                return first;
            }
            else
            {
                return second;
            }
        }

        static void Main()
        {
            string path = @"c:\Users\connor\Documents\GitHub\BinarySearch\RandomWords.txt";
            string restart = "y";

            string[] words = File.ReadAllLines(path, Encoding.UTF8);
            Array.Sort(words);

            while (restart == "y")
            {
                Console.Write("Do you want to display the list of words (1), find a word's index (2) or find a word using it's index (3)? (1/2/3): ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "1")
                {
                    foreach (var word in words)
                    {
                        Console.Write("[" + Array.IndexOf(words, word) + "]" + " ");
                        Console.WriteLine(word);
                    }
                    Console.WriteLine();

                    Console.Write("Do you want to try another option? (y/n): ");
                    restart = Console.ReadLine();
                }
                else if (choice == "2")
                {
                    Console.Write("Please enter the word you would like to find the index of: ");
                    string input = Console.ReadLine();

                    Array halved = HalfArray(words, input);
                    string[] HalvedArray = halved.OfType<object>().Select(word => word.ToString()).ToArray();
                    int HalvedArrayCount = HalvedArray.Count();

                    while (HalvedArrayCount != 1)
                    {
                        halved = HalfArray(HalvedArray, input);
                        HalvedArray = halved.OfType<object>().Select(word => word.ToString()).ToArray();
                        HalvedArrayCount = HalvedArray.Count();

                        if (Array.IndexOf(words, input) == -1)
                        {
                            Console.WriteLine("the word '" + input + "' is not in the file.");
                            Console.Write("Do you want to add it to the list? (y/n): ");
                            string add = Console.ReadLine();

                            if (add == "y")
                            {
                                TextWriter tsw = new StreamWriter(path, true);
                                tsw.WriteLine();
                                tsw.Write(input);
                                tsw.Close();
                                Console.WriteLine("The word has been added.");
                            }
                            else
                            {
                                Console.WriteLine("The word has not been added.");
                            }
                            Console.WriteLine();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("The word '" + input + "' has an index of [" + Array.IndexOf(words, input) + "].");
                            Console.WriteLine();
                            break;
                        }
                    }

                    Console.Write("Do you want to try another option? (y/n): ");
                    restart = Console.ReadLine();
                }
                else
                {
                    int MaxIndex = words.Length - 1;

                    int repeat = 1;
                    while (repeat == 1)
                    {
                        Console.Write("Please enter the index of the word you wish to find. (0-" + MaxIndex + "): ");
                        string input = Console.ReadLine();
                        int IntInput = System.Convert.ToInt32(input);

                        if (IntInput <= MaxIndex)
                        {
                            Console.WriteLine("The word with index [" + IntInput + "] is " + words[IntInput] + ".");
                            repeat--;
                        }
                        else
                        {
                            Console.WriteLine("That value is not within the specified range.");
                        }
                        Console.WriteLine();
                    }
                    Console.Write("Do you want to try another option? (y/n): ");
                    restart = Console.ReadLine();
                }
            }
        }
    }
}