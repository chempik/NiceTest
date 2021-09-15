using System;
using System.Collections.Generic;
using System.Text;

namespace NiceTest
{
    public class Result
    {
        public int NumberOfResults { get; internal set; }
        public List<int> LineNumber { get; internal set; }

        public Result() { }

        public Result (int numberOResults, List<int> lineNumber)
        {
            NumberOfResults = numberOResults;
            LineNumber = lineNumber;
        }
    }
}
