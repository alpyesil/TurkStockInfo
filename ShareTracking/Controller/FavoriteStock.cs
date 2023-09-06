using ShareTracking.Helper;
using ShareTracking.Model;

namespace ShareTracking.Controller;

public class FavoriteStock
{
    public void AddFavoriteStock(StockData stockData)
    {
        if (System.IO.File.Exists("Hisse/FavoriteStock.json"))
        {
            List<StockData> stockList = GetAllFavoriteStock();
            
            stockList.Add(stockData);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(stockList, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText("Hisse/FavoriteStock.json", json);
            return;
        }
        else
        {
            string json = System.IO.File.ReadAllText("Hisse/FavoriteStock.json");
            List<StockData> stockList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StockData>>(json);
        }
    }

    public List<StockData> GetAllFavoriteStock()
    {
        if (System.IO.File.Exists("Hisse/FavoriteStock.json") == false)
            return new List<StockData>();
        
        string json = System.IO.File.ReadAllText("Hisse/FavoriteStock.json");
        List<StockData> stockList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StockData>>(json);
        
        return UpdateFavoriteStockValue(stockList);
    }

    public void DeleteStock(string name)
    {
        if (System.IO.File.Exists("Hisse/FavoriteStock.json") == false)
            return;
        string json = System.IO.File.ReadAllText("Hisse/FavoriteStock.json");
        List<StockData> stockList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StockData>>(json);
        StockData stockData = stockList.Find(x => x.Hisse == name);
        stockList.Remove(stockData);

        json = Newtonsoft.Json.JsonConvert.SerializeObject(stockList, Newtonsoft.Json.Formatting.Indented);
        System.IO.File.WriteAllText("Hisse/FavoriteStock.json", json);
        
    }

    public List<StockData> UpdateFavoriteStockValue(List<StockData> stockList)
    {
        ScrapeStock scrapeStock = new ScrapeStock();

        string localPath = new FindPath().GetFindPath();
        
        List<StockData> arrangeStockList = new List<StockData>();
        
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVYZ";
        
        foreach (char letter in alphabet)
        {
            string sharing = letter.ToString();
            string url = $"https://www.bloomberght.com/borsa/hisseler/{sharing}";

            ScrapeStock scraper = new ScrapeStock();
            List<StockData> stockL2ist = scraper.ScrapeStockData(url);
            arrangeStockList.AddRange(stockL2ist);
        }

        foreach (StockData stockData in stockList)
        {
            StockData newStockData = arrangeStockList.Find(x => x.Hisse == stockData.Hisse);
            stockData.Son = newStockData.Son;
            stockData.Dün = newStockData.Dün;
            stockData.Yüzde = newStockData.Yüzde;
            stockData.Yüksek = newStockData.Yüksek;
            stockData.Düşük = newStockData.Düşük;
            stockData.HacimLot = newStockData.HacimLot;
            stockData.HacimTL = newStockData.HacimTL;
        }

        string json = Newtonsoft.Json.JsonConvert.SerializeObject(stockList, Newtonsoft.Json.Formatting.Indented);

        System.IO.File.WriteAllText("Hisse/FavoriteStock.json", json);
        
        return stockList;
    }
}