using System.Collections.Generic;

namespace ProgramLib.Packaging.Css {
    public class CssRule : BaseCssNode {
        public string Selectors { get; }
        public List<DeclarationBlock> DeclarationBlocks { get; set; }
        public CssRule(string selectors) {
            DeclarationBlocks = new List<DeclarationBlock>();
            Selectors = selectors;
        }
        private string BlockToString() {
            string result = "";
            foreach (var dataLine in DeclarationBlocks)
                result += dataLine.ToString();
            return result;
        }

        public override string ToString() => $"{Selectors} {{\n{BlockToString()}}}\n";

        public class DeclarationBlock {
            public string Property { get; }
            public string Value { get; }
            public DeclarationBlock(string property, string value) {
                Property = property;
                Value = value;
            }
            public override string ToString() => $"  {Property} : {Value};\n";
        }
    }
}
