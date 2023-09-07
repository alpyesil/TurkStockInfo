# Stock and IPO information pulling application for the Turkish region

This application is a tool designed to fetch and display stock and IPO (Initial Public Offering) information in the Turkish region. This README file will provide you with basic instructions on how to install, run, and use the application.

## Features

- View data for all stocks: You can browse and access data for all available stocks in the Turkish region.
- View specific stock: Easily access information for a specific stock by searching for its symbol or name.
- Add favorite stocks: You have the option to add stocks to your favorites list for quick access and tracking.
- Manage favorite stocks: View and manage your favorite stocks, including the ability to edit or remove them as needed.
- Access IPO (Initial Public Offering) list: Retrieve a list of companies that have recently gone public.

These features provide a comprehensive overview of the stock market in the Turkish region and enable you to track your favorite stocks efficiently.

## Prerequisites

- .NET Core SDK 6 (or later)

## Usage

1. Clone this repository:

```bash
git clone https://github.com/alpyesil/TurkStockInfo.git
```

2. Navigate to the project directory:

```bash
cd TurkStockInfo
```

3. Run the application:

```bash
dotnet run
```

## Sample All Stock Output
```bash
The application provides output in the following format:
[
  {
    "Hisse": "ZEDUR",
    "Son": "79,85",
    "Dün": "81,20",
    "Yüzde": "-1,66",
    "Yüksek": "82,85",
    "Düsük": "78,90",
    "HacimLot": "422.566",
    "HacimTL": "33.749.093"
  },
  {
    "Hisse": "ZOREN",
    "Son": "4,85",
    "Dün": "4,83",
    "Yüzde": "0,41",
    "Yüksek": "4,93",
    "Düsük": "4,83",
    "HacimLot": "185.250.826",
    "HacimTL": "903.589.305"
  },
  {
    "Hisse": "ZRGYO",
    "Son": "5,47",
    "Dün": "5,56",
    "Yüzde": "-1,62",
    "Yüksek": "5,61",
    "Düsük": "5,46",
    "HacimLot": "4.574.166",
    "HacimTL": "25.310.093"
  }
]
```


## Sample Specific Stock Output
```bash
The application provides output in the following format:
{
  "Hisse": "KCHOL",
  "Son": "142,10",
  "Dün": "141,90",
  "Yüzde": "0,14",
  "Yüksek": "143,70",
  "Düsük": "141,90",
  "HacimLot": "8.664.209",
  "HacimTL": "1.236.821.437"
}
```


## Sample Favorite Stock Output
```bash
The application provides output in the following format:
{
  "Hisse": "ADEL",
  "Son": "640,20",
  "Dün": "660,00",
  "Yüzde": "-3,00",
  "Yüksek": "670,00",
  "Düsük": "630,00",
  "HacimLot": "283.220",
  "HacimTL": "181.647.171"
},
{
  "Hisse": "ADESE",
  "Son": "2,03",
  "Dün": "2,02",
  "Yüzde": "0,50",
  "Yüksek": "2,06",
  "Düsük": "2,01",
  "HacimLot": "44.261.863",
  "HacimTL": "90.111.607"
}
```

## Sample IPO Output
```bash
The application provides output in the following format:
[
  {
    "IsNew": true,
    "Description": "Talep toplaniyor",
    "IsCompleted": true,
    "Code": "GIPTA",
    "Date": "6-7-8 Eylül 2023"
  },
  {
    "IsNew": true,
    "Description": "",
    "IsCompleted": false,
    "Code": "TARKM",
    "Date": "5-6 Eylül 2023"
  },
  {
    "IsNew": false,
    "Description": "",
    "IsCompleted": false,
    "Code": "EBEBK",
    "Date": "29-31 Agustos, 1 Eylül 2023"
  },
  {
    "IsNew": false,
    "Description": "",
    "IsCompleted": false,
    "Code": "KZGYO",
    "Date": "21-22-23 Agustos 2023"
  },
  {
    "IsNew": false,
    "Description": "",
    "IsCompleted": false,
    "Code": "BYDNR",
    "Date": "15-16 Agustos 2023"
  }
]
```

## Explanation Stock

```bash
'Hisse' (Stock): Stock symbol or name.
'Son' (Last): The last traded price for the stock.
'Dün' (Previous): The closing price of the stock from the previous trading day.
'Yüzde' (Percentage): The percentage change in the stock price compared to the previous closing price.
'Yüksek' (High): The highest price the stock reached during the trading session.
'Düşük' (Low): The lowest price the stock reached during the trading session.
'HacimLot' (Volume in Lots): The trading volume in lots for the stock.
'HacimTL' (Volume in Turkish Lira): The trading volume in Turkish Lira for the stock.
```

## Explanation IPO

```bash
'IsNew': Indicates if the IPO is new or ongoing (true for new, false for ongoing).
'Description': A brief description or status of the IPO.
'IsCompleted': Indicates if the IPO process is completed (true for completed, false for ongoing).
'Code': The code or symbol associated with the IPO.
'Date': The date range when the IPO is scheduled or was completed.
```


## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
