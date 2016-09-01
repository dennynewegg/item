using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace stock
{
    [Serializable]
    public class StockEntity
    {
        public string StockCode { get; set; }
        public string StockName { get; set; }
        public DateTime? InDate { get; set; }
        public decimal? Open { get; set; }
        public decimal? Close { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public decimal? Volume { get; set; }
        public decimal? Percent { get; set; }
        public decimal? Turnover { get; set; }

        public decimal? AvgCost
        {
            get
            {
                if (Turnover.GetValueOrDefault() > 0
                    && Volume.GetValueOrDefault() > 0)
                {
                    return Math.Round(Turnover.Value/Volume.Value, 2);
                }
                if (Close.HasValue)
                {
                    return Close;
                }
                return null;
            }
        }
    }
}
