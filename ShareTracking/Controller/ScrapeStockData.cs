using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using ShareTracking.Model;

namespace ShareTracking.Controller;

public class ScrapeStock
{
    public  List<StockData> ScrapeStockData(string html)
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
                Son = Convert.ToDouble(trMatch.Groups[2].Value),
                Dün = Convert.ToDouble(trMatch.Groups[3].Value),
                Yüzde = double.Parse(trMatch.Groups[4].Value.Replace(",", ".").Replace("%", ""), CultureInfo.InvariantCulture),

                Yüksek = Convert.ToDouble(trMatch.Groups[5].Value),
                Düşük = Convert.ToDouble(trMatch.Groups[6].Value),
                HacimLot = double.Parse(trMatch.Groups[7].Value.Replace(".", "").Replace(",", "."), CultureInfo.InvariantCulture),
                HacimTL = double.Parse(trMatch.Groups[8].Value.Replace(".", "").Replace(",", "."), CultureInfo.InvariantCulture)
            };


            stockList.Add(stock);
        }

        return stockList;
    }
}