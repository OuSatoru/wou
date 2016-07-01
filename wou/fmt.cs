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
            lns[0] = addfont(lns[0], "Consolas");
            lns[0] = addfont(lns[0], "微软雅黑");
            
            if (!lns[1].StartsWith("{\\colortbl"))
            {
                List<string> s = new List<string>(lns);
                s.Insert(1, "{\\colortbl ;}");
                lns = s.ToArray();
            }
            lns[1] = addcolor(lns[1], 0, 0, 160);
            lns[1] = addcolor(lns[1], 0, 50, 160);
            lns[1] = addcolor(lns[1], 255, 0, 0);
            ori = string.Join("\n", lns);
            /*全行、包括、关键字*/
            ctrld(ref ori, style.onehash, "Consolas", 48, rgb: new int[] { 0, 0, 160 });
            ctrld(ref ori, style.twohash, "Consolas", 40, rgb: new int[] { 0, 0, 160 });
            ctrld(ref ori, style.threehash, "Consolas", 32, rgb: new int[] { 0, 50, 160 });
            ctrld(ref ori, style.fourhash, "Consolas", 32, rgb: new int[] { 0, 50, 160 });
            ctrld(ref ori, style.exclamation, rgb: new int[] { 0, 0, 160 });
            
            //ctrld(ref ori, style.oneasterisk, italic: true, rgb: new int[] { 255, 0, 0 });
            ctrld(ref ori, style.twoasterisk, bold: true, rgb: new int[] { 255, 0, 0 });
            cjk(ref ori);
            return ori;
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
        static string torgxbyte(string cha)
        {
            byte[] bt = Encoding.GetEncoding("gbk").GetBytes(cha);
            if (bt.Length == cha.Length)
            {
                return cha;
            }
            else
            {
                string t = "";
                for (int i = 0; i < bt.Length; i++)
                {
                    t += "\\\\'" + bt[i].ToString("x2");
                }
                return t;
            }
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
        static void ctrld(ref string ori, string dest, string font = "Consolas", int fsize = 0, bool bold = false, bool italic = false,bool under = false, params int[] rgb)
        {
            Regex rgxd = new Regex(dest);
            if (rgxd.IsMatch(ori))
            {
                Regex rgxf = new Regex(@"(?<={)(\\f\d+)(?=\\f((?!}).)*" + torgxbyte(font) + @")");
                //Regex rgxc = new Regex(@"{\\colortbl.*blue\d+;}");
                Regex rgxc = new Regex(@"(?<=\\(red|green|blue))(\d*?)(?=\\|;)");
                string fontf = rgxf.Match(ori).Value;
                string fontsf = fsize == 0 ? "" : "\\fs" + fsize.ToString();
                int colori = 0;
                string colorf = "";
                if (rgb != new int[] { 0, 0, 0 })
                {
                    MatchCollection colormc = rgxc.Matches(ori);
                    for (int i = 0; i < colormc.Count; i += 3)
                    {
                        if (rgb[0].ToString() == colormc[i].Value && rgb[1].ToString() == colormc[i + 1].Value && rgb[2].ToString() == colormc[i + 2].Value)
                        {
                            colori = i / 3 + 1;
                            break;
                        }
                    }
                    colorf = "\\cf" + colori.ToString();
                }
                string boldf = bold ? "\\b" : "";
                string boldn = "\\b0";
                string italicf = italic ? "\\i" : "";
                string itaticn = "\\i0";
                string underf = under ? "\\ul" : "";
                string undern = "\\ulnone";
                //Regex rgxtype;
                switch (dest)
                {
                    case style.onehash:
                        Regex rgxtype = new Regex(style.findonehash);
                        ori = rgxtype.Replace(ori, "");
                        break;
                    case style.twohash:
                        Regex rgxtype2 = new Regex(style.findtwohash);
                        ori = rgxtype2.Replace(ori, "");
                        break;
                    case style.threehash:
                        Regex rgxtype3 = new Regex(style.findthreehash);
                        ori = rgxtype3.Replace(ori, "");
                        break;
                    case style.fourhash:
                        Regex rgxtype4 = new Regex(style.findfourhash);
                        ori = rgxtype4.Replace(ori, "");
                        break;
                    default:
                        break;
                }
                //Regex rgxtype = new Regex(@"(\\((f|fs|cf)\d+)|ul|i|b)(?=\S*? #\S*)");
                //ori = rgxtype.Replace(ori, "");
                MatchCollection mc = rgxd.Matches(ori);
                for(int i = 0; i < mc.Count; i++)
                {
                    ori = rgxd.Replace(ori, colorf + fontf + fontsf + boldf + italicf + underf + " " + mc[i].Value+"\\b0\\i0\\ulnone");
                }
                
            }
        }
        static void cjk(ref string ori)
        {
            string[] lns = ori.Split('\n');
            string line1 = lns[0];
            string line2 = "";
            Regex rgxf = new Regex(@"(?<={)(\\f\d+)(?=\\f((?!}).)*" + torgxbyte("微软雅黑") + @")");
            Regex rgxfs = new Regex(@"(?<={)(\\f\d+)(?=\\f((?!}).)*" + torgxbyte("宋体") + @")");
            Regex rgxfc = new Regex(@"(?<={)(\\f\d+)(?=\\f((?!}).)*Consolas)");
            string yahei = rgxf.Match(line1).Value;
            string song = rgxfs.Match(line1).Value;
            string consolas = rgxfc.Match(line1).Value;
            List<string> ls = lns.ToList();
            string maintext;
            if (lns[1].StartsWith("{\\colortbl"))
            {
                line2 = lns[1];
                ls.RemoveRange(0, 2);
                maintext = string.Join("\n", ls.ToArray());
            }
            else
            {

                ls.RemoveAt(0);
                maintext = string.Join("\n", ls.ToArray());
            }
            Regex rgxc = new Regex(@"(\\f\d+)(\\'([a-z0-9]){2})+");
            MatchCollection mc = rgxc.Matches(maintext);
            for(int i = 0; i < mc.Count; i++)
            {
                maintext = rgxc.Replace(maintext, yahei + mc[i].Value.Replace(song, "") + consolas);
            }
            string[] s = new string[] { line1, line2, maintext };
            ori = string.Join("\n", s);
        }
        static string removemany(string ori, params string[] st)
        {
            foreach(string s in st)
            {
                ori = ori.Replace(s, "");
            }
            return ori;
        }
    }
}
