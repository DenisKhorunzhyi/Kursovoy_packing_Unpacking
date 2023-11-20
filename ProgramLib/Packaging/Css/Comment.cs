namespace ProgramLib.Packaging.Css {
    public class Comment : BaseCssNode {
        public string Text { get; }
        public Comment(string text) => Text = text;
        public override string ToString() => $"/*{Text}*/\n";
    }
}
