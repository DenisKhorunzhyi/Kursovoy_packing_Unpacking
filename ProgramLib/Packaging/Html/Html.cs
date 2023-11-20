using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLib.Packaging.Html
{
    public class Html : BaseTypeFile, IPackaging
    {
        public void Packaging(string path)
        {
            throw new NotImplementedException();
        }

        public void Packaging(string pathSource, string pathDestination)
        {
            File.WriteAllText(pathDestination, "");
            Tree tree = new Tree();
            using (StreamReader reader = new StreamReader(pathSource))
            {
                long fileSize = new FileInfo(pathSource).Length;//Bytes
                int counter = 1;
                string remainder = "";
                while (!reader.EndOfStream)
                {
                    char[] buffer = (fileSize > (counter * blockSize)) ? new char[blockSize]
                                                                       : new char[((counter - 1) == 0) ? fileSize
                                                                                                        : fileSize - ((counter - 1) * blockSize)];
                    reader.ReadBlock(buffer, 0, (buffer.Length < blockSize) ? buffer.Length : blockSize);
                    string blockOfLine = new string(buffer);
                    remainder += blockOfLine;
                    //remainder = CleanTags(ref remainder) + remainder;

                    tree.CreateTree(ref remainder);//.....
                    counter++;
                }
                tree.currentNode = tree.root;
                string toSave = "";
                tree.PackagingToString(ref toSave);
                WriteToFile(toSave, pathDestination);//...
            }
        }

        public void Unpackaging(string path)
        {
            throw new NotImplementedException();
        }

        public void Unpackaging(string pathSource, string pathDestination)
        {
            File.WriteAllText(pathDestination, "");
            using (StreamReader reader = new StreamReader(pathSource))
            {
                long fileSize = new FileInfo(pathSource).Length;//Bytes
                int counter = 1;
                string remainder = "";
                while (!reader.EndOfStream)
                {
                    char[] buffer = (fileSize > (counter * blockSize)) ? new char[blockSize]
                                                                       : new char[((counter - 1) == 0) ? fileSize
                                                                                                        : ((counter - 1) * blockSize) - fileSize];
                    reader.ReadBlock(buffer, ((counter - 1) * blockSize), (buffer.Length < blockSize) ? buffer.Length : blockSize);
                    string blockOfLine = new string(buffer);
                    remainder += blockOfLine;
                    //WriteToFile(Unpack(ref remainder), pathDestination);
                    counter++;
                }
            }
        }

        private string CleanTags(ref string text)
        {
            string result = "";
            do
            {
                int indexOpen = text.IndexOf("<");
                int indexClose = text.IndexOf(">");

                if (indexOpen >= 0 && indexClose > 0 && indexOpen < indexClose)//тег полностью присутствует в блоке данных
                {
                    result += text.Substring(0, indexOpen).Trim() + " ";
                    result += CleanTag(text.Substring(indexOpen, indexClose - indexOpen + 1));
                    text = text.Remove(0, indexClose + 1);
                }
                else if(indexClose == -1 && indexOpen == -1)//теги закончились
                {
                    result += text.Trim();
                    text = "";
                    return result;
                }
                else if (indexOpen > 0)//тег начинается в блоке данных, но обрывается 
                {
                    return result;
                }
            }
            while (text.Length > 0);
            return result;
        }
        private string CleanTag(string tag)
        {
            string result = "";
            foreach(char s in tag)
                if (s > 32) result += s;
            return result;
        }
    }
}
