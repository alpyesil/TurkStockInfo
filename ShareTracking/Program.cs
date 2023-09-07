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

        private static void Menu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ 1.Bist 100 menü girişi için 1'e basınız.                       ║");
            Console.WriteLine("║ 2.Bist TUM menü girişi için 2'ye basınız.                      ║");
            Console.WriteLine("║ 6.Çıkış.                                                       ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
            Console.Write("Seçiminiz: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            EnterMenu(choice);
        }

        private static void EnterMenu(int choice)
        {
            switch (choice)
            {
                case 1:
                    BistOneHundred bistOneHundred = new BistOneHundred();
                    bistOneHundred.BistOneHundredMenu();
                    break;
                case 2:
                    BistStock bistStock = new BistStock();
                    bistStock.Menu();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }

            Console.Clear();
        }

        public void NonStaticMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ 1.Bist 100 menü girişi için 1'e basınız.                       ║");
            Console.WriteLine("║ 2.Bist TUM menü girişi için 2'ye basınız.                      ║");
            Console.WriteLine("║ 6.Çıkış.                                                       ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
            Console.Write("Seçiminiz: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            EnterMenu(choice);
        }
        
    }
}