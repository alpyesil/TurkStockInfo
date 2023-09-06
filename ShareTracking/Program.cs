// See https://aka.ms/new-console-template for more information

using ShareTracking.Controller;
using ShareTracking.Helper;
using ShareTracking.Model;

namespace ShareTracking
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }


        public static void AllSharing()
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVYZ";

            FindPath findPath = new FindPath();

            string localPath = findPath.GetFindPath();  
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
            Console.WriteLine(json);
            
            Menu();
        }

        public static void GetSharing()
        {
            Console.WriteLine("Hisse senedi kodunu giriniz: ");
            string sharing = Console.ReadLine();
            GetStock getStock = new GetStock();
            StockData data = getStock.GetStockData(sharing);
            
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(json);

            Console.WriteLine("");
            Console.WriteLine("Favori hisse eklemek için 1'e basınız.");
            string choice = Console.ReadLine();
            
            if (choice == "1")
            {
                FavoriteStock favoriteStock = new FavoriteStock();
                favoriteStock.AddFavoriteStock(data);
            }
            
            Menu();
        }
        
        public static void ShowFavoriteStock()
        {
            FavoriteStock favoriteStock = new FavoriteStock();
            List<StockData> stockList = favoriteStock.GetAllFavoriteStock();
            
            foreach (var stock in stockList)
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(stock, Newtonsoft.Json.Formatting.Indented);
                Console.WriteLine(json);
            }
            
            Menu();
        }

        public static void DeleteFavoriStock()
        {
            FavoriteStock favoriteStock = new FavoriteStock();
            ShowFavoriteStock();
            Console.WriteLine("");
            Console.WriteLine("Silmek istediğiniz hisse senedi kodunu giriniz: ");
            string name = Console.ReadLine();
            
            favoriteStock.DeleteStock(name);
        }

        public static void Menu()
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ 1.Tüm hisse senetlerinin verilerini öğrenmek için 1'e basınız. ║");
            Console.WriteLine("║ 2.Öğrenmek istediğiniz spesifik hisse senedi için 2'ye basınız.║");
            Console.WriteLine("║ 3.Favori hisselerir görmek için 3'e basınız.                   ║");
            Console.WriteLine("║ 4.Favori hisse silmek için 4'e basınız.                        ║");
            Console.WriteLine("║ 5.Çıkış.                                                       ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
            Console.WriteLine("");
            Console.Write("Seçiminiz: ");


            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AllSharing();
                    break;
                case 2:
                    GetSharing();
                    break;
                case 3:
                    ShowFavoriteStock();
                    break;
                case 4:
                    DeleteFavoriStock();
                    break;
                case 5:
                    // Çıkış
                    Environment.Exit(0);
                    break;
            }
        }
    }
}