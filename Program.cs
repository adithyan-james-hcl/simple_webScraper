using System;
using CsvHelper;
using HtmlAgilityPack;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

namespace simple_web_scraper
{
    class Program
    {
        static void Main(string[] args)
        {


            string[] url = new string[2] { "https://www.mcdonalds.com/ca/en-ca/full-menu/beef.html", "https://www.mcdonalds.com/ca/en-ca/full-menu/breakfast.html" };
            string class_name = "categories-item-name";
            scraper(url[1], class_name,"mcd.csv");  


        }


        public static void scraper( string url, string class_name)
        {

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            var HeaderNames = doc.DocumentNode.SelectNodes("//span[@class='" + class_name + "']");

            
            foreach (var item in HeaderNames)
            {
                
                Console.WriteLine(item.InnerText);
            }


        }

        public static void scraper(string url, string class_name, string filename)
        {

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            var writer = new StreamWriter(filename);

            var HeaderNames = doc.DocumentNode.SelectNodes("//span[@class='" + class_name + "']");
         
            var titles = new List<Row>();
            foreach (var item in HeaderNames)
            {
                titles.Add(new Row { Title = item.InnerText });

            }
           
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(titles);
            }

        }


        public class Row
        {
            public string Title { get; set; }
        }
    }
}
 