using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using ShareTracking.Model;

namespace ShareTracking.Controller;

public class NewScrapeStock
{
    public List<Stock> ScrapeStockData(string html)
    {
        List<Stock> stockList = new List<Stock>();
        // Replace "yourURL" with the actual URL you want to scrape
        string htmlContent = new WebClient().DownloadString(html);


        string ulPattern = @"<ul class=""live-stock-item item-.*?"" data-symbol=""(.*?)"".*?>(.*?)<\/ul>";
        Regex ulRegex = new Regex(ulPattern, RegexOptions.Singleline);

        MatchCollection ulMatches = ulRegex.Matches(htmlContent);

        foreach (Match ulMatch in ulMatches)
        {
            Stock stock = new Stock
            {
                Name = ulMatch.Groups[1].Value.Trim()
            };

            // Use regex pattern to match information within li tags inside the ul
            string liPattern = @"<li class=""cell\d+.*?"".*?id=""h_td_.*?"">(.*?)<\/li>";
            Regex liRegex = new Regex(liPattern, RegexOptions.Singleline);

            MatchCollection liMatches = liRegex.Matches(ulMatch.Groups[2].Value);

            stock.Last = liMatches[0].Groups[1].Value.Trim();
            stock.Buy = liMatches[1].Groups[1].Value.Trim();
            stock.Sell = liMatches[2].Groups[1].Value.Trim();
            stock.High = liMatches[3].Groups[1].Value.Trim();
            stock.Low = liMatches[4].Groups[1].Value.Trim();
            stock.Average = liMatches[5].Groups[1].Value.Trim();
            stock.Percentage = liMatches[6].Groups[1].Value.Trim();
            stock.VolumeLot = liMatches[11].Groups[1].Value.Trim();
            stock.VolumeTL = liMatches[12].Groups[1].Value.Trim();
            stock.LastDate = liMatches[13].Groups[1].Value.Trim();
            
            stockList.Add(stock);
        }

        return stockList;
    }
}

public class Stock
{
    public string Name { get; set; }
    public string Last { get; set; }
    public string Buy { get; set; }
    public string Sell { get; set; }
    public string High { get; set; }
    public string Low { get; set; }
    public string Average { get; set; }
    public string Percentage { get; set; }
    public string VolumeLot { get; set; }
    public string VolumeTL { get; set; }
    public string LastDate { get; set; }
}