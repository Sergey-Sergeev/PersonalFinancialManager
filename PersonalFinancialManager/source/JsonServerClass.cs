using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManager.source
{
    public class JsonServerClass
    {
        public int code { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public Json json { get; set; }
        }

        public class Item
        {
            public string name { get; set; }
            public int price { get; set; }
            public double quantity { get; set; }
            public int sum { get; set; }
        }

        public class Json
        {           
            public string dateTime { get; set; }            
            public int totalSum { get; set; }
            public List<Item> items { get; set; }
            public int cashTotalSum { get; set; }
            public int ecashTotalSum { get; set; }                        
            public string retailPlaceAddress { get; set; }            
        }
    }
}
