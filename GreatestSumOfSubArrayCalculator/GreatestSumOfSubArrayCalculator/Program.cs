using System;
using System.Collections.Generic;

namespace GreatestSumOfSubArrayCalculator
{
	class Program
	{
		static void Main(string[] args)
		{
			/* 
			 * Coded by Jordan Rackley, at github at https://github.com/j-r-development/. Website at http://jordanrackley.dx.am/. 
			 * This is a simple console app that calculates the subarray with the largest sum. This app supports only integers, and arrays of any size.
			 * This app includes 2 initial modes. The first shows the algorithm working on a variety of sample inputs.
			 * The second takes in and validates user input, turns it into a manipulatable data structure of ints (list) and then runs the algorithm.
			 */

			//allow program to run indefinitely without needing to be rebooted between runs
			bool noExitCommand = true;
			while (noExitCommand)
			{

				//declaring for later use
				//using lists because suppor of the add feature makes this a lot easier. similar data structure to array, but easier to manipulate
				List<int> userIntList = new List<int>();
				bool nonValidEntry = true;

				//intro text
				Console.WriteLine("This console app will find the greatest sum of any possible subarray given a comma separated array of integers.");
				Console.WriteLine();


				Console.WriteLine("You can choose to see a sample of default examples, or input your own.");
				Console.WriteLine("Do you want to see the examples? If so, type y or yes. To input your own, press enter or any other input.");
				string seeExamples = Console.ReadLine();

				//showing examples
				if (seeExamples.ToLower() == "y" || seeExamples.ToLower() == "yes")
				{
					//using examples from the original question to demonstrate the algorithm
					List<int> exampleList1 = new List<int> { 1, 2, 3 };
					List<int> exampleList2 = new List<int> { 3, 5, -10, 1, 2, 3, 4 };
					List<int> exampleList3 = new List<int> { 1, 2, 3, 4, -2, 3 };
					List<int> exampleList4 = new List<int> { -2, 1, -3, 4, -1, 2, 1, -5, 4 };

					List<int> example1Subarray = CalculateSubarray(exampleList1);
					List<int> example2Subarray = CalculateSubarray(exampleList2);
					List<int> example3Subarray = CalculateSubarray(exampleList3);
					List<int> example4Subarray = CalculateSubarray(exampleList4);

					//display data
					Console.WriteLine("Here are the examples:");
					Console.WriteLine();

					Console.WriteLine("Example Array 1");
					Console.WriteLine("Starting Array: [1, 2, 3] expected max subarray: 1, 2, 3 \t expected max subarray sum: 6");
					Console.WriteLine("Expected Max Subarray: 1, 2, 3");
					Console.WriteLine("Expected Max Subarray sum: 6");
					Console.WriteLine();
					reportFinalResults(exampleList1.ToArray(), example1Subarray.ToArray(), getSum(example1Subarray));

					Console.WriteLine("Example Array 2");
					Console.WriteLine("Starting Array: [3, 5, -10, 1, 2, 3, 4]");
					Console.WriteLine("Expected Max Subarray:  1, 2, 3, 4");
					Console.WriteLine("Expected Max Subarray sum: 10");
					Console.WriteLine();
					reportFinalResults(exampleList2.ToArray(), example2Subarray.ToArray(), getSum(example2Subarray));

					Console.WriteLine("Example Array 3");
					Console.WriteLine("Starting Array: [1, 2, 3, 4, -2, 3]");
					Console.WriteLine("Expected Max Subarray: 1, 2, 3, 4, -2, 3");
					Console.WriteLine("Expected Max Subarray sum: 11");
					Console.WriteLine();
					reportFinalResults(exampleList3.ToArray(), example3Subarray.ToArray(), getSum(example3Subarray));

					Console.WriteLine("Example Array 4");
					Console.WriteLine("Starting Array: [-2, 1, -3, 4, -1, 2, 1, -5, 4]");
					Console.WriteLine("Expected Max Subarray: 4, -1, 2, 1 ");
					Console.WriteLine("Expected Max Subarray sum: 6");
					Console.WriteLine();
					reportFinalResults(exampleList4.ToArray(), example4Subarray.ToArray(), getSum(example4Subarray));

					Console.WriteLine("Type any input to restart this app, or type x or exit to exit.");
					string exitCommand = Console.ReadLine();
					if (exitCommand.ToLower() == "x" || exitCommand.ToLower() == "exit")
					{
						noExitCommand = false;
					}
					else
					{
						Console.Clear();
					}
				}

				//allowing user to pick their own array
				else
				{
					//validation - must enter number
					while (nonValidEntry)
					{
						//gathering data
						Console.WriteLine("Input your array, separating integers with commas. Example: 4, -6, 20, -2.");
						string userInput = Console.ReadLine();

						//validate for blank input
						if (string.IsNullOrWhiteSpace(userInput))
						{
							userInput = "x";
						}

						//regulate userInput entry by trimming extra white space and homogonizing delimeter
						userInput = userInput.Replace(" ", string.Empty);
						userInput = userInput.Replace(",", ", ");

						//splitting user input into array
						string[] delimeters = { ", " };
						string[] userStringArray = userInput.Split(delimeters, System.StringSplitOptions.RemoveEmptyEntries);
						bool success = true;

						//converting array to ints
						foreach (var input in userStringArray)
						{
							//Console.WriteLine(userInput);
							int intInput;
							string inputTrim = input.Trim();
							Int32.TryParse(inputTrim, out intInput);
							//Console.WriteLine("intInput = " + intInput);
							userIntList.Add(intInput);

							//check TryParse output for fail 
							if (inputTrim != "0" && intInput == 0)
							{
								success = false;
							}
							//Console.WriteLine("success = " + success);

						}

						//bool is false on valid input
						if (success)
						{
							string userIntListString = "";
							foreach (var userInt in userIntList)
							{
								userIntListString += userInt.ToString();
								userIntListString += ", ";
							}
							userIntListString = userIntListString.Trim();
							userIntListString = userIntListString.TrimEnd(',');

							Console.WriteLine("Valid Input. Here is your array. If this is correct, type yes or y. If incorrect, use any other input");
							Console.WriteLine(userIntListString);
							string isItRight = Console.ReadLine();

							//exit clause
							if (isItRight.ToLower() == "y" || isItRight.ToLower() == "yes")
							{
								nonValidEntry = false;
								Console.WriteLine("Input accepted. Press enter to begin calculating the subarray with the highest sum.");
								Console.ReadLine();
							}
							else
							{
								Console.WriteLine("Invalid input detected. Please try again.");
								userIntList.Clear();
								Console.WriteLine();
							}

						}
						else
						{
							Console.WriteLine("Invalid or unconfirmed input. Please only input itengers.");
							Console.WriteLine();
						}
					}

					//call function to test dynamically made list.
					List<int> greatestSumSubarray = CalculateSubarray(userIntList);

					//report findings
					reportFinalResults(userIntList.ToArray(), greatestSumSubarray.ToArray(), getSum(greatestSumSubarray));

					Console.WriteLine("Type any input to restart this app, or type x or exit to exit.");
					string exitCommand = Console.ReadLine();
					if (exitCommand.ToLower() == "x" || exitCommand.ToLower() == "exit")
					{
						noExitCommand = false;
					}
					else
					{
						Console.Clear();
					}
				}
			}
		}

		private static List<int> CalculateSubarray(List<int> userIntList)
		{
			List<int> maxSubArrayList = new List<int>();
			int maxSubArraySum = 0;

			//original method created to check single digit subarray. base for second version that can calculate any size subarray with max value
			/*
			//check for single digit subarray with largest sum
			foreach (var userInt in userIntList)
			{
				if (userInt > maxSubArraySum)
				{
					maxSubArraySum = userInt;
					Console.WriteLine("maxSubArraySum" + maxSubArraySum);

					maxSubArray.Clear();
					maxSubArray.Add(userInt);

					reportResults(userInt, userIntList.ToArray());

				}
			}
			*/


			//supports starting at 1 variable, finds the biggest subarray 
			int variableAmount = 1;
			bool maxSubArrayUnknown = true;

			while (maxSubArrayUnknown)
			{
				//Console.WriteLine("variableAmount: " + variableAmount);
				//Console.WriteLine("userIntList Count: " + userIntList.Count);
				List<int> testIntList = new List<int>();

				//patterns showed that amount of results is always total count in list subtracted by one less than the amount of variables
				//calculating end of the list here means that we don't have to keep track of it in the loop itself. 
				for (int i = 0; i < userIntList.Count - (variableAmount - 1); i++)
				{
					//Console.WriteLine("index: " + i);

					//clear and add values for current list
					testIntList.Clear();
					for (int j = 0; j < variableAmount; j++)
					{
						testIntList.Add(userIntList[i + j]);
						//will add i + 0, i + 1, etc to the list until hits variableAmount
						//when variableAmount is 1, it will only include i + 0
						//only necessary to go left to right, as you'll hit every permutation
					}

					//getSum will calculate sum of array
					int testSum = getSum(testIntList);

					if (testSum > maxSubArraySum)
					{
						maxSubArraySum = testSum;
						//Console.WriteLine("new maxSubArraySum: " + maxSubArraySum);

						maxSubArrayList.Clear();
						foreach (var testInt in testIntList)
						{
							maxSubArrayList.Add(testInt);
						}

						//reportCurrentResults(maxSubArrayList.ToArray(), maxSubArraySum);

					}
				}

				//if less than total count, increment variables
				if (variableAmount < userIntList.Count)
				{
					variableAmount += 1;
				}
				//if equal to total count, current subarray includes all elements and max subarray should be found
				else if (variableAmount == userIntList.Count)
				{
					maxSubArrayUnknown = false;
				}
				//default
				else
				{
					Console.WriteLine("If you are seeing this, there is an error in the logic and the variableamount is exceeding the count, which should never happen.");
				}

			}

			return maxSubArrayList;
		}


		private static void reportCurrentResults(int[] resultArray, int results)
		{
			//report current results
			Console.WriteLine("The maximum sum so far is" + results);
			Console.WriteLine("The maximum subarray so far is" + string.Join("\n", resultArray));
			Console.WriteLine();
		}

		private static void reportFinalResults(int[] originalArray, int[] resultArray, int results)
		{
			//turn arrays into strings for cleaner display
			string originalArrayString = "";
			string resultArrayString = "";

			foreach (var originalInt in originalArray)
			{
				originalArrayString += originalInt.ToString();
				originalArrayString += ", ";
			}

			foreach (var resultInt in resultArray)
			{
				resultArrayString += resultInt.ToString();
				resultArrayString += ", ";
			}

			originalArrayString = originalArrayString.Trim();
			resultArrayString = resultArrayString.Trim();
			originalArrayString = originalArrayString.TrimEnd(',');
			resultArrayString = resultArrayString.TrimEnd(',');

			//report final results
			Console.WriteLine("The subarray with the greatest sum has been found");
			Console.WriteLine("The original array was: " + originalArrayString);
			Console.WriteLine("The subarray with the greatest sum is: " + resultArrayString);
			Console.WriteLine("The sum of this subarray is: " + results);
			Console.WriteLine("Press enter to continue");
			Console.ReadLine();
		}

		private static int getSum(List<int> resultArray)
		{
			//simple addition of subarray elemnts
			int sum = 0;
			foreach (var result in resultArray)
			{
				sum += result;
			}
			return sum;
		}
	}
}
