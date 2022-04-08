using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace SExperiment
{
    public class TravelRoute
    {
        public string honnan, hova;
        public TravelRoute(string s)
        {
            //this.honnan = "";
            // this.hova="";
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            string honnan = "";
            string hova = "";
            Console.WriteLine("Kérem adja meg honnan szeretne utazni: ");
            honnan = Console.ReadLine();
            Console.WriteLine("Kérem adja meg hova szeretne utazni: ");
            hova = Console.ReadLine();

            try
            {
                string webAddr = "https://menetrendek.hu/menetrend/interface/index.php";

                var httpWebRequest = WebRequest.CreateHttp(webAddr);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                    string json = "{ \"func\" : \"getRoutes\", \"params\" :{\"naptipus\":\"0\",\"datum\":\"2022-04-08\",\"honnan\":\"" + honnan + "\",\"hova\":\"" + hova + "\",\"hour\":\"0\",\"min\":\"0\",\"preferencia\":\"0\"}}";
                    streamWriter.Write(json);
                    streamWriter.Flush();

                }


                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    var responseText = streamReader.ReadToEnd();
                    //   Console.WriteLine(responseText);
                    var data = (JObject)JsonConvert.DeserializeObject(responseText);

                   
                    for (int i = 1; i < responseText.Length; i++)
                    {
                        var indulasihely = data["results"]["talalatok"][$"{i}"]["indulasi_hely"];
                        var erkezesihely = data["results"]["talalatok"][$"{i}"]["erkezesi_hely"];
                        var erkezesiido = data["results"]["talalatok"][$"{i}"]["indulasi_ido"];
                        var indulasiido = data["results"]["talalatok"][$"{i}"]["erkezesi_ido"];
                        Console.WriteLine($"A busz/vonat {indulasiido}-kor indul {indulasihely}-ról");
                        Console.WriteLine($"A busz/vonat {erkezesiido}-kor érkezik {erkezesihely}-ra");




                    }
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