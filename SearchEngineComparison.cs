using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net.Http;
using SearchEngineComparison.Results;

namespace SearchEngineComparison
{
    class SearchEngineComparison
    {
        static void Main(string[] args)
        {
            try
            {       
                //If there is any argument when the .exe is called the program begins
                if (args.Length > 0)
	            {
                    Result.searchAndPrintResults(args);
	            }
                else
	            {
                    Console.Write("You have to provide at least one argument");
	            }
            }
            catch(Exception)
            {
                Console.Write("There was an error.");
            }
            Console.ReadKey();
        }
    }
}

