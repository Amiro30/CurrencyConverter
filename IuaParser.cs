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
    class IuaParser
    {
       private readonly List<float> coursesCollection = new List<float>();
      
        public float[,] CurrencyTable = new float[2, 4];

        public void FillCurrencyTable()
        {
            CurrencyTable[0, 0] = coursesCollection[0];
            CurrencyTable[0, 1] = coursesCollection[2];
            CurrencyTable[0, 2] = coursesCollection[4];
            CurrencyTable[0, 3] = 1f;

            CurrencyTable[1, 0] = coursesCollection[1];
            CurrencyTable[1, 1] = coursesCollection[3];
            CurrencyTable[1, 2] = coursesCollection[5];
            CurrencyTable[1, 3] = 1;
        }
        
        public IuaParser(string url)
        {
           
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.Referer = "user-agent";
            //request.UserAgent = "Chrome";

            WebResponse response = request.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string urlread = reader.ReadToEnd();
            reader.Close();
            response.Close();
            dataStream.Close();
           
            MatchCollection Courses = Regex.Matches(urlread, @"<td><span class=""value \W*\w*""><span>(\d{1,2}\.\d{1,4})</span>");

            foreach (Match m in Courses)
            {
                coursesCollection.Add(float.Parse(m.Groups[1].ToString().Replace(".", ","))); //write to List all digits, that match
            }
         FillCurrencyTable();
        }
    }
}

