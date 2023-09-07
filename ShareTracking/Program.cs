// See https://aka.ms/new-console-template for more information

using ShareTracking.Controller;
using ShareTracking.Helper;
using ShareTracking.Model;

namespace ShareTracking
{
    class Program
    {
        private static string halkaArzUrl = "https://halkarz.com/";

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

            string json =
                Newtonsoft.Json.JsonConvert.SerializeObject(arrangeStockList, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(localPath, json);
            Console.Clear();
            Console.WriteLine(json);

            InAppMenu();
        }

        public static void GetSharing()
        {
            Console.Clear();
            Console.WriteLine("Hisse senedi kodunu giriniz: ");
            Console.Write("KODUNUZ: ");
            string sharing = Console.ReadLine();
            GetStock getStock = new GetStock();
            StockData data = getStock.GetStockData(sharing);

            if (data == null)
            {
                Console.Clear();
                Console.WriteLine("Hisse senedi bulunamadı.");
                InAppMenu();
            }

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
            
            
            Console.Clear();
            Console.WriteLine(json);

            Console.WriteLine("");
            Console.WriteLine("Favori hisse eklemek için 1'e basınız.");
            Console.Write("Seçiminiz:");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                FavoriteStock favoriteStock = new FavoriteStock();
                
                var response = favoriteStock.AddFavoriteStock(data);
                Console.Clear();
                Console.WriteLine(response);
                Console.WriteLine();
            }
            
            InAppMenu();
        }

        public static void ShowFavoriteStock()
        {
            Console.Clear();
            FavoriteStock favoriteStock = new FavoriteStock();
            List<StockData> stockList = favoriteStock.GetAllFavoriteStock();

            if (stockList.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Favori hisse bulunamadı.");
                InAppMenu();
            }

            Console.Clear();
            foreach (var stock in stockList)
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(stock, Newtonsoft.Json.Formatting.Indented);
                Console.WriteLine(json);
            }

            InAppMenu();
        }


        private static void InAppMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("Ana menüye dönmek için 1'e basınız.");
            Console.WriteLine("Çıkış yapmak için 2'ye basınız.");
            Console.Write("Seçiminiz: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            EnterMenu(choice);
        }

        private static void EnterMenu(int choice)
        {
            switch (choice)
            {
                case 1:
                    Menu();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
            }
            Console.Clear();
        }


        public static void DeleteFavoriStock()
        {
            FavoriteStock favoriteStock = new FavoriteStock();
            Console.WriteLine("");
            Console.WriteLine("Silmek istediğiniz hisse senedi kodunu giriniz: ");
            Console.Write("KODUNUZ: ");
            string name = Console.ReadLine();

            var response = favoriteStock.DeleteStock(name);

            Console.WriteLine(response);
            InAppMenu();
        }

        public static void ShowStockListingInfo()
        {
            StockListingInfo stockListingInfo = new StockListingInfo();
            string json = stockListingInfo.ScrapeStockData(halkaArzUrl);
            Console.Clear();
            Console.WriteLine(json);
            Console.WriteLine("");
            InAppMenu();
        }

        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ 1.Tüm hisse senetlerinin verilerini öğrenmek için 1'e basınız. ║");
            Console.WriteLine("║ 2.Öğrenmek istediğiniz spesifik hisse senedi için 2'ye basınız.║");
            Console.WriteLine("║ 3.Favori hisselerir görmek için 3'e basınız.                   ║");
            Console.WriteLine("║ 4.Favori hisse silmek için 4'e basınız.                        ║");
            Console.WriteLine("║ 5.Halka Arzları Listelemek için 5'e basınız.                   ║");
            Console.WriteLine("║ 6.Çıkış.                                                       ║");
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
                    ShowStockListingInfo();
                    break;
                case 6:
                    // Çıkış
                    Environment.Exit(0);
                    break;
            }

            Console.Clear();
        }
    }
}