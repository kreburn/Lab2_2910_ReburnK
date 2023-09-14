using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_2910_ReburnK
{
    internal class VideoGames : IComparable<VideoGames>
    {
        public string Name { get; set; }
        public string Platform { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public double NA_Sales { get; set; }
        public double EU_Sales { get; set; }
        public double JP_Sales { get; set; }
        public double Other_Sales { get; set; }
        public double Global_Sales { get; set; }

        public VideoGames() { }

        public VideoGames(string name, string platform, int year, string genre, string publisher, double naSales, double euSales, double jpSales, double otherSales, double globalSales)
        {
            Name = name;
            Platform = platform;
            Year = year;
            Genre = genre;
            Publisher = publisher;
            NA_Sales = naSales;
            EU_Sales = euSales;
            JP_Sales = jpSales;
            Other_Sales = otherSales;
            Global_Sales = globalSales;
        }

        public int CompareTo(VideoGames? other)
        {
            return Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            string msg = "";

            msg += $"Name: {this.Name}\n";
            msg += $"Platform: {this.Platform}\n";
            msg += $"Year: {this.Year}\n";
            msg += $"Genre: {this.Genre}\n";
            msg += $"Publisher: {this.Publisher} \n";
            /*msg += $"NA Sales: {this.NA_Sales}\n";
            msg += $"EU Sales: {this.EU_Sales}\n";
            msg += $"JP Sales: {this.JP_Sales} \n";
            msg += $"Other Sales: {this.Other_Sales}\n";
            msg += $"Global Sales: {this.Global_Sales} \n";*/

            return msg;
        }

    }

}
