using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace SearchEngineComparison.SearchEnginesFinders
{
    public class SearchEngine
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Expression { get; set; }

        //Function for returning the name, address(url) and search expression for search engines
        public static List<SearchEngine> getSearchEngines(){
            //Declaring a list of search engines availables for search performing
            List<SearchEngine> searchEngines = new List<SearchEngine>();
            searchEngines.Add(new SearchEngine{Name = "Bing", Address = "https://www.bing.com/search?q=", Expression = "(?<=<span class=\"sb_count\"(.*)>)(.*?)(?=<\\/span>)"});
            searchEngines.Add(new SearchEngine{Name = "Sogou", Address = "https://www.sogou.com/web?query=", Expression = "(?<=<p class=\"num-tips\"(.*)>)(.*?)(?=<\\/p>)"});
            
            return searchEngines;
        }
    }
}