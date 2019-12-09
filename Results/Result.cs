using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using SearchEngineComparison.SearchEnginesFinders;
using SearchEngineComparison.CountFinders;

namespace SearchEngineComparison.Results
{
    //Class for returning the search engine, keyword and count of hits result
    public class Result
    {
        public string SearchEngineName { get; set; }
        public string Keyword { get; set; }
        public long Count { get; set; }
        
        public static async void searchAndPrintResults(string[] keywords)
        {
            //Get all registered search engines
            var searchEngines = SearchEngine.getSearchEngines();
            List<Result> results = new List<Result>();
            foreach (var searchEngine in searchEngines)
            {
                List<Result> res = await CountFinder.find(searchEngine, keywords);           
                foreach (var group in res)
                {
                    results.Add(group);
                }
            }
            //Print search result by keyword
            printSearchResults(results, searchEngines, keywords);
            //Print winner by search engine
            printWinnerBySearchEngine(results, searchEngines, keywords);
            //Print total winner
            printTotalWinner(results, searchEngines, keywords);            
        }

        private static void printSearchResults(List<Result> results, List<SearchEngine> searchEngines, string[] keywords){
            foreach (var keyword in keywords)
            {
                Console.Write(keyword + ": ");
                List<Result> keywordResults = results.FindAll(s => s.Keyword.Contains(keyword));
                foreach (var item in keywordResults)
                {
                    Console.Write("{0}: {1} ", item.SearchEngineName, item.Count);
                }
                Console.WriteLine();
            }
        }

        private static void printWinnerBySearchEngine(List<Result> results, List<SearchEngine> searchEngines, string[] keywords){
            foreach (var searchEngine in searchEngines)
            {                
                List<Result> searchEngineResults = results.FindAll(s => s.SearchEngineName.Contains(searchEngine.Name));
                searchEngineResults.Sort((a, b) => -1* a.Count.CompareTo(b.Count));
                Console.WriteLine(searchEngine.Name + " winner: " + searchEngineResults[0].Keyword);
            }
        }

        private static void printTotalWinner(List<Result> results, List<SearchEngine> searchEngines, string[] keywords){
            long maxCount = new long();
            string totalWinner = "";
            foreach (var keyword in keywords)
            {     
                long resultCount = new long();
                List<Result> maxCountResults = results.FindAll(s => s.Keyword.Contains(keyword));
                foreach (var countSearch in maxCountResults)
	            {
                    resultCount += countSearch.Count;
                    if (resultCount > maxCount)
	                {
                        maxCount = resultCount;
                        totalWinner = keyword;
	                }
	            }                
            }
            Console.WriteLine("Total winner: " + totalWinner);
        }        
    }
}