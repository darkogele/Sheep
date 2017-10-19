using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepShop.Logic
{
    public class LastShavedInfoModel
    {

        public string name { get; set; }
        public decimal age { get; set; }
        public decimal age_last_shaved { get; set; }
    }

    public class HerdInfo
    {
        public List<LastShavedInfoModel> herd { get; set; }
    }
}
