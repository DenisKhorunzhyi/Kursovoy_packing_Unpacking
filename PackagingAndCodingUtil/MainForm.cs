using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackagingAndCodingUtil
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void doOperationOnText_Click(object sender, EventArgs e)
        {
            string[] words =
               {
                "Dot",
                "Net",
                "Perls",
                "is",
                "a",
                "nice",
                "website."
            };
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                Color color = Color.Yellow;
                {
                    textIn.SelectionBackColor = color;
                    textIn.AppendText(word);
                    textIn.SelectionBackColor = Color.AliceBlue;
                    textIn.AppendText(" ");
                }
            }
        }

        private void chooseFiles_Click(object sender, EventArgs e)
        {

        }

        private void doOperationOnFiles_Click(object sender, EventArgs e)
        {

        }

        private void endSeparatorCB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void encodeCB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void spaceOrTabCB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void replaceTextCB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void coloringTextCB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Clear()
        {

        }
        private void InstallStartState()
        {

        }
        private void BlockingAllComboBoxesAndChildsItem()
        {

        }
        private List<string> ToListPath(string allPathes)
        {
            throw new NotImplementedException();
        }
        private string ToStringPath(List<string> allPathes)
        {
            throw new NotImplementedException();
        }
        private string ColoringText(string allText, string forColoring)
        {
            throw new NotImplementedException();
        }


        #region 2 Tab

        private void chooseDirectory_Click(object sender, EventArgs e)
        {

        }

        private void packaging_Click(object sender, EventArgs e)
        {

        }

        private void unpackaging_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private void formTabs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
