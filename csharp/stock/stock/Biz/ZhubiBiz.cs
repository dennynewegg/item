using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
{
    public static class ZhubiBiz
    {
        public static ZhubiDTO GetZhubiDTO(string code,DateTime date)
        {
            var dto = ZhubiDAL.GetZhubi(code, date);
            if (dto == null)
            {
                var xls = SinaBiz.GetZhubi(code, date);
                if (!string.IsNullOrEmpty(xls))
                {
                    dto = new ZhubiDTO
                    {
                        StockCode = StockHelper.GetShortCode(code)
                    ,
                        InDate = date.Date
                    ,
                        Xls = xls
                    };
                    ZhubiDAL.Insert(dto);
                }
            }
            return dto;
        }

        public static void BuildUpLimit(StockEntity stockInfo)
        {
            if(stockInfo.Percent>9
                && stockInfo.Close==stockInfo.High)
            {
                try
                {
                    var zhubi = GetZhubiDTO(stockInfo.StockCode, stockInfo.InDate.Value);
                    if (zhubi != null)
                    {
                        stockInfo.UpLimitTime = zhubi.UpLimitTime();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHelper.Handler(ex);
                }
            }
        }

        public static void BuildUpLimit(List<StockEntity> dailyList)
        {
            dailyList.ForEach(BuildUpLimit);
        }
    }
}
