using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLib.Packaging.Html
{
    public class BaseHtmlNode
    {
        public int DeepLevel { get; private set; }
        public string Text { get; set; }
        public string TagOpen { get; private set; }//<div>, <p>...
        public string TagClose { get; set; }//</div>, </p>...
        public readonly TypeTag typeTag;
        /// <summary>
        /// Конструктор для инициализации парного тега
        /// </summary>
        /// <param name="deepLevel"></param>
        /// <param name="text"></param>
        /// <param name="tagOpen"></param>
        /// <param name="tagClose"></param>
        public BaseHtmlNode(int deepLevel, string text, string tagOpen, string tagClose)
        {
            DeepLevel = deepLevel;
            Text = text;
            TagOpen = tagOpen;
            TagClose = tagClose;
            typeTag = TypeTag.Double;
        }
        /// <summary>
        /// Конструктор для инициализации одинарного тега
        /// </summary>
        /// <param name="deepLevel"></param>
        /// <param name="text">поное наполнение тега</param>
        public BaseHtmlNode(int deepLevel, string text)
        {
            DeepLevel = deepLevel;
            Text = text;
            typeTag = TypeTag.Single;
        }

        public enum TypeTag
        {
            Double, Single
        }
    }
}
