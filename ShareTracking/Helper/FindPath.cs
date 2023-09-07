namespace ShareTracking.Helper;

public class FindPath
{
    public string GetFindPath()
    {
        PlatformID platform = Environment.OSVersion.Platform;

        if (!Directory.Exists("Hisse"))
        {
            Directory.CreateDirectory("Hisse");
        }

        return $"Hisse/hisse.json";
    }
    
    public string GetStockListingPath()
    {
        PlatformID platform = Environment.OSVersion.Platform;

        if (!Directory.Exists("StockListing"))
        {
            Directory.CreateDirectory("StockListing");
        }

        return $"StockListing/{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";
    }
    
    public string GetBistOneHundredPath()
    {
        PlatformID platform = Environment.OSVersion.Platform;

        if (!Directory.Exists("BistOneHundred"))
        {
            Directory.CreateDirectory("BistOneHundred");
        }

        return $"BistOneHundred/bist100.json";
    }
    
    public string GetBistFavoritesPath()
    {
        PlatformID platform = Environment.OSVersion.Platform;

        if (!Directory.Exists("BistOneHundred/BistFavorites"))
        {
            Directory.CreateDirectory("BistOneHundred/BistFavorites");
        }

        return $"BistOneHundred/BistFavorites/bistFavorites.json";
    }
}