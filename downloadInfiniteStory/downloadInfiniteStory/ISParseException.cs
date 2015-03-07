using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace downloadInfiniteStory
{
    class ISParseException : Exception
    {
        private string p;
        private Exception e;

        public ISParseException()
            :base()
        { }

        public ISParseException(string p)
            :base(p)
        { }

        public ISParseException(string p, Exception e)
            :base(p,e)
        { }
    }
}
