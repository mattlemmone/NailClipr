using EliteMMO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NailClipr.Classes
{
    class FFXIAH
    {
        public static List<FFXIAH.Sale> sales = new List<FFXIAH.Sale>();
        public const string baseUrl = "http://www.ffxiah.com/item/";
        public const int maxSales = 5;

        public class Item
        {
            public bool isStack;
            public int stock;
        }
        public class Sale
        {
            public string
            date,
            seller,
            buyer;
            public int price;
        }
        public class RegExs
        {
            public static List<string> list = new List<string>(new string[] {
                    date, seller, buyer, price
                });
            public const string
            itemSale = @"Item.sales = \[\{(?<match>.*)\}\]",
            date = "\"saleon\":(?<match>[0-9]+)",
            price = "\"price\":(?<match>[0-9]+)",
            seller = "\"seller_name\":\"(?<match>[A-z]+)\"",
            buyer = "\"buyer_name\":\"(?<match>[A-z]+)\"",
            server = "\"seller_server\":(?<match>[0-9]+)",
            stock = @"<span class=stock>(?<match>[0-9]+)<\/span>",
            name = @"<title>(?<match>[A-z\s]+)";
        }

        public static string GetHTML(string url, int server)
        {
            WebClient client = new WebClient();
            {
                client.Headers.Add(HttpRequestHeader.Cookie, "sid=" + server);
                return client.DownloadString(url);
            }
        }
        public static void PrintSales(EliteAPI api, Structs.InventoryItem invItem, Item item, int numSales)
        {
            var stackStr = item.isStack ? " x" + invItem.stackSize : "";
            string sep = "---------------------------";

            Chat.SendEcho(api, sep);
            Chat.SendEcho(api, invItem.name + stackStr + " (" + invItem.id + ")" + " @ " + item.stock + " in stock.");
            if (numSales == 0)
            {
                Chat.SendEcho(api, "No sales found.");
                Chat.SendEcho(api, sep);
                return;
            }
            foreach (var sale in sales)
            {

                Chat.SendEcho(api,
                    sale.date + " | " + sale.seller + " --> " + sale.buyer + " @ " + sale.price
                    );
            }
            Chat.SendEcho(api, sep);
        }
    }
}

