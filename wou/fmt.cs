using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            lns[0] = addfont(lns[0], "幼圆");
            if (!lns[1].StartsWith("{\\colortbl"))
            {
                List<string> s = new List<string>(lns);
                s.Insert(1, "{\\colortbl ;}");
                lns = s.ToArray();
            }
            lns[1] = addcolor(lns[1], 255, 0, 0);
            return string.Join("\n", lns);
        }
        static string addfont(string line, string font)
        {
            line = line.TrimEnd(Convert.ToChar(13));
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
            if(fontbyte == font)
            {
                newfont = "{\\f" + fontnum.ToString() + @"\fnil\fcharset0 " + font + ";}";
            }
            else
            {
                newfont = "{\\f" + fontnum.ToString() + @"\fnil\fcharset134 " + fontbyte + ";}";
            }
            
            return line.Insert(line.Length - 1, newfont);
        }
        static string tobyte(string cha)
        {
            byte[] bt = Encoding.GetEncoding("gbk").GetBytes(cha);
            if (bt.Length == cha.Length)
            {
                return cha;
            }else {
                string t = "";
                for (int i = 0; i < bt.Length; i++)
                {
                    t += "\\'" + bt[i].ToString("x2");
                }
                return t;
            }
            
        }
        static string addcolor(string line, int red, int green, int blue)
        {
            line = line.TrimEnd(Convert.ToChar(13));
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
        static void ctrld(ref string ori, string dest, string font = "宋体", int fsize = 18, bool bold = false, bool italic = false, params int[] rgb)
        {
            if (ori.Contains(dest))
            {
                Regex rgxf = new Regex(@"(?<={)(\\f\d+)(?=\\f.*" + tobyte(font) + @")");
                //Regex rgxc = new Regex(@"{\\colortbl.*blue\d+;}");
                Regex rgxc = new Regex(@"(?<=\\(red|green|blue))(\d*?)(?=\\|;)");
                string fontf = rgxf.Match(ori).Value;
                string fontsf = "\\fs" + fsize.ToString();
                int colori = 0;
                string colorf = "";
                if (rgb != new int[] { 0, 0, 0 })
                {
                    MatchCollection colormc = rgxc.Matches(ori);
                    for (int i = 0; i < colormc.Count; i += 3)
                    {
                        if (rgb[0].ToString() == colormc[i].Value && rgb[1].ToString() == colormc[i + 1].Value && rgb[2].ToString() == colormc[i + 2].Value)
                        {
                            colori = i / 3;
                            break;
                        }
                    }
                    colorf = "\\cf" + colori.ToString();
                }
                string boldf = "";
                if (!bold) {
                    boldf = 
                }
            }
        }
    }
}
