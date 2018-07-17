using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConverterNew
{
    class MinfinParser
    {
        private readonly List<float> buyCollection = new List<float>();
        private readonly List<float> saleCollection = new List<float>();
        WebClient client = new WebClient();
        
        public float[,] CurrencyTable = new float[2,4];

        public void FillCurrencyTable()
        {
            CurrencyTable[0, 0] = buyCollection[0];
            CurrencyTable[0, 1] = buyCollection[1];
            CurrencyTable[0, 2] = buyCollection[2];
            CurrencyTable[0, 3] = 1f;

            CurrencyTable[1, 0] = saleCollection[0];
            CurrencyTable[1, 1] = saleCollection[2];
            CurrencyTable[1, 2] = saleCollection[4];
            CurrencyTable[1, 3] = 1;
        }



        public MinfinParser(string url)
        {
            
            client.Encoding = Encoding.UTF8;
            client.Headers.Add("user-agent", "Only a test!");

            string urlread = client.DownloadString(url);

            MatchCollection buyCourses = Regex.Matches(urlread,
                @"data-small=""Покупка / Продажа"">\n*\t*\s*\r*(\d{1,2}\.\d{1,4})\n*\t*\s*\r*<span");
            MatchCollection saleCourses = Regex.Matches(urlread,
                @"</span>\n*\t*\s*\r*(\d{1,2}\.\d{1,4})\n*\t*\s*\r*</td>");

            foreach (Match m in buyCourses)
            {
                buyCollection.Add(float.Parse(m.Groups[1].ToString().Replace(".", ",")));
                    //write to List all digits, that match
            }

            foreach (Match m in saleCourses)
            {
                saleCollection.Add(float.Parse(m.Groups[1].ToString().Replace(".", ",")));
                    //write to List all digits, that match
            }
            FillCurrencyTable();
        }
    }
}
