using ProgramLib.Packaging;
using ProgramLib.Packaging.Css;
using ProgramLib.Packaging.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackagingAndCodingUtil
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //HTML
            string[] names = Directory.GetFiles("tic-tac-bot-master", "*.html");
            foreach (var name in names)
            {
                new Html().Packaging(name);
            }

            //string[] names = Directory.GetFiles("tic-tac-bot-master", "*.html");
            //foreach (var name in names)
            //{
            //    new Html().Unpackaging(name);
            //}

            //CSS
            //string[] names = Directory.GetFiles("tic-tac-bot-master", "*.css");
            //foreach (var name in names)
            //{
            //    new Css().Packaging(name);
            //}

            //string[] names = Directory.GetFiles("tic-tac-bot-master", "*.css");
            //foreach (var name in names)
            //{
            //    new Css().Unpackaging(name);
            //}


            //new Css().Packaging(@"template-style.css", "paked.css");
            //new Css().Unpackaging(@"paked.css", "unpaked.css");

            new Html().Packaging(@"tic-tac-bot-master\tic-tac-bot-master\index.html", "result.html");
            //new Css().Unpackaging(@"paked.css", "unpaked.css");

            //JS
            //new Js().Unpackaging(@"jquery.js", @"1jquery.js");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
