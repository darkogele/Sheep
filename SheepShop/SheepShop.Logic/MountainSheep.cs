using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SheepShop.Logic
{
    public class MountainSheep
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("age")]
        public decimal Age { get; set; }
        [XmlAttribute("sex")]
        public string Sex { get; set; }

        //used in calculations for wool
        public int NextShavingDay { get; set; }
    }

    [XmlRoot(ElementName = "herd")]
    public class Herd
    {
        [XmlElement(ElementName = "mountainsheep")]
        public MountainSheep[] sheeps { get; set; }
    }
}
