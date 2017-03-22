using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using ServiceStack;
using StockBiz.Entity;
using System.IO;
using System.Data;

namespace StockBiz
{
    public static class THSBiz
    {
        const string limitUpurl = "http://eq.10jqka.com.cn/wencai/index.php";
        const string categoryUrl = "http://q.10jqka.com.cn/stock/gn";
        const string categoryJsonUrl = "http://q.10jqka.com.cn/interface/stock/detail/zdf/desc/{0}/3/{1}";
        const string yjggurl = "http://data.10jqka.com.cn/financial/yjgg/date/{0}/board/ALL/field/DECLAREDATE/order/desc/page/{1}/ajax/1/";
            //"http://data.10jqka.com.cn/financial/yjgg/date/{0}/board/ALL/field/DECLAREDATE/order/desc/ajax/{1}/";

        public static List<StockNewsEntity> LimitUpNews()
        {
            var client = new RestClient();
            client.WebReq.Headers.Add(HttpRequestHeader.UserAgent,
                "Hexin_Gphone/9.27.02 (Royal Flush) hxtheme/1 innerversion/G037.08.191.1.32 userid/29431914");
            var json = client.GetString(limitUpurl);
            var dto = SerializeHelper.JsonDeserialize<thsLimitUp>(json);
            if (dto.success)
            {
                return dto.data.data.Select(item => new StockNewsEntity()
                {
                    StockCode = item.code,
                    StockName = item.name,
                    InDate = item.news.pubtime.Date,
                    PubDate = item.news.pubtime,
                    CategoryID = item.data.Select(category => category.id).ToList().Distinct().Join(";"),
                    CategoryName = item.data.Select(category => category.name).ToList().Distinct().Join(";"),
                    Percent = item.rate,
                    Memo = item.news.summ,
                    Url = item.news.url,
                    Title = item.news.title
                }).ToList();
            }
            else
            {
                LogFactory.Instance.Write(dto.message);
            }
            return null;
        }

        public static List<CategoryStockDTO> GetCategoryList()
        {
            string html = new RestClient().GetString(categoryUrl);
            var links = XmlHelper.GetNodes(XmlHelper.ToXml(html), "div", "class", "category m_links")
                .First().GetNodes("a")
                .Select(node =>
                {
                    return new CategoryStockDTO()
                    {
                        CategoryName = node.InnerText
                        ,Url =node.Attributes["href"].Value.Trim('/')
                    };
                });

            var categoryList = new List<CategoryStockDTO>(1000);
           foreach (var category in links)
            {
                try
                {
                    category.CategoryCode = category.Url.Substring(category.Url.LastIndexOf("/", category.Url.Length - 2)+1);
                    categoryList.AddRange(GetCategoryList(category));
                }
                catch(Exception ex)
                {
                    ExceptionHelper.Handler(ex);
                }

            }
            return categoryList;

        }

        public static List<StockFinanceEntity> GetFinanceList(DateTime reportdate,DateTime endDate)
        {
            
            var restClient = new RestClient();
            restClient.WebReq.Headers["X-Requested-With"] = "XMLHttpRequest";
            var date = DateTime.Now;
            int loop = 0;
            List<StockFinanceEntity> resultList = new List<StockFinanceEntity>(3500);
            while (date.Date > endDate.Date)
            {
                loop++;
                string url = string.Format(yjggurl, reportdate.ToString("yyyy-MM-dd"),loop);
               
                try
                {
                    var html = restClient.GetString(url);
                    int totalSize;
                    var list = ParseFinanceList(html,out totalSize);
                    if(list.IsNullOrEmpty() )
                    {
                        break;
                    }
                    date = list.Select(item => item.InDate.Value).Min();
                    list = list.Where(item => item.InDate > endDate).ToList();
                    if (list.IsNullOrEmpty())
                    {
                        break;
                    }
                    resultList.AddRange(list);
                    if(loop>totalSize)
                    {
                        break;
                    }
                }
                catch(Exception ex)
                {
                    ExceptionHelper.Handler(ex);
                    break;
                }
              
            }
            resultList.ForEach(item => item.ReportDate = reportdate);
            return resultList;
        }

        public static List<StockFinanceEntity> ParseFinanceList(string html,out int totalPage)
        {

            // < td > 5 </ td >
            //< td class="tc"><a href = "http://stockpage.10jqka.com.cn/600877/" target="_blank">600877</a></td>
            //<td><a href = "http://stockpage.10jqka.com.cn/600877/" target="_blank"  code="hs_600877" class="J_showCanvas">中国嘉陵</a></td>
            //<td class="tc">2016-09-30</td>
            //<td class="tr">3.41亿</td> 营业收入  4
            //<td class="tr c-fall">-47.41</td> 营业收入同比增长  5
            //<td class="tr c-rise">8.08</td> 营业收入季度环比 增长
            //<td class="tr">-8205.30万</td>  净利润7
            //<td class="tr c-fall">-0.10</td>  净利润同比增长
            //<td class="tr c-rise">100.83</td>净利润季度环比 增长
            //<td class="tr">-0.12</td> EPS 10 
            //<td class="tr">-0.12</td> 每股净资产 11
            //<td class="tr ">0.00</td>
            //<td class="tr">-0.22</td>
            //<td class="tr c-rise">6.14</td>

            totalPage = 54;
            var tbodyIndex = html.IndexOf("<tbody>");
            var tbodyEnd = html.LastIndexOf("</table>");
            try
            {
                var pagehtml = html.Substring(tbodyEnd + 8);
                var pageStr = XmlHelper.ToXml(pagehtml)
                    .GetNodes("span", "class", "page_info").First().InnerText;
                var pageSize = pageStr.Substring(pageStr.IndexOf("/")+1);
                totalPage = int.Parse(pageSize);
            }
            catch(Exception ex)
            {
                ExceptionHelper.Handler(ex);
            }

            if (tbodyIndex>0
                && tbodyEnd>tbodyIndex)
            {
                var tr = XmlHelper.ToXml(html.Substring(tbodyIndex, tbodyEnd - tbodyIndex)).GetNodes("tr");
                var list = tr.Select(node =>
                {
                    var tdNodes = node.GetNodes("td");
                    return new StockFinanceEntity
                    {
                        StockCode = tdNodes[1].InnerText,
                        StockName = tdNodes[2].InnerText,
                        MainIncome = StringHelper.ToDec(tdNodes[4].InnerText),
                        InDate = DateTime.Parse(tdNodes[3].InnerText),
                        EPS = StringHelper.ToDec(tdNodes[10].InnerText),
                        NetAssPerShare = StringHelper.ToDec(tdNodes[11].InnerText),
                        NetProfit = (Int64)StringHelper.ToDec(tdNodes[7].InnerText)
                    };
                }).ToList();
                return list;
            }

           

            return null;        
        }

        private static List<CategoryStockDTO> GetCategoryList(CategoryStockDTO category,int page=1)
        {
            List<CategoryStockDTO> list = new List<CategoryStockDTO>(100);
            var url = string.Format(categoryJsonUrl, page, category.CategoryCode);
            var cat = new RestClient().GetJson<thsCategory>(url);
            if(cat != null)
            {
                list.AddRange(cat.data.Select(item=>
                new CategoryStockDTO() {
                    CategoryName =category.CategoryName
                    ,StockCode=item.stockcode
                    ,CategoryCode = category.CategoryCode
                    ,StockName = item.stockname
                    ,Source = "ths"
                }));
                if (list.Count == 50)
                {
                    list.AddRange(GetCategoryList(category, page + 1));
                }
            }
            return list;

        }

        public static List<StockFinanceEntity> DownloadFinance(StockEntity stock)
        {
            try
            {
                var stockcode = stock.StockCode;
                string url = string.Format("http://basic.10jqka.com.cn/{0}/xls/mainreport.xls", StockHelper.GetShortCode(stockcode));
                var buffer = new RestClient().GetData(url);
                if (buffer == null
                    || buffer.Length == 0)
                {
                    return null;
                }
                var path = "Finance\\" + stockcode + ".xls";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllBytes(path, buffer);
                return ReadFromExcel(stock, path);
            }
            catch (Exception ex)
            {
                ExceptionHelper.Handler(ex);
                return null;
            }
        }

        public static List<StockFinanceEntity> ReadFromExcel(StockEntity stock, string path)
        {
            var tb = SqlHelper.LoadDataFromExcel(path);
          

            var list = new List<StockFinanceEntity>(tb.Rows.Count);
         

            for (var i = 0; i < tb.Rows.Count; i++)
            {
                try
                {

                    DataRow row = tb.Rows[i];
                    //DateTime date;
                    //if (!DateTime.TryParse(dsc.Tables[0].Columns[i].ColumnName, out date))
                    //{
                    //    break;
                    //}

                    var sf = new StockFinanceEntity
                    {
                        StockCode = stock.StockCode,
                        StockName = stock.StockName,
                        ReportDate =( DateTime) row["date"],
                        EPS = row.ReadDec("基本每股收益(元)"),
                        NetProfit = row.ReadDec("净利润(万元)"),
                        NetProfitGrowth = row.ReadDec("净利润同比增长率(%)"),
                        NetIncoming = row.ReadDec("扣非净利润(万元)"),
                        MainIncome = row.ReadDec("营业总收入(万元)"),
                        NetAssPerShare = row.ReadDec("每股净资产(元)"),
                        Fund = row.ReadDec("每股资本公积金(元)"),
                        UndistributedProfit = row.ReadDec("每股未分配利润(元)"),
                        CashPerShare = row.ReadDec("每股经营现金流(元)"),
                        GrossMargin = row.ReadDec("销售毛利率(%)"),
                        InDate = DateTime.Now
                    };

                    list.Add(sf);
                }
                catch (Exception ex)
                {
                    ExceptionHelper.Handler(ex);
                    LogFactory.Instance.Write("Index:" + i);
                }
            }
            return list;
        }

        class thsCategory
        {
            public List<thsCategoryitem> data { get; set; }
        }
        class thsCategoryitem
        {
            public string stockname { get; set; }
            public string stockcode { get; set; }
        }
        class thsLimitUp
        {
            public thsbody data { get; set; }
            public bool success { get; set; }
            public string message { get; set; }
        }

        class thsbody
        {
            public List<thsLimitItem> data { get; set; }
        }

        class thsLimitItem
        {
            public string code { get; set; }
            public string name { get; set; }
            public decimal? rate { get; set; }
            public List<thscategory> data { get; set; }
            public thsnews news { get; set; }
        }

        class thsnews
        {
            public DateTime pubtime { get; set; }
            public string summ { get; set; }
            public string url { get; set; }
            public string title { get; set; }
        }
         class thscategory
        {
            public string id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
        }
    }
}
