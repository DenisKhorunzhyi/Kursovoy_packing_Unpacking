namespace ProgramLib.Packaging.Html
{
    /// <summary>
    /// Класс представляющий один узел дерева
    /// </summary>
    public class Node
    {
        /// <summary>
        /// тег
        /// </summary>
        public BaseHtmlNode HtmlNode { get; set; }
        /// <summary>
        /// родитель (тег, в который вкладывается текущий тег)
        /// </summary>
        public Node Parent { get; set; }
        /// <summary>
        /// Первый потомок (первый вложенный тег)
        /// </summary>
        public Node FirstChild { get; set; }
        /// <summary>
        /// Предыдущий сосед (предыдущий тег который на том же уровне что и текущий)
        /// </summary>
        public Node BackSibling { get; set; }
        /// <summary>
        /// Следующий сосед (следующий тег который на том же уровне что и текущий)
        /// </summary>
        public Node NextSibling { get; set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="htmlNode">Информация об теге</param>
        /// <param name="firstChild">Первый потомок</param>
        /// <param name="nextSibling">Следующий сосед</param>
        public Node(BaseHtmlNode htmlNode){
            this.HtmlNode = htmlNode;
            this.Parent = null;
            this.FirstChild = null;
            this.BackSibling = null;
            this.NextSibling = null;
        }
        public Node(BaseHtmlNode htmlNode, Node firstChild, Node backSibling, Node nextSibling, Node parent) {
            this.HtmlNode = htmlNode;
            this.Parent = parent;
            this.FirstChild = firstChild;
            this.BackSibling = backSibling;
            this.NextSibling = nextSibling;
        }
    }
}
