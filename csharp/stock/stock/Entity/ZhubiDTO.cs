using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
{
    public  class ZhubiDTO
    {
        private List<ZhubiItem> _items;
        public string StockCode { get; set; }
        public DateTime InDate { get; set; }
        public string Xls { get; set; }

        public List<ZhubiItem> ItemList
        {
            get
            {
                if (_items == null)
                {
                    var csv = Xls;
                    if (!string.IsNullOrWhiteSpace(csv)
                        && !csv.Contains("当天没有数据"))
                    {
                        _items = SerializeHelper.CsvDeserialize<List<ZhubiItem>>(csv);
                        //_items.RemoveAll(item => !string.IsNullOrWhiteSpace(item.Time));
                        _items.Sort((a, b) => a.Time.CompareTo(b.Time));
                    }
                }
                if(_items == null)
                {
                    _items = new List<ZhubiItem>();
                }
                return _items;
            }
        }

        public string UpLimitTime()
        {
            if(ItemList.IsEmpty())
            {
                return string.Empty;
            }

            var list = ItemList;
            var maxPrice = list.First().Price.GetValueOrDefault();
            var time = string.Empty;
            foreach(var item in list)
            {
                if (item.Price.GetValueOrDefault() > 0
                    && item.Qty.GetValueOrDefault() > 0)
                {
                    if (maxPrice < item.Price.Value)
                    {
                        maxPrice = item.Price.Value;
                        time = item.Time;
                    }
                }
            }
            return time;
        }

        

    }

    public class ZhubiItem
    {
        public string Time { get; set; }
        public decimal? Price { get; set; }
        public decimal? ChangePrice { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Amount { get; set; }
        public string Direct { get; set; }
    }
}
