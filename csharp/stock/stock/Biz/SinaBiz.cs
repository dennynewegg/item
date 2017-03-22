using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using ServiceStack;
using ServiceStack.Text;
using ServiceStack.Text.Jsv;
using System.Text.RegularExpressions;

namespace StockBiz
{
    public static class SinaBiz
    {
        #region GetTrade
        private const string hq163url =
           "http://vip.stock.finance.sina.com.cn/quotes_service/api/json_v2.php/Market_Center.getHQNodeData?page=0&num=3500&sort=changepercent&asc=0&node=hs_a";

        public static List<StockEntity> GetTradeList(int count = 3500)
        {
            var client = new RestClient();
            var str = client.GetString(hq163url);

            var result = SerializeHelper.JsvDeserialize<List<hqitem>>(str);
            if (result != null
                && result.Count()>0)
            {
                string firstCode = result.First().code;
                var stockDate = GetRealTime(new List<string>() { firstCode }).InDate;
            

            return result
                    .Where(item=>item.volume.GetValueOrDefault()>0)
                    .Select(item =>
                    new StockEntity
                    {
                        StockCode = item.code,
                        Open = item.open,
                        Low = item.low,
                        High = item.high,
                        Percent = item.changepercent,
                        Close = item.trade,
                        StockName = item.name,
                        InDate = stockDate,
                        Volume = item.volume,
                        Amount = item.amount,
                        Turnover =Math.Round( item.turnoverratio.Value)
                    }
                ).ToList();
            }
            return new List<StockEntity>();
        }
        private class hqitem
        {
            public decimal? high { get; set; }
            public decimal? low { get; set; }
            public decimal? open { get; set; }
            public decimal? changepercent { get; set; }
            public decimal? trade { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public string ticktime { get; set; }
            public decimal? volume { get; set; }
            public decimal? amount { get; set; }
            public decimal? turnoverratio { get; set; }
        }
        public static StockEntity GetRealTime(List<string> stockCodeList)
        {
            string codes =  stockCodeList.Select(str => StockHelper.GetLongCode(str))
                .Join(",");
            string url = "http://hq.sinajs.cn/list=" + codes;
            var client = new RestClient();
            var dataStr = client.GetString(url);
            var stockStrAry = dataStr.Split(';');
            //var list 

            //foreach (var stockStr in stockStrAry)
            //{
            var stockStr = stockStrAry.First();
            var eqIndex = stockStr.IndexOf("=");
            var stockData = stockStr.Substring(eqIndex + 2);
            stockData = stockData.Substring(0, stockData.Length - 1);
            var stockDatas = stockData.Split(',');

            return new StockEntity
            {
                InDate = DateTime.Parse( stockDatas[30]+" "+stockDatas[31])
            };
            //}
        }
        #endregion

        #region LimitSell
        private const string sinaxxjjUrl =
            "http://vip.stock.finance.sina.com.cn/q/go.php/vInvestConsult/kind/xsjj/index.phtml?bdate={0}&edate={1}&p={2}&num=60";
        public static List<StockEntity> GetLimitSellList(DateTime from,DateTime to,int pageIndex = 1)
        {
            var list = new List<StockEntity>(100);

            var client = new RestClient();
            var html = client.GetString(string.Format(sinaxxjjUrl, from.ToString("yyyy-MM-dd"),to.ToString("yyyy-MM-dd"), pageIndex));
            list.AddRange(ParseLimitSellList(html));
            if(list.Count == 60)
            {
                pageIndex++;
                list.AddRange(GetLimitSellList(from, to, pageIndex));
            }
            return list;
        }

        private static List<StockEntity> ParseLimitSellList(string html)
        {
            var xml = XmlHelper.ToXml(html);
            var trNodes  =  XmlHelper.GetNodes(xml, "table", "class", "list_table")
                .FirstOrDefault().GetNodes("tr")
                .Skip(1);

            var list = new List<StockEntity>(trNodes.Count());
            foreach(var tr in trNodes)
            {
                var tdNodes = tr.GetNodes("td");
                if (tdNodes.Count() == 9)
                {
                    try
                    {
                        list.Add(new StockEntity
                        {
                            StockCode = tdNodes[0].InnerText,
                            StockName = tdNodes[1].InnerText,
                            InDate = DateTime.Parse(tdNodes[4].InnerText),
                            Volume = Decimal.Parse(tdNodes[5].InnerText)
                        });
                    }
                    catch(Exception ex)
                    {
                        ExceptionHelper.Handler(ex);
                    }
                }
            }
            return list;
              
        }



        #endregion

        #region tradeTran
        const string tradeTranurl= "http://market.finance.sina.com.cn/downxls.php?date={0}&symbol={1}";

        public static List<TradeTranDTO> GetTradeList(string code,DateTime from,DateTime? to = null)
        {
            to = to.GetValueOrDefault(DateTime.Now).Date;
            var shortCode = StockHelper.GetShortCode(code);
            code = StockHelper.GetLongCode(code);
            
            List<TradeTranDTO> list = new List<TradeTranDTO>(100);
            var curr = from.Date;
            while (curr <= to)
            {
                var client = new RestClient();
                var csv = client.GetString(string.Format(tradeTranurl, curr.ToString("yyyy-MM-dd"), code));
                if (!csv.IsNullOrEmpty()
                    && csv.Length > 1000)
                {
                    csv = csv.Replace("\t", ",");
                    csv = csv.Replace("--", "0");
                    csv = csv.Replace("成交时间,成交价,价格变动,成交量(手),成交额(元),性质", "Time,Price,PriceChange,Qty,Amount,Vector");
                    list.Add(new TradeTranDTO() { StockCode = shortCode, InDate = curr, Detail = csv });
                }
                curr = curr.AddDays(1);
            }
            return list;
        }


        #endregion

        #region Longhu
        const string longhuListUrl =
            "http://vip.stock.finance.sina.com.cn/q/go.php/vInvestConsult/kind/lhb/index.phtml?tradedate={0}";
        static Regex detailRegex = new Regex(@"\'(\d{2})\',\'(\d{6})\',\'([0-9-]{10})\'", RegexOptions.Compiled);

        public static List<LonghuDTO> GetLonghuList(DateTime date)
        {
            string url = string.Format(longhuListUrl, date.ToString("yyyy-MM-dd"));
            RestClient client = new RestClient();
            var html =  client.GetString(url);
            var trnodes = XmlHelper.ToXml(html)
                .GetNodes("table", "id", "dataTable")
                .SelectMany(node => node.GetNodes("tr", "class", "head"));
            var typeName = string.Empty;
            var list = new List<LonghuDTO>(100);
            foreach(var tr in trnodes)
            {
                var tdnodes = tr.GetNodes("td");
                if(tdnodes.Count == 1)
                {
                    typeName = tdnodes.First().InnerText
                        .Replace(Environment.NewLine,"")
                        .Trim();
                    continue;
                }
                else if(tdnodes.Count == 8)
                {
                    if(tdnodes.First().InnerText.IsEqual("序号"))
                    {
                        continue;
                    }
                    var detail = tdnodes[7].InnerXml;
                    var match = detailRegex.Match(detail);
                    if (match.Success)
                    {
                        var longhu = new LonghuDTO
                        {
                            StockCode = tdnodes[1].InnerText,
                            StockName = tdnodes[2].InnerText,
                            TypeName = typeName,
                            TypeCode = match.Groups[1].Value,
                            InDate = DateTime.Parse(match.Groups[3].Value)
                        };
                        list.Add(longhu);
                    }
                }
            }
            return list.SelectMany(lh => GetLonghuDetail(lh))
                .ToList() ;
        }

        const string longhuDetailUrl =
            "http://vip.stock.finance.sina.com.cn/q/api/jsonp.php/var%20details=/InvestConsultService.getLHBComBSData?symbol={0}&tradedate={1}&type={2}";
        private static List<LonghuDTO> GetLonghuDetail(LonghuDTO longhu)
        {
            try
            {
                var url = string.Format(longhuDetailUrl, longhu.StockCode
                , longhu.InDate.Value.ToString("yyyy-MM-dd")
                , longhu.TypeCode);
                var jsonstart = "details=((";
                var client = new RestClient();
                var html = client.GetString(url);
                if (html.Length > 100)
                {
                    var index = html.IndexOf(jsonstart);
                    html = html.Substring(index + jsonstart.Length);
                    html = html.Substring(0, html.Length - 2);
                    var detail = SerializeHelper.JsvDeserialize<longhuDetail>(html);
                    var list = new List<LonghuDTO>(10);
                    list.AddRange(detail.buy.Select(item => item.ToLonghu(longhu)));
                    list.AddRange(detail.sell.Select(item => item.ToLonghu(longhu)));
                    return list;
                }
            }
            catch
            {

            }
            return new List<LonghuDTO>();
        }

        public class longhuDetail
        {
            public List<longhuDetailItem> buy { get; set; }
            public List<longhuDetailItem> sell { get; set; }
        }

        public class longhuDetailItem
        {
            public String comName { get; set; }
            public decimal sellAmount { get; set; }
            public decimal buyAmount { get; set; }
            public LonghuDTO ToLonghu(LonghuDTO longhu)
            {
                return new LonghuDTO
                {
                    StockCode = longhu.StockCode,
                    InDate = longhu.InDate,
                    StockName = longhu.StockName,
                    ComName = comName,
                    SellAmount =sellAmount,
                    BuyAmount = buyAmount,
                    TypeCode = longhu.TypeCode,
                    TypeName = longhu.TypeName
                };
            }
           
        }


        #endregion

        #region zhubi
        const string zhubiUrl = "http://market.finance.sina.com.cn/downxls.php?date={0}&symbol={1}";
        public static string GetZhubi(string code,DateTime date)
        {
            var url = string.Format(zhubiUrl, date.ToString("yyyy-MM-dd"), StockHelper.GetLongCode(code));
            string html = new RestClient().GetString(url);
            if(html.Contains("没有当天数据"))
            {
                return string.Empty;

            }

            html = html.Replace("成交时间	成交价	价格变动	成交量(手)	成交额(元)	性质", "Time	Price	ChangePrice	Qty	Amount	Direct")
                .Replace("--", "0").Replace("\t", ",") ;
            return html;

        }

        #endregion

        #region Finnal
        const string financeUrl = "http://vip.stock.finance.sina.com.cn/corp/go.php/vFD_FinanceSummary/stockid/{0}.phtml";





        #endregion

    }




}
