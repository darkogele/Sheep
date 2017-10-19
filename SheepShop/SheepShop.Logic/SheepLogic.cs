using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace SheepShop.Logic
{
    public class SheepLogic
    {

        private Herd ConvertXmlToObject(string xmlFile)
        {
            //read the xml file
            string text;
            using (StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath(xmlFile)))
            {
                text = sr.ReadToEnd();
            }

            //deserialize xml string to object
            XmlSerializer serializer = new XmlSerializer(typeof(Herd));
            var herdInfo = new Herd();
            herdInfo = (Herd)serializer.Deserialize(new StringReader(text));

            return herdInfo;
        }

        //SHEEP-1 
        public StockInfo GetStockInfo(string xmlFile, int time)
        {

            var herdInfo = ConvertXmlToObject(xmlFile);

            //implement stock logics
            var result = new StockInfo();

            decimal totalLitersOfMilk = 0;
            int totalSkinsOfWool = 3;


            for (int i = 1; i <= time; i++)
            {
                foreach (var sheep in herdInfo.sheeps)
                {
                    var ageinDays = sheep.Age * 100;
                    ageinDays += i;
                    //sheep.Age = (ageinDays / 100).ToString();

                    if (ageinDays < 1000)
                    {
                        //formula for milk per day(50 - D*0.03) = > the M is suffix that indicates a decimal number
                        var litersOfMilk = 50 - ((ageinDays - 1) * 0.03M);
                        totalLitersOfMilk += litersOfMilk;

                        if (sheep.NextShavingDay == 0)
                        {
                            sheep.NextShavingDay = Convert.ToInt32((sheep.Age * 100) + Math.Floor(8 + (ageinDays * 0.01M)) + 2);
                        }

                        if (ageinDays == sheep.NextShavingDay)
                        {
                            //shaving time
                            totalSkinsOfWool += 1;
                            sheep.NextShavingDay += Convert.ToInt32(Math.Floor(8 + (ageinDays * 0.01M)) + 1);
                        }
                    }
                }
            }

            result.LitersOfMilk = totalLitersOfMilk;
            result.SkinsOfWool = totalSkinsOfWool;



            return result;
        }

        //SHEEP-2
        public HerdInfo GetHerdLastShavedInfo(int time)
        {
            var herdInfo = ConvertXmlToObject(ConfigurationManager.AppSettings["stockFileLocation"]);

            var result = new HerdInfo();

            result.herd = new List<LastShavedInfoModel>();
            foreach (var sheep in herdInfo.sheeps)
            {
                
                var ageInDays = sheep.Age * 100;

                if (ageInDays + time < 1000)
                {
                    var previousShavingDay = (ageInDays + time) - (Math.Floor(8 + (((sheep.Age * 100) + time + 12) * 0.01M)) + 1);

                    if (previousShavingDay <= ageInDays)
                    {
                        result.herd.Add(new LastShavedInfoModel
                        {
                            age = (ageInDays + time) / 100,
                            name = sheep.Name,
                            age_last_shaved = (sheep.Age * 100) / 100
                        });
                    }
                    else
                    {
                        result.herd.Add(new LastShavedInfoModel
                        {
                            age = (ageInDays + time) / 100,
                            name = sheep.Name,
                            age_last_shaved = previousShavingDay / 100
                        });
                    }
                }
                

            }

            return result;
        }
    }
}
