using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wou
{
    class style
    {
        /*  regexes for rtf
        public const string onehash = @"(?<=\s)(#(?!#).*)(?=\\.*par)";
        public const string twohash = @"(?<=\s)(##(?!#).*)(?=\\.*par)";
        public const string threehash = @"(?<=\s)(###(?!#).*)(?=\\.*par)";
        public const string fourhash = @"(?<=\s)(####.*)(?=\\.*par)";
        public const string exclamation = @"\!\[.*\]\(.*\)";
        public const string oneasterisk = @"\*((?!\*).)+\*";
        public const string oneunder = @"_((?!_).)+_";
        public const string twoasterisk = @"\*\*(\s|\S)*?\*\*";
        public const string twounder = @"__(\s|\S)*?__";
        public const string bracklet = @"(<.+>)|(\(.+\))|(\[.+\])";
        public const string kw = @"&gt;";
        public const string findonehash = @"(\\((f|fs|cf)\d+)|ul|i|b)(?=\S*? #(?!#)\S*)";
        public const string findtwohash = @"(\\((f|fs|cf)\d+)|ul|i|b)(?=\S*? ##(?!#)\S*)";
        public const string findthreehash = @"(\\((f|fs|cf)\d+)|ul|i|b)(?=\S*? ###(?!#)\S*)";
        public const string findfourhash = @"(\\((f|fs|cf)\d+)|ul|i|b)(?=\S*? ####\S*)";
        */
        public const string onehash = @"(^|\s)#(?!#).*";
        public const string twohash = @"(^|\s)##(?!#).*";
        public const string threehash = @"(^|\s)###(?!#).*";
        public const string fourhash = @"(^|\s)####.*";
        public const string exclamation = @"\!\[.*\]\(.*\)";
        public const string oneasterisk = @"\*((?!\*).)+\*";
        public const string oneunder = @"_((?!_).)+_";
        public const string twoasterisk = @"\*\*(\s|\S)*?\*\*";
        public const string twounder = @"__(\s|\S)*?__";
        public const string bracklet = @"(<.+>)|(\(.+\))|(\[.+\])";
        public const string kw = @"&gt;";
        public const string findonehash = @"(\\((f|fs|cf)\d+)|ul|i|b)(?=\S*? #(?!#)\S*)";
        public const string findtwohash = @"(\\((f|fs|cf)\d+)|ul|i|b)(?=\S*? ##(?!#)\S*)";
        public const string findthreehash = @"(\\((f|fs|cf)\d+)|ul|i|b)(?=\S*? ###(?!#)\S*)";
        public const string findfourhash = @"(\\((f|fs|cf)\d+)|ul|i|b)(?=\S*? ####\S*)";
    }
}
