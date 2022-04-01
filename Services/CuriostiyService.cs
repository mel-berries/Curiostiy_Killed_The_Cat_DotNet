using System.Collections.Generic;
using Curiosity.Models;
using System.IO;
using System.Text.Json;
using System.Text;

using RestSharp;

namespace Curiosity.Services{
    public class CuriosityService{
        public CuriosityService(){

        }
        public List<CuriosityData> GetImages(string robot){
            String demoKey = System.Environment.GetEnvironmentVariable("DEMO_KEY", EnvironmentVariableTarget.User);

            Console.WriteLine("hello mars");
            var client = new RestClient($"https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?sol=1000&camera&page&api_key={demoKey}");
            var request = new RestRequest(Method.GET);

            var images = new List<CuriosityData>();

            IRestResponse response = client.Execute(request);

            var deserialized = JsonSerializer.Deserialize<Root>(response.Content);
            deserialized.photos.ForEach(r => {
                if(r.rover.name == robot){
                    images.Add(new CuriosityData() { id = r.id, sol = r.sol, cam_name = r.camera.name, img_src = r.img_src, earth_date = r.earth_date, rover_name = r.rover.name, cat_found = 'N' });
                };
            });

            return images;
        }
    
        public List<CuriosityData> FindCat(List<CuriosityData> curiosityData = null){
            //super advanced AI that looks for cats
            //-> =^ _.^=
            var positive = new List<CuriosityData>();

            curiosityData.ForEach(r => {
                if(r.id == 424905){
                    positive.Add(new CuriosityData() { id = r.id, sol = r.sol, cam_name = r.cam_name, img_src = r.img_src, earth_date = r.earth_date, rover_name = r.rover_name, cat_found = 'Y' });
                    Console.WriteLine("Cat Found! Lasers charging...");
                };
            });

            return positive;
        }

        public string InsertInfo(List<CuriosityData> curiosityData = null, string filePath = null){
                var sb = new StringBuilder();
                var header = "";
                var info = typeof(CuriosityData).GetProperties();

                foreach (var prop in typeof(CuriosityData).GetProperties())
                {
                    header += prop.Name + ",";
                }

                header = header.Substring(0, header.Length - 2);
                sb.AppendLine(header);
                TextWriter fsw = new StreamWriter(filePath, false);
                fsw.Write(sb.ToString());
                fsw.Close();

                foreach (var obj in curiosityData)
                {
                    sb = new StringBuilder();
                    var line = "";
                    foreach (var prop in info)
                    {
                        line += prop.GetValue(obj, null) + ",";
                    }
                    line = line.Substring(0, line.Length);
                    sb.AppendLine(line);
                    TextWriter sw = new StreamWriter(filePath, true);
                    sw.Write(sb.ToString());
                    sw.Close(); 
                };

            return "Success";
        }

        // public void SendEmail(excel){
               //smtpClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
        // }
    }
}