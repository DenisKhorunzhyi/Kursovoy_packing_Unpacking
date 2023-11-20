using System;
using System.Collections.Generic;

namespace ProgramLib.Packaging
{
    public class PackagingMaster
    {
        public PackagingMaster()
        {
            throw new NotImplementedException();
        }

        public void Unpackaging(List<string> pathToFiles)
        {
            foreach(string path in pathToFiles)
            {
                try
                {
                    var obj = GetPackagingObject(GetTypeFile(path));
                    obj.Unpackaging(path);
                }
                catch { }
            }
        }
        public void Packaging(List<string> pathToFiles)
        {
            foreach (string path in pathToFiles)
            {
                try
                {
                    var obj = GetPackagingObject(GetTypeFile(path));
                    obj.Packaging(path);
                }
                catch { }
            }
        }

        public IPackaging GetPackagingObject(string type)
        {
            IPackaging result = null;
            switch (type.ToLower())
            {
                case "js":
                    result = new Js();
                    break;
                case "css":
                    result = new Css.Css();
                    break;
                case "html":
                    result = new Html.Html();
                    break;
                default :
                    throw new FormatException();
            }
            return result;
        }
        private string GetTypeFile(string pathToFile)
        {
            throw new NotImplementedException();
        }
    }
}
