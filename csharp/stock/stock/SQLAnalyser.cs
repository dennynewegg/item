using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace stock
{
    public static class SQLAnalyser
    {
        //private const string PARAM_EXPRESSION = @"[.\n]*@(?<Variable>\w+)\s*[.\n]*";
        private const string PARAM_EXPRESSION = @"([^@]+|^)@(?<Variable>\w+)[.\n]*";
        private const string VAR_EXPRESSION = @"\bDECLARE\s*@(?<Variable>\w+)\s*[.\n]*";
        private static Regex s_RegexParam = new Regex(PARAM_EXPRESSION, RegexOptions.Compiled | RegexOptions.Singleline);
        private static Regex s_RegexVar = new Regex(VAR_EXPRESSION, RegexOptions.Compiled);

        public static void Analyse(string sqlText, List<string> paramList, List<string> varList)
        {
            paramList.Clear();
            varList.Clear();

            MatchCollection matchCollection = s_RegexVar.Matches(sqlText);
            for (int i = 0; i < matchCollection.Count; i++)
            {
                string txt = matchCollection[i].Groups["Variable"].Value.Trim().ToUpper();
                if (!varList.Contains(txt))
                {
                    varList.Add(txt);
                }
            }

            matchCollection = s_RegexParam.Matches(sqlText);
            for (int i = 0; i < matchCollection.Count; i++)
            {
                string txt = matchCollection[i].Groups["Variable"].Value.Trim().ToUpper();
                if (!paramList.Contains(txt) && !varList.Contains(txt))
                {
                    paramList.Add(txt);
                }
            }
        }

        public static List<string> GetInputParamList(string sqlText)
        {
            List<string> paramList  = new List<string>(10);
            List<string> varList = new List<string>(50);
            Analyse(sqlText, paramList, varList);
            return paramList.Select(item=>"@"+item).ToList();
        }
    }
}
