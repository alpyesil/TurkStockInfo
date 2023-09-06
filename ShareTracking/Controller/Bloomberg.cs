using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

namespace ShareTracking.Controller
{
    public class Bloomberg
    {
        private Dictionary<string, Dictionary<string, double>> bloomberg =
            new Dictionary<string, Dictionary<string, double>>();

        private int afterDecimal = 4;
        private string[] items = { "ALIŞ", "SATIŞ", "NET", "EN YÜKSEK", "EN DÜŞÜK", "HACİM (TL)", "HACİM (LOT)" };
        private string bloombergURL = "https://www.bloomberght.com/borsa/hisse/";

        public Bloomberg(string sharing)
        {
            string webContent = new WebClient().DownloadString($"{bloombergURL}{sharing}");

            string divPattern = @"<div class=""detail"">.*?<\/ul>.*?<\/div>";
            string liPattern = @"<li>(.*?)<\/li>";
            string smallPattern = @"<small>(.*?)<\/small>";
            string spanPattern = @"<span>(.*?)<\/span>";

            Regex divRegex = new Regex(divPattern, RegexOptions.Singleline);
            Regex liRegex = new Regex(liPattern, RegexOptions.Singleline);
            Regex smallRegex = new Regex(smallPattern);
            Regex spanRegex = new Regex(spanPattern);

            Match divMatch = divRegex.Match(webContent);

            if (divMatch.Success)
            {
                MatchCollection liMatches = liRegex.Matches(divMatch.Value);

                for (int i = 0; i < liMatches.Count; i++)
                {
                    Match liMatch = liMatches[i];
                    MatchCollection smallMatches = smallRegex.Matches(liMatch.Groups[1].Value);
                    MatchCollection spanMatches = spanRegex.Matches(liMatch.Groups[1].Value);

                    Dictionary<string, double> values = new Dictionary<string, double>();

                    for (int j = 0; j < smallMatches.Count; j++)
                    {
                        string key = smallMatches[j].Groups[1].Value.Trim(':');
                        string value = spanMatches[j].Groups[1].Value;
                        
                        // Temizleme ve ondalık ayıraç işlemi
                        value = value.Replace(".", "").Replace(",", ".");
                        
                        // Büyük değeri işleme
                        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double parsedValue))
                        {
                            values.Add(key, Math.Round(parsedValue, afterDecimal));
                        }
                        else
                        {
                            Console.WriteLine($"Hata: Değer çıkartılamadı - {value}");
                        }
                    }

                    bloomberg.Add($"Data-{i + 1}", values);
                }
            }
            else
            {
                Console.WriteLine("Verileri çıkaramadım.");
            }
        }

        public string JSON()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(bloomberg, Newtonsoft.Json.Formatting.Indented);
        }
    }
}
