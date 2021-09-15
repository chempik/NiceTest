using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NiceTest
{
    public class Substring
    {
        private IFileSystem _fileSystem;
        public Substring(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public Result Check(string file, Setting setting, string substring)
        {
            var fileReader = new FileReader(_fileSystem);
            string fileСontent = fileReader.Read(file);
            var regexOption = RegexOptions.None;

            if (setting.IgnoreSpace)
            {
                substring = IgnoreSpace(substring);
            }
            else
            {
                substring = Regex.Escape(substring);
            }

            if (setting.CaseSensitive)
            {
                regexOption |= RegexOptions.IgnoreCase;
            }

            if (setting.FullWord)
            {
               substring = "(\\b|\\W)" + substring + "(\\b|\\W)";
            }

            var enterRegex = new Regex("\\r\\n");
            var enterList = enterRegex.Matches(fileСontent).ToList();

            var regex = new Regex(substring, regexOption);
            var list = regex.Matches(fileСontent);
            
            var lineNumber = new HashSet<int>();
            for (int i = 0; i < list.Count; i++)
            { 
                lineNumber.Add(enterList.FindLastIndex(x => x.Index < list[i].Index) + 2);
            }

            return new Result(list.Count, lineNumber.ToList());
        }

        private string IgnoreSpace (string substring)
        {
            var substringSplit = substring.ToCharArray();
            string tmp = "";

            for (int i = 0; i < substringSplit.Length - 1; i++)
            {
                tmp = tmp + Regex.Escape(substringSplit[i].ToString()) + "(\\s|\\n|\\r\\n|)*";
            }

            tmp = tmp + Regex.Escape(substringSplit[substringSplit.Length - 1].ToString());
            return tmp;
        }
    }
}
