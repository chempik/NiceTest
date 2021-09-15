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
        private const string _wordBoundaryRegex = "(\\b|\\W)";
        private const string _ignoreSpaceAndEnterRegex = "(\\s|\\n|\\r\\n|)*";
        private const string _enterRegex = "\\r\\n";
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

            substring = IgnoreSpace(substring, setting.IgnoreSpace);

            if (setting.CaseSensitive)
            {
                regexOption |= RegexOptions.IgnoreCase;
            }

            if (setting.FullWord)
            {
               substring = _wordBoundaryRegex + substring + _wordBoundaryRegex;
            }

            var enterSplitList = RuleRegex(new Regex(_enterRegex), fileСontent);

            var ruleResultList = RuleRegex(new Regex(substring, regexOption), fileСontent);

            return new Result(ruleResultList.Count, StringNumbers(ruleResultList,enterSplitList));
        }
        private List<int> StringNumbers(List<Match> ruleResultList, List<Match> enterSplitList)
        {
            var lineNumber = new HashSet<int>();

            for (int i = 0; i < ruleResultList.Count; i++)
            {
                lineNumber.Add(enterSplitList.FindLastIndex(x => x.Index < ruleResultList[i].Index) + 2);
            }

            return lineNumber.ToList();
        }

        private List<Match> RuleRegex(Regex regex, string fileСontent)
        {
           return regex.Matches(fileСontent).ToList();
        } 

        private string IgnoreSpace (string substring, bool ignoreSpace) 
        {
            if (ignoreSpace)
            {
                var substringSplit = substring.ToCharArray();
                string tmp = "";

                for (int i = 0; i < substringSplit.Length - 1; i++)
                {
                    tmp = tmp + Regex.Escape(substringSplit[i].ToString()) + _ignoreSpaceAndEnterRegex;
                }

                tmp = tmp + Regex.Escape(substringSplit[substringSplit.Length - 1].ToString());
                return tmp;
            }

            else
            {
                return Regex.Escape(substring);
            }
        }
    }
}
