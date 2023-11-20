using SoftCircuits.JavaScriptFormatter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLib.Packaging
{
    public class Js : BaseTypeFile, IPackaging
    {
        public void Packaging(string path) {
            string newName = Path.Combine(Path.GetDirectoryName(path), "~" + Path.GetFileName(path));
            Packaging(path, newName);
            File.Copy(newName, path, true);
            File.Delete(newName);
        }

        public void Packaging(string pathSource, string pathDestination)
        {
            //File.WriteAllText(pathDestination, "");
            //using (StreamReader reader = new StreamReader(pathSource))
            //{
            //    long fileSize = new FileInfo(pathSource).Length;//Bytes
            //    int counter = 1;
            //    string remainder = "";
            //    while (!reader.EndOfStream)
            //    {
            //        char[] buffer = (fileSize > (counter * blockSize)) ? new char[blockSize]
            //                                                           : new char[((counter - 1) == 0) ? fileSize
            //                                                                                            : fileSize - ((counter - 1) * blockSize)];
            //        reader.ReadBlock(buffer, 0, (buffer.Length < blockSize) ? buffer.Length : blockSize);
            //        string blockOfLine = new string(buffer);
            //        remainder += blockOfLine;
            //        remainder = SplitText(remainder);
            //        WriteToFile(DeleteComments(ref remainder), pathDestination);
            //        counter++;
            //    }
            //}
        }

        public void Unpackaging(string path)
        {
            string newName = Path.Combine(Path.GetDirectoryName(path), "~" + Path.GetFileName(path));
            Unpackaging(path, newName);
            File.Copy(newName, path, true);
            File.Delete(newName);
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
                                                                                                        : fileSize - ((counter - 1) * blockSize)];
                    reader.ReadBlock(buffer, 0, (buffer.Length < blockSize) ? buffer.Length : blockSize);
                    string blockOfLine = new string(buffer);
                    remainder += blockOfLine;

                    JavaScriptFormatter f = new JavaScriptFormatter(new FormatOptions());
                    remainder = f.Format(remainder);

                    WriteToFile(remainder, pathDestination);
                    counter++;
                }
            }
        }


    }
}
