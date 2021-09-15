using System;
using System.Collections.Generic;
using System.Text;

namespace NiceTest
{
    public class Setting
    {
        public bool CaseSensitive { get; set; }
        public bool FullWord { get; set; }
        public bool IgnoreSpace { get; set; }

        public Setting (bool caseSensitive, bool fullWord, bool ignoreSpace)
        {
            CaseSensitive = caseSensitive;
            FullWord = fullWord;
            IgnoreSpace = ignoreSpace;
        }
        public Setting() { }
    }
}
