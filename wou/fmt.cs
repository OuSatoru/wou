using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace wou
{
    class fmt
    {
        public static string rtfmt(string ori)
        {
            string[] lns = ori.Split('\n');
            lns[0] = addfont(lns[0], "微软雅黑");
            if (!lns[1].StartsWith("{\\colortbl"))
            {
                List<string> s = new List<string>(lns);
                s.Insert(1, "{\\colortbl ;");
                lns = s.ToArray();
            }

        }
        static string addfont(string line, string font)
        {
            int fontnum;
            string newfont;
            string fontbyte = tobyte(font);
            Regex rgx2 = new Regex(@"(?<=(fcharset\d+\s))(.*?)(?=;})");
            for(int i = 0; i < rgx2.Matches(line).Count; i++)
            {
                if(fontbyte == rgx2.Matches(line)[i].Value)
                {
                    return line;
                }
            }
            Regex rgx = new Regex(@"{\\f\d+\\.*?fcharset");
            fontnum = rgx.Matches(line).Count;
            newfont = "{\\f" + fontnum.ToString() + @"\fnil\fcharset134 " + fontbyte + ";}";
            return line.Insert(line.Length - 1, newfont);
        }
        static string tobyte(string cha)
        {
            string t = "";
            byte[] bt = Encoding.GetEncoding("gbk").GetBytes(cha);
            for(int i = 0; i < bt.Length; i++)
            {
                t += "\\'" + bt[i].ToString("x2");
            }
            return t;
        }
        static string addcolor(string line, int red, int green, int blue)
        {
            string newcolor;
            Regex rgb = new Regex(@"(?<=\\(red|green|blue))(\d*?)(?=\\|;)");
            MatchCollection mc = rgb.Matches(line);
            for(int i = 0; i < mc.Count; i+=3)
            {
                if (red.ToString() == mc[i].Value && green.ToString() == mc[i + 1].Value && blue.ToString() == mc[i + 2].Value)
                {
                    return line;
                }
            }
            newcolor = "\\red" + red.ToString() + "\\green" + green.ToString() + "\\blue" + blue.ToString() + ";";
            return line.Insert(line.Length - 1, newcolor);
        }
    }
}
