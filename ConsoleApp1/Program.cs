using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace SExperiment
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            try
            {
                string webAddr = "https://menetrendek.hu/menetrend/interface/index.php";

                var httpWebRequest = WebRequest.CreateHttp(webAddr);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                    string json = "{ \"func\" : \"getRoutes\", \"params\" :{\"naptipus\":\"0\",\"datum\":\"2022-04-07\",\"honnan\":\"Mátészalka\",\"hova\":\"Nyírcsaholy\",\"hour\":\"0\",\"min\":\"0\",\"preferencia\":\"0\"}}";
  
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    Console.WriteLine(responseText);

                    //Now you have your response.
                    //or false depending on information in the response     
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            Console.ReadKey();
        }
    }
}
