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
            //creates an array out of all the words, and sorts it lexically
            string[] words = File.ReadAllLines(@"c:\Users\connor\Documents\GitHub\BinarySearch\RandomWords.txt", Encoding.UTF8);
            Array.Sort(words);

            //halves the given array, into "first" and "second" halves
            int mid = array.Length / 2;
            string[] first = array.Cast<string>().Take(mid).ToArray();
            string[] second = array.Cast<string>().Skip(mid).ToArray();

            //compares the user's input to the middlemost word of the array
            int LexCompare = String.Compare(input, words[mid]);

            //returns the half of the array that the word is in, based on the comparison result
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
            Console.SetWindowSize(118, 35);
            
            //sets "path" to the location of the text file containing a list of random words
            string path = @"c:\Users\connor\Documents\GitHub\BinarySearch\RandomWords.txt";
            string restart = "y";

            //creates an array out of all the words, and sorts it lexically
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
                        //prints the word, as well as it's index to the left of it when the user selects choice 1
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

                    //creates an array called "halved" from the returned array of "HalfArray"
                    Array halved = HalfArray(words, input);
                    //converts this Array to type string[]
                    string[] HalvedArray = halved.OfType<object>().Select(word => word.ToString()).ToArray();
                    //keeps track of how big the array is
                    int HalvedArrayCount = HalvedArray.Count();

                    //the search will stop when there is only one item left in the array
                    while (HalvedArrayCount != 1)
                    {
                        //continuously repeats the last 3 steps until there is only one item left in the array
                        halved = HalfArray(HalvedArray, input);
                        HalvedArray = halved.OfType<object>().Select(word => word.ToString()).ToArray();
                        HalvedArrayCount = HalvedArray.Count();

                        //checks if the word entered is in the list
                        if (Array.IndexOf(words, input) == -1)
                        {
                            //if no, then it allows the user to add it to the list of words
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
                            //if yes, prints the index of the word
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
                    //keeps track of the max index of the array
                    int MaxIndex = words.Length - 1;

                    int repeat = 1;
                    while (repeat == 1)
                    {
                        Console.Write("Please enter the index of the word you wish to find. (0-" + MaxIndex + "): ");
                        string input = Console.ReadLine();
                        int IntInput = System.Convert.ToInt32(input);

                        //checks whether the index entered is within range
                        if (IntInput <= MaxIndex && IntInput >= 0)
                        {
                            //if yes, prints the word
                            Console.WriteLine("The word with index [" + IntInput + "] is " + words[IntInput] + ".");
                            repeat--;
                        }
                        else
                        {
                            //if no, tells the user that it is not within range, and asks again
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
