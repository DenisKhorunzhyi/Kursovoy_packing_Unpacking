namespace ProgramLib.Packaging.Css {
    public class MainLine : BaseCssNode {
        public string Text { get; }
        public MainLine(string text) => Text = text;
        public override string ToString() => $"{Text};\n";
    }
}
