using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
{
    [Serializable]
    public class LonghuDTO:StockEntity
    {
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string ComName { get; set; }
        public decimal? SellAmount { get; set; }
        public decimal? BuyAmount { get; set; }
        public decimal? NetAmount
        {
            get
            {
                return BuyAmount.GetValueOrDefault() -
                    Math.Abs(SellAmount.GetValueOrDefault());
            }
        }

        public bool IsEqual(LonghuDTO lh)
        {
            if(this == lh)
            {
                return true;
            }
            return this.StockCode.IsEqual(lh.StockCode)
                && this.ComName.IsEqual(lh.ComName)
                && this.BuyAmount == lh.BuyAmount
                && this.SellAmount == lh.SellAmount;
        }

    }
}
