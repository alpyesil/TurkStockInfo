using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ShareTracking.Helper;

public class StockListingInfo
{
    public string ScrapeStockData(string html)
    {
        if (File.Exists($"StockListing/{DateTime.Today}.json"))
        {
            string asd =
                File.ReadAllText(
                    $"StockListing/{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json");
            return asd;
        }


        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = web.Load(html);

        List<StockInfo> stockList = new List<StockInfo>();

        var liNodes = doc.DocumentNode.SelectNodes("//li/article");

        if (liNodes != null)
        {
            var count = 0;
            string ttname = "";
            foreach (var articleNode in liNodes)
            {
                var ilBadgeNode = articleNode.SelectSingleNode(".//div[contains(@class, 'il-badge')]");
                bool isNew = ilBadgeNode.SelectSingleNode(".//div[contains(@class, 'il-new')]") != null;

                var ttNode = ilBadgeNode.SelectSingleNode(".//div[contains(@class, 'il-tt')]");
                bool isCompleted = ttNode != null && ttNode.InnerText.Trim() == "Talep toplanıyor";

                if (isCompleted)
                {
                    ttname = "Talep toplanıyor";
                }
                else
                {
                    ttname = "";
                }


                var bistKodNode = articleNode.SelectSingleNode(".//span[@class='il-bist-kod']");
                string bistKod = bistKodNode.InnerText.Trim();

                var tarihNode = articleNode.SelectSingleNode(".//span[@class='il-halka-arz-tarihi']/time");
                string tarih = tarihNode.Attributes["datetime"].Value;

                StockInfo stock = new StockInfo
                {
                    IsNew = isNew,
                    Description = ttname,
                    IsCompleted = isCompleted,
                    Code = bistKod,
                    Date = tarih
                };

                count++;

                stockList.Add(stock);

                if (count == 10)
                {
                    break;
                }
            }
        }

        var json = JsonConvert.SerializeObject(stockList, Formatting.Indented);

        FindPath findPath = new FindPath();

        var localPath = findPath.GetStockListingPath();

        System.IO.File.WriteAllText(localPath, json);

        return json;
    }
}

public class StockInfo
{
    public bool IsNew { get; set; }

    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public string Code { get; set; }
    public string Date { get; set; }
}