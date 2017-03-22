
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StockBiz
{
    public static class TaogulaBiz
    {
        const string NewLine = "@@@####@@@";
        static string[] articleUrlList =new [] { "http://www.taogula.com/forum-47-{0}.html" ,
                "http://www.taogula.com/forum-180-{0}.html" };
        public static List<ArticleDTO> GetArticleList(DateTime? date)
        {
            date = date.GetValueOrDefault(DateTime.Now.AddMonths(-2));

            List<ArticleDTO> list = new List<ArticleDTO>(100000);
            foreach (var articleUrl in articleUrlList)
            {
                try
                {
                    int loop = 1;
                    while (loop <2)
                    {
                        var client = new RestClient();
                        var html = client.GetString(string.Format(articleUrl, loop));
                        var articleList = GetHrefList(html);
                        if (articleList.IsEmpty())
                        {
                            break;
                        }
                        var tmp = articleList.FindAll(item => item.InDate > date);
                        list.AddRange(tmp);
                        if (tmp.Count < articleList.Count)
                        {
                            break;
                        }
                        loop++;
                    }
                }
                catch(Exception ex)
                {
                    ExceptionHelper.Handler(ex);
                }
            }
            return list;
            
        }

        static List<ArticleDTO> GetHrefList(string html)
        {
            var xml = XmlHelper.ToXml(html);
            var tableXml = XmlHelper.GetNodes(xml,"table", "id", "threadlisttableid").FirstOrDefault();
            var tbodyNodes = XmlHelper.GetNodes(tableXml, "tbody").Skip<XmlNode>(2);
            var list = new List<ArticleDTO>(tbodyNodes.Count());

            foreach(var node in tbodyNodes)
            {
                var anode = XmlHelper.GetNodes(node, "a", "class", "s xst").FirstOrDefault();
                if (anode != null)
                {
                    var tdNodes = XmlHelper.GetNodes(node, "td");
                    if (tdNodes.Count == 4)
                    {
                        var spanNode = XmlHelper.GetNodes(tdNodes[1], "span").FirstOrDefault();
                        //var tdNode = tdNodes[1];
                        if (spanNode != null)
                        {
                            var type = "tgl";
                            if(anode.InnerText.Contains("涨停板复盘"))
                            {
                                type =type+ "涨停板复盘";
                            }
                            else if(anode.InnerText.Contains("盘前资讯"))
                            {
                                type =type+"盘前资讯";
                            }

                            list.Add(new ArticleDTO()
                            {
                                Url = anode.Attributes["href"].Value,
                                Title = anode.InnerText,
                                InDate = DateTime.Parse(spanNode.InnerText),
                                ArticleType = type
                            });
                        }
                    }
                }
            }

            return list;
               
        }

        public static string GetArticle(string url)
        {
            var client = new RestClient();
            var html = client.GetString(url);
            if(!string.IsNullOrWhiteSpace(html))
            {
                var s = html.IndexOf("<td class=\"t_f\"");
                if (s > -1)
                {
                    var e = html.IndexOf("</td>", s);
                    if (e > -1)
                    {
                        return html.Substring(s, e - s + 5);
                    }
                }

            }
            return string.Empty;


            //var xml = XmlHelper.ToXml(html);
            //try
            //{
            //    return XmlHelper.GetNodes(xml, "td", "class", "t_f").FirstOrDefault().InnerText;
            //}
            //catch
            //{
            //    return string.Empty;
            //}
        }
    }
}
