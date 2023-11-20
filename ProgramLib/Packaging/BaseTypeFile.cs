using System.IO;

namespace ProgramLib.Packaging
{
    public class BaseTypeFile {
        public const int blockSize = 1024;//* 1024;//1 MB

        public virtual void WriteToFile(string text, string path) {
            var writer = new StreamWriter(path, true);
            writer.Write(text);
            writer.Close();
        }

        public string SplitText(string text) {
            text = text.Replace("\r", "");
            text = text.Replace("\n", "");
            text = text.Replace("\t", "");

            string outText = "";
            var l = text.Split(' ');
            foreach (var a in l)
                outText += (a.Trim().Length > 0) ? a.Trim() + " " : "";
            return outText.Trim();
        }
    }
}
