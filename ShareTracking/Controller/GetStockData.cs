using Newtonsoft.Json;
using ShareTracking.Helper;
using ShareTracking.Model;

namespace ShareTracking.Controller;

public class GetStock
{
    public StockData GetStockData(string hisse)
    {
        FindPath findPath = new FindPath();
        
        string localPath = findPath.GetFindPath();

        if(File.Exists(localPath))
        {
            string json = File.ReadAllText(localPath);
            List<StockData> stockList = JsonConvert.DeserializeObject<List<StockData>>(json);
            StockData stockData = stockList.Find(x => x.Hisse == hisse);
            return stockData;
        }
        else
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVYZ";
            List<StockData> arrangeStockList = new List<StockData>();
            
            foreach (char letter in alphabet)
            {
                string sharing = letter.ToString();
                string url = $"https://www.bloomberght.com/borsa/hisseler/{sharing}";

                ScrapeStock scraper = new ScrapeStock();
                List<StockData> stockList = scraper.ScrapeStockData(url);
                arrangeStockList.AddRange(stockList);
            }
            
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(arrangeStockList, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(localPath, json);

            StockData stockData = arrangeStockList.Find(x => x.Hisse == hisse);
            return stockData;
        }
    }
}