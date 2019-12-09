using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using SearchEngineComparison.SearchEnginesFinders;
using SearchEngineComparison.Results;

namespace SearchEngineComparison.CountFinders
{
    public static class CountFinder
    {        
        //Async task that returns a List<Result> object with the results of the searchEngine used
        public static async Task<List<Result>> find(SearchEngine searchEngine, String[] keywords)
        {
            List<Result> results = new List<Result>();
            foreach (var keyword in keywords)
	        {
                var client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.GZip });
                var HTMLcontent = await client.GetStringAsync(searchEngine.Address + keyword);
                Regex rx = new Regex(searchEngine.Expression, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matches = rx.Matches(HTMLcontent);
                string count = "";
		        foreach (Match t in new Regex("\\d", RegexOptions.Compiled | RegexOptions.IgnoreCase).Matches( matches[0].ToString() ) )
		        {
			        count += t.Value;
		        }
                Result result = new Result{ SearchEngineName = searchEngine.Name, Keyword = keyword, Count = long.Parse(count) };
                results.Add(result);
	        }
            return results;
        }
    }
}