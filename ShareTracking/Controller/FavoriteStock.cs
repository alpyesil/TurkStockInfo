using ShareTracking.Helper;
using ShareTracking.Model;

namespace ShareTracking.Controller;

public class FavoriteStock
{
    public string AddFavoriteStock(StockData stockData)
    {
        if (System.IO.File.Exists("Hisse/FavoriteStock.json"))
        {
            List<StockData> stockList = GetAllFavoriteStock();

            if (stockList.Find(x => x.Hisse == stockData.Hisse) != null)
                return "Bu hisse zaten favorilerde.";

            stockList.Add(stockData);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(stockList, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText("Hisse/FavoriteStock.json", json);
            return $"{stockData.Hisse} hissesi favorilere eklendi.";
        }
        else
        {
            List<StockData> stockList = new();
            stockList.Add(stockData);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(stockList, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText("Hisse/FavoriteStock.json", json);

            return $"{stockData.Hisse} hissesi favorilere eklendi.";
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

    public string DeleteStock(string name)
    {
        if (System.IO.File.Exists("Hisse/FavoriteStock.json") == false)
            return "Favori hisse bulunamadı.";


        string json = System.IO.File.ReadAllText("Hisse/FavoriteStock.json");
        List<StockData> stockList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StockData>>(json);
        StockData stockData = stockList.Find(x => x.Hisse == name);

        if (stockData == null)
            return "Favori hisse bulunamadı.";


        stockList.Remove(stockData);

        json = Newtonsoft.Json.JsonConvert.SerializeObject(stockList, Newtonsoft.Json.Formatting.Indented);
        System.IO.File.WriteAllText("Hisse/FavoriteStock.json", json);

        return $"{name} hissesi favorilerden kaldırıldı.";
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


    public string AddBistOneHundredFavoriteStock(StockData stockData)
    {
        if (System.IO.File.Exists("BistOneHundred/BistFavorites/bistFavorites.json"))
        {
            List<StockData> stockList = GetAllBistOneHundredFavoriteStock();

            if (stockList.Find(x => x.Hisse == stockData.Hisse) != null)
                return "Bu hisse zaten favorilerde.";

            stockList.Add(stockData);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(stockList, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText("BistOneHundred/BistFavorites/bistFavorites.json", json);
            return $"{stockData.Hisse} hissesi favorilere eklendi.";
        }
        else
        {
            List<StockData> stockList = new();
            stockList.Add(stockData);

            FindPath findPath = new FindPath();
            var localPath = findPath.GetBistFavoritesPath();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(stockList, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(localPath, json);

            return $"{stockData.Hisse} hissesi favorilere eklendi.";
        }
    }

    public List<StockData> GetAllBistOneHundredFavoriteStock()
    {
        if (System.IO.File.Exists("BistOneHundred/BistFavorites/bistFavorites.json") == false)
            return new List<StockData>();

        string json = System.IO.File.ReadAllText("BistOneHundred/BistFavorites/bistFavorites.json");
        List<StockData> stockList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StockData>>(json);

        return UpdateBistOneHundredFavoriteStockValue(stockList);
    }

    public string DeleteBistOneHundredStock(string name)
    {
        if (System.IO.File.Exists("BistOneHundred/BistFavorites/bistFavorites.json") == false)
            return "Favori hisse bulunamadı.";


        string json = System.IO.File.ReadAllText("BistOneHundred/BistFavorites/bistFavorites.json");
        List<StockData> stockList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StockData>>(json);
        StockData stockData = stockList.Find(x => x.Hisse == name);

        if (stockData == null)
            return "Favori hisse bulunamadı.";


        stockList.Remove(stockData);

        json = Newtonsoft.Json.JsonConvert.SerializeObject(stockList, Newtonsoft.Json.Formatting.Indented);
        System.IO.File.WriteAllText("BistOneHundred/BistFavorites/bistFavorites.json", json);

        return $"{name} hissesi favorilerden kaldırıldı.";
    }

    public List<StockData> UpdateBistOneHundredFavoriteStockValue(List<StockData> stockList)
    {
        ScrapeStock scrapeStock = new ScrapeStock();

        string localPath = new FindPath().GetFindPath();

        List<StockData> arrangeStockList = new List<StockData>();

        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVYZ";

        foreach (char letter in alphabet)
        {
            string sharing = letter.ToString();
            string url = $"https://www.bloomberght.com/borsa/hisseler/bist-100-endeksi/{sharing}";

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

        System.IO.File.WriteAllText("BistOneHundred/BistFavorites/bistFavorites.json", json);

        return stockList;
    }
}