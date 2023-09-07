using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using ShareTracking.Model;

namespace ShareTracking.Controller;

public class ScrapeStock
{
    public List<StockData> ScrapeStockData(string html)
    {
        List<StockData> stockList = new List<StockData>();
        string webContent = new WebClient().DownloadString(html);
        
        string trPattern = @"<tr>\s*<td>\s*<a[^>]+>\s*<i[^>]+><\/i><span>([^<]+)<\/span>\s*<\/a>\s*<\/td>\s*<td>([^<]+)<\/td>\s*<td>([^<]+)<\/td>\s*<td>([^<]+)<\/td>\s*<td>([^<]+)<\/td>\s*<td>([^<]+)<\/td>\s*<td>([^<]+)<\/td>\s*<td>([^<]+)<\/td>\s*<\/tr>";
        Regex trRegex = new Regex(trPattern, RegexOptions.Singleline);

        MatchCollection trMatches = trRegex.Matches(webContent);

        foreach (Match trMatch in trMatches)
        {
            StockData stock = new StockData
            {
                Hisse = trMatch.Groups[1].Value,
                Son = trMatch.Groups[2].Value,
                Dün = trMatch.Groups[3].Value,
                Yüzde = trMatch.Groups[4].Value,
                Yüksek = trMatch.Groups[5].Value,
                Düşük = trMatch.Groups[6].Value,
                HacimLot = trMatch.Groups[7].Value,
                HacimTL = trMatch.Groups[8].Value
            };


            stockList.Add(stock);
        }

        return stockList;
    }

   
}