using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLib.Packaging.Html
{
    public class Tree
    {
        /// <summary>
        /// Первый элемент дерева или своего рода корень
        /// </summary>
        public Node root { get; private set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="node">первый элемент дерева</param>
        public Tree(Node node) {
            root = new Node(node.HtmlNode, node.FirstChild, node.BackSibling, node.NextSibling, node.Parent);
            currentNode = root;
        }
        /// <summary>
        /// Конструктор для сериализатора
        /// </summary>
        public Tree() {
            root = new Node(new BaseHtmlNode(0, ""));
            currentNode = root;
        }

        public void CreateTree(ref string text)
        {
            do
            {
                try
                {
                    CreateOneLevelTree(ref text, currentNode);
                }
                catch
                {
                    return;
                }
                currentNode = GetParentNode(currentNode);
            }
            while (text.Length > 0);
        }
        public Node currentNode;
        public void CreateOneLevelTree(ref string text, Node parrent)
        {
            int deepLevel = parrent.HtmlNode.DeepLevel + 1;
            do
            {
                text = text.Trim();
                int indexOpenTag = text.IndexOf("<");

                if (indexOpenTag == 0) { }
                else if (indexOpenTag > 0)
                {//перед тегом был текст который принадлежит родителю
                    string _text = text.Substring(0, indexOpenTag);
                    parrent.HtmlNode.Text = _text;
                    text = text.Remove(0, indexOpenTag);
                    indexOpenTag = 0;
                }
                else if (indexOpenTag == -1 && text.Length > 0)
                {//присутствует комбинация текста но тега не обнаружена - обрыв
                    currentNode = parrent;
                    throw new IndexOutOfRangeException("");
                }
                else if (indexOpenTag == text.Length - 1)
                {//индекс отрывающего тега является последним в тексте - обрыв
                    currentNode = parrent;
                    throw new IndexOutOfRangeException("");
                }

                int indexCloseTag = text.IndexOf(">");

                if (text.Substring(indexOpenTag, 2).Equals("</")){//обнаружен конец родительского тега
                    if(indexCloseTag == -1)
                    {//если управление доходит сюда, то значит не получилось найти конец закрывающего тега - присутствует обрыв
                        currentNode = parrent;
                        throw new IndexOutOfRangeException("");
                    }
                    else
                    {
                        parrent.HtmlNode.TagClose = text.Substring(indexOpenTag, indexCloseTag - indexOpenTag + 1);
                        text = text.Remove(indexOpenTag, indexCloseTag - indexOpenTag + 1);
                        return;
                    }
                }
                
                if(((indexOpenTag + 4) < text.Length) && text.Substring(indexOpenTag, 4).Equals("<!--"))
                {//текущий тег комментарий
                    int indexCloseComment = text.IndexOf("-->");
                    if(indexCloseComment > 0)
                    {
                        string content = text.Substring(indexOpenTag, indexCloseComment - indexOpenTag + 3);
                        Node newNode = new Node(new BaseHtmlNode(deepLevel, content));
                        AddSubNode(parrent, newNode);
                        text = text.Remove(indexOpenTag, (indexCloseComment + 3) - indexOpenTag);
                    }
                    else
                    {//комментарий обрывается значит нужно получить продолжение строки что бы полностью собрать тег
                        currentNode = parrent;
                        throw new IndexOutOfRangeException("");
                    }
                    continue;
                }

                //работа с одноуровневыми тегами
                {
                    int indexCloseTag1 = text.IndexOf(">");
                    int indexCloseTag2 = text.IndexOf("/>");
                    indexCloseTag = -1;
                    do
                    {
                        if (IsSingleTag(text, indexOpenTag))//одиночные теги
                        {
                            if ((indexCloseTag1 < indexCloseTag2 && indexCloseTag1 != -1 && indexCloseTag2 != -1) ||
                                                                    indexCloseTag1 != -1 && indexCloseTag2 == -1)
                            {
                                indexCloseTag = indexCloseTag1;
                                string content = text.Substring(indexOpenTag, indexCloseTag - indexOpenTag + 1);
                                Node newNode = new Node(new BaseHtmlNode(deepLevel, content));
                                AddSubNode(parrent, newNode);
                                text = text.Remove(indexOpenTag, indexCloseTag - indexOpenTag + 1);
                            }
                            else if ((indexCloseTag2 < indexCloseTag1 && indexCloseTag2 != -1 && indexCloseTag1 != -1) ||
                                                                         indexCloseTag2 != -1 && indexCloseTag1 == -1)
                            {
                                indexCloseTag = indexCloseTag2;
                                string content = text.Substring(indexOpenTag, indexCloseTag - indexOpenTag + 2);
                                Node newNode = new Node(new BaseHtmlNode(deepLevel, content));
                                AddSubNode(parrent, newNode);
                                text = text.Remove(indexOpenTag, indexCloseTag - indexOpenTag + 2);
                            }
                            else
                            {// обрыв
                                currentNode = parrent;
                                throw new IndexOutOfRangeException("");
                            }
                            break;

                        }
                        else if ((indexCloseTag2 != -1 && indexCloseTag1 != -1 && indexCloseTag2 < indexCloseTag1) ||
                                 (indexCloseTag2 != -1 && indexCloseTag1 == -1))//     />
                        {
                            indexCloseTag = indexCloseTag2;
                            string content = text.Substring(indexOpenTag, indexCloseTag - indexOpenTag + 2);
                            text = text.Remove(indexOpenTag, indexCloseTag - indexOpenTag + 2);
                            Node newNode = new Node(new BaseHtmlNode(deepLevel, content));
                            AddSubNode(parrent, newNode);
                            break;
                        }
                        else if ((indexCloseTag1 != -1 && indexCloseTag2 != -1 && indexCloseTag1 < indexCloseTag2) || 
                                 (indexCloseTag1 != -1 && indexCloseTag2 == -1))//     >
                        {
                            indexCloseTag = indexCloseTag1;
                            string tag = text.Substring(indexOpenTag, indexCloseTag - indexOpenTag + 1);
                            text = text.Remove(indexOpenTag, indexCloseTag - indexOpenTag + 1);
                            Node newNode = new Node(new BaseHtmlNode(deepLevel, "", tag, ""));
                            AddSubNode(parrent, newNode);
                            CreateOneLevelTree(ref text, newNode);
                            break;
                        }
                        else
                        {//конец тега не находится в блоке текста - обрыв
                            currentNode = parrent;
                            throw new IndexOutOfRangeException("");
                        }
                    } while (text.Length > 0);
                }
            }
            while (text.Trim().Length > 0);
        }

        public void PackagingToString(ref string result)
        {
            Node startNode = currentNode;

            var queue = new Queue<Node>();
            queue.Enqueue(startNode.FirstChild);
            if (queue.Peek() == null)
            {
                return;
            }
            else//проход по соседним (паралельным тегам)
            {
                while (queue.Peek() != null && queue.Count != 0)
                {
                    if(queue.Peek().HtmlNode.typeTag == BaseHtmlNode.TypeTag.Double)
                    {
                        result += queue.Peek().HtmlNode.TagOpen;
                        result += queue.Peek().HtmlNode.Text;
                        currentNode = queue.Peek();
                        PackagingToString(ref result);
                        result += queue.Peek().HtmlNode.TagClose;
                    }
                    else if (queue.Peek().HtmlNode.typeTag == BaseHtmlNode.TypeTag.Single)
                    {
                        result += queue.Peek().HtmlNode.Text;
                    }
                    queue.Enqueue(queue.Dequeue().NextSibling);
                }
            }
        }

        private bool IsSingleTag(string text, int indexOpenTag)
        {
            string[] list = new string[] {
                "!doctype","area","base","br","col","embed","hr","img","input","keygen","link","meta","param","source","track","wbr"
            };
            int indexCloseTag1 = text.IndexOf(">", indexOpenTag);
            int indexCloseTag2 = text.IndexOf("/>", indexOpenTag);
            string tag = "";
            if ((indexCloseTag1 < indexCloseTag2 && indexCloseTag1 != -1 && indexCloseTag2 != -1) ||
                                                    indexCloseTag1 != -1 && indexCloseTag2 == -1)
            {
                tag = text.Substring(indexOpenTag, indexCloseTag1 - indexOpenTag + 1);
            }
            else if ((indexCloseTag2 < indexCloseTag1 && indexCloseTag2 != -1 && indexCloseTag1 != -1) ||
                                                        indexCloseTag2 != -1 && indexCloseTag1 == -1)
            {
                tag = text.Substring(indexOpenTag, indexCloseTag2 - indexOpenTag + 2);
            }
            
            for (int i = 0; i < list.Length; i++)
                if (tag.ToLower().Contains(list[i])) return true;
            return false;
        }
        private int LessIndexClose(string text)
        {
            int indexCloseTag1 = text.IndexOf(">");
            int indexCloseTag2 = text.IndexOf("/>");
            if (indexCloseTag1 < indexCloseTag2 && indexCloseTag1 != -1)
                return indexCloseTag1;
            else if (indexCloseTag2 < indexCloseTag1 && indexCloseTag2 != -1)
                return indexCloseTag2;
            else throw new IndexOutOfRangeException("");
        }

        /// <summary>
        /// Дабавление узла в дочерний елемент родителя или, если дочерний елемент есть, 
        /// то выставляет соседний елемент к дочернему (соседний к дочернему в самый конец 
        /// если таких элементов больше 1)
        /// </summary>
        /// <param name="Parent"></param>
        /// <param name="node"></param>
        public void AddSubNode(Node Parent, Node node)
        {
            var queue = new Queue<Node>(); // создать новую очередь
            queue.Enqueue(Parent.FirstChild); // поместить в очередь первый элемент с одного уровня
            if (queue.Peek() == null)//когда нужно дабавить новый подуровень
            {
                //node.BackSibling = null;
                node.Parent = Parent;
                Parent.FirstChild = node;
            }
            else//когда нужно дабавить в уже созданый ранее подуровень
            {
                while (queue.Count != 0)
                {
                    if (queue.Peek().NextSibling != null)
                    {
                        queue.Enqueue(queue.Peek().NextSibling);
                    }
                    else//нашли место для добавления нового узла
                    {
                        node.BackSibling = queue.Peek();
                        queue.Peek().NextSibling = node;
                    }
                    queue.Dequeue();
                }
            }
        }
        private Node GetParentNode(Node CurrentNode)
        {
            var queue = new Queue<Node>();
            //queue.Enqueue(CurrentNode.Parent);
            if (CurrentNode.Parent != null)//родительский тег было сразу обнаружено у загруженого тега
            {
                return CurrentNode.Parent;
            }
            else//проход по соседним (паралельным тегам) для поиска родительского тега
            {
                queue.Enqueue(CurrentNode.BackSibling);
                while (queue.Count != 0)
                {
                    if(queue.Peek().Parent != null)
                    {
                        return queue.Peek().Parent;
                    }
                    else if (queue.Peek().BackSibling != null)
                    {
                        queue.Enqueue(queue.Peek().BackSibling);
                    }
                    else
                    {
                        return queue.Peek();
                    }
                    queue.Dequeue();
                }
            }
            return null;
        }
        //private Node GetDeepestNode(Node CurrentNode)
        //{
        //    var queue = new Queue<Node>();
        //    queue.Enqueue(CurrentNode.Parent);
        //    if (queue.Peek() != null)//родительский тег было сразу обнаружено у загруженого тега
        //    {
        //        return queue.Peek();
        //    }
        //    else//проход по соседним (паралельным тегам) для поиска родительского тега
        //    {
        //        while (queue.Count != 0)
        //        {
        //            if (queue.Peek().BackSibling != null)
        //            {
        //                queue.Enqueue(queue.Peek().NextSibling);
        //            }
        //            else if (queue.Peek().Parent != null)
        //            {
        //                return queue.Peek().Parent;
        //            }
        //            else
        //            {
        //                return queue.Peek();
        //            }
        //            queue.Dequeue();
        //        }
        //    }
        //    return null;
        //}












    }
}
