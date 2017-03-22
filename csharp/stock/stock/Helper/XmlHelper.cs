using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Sgml;
using System.IO;


namespace StockBiz
{
    public static class XmlHelper
    {
        public static XmlNode ToXml(string html)
        {
            SgmlReader sgmlRdr = new SgmlReader();
            sgmlRdr.DocType = "HTML";
            sgmlRdr.InputStream = new StringReader(html);
            XmlDocument sgmlDoc = new XmlDocument();
            sgmlDoc.Load(sgmlRdr);
            return sgmlDoc;
        }

        public static XmlNode GetXmlNode(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc;
        }

        public static List<XmlNode> GetNodes(this XmlNode xml,string nodeName)
        {
            return GetNodes(xml, node => node.Name.IsEqual(nodeName));
        }
        

        public static List<XmlNode> GetNodes(this XmlNode xmlNode,string nodeName,string attName,string attValue)
        {
            return GetNodes(xmlNode,   node => 
                                node.Name.IsEqual(nodeName)
                                && node.Attributes.Cast<XmlAttribute>()
                                .Any(att =>att.Name.IsEqual(attName)
                                && att.Value.Contains(attValue)));
        }

        public static List<XmlNode> GetNodes(this XmlNode node,Func<XmlNode,bool> match)
        {
            if (node == null)
            {
                return new List<XmlNode>();
            }

            if(match(node))
            {
                return new List<XmlNode>() { node };
            }

            var list = new List<XmlNode>(10);
            if(node.ChildNodes!=null)
            {
                foreach(XmlNode cnode in node.ChildNodes)
                {
                    list.AddRange(GetNodes(cnode, match));
                }
            }
            return list;
        }

    }
}
