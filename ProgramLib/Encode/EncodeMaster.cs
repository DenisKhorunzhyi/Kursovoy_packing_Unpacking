using System;
using System.Collections.Generic;
using System.Text;

namespace PackagingAndCodingUtil.Encode
{
    public class EncodeMaster
    {
        public Encoding TableEncoding { get; private set; }

        public EncodeMaster() { }

        public void ReplaceEndSeparator(List<string> pathToFiles)
        {
            throw new NotImplementedException();
        }
        public string ReplaceEndSeparator(string text)
        {
            throw new NotImplementedException();
        }
        
        public void Convert(Encoding srcEncoding, Encoding dstEncoding, List<string> pathToFiles)
        {
            throw new NotImplementedException();
        }
        public void Convert(Encoding srcEncoding, Encoding dstEncoding, byte[] bytes)
        {
            throw new NotImplementedException();
        }
        public void Convert(Encoding srcEncoding, Encoding dstEncoding, string text)
        {
            throw new NotImplementedException();
        }

        public string ReplaceSpaceOrTab(List<string> pathToFiles, string resultSymbol)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Заміна символу табуляції на пробіли чи навпаки
        /// </summary>
        /// <param name="text">початковий текст</param>
        /// <param name="resultSymbol">на який символ необхідно замінити</param>
        /// <returns></returns>
        public string ReplaceSpaceOrTab(string text, string resultSymbol)
        {
            string result = text;
            switch (resultSymbol)
            {
                case "\t":
                    result = ReplaceAll(result, "   ", "\t");
                    break;
                case " ":
                    result = ReplaceAll(result, "\t", "   ");
                    break;
                default: throw new FormatException();
            }
            return result;
        }

        public string ReplaceAll(List<string> pathToFiles, string source, string destination)
        {
            throw new NotImplementedException();
        }
        public string ReplaceAll(string text, string source, string destination)
        {
            throw new NotImplementedException();
        }
    }
}
