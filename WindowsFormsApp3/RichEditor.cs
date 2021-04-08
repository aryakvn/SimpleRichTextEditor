using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{

    /// <summary>
    /// A richTextBox editor library
    /// </summary>
    public class RichEditor
    {
        public RichTextBox richTextBox;
        public SaveFileDialog saveDialog;
        public OpenFileDialog rtfOpen;
        public OpenFileDialog imgOpen;
        public ToolStripStatusLabel statusBar;
        public FontDialog fontDialog;
        public ColorDialog colorDialog;
        public Button forColorIndicator;
        public Button backColorIndicator;
        public Form1 parrent;
        public bool isUnSaved = false;
        public int lastWordIndex;
        public bool isInSearch;
        public string findWord;


        /// <summary>
        /// Init 
        /// </summary>
        /// <param name="richTextBox">richTextBox instance</param>
        /// <param name="saveDialog"> saveDialog instance </param>
        /// <param name="rtfOpen"></param>
        /// <param name="imgOpen"></param>
        /// <param name="statusBar"></param>
        /// <param name="fontDialog"></param>
        /// <param name="colorDialog"></param>
        /// <param name="forColorIndicator"></param>
        /// <param name="backColorIndicator"></param>
        /// <param name="parrent"></param>
        public RichEditor(RichTextBox richTextBox,
             SaveFileDialog saveDialog,
             OpenFileDialog rtfOpen,
             OpenFileDialog imgOpen,
             ToolStripStatusLabel statusBar,
             FontDialog fontDialog,
             ColorDialog colorDialog,
             Button forColorIndicator,
             Button backColorIndicator,
             Form1 parrent)
        {
            this.richTextBox = richTextBox;
            this.saveDialog = saveDialog;
            this.rtfOpen = rtfOpen;
            this.imgOpen = imgOpen;
            this.statusBar = statusBar;
            this.fontDialog = fontDialog;
            this.colorDialog = colorDialog;
            this.forColorIndicator = forColorIndicator;
            this.backColorIndicator = backColorIndicator;
            this.parrent = parrent;

            this.forColorIndicator.BackColor = this.richTextBox.SelectionColor;
            this.backColorIndicator.BackColor = this.richTextBox.SelectionBackColor;
        }

        public void newAction()
        {
            if (this.isUnSaved)
            {
                DialogResult confirm = MessageBox.Show("Discard Previous Changes ?", "Text Editor", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.No) return;
            }
            this.richTextBox.Text = "";
            this.parrent.Text = "Untitled";
            this.statusBar.Text = "Open";

        }

        public void saveAction()
        {
            DialogResult save = this.saveDialog.ShowDialog();
            if (save == DialogResult.OK)
            {
                this.parrent.Text = this.saveDialog.FileName.ToString();
                this.richTextBox.SaveFile(this.saveDialog.FileName);
                this.isUnSaved = false;
                this.statusBar.Text = "Saved Changes";
            }
            else
            {
                this.parrent.Text += " - Unsaved";
                this.statusBar.Text = "Unsaved Changes";
            }
        }

        public void openAction()
        {
            DialogResult open = this.rtfOpen.ShowDialog();
            if (open == DialogResult.OK)
            {
                if (this.isUnSaved)
                {
                    DialogResult confirm = MessageBox.Show("Discard Previous Changes ?", "Text Editor", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.No) return;
                }
                this.richTextBox.LoadFile(this.rtfOpen.FileName);
                this.parrent.Text = this.rtfOpen.FileName.ToString();
                this.isUnSaved = false;
            }

        }

        public void copyAction()
        {
            this.richTextBox.Copy();
            this.statusBar.Text = "Copied";
        }

        public void cutAction()
        {
            this.richTextBox.Cut();
            this.statusBar.Text = "Cut";
        }

        public void pasteAction()
        {
            this.richTextBox.Paste();
        }

        public void undoAction()
        {
            this.richTextBox.Undo();
        }

        public void redoAction()
        {
            this.richTextBox.Redo();
        }

        public void selectAllAction()
        {
            this.richTextBox.SelectAll();
        }

        public void leftAlignAction()
        {
            this.richTextBox.SelectionAlignment = HorizontalAlignment.Left;
        }

        public void centerAlignAction()
        {
            this.richTextBox.SelectionAlignment = HorizontalAlignment.Center;
        }

        public void rightAlignAction()
        {
            this.richTextBox.SelectionAlignment = HorizontalAlignment.Right;
        }

        public void fontAction()
        {
            DialogResult fontRes = this.fontDialog.ShowDialog();
            if (fontRes == DialogResult.OK)
            {
                this.richTextBox.SelectionFont = this.fontDialog.Font;
                this.statusBar.Text = "Font Changed";
            }

        }

        public void colorAction()
        {
            DialogResult colorRes = this.colorDialog.ShowDialog();
            if (colorRes == DialogResult.OK)
            {
                this.richTextBox.SelectionColor = this.colorDialog.Color;
                this.forColorIndicator.BackColor = this.colorDialog.Color;
                this.statusBar.Text = "Color Changed";
            }
        }


        public void TextChanged()
        {
            this.isUnSaved = true;
            this.statusBar.Text = "Unsaved Changes";
        }

        public bool findAction()
        {
            Find find;
            find = new Find(false);
            DialogResult findDiaRes = find.ShowDialog();

            string word = find.textBox1.Text;
            this.findWord = word;

            if (findDiaRes == DialogResult.Cancel) return false;
            if (word == string.Empty)
                return false;

            int startIndex = this.lastWordIndex;
            int index;
            index = this.richTextBox.Text.IndexOf(word, startIndex);

            this.richTextBox.Focus();
            this.richTextBox.Select(index, word.Length);

            startIndex = index + word.Length;
            this.lastWordIndex = startIndex;

            return (this.richTextBox.Text.IndexOf(this.findWord, startIndex)) != -1;

        }

        public bool findNextAction()
        {
            int startIndex = this.lastWordIndex;
            int index;
            index = this.richTextBox.Text.IndexOf(this.findWord, startIndex);

            this.richTextBox.Focus();
            this.richTextBox.Select(index, this.findWord.Length);

            startIndex = index + this.findWord.Length;
            this.lastWordIndex = startIndex;

            return (this.richTextBox.Text.IndexOf(this.findWord, startIndex)) != -1;

        }

        public void insertImageAction()
        {
            DialogResult insertImageDialog = this.imgOpen.ShowDialog();

            if (insertImageDialog == DialogResult.OK)
            {
                IDataObject before = Clipboard.GetDataObject();
                Clipboard.SetImage(Image.FromFile(this.imgOpen.FileName));
                this.richTextBox.Paste();
                Clipboard.SetDataObject(before);
            }
        }

        public void LTRAction()
        {
            this.richTextBox.RightToLeft = RightToLeft.No;
        }

        public void RTLAction()
        {
            this.richTextBox.RightToLeft = RightToLeft.Yes;
        }

        public void bulletListAction()
        {
            this.richTextBox.SelectionBullet = !this.richTextBox.SelectionBullet;
        }

        public void boldAction()
        {
            Font currentFont = this.richTextBox.Font;
            FontStyle newFontStyle;
            if (this.richTextBox.SelectionFont.Bold == true)
            {
                newFontStyle = FontStyle.Regular;
            }
            else
            {
                newFontStyle = FontStyle.Bold;
            }

            this.richTextBox.SelectionFont = new Font(
               currentFont.FontFamily,
               currentFont.Size,
               newFontStyle
            );
        }

        public void italicAction()
        {
            Font currentFont = this.richTextBox.Font;
            FontStyle newFontStyle;
            if (this.richTextBox.SelectionFont.Italic == true)
            {
                newFontStyle = FontStyle.Regular;
            }
            else
            {
                newFontStyle = FontStyle.Italic;
            }

            this.richTextBox.SelectionFont = new Font(
               currentFont.FontFamily,
               currentFont.Size,
               newFontStyle
            );
        }

        public void underlineAction()
        {
            Font currentFont = this.richTextBox.Font;
            FontStyle newFontStyle;
            if (this.richTextBox.SelectionFont.Underline == true)
            {
                newFontStyle = FontStyle.Regular;
            }
            else
            {
                newFontStyle = FontStyle.Underline;
            }

            this.richTextBox.SelectionFont = new Font(
               currentFont.FontFamily,
               currentFont.Size,
               newFontStyle
            );
        }

        public void strikeAction()
        {
            Font currentFont = this.richTextBox.Font;
            FontStyle newFontStyle;
            if (this.richTextBox.SelectionFont.Strikeout == true)
            {
                newFontStyle = FontStyle.Regular;
            }
            else
            {
                newFontStyle = FontStyle.Strikeout;
            }

            this.richTextBox.SelectionFont = new Font(
               currentFont.FontFamily,
               currentFont.Size,
               newFontStyle
            );
        }

        public bool wordWrapAction()
        {
            this.richTextBox.WordWrap = !this.richTextBox.WordWrap;
            return this.richTextBox.WordWrap;
        }

        public int zoomAction(object sender)
        {
            float zoom;
            string value = (sender as ToolStripMenuItem).Text;
            value = value.Replace("%", "");
            float.TryParse(value, out zoom);
            this.richTextBox.ZoomFactor = zoom / 100;
            return (int)zoom;

        }

        public string zoomAction(string text)
        {
            float zoom;
            float.TryParse(text, out zoom);
            zoom /= 100;
            if (zoom >= 1)
            {
                this.richTextBox.ZoomFactor = zoom;
            }
            return Convert.ToString((int)zoom);

        }

        public void highlightAction()
        {
            DialogResult colorRes = this.colorDialog.ShowDialog();
            if (colorRes == DialogResult.OK)
            {
                this.richTextBox.SelectionBackColor = this.colorDialog.Color;
                this.backColorIndicator.BackColor = this.colorDialog.Color;
                this.statusBar.Text = "Back Color Changed";
            }
        }

        public void indentIncrease()
        {
            this.richTextBox.SelectionIndent += 4;
            this.richTextBox.Focus();
        }

        public void indentDecrease()
        {
            if (this.richTextBox.SelectionIndent >= 4)
                this.richTextBox.SelectionIndent -= 4;
            this.richTextBox.Focus();
        }

        public void swapColor()
        {
            Color temp;
            temp = this.backColorIndicator.BackColor;
            this.backColorIndicator.BackColor = this.forColorIndicator.BackColor;
            this.forColorIndicator.BackColor = temp;

            this.richTextBox.SelectionColor = this.forColorIndicator.BackColor;
            this.richTextBox.SelectionBackColor = this.backColorIndicator.BackColor;

            this.richTextBox.Focus();
        }

        public void markdown()
        {
            //bold 
            Regex blodRg = new Regex(@"\*{2}(.*?)\*{2}");
            Regex italicRg = new Regex(@"_{2}(.*?)_{2}");

            Font currentFont = this.richTextBox.Font;
            FontStyle boldFont = FontStyle.Bold;
            FontStyle italicFont = FontStyle.Italic;


            //Apply bold pattern
            MatchCollection matchCollection = blodRg.Matches(this.richTextBox.Text);
            for (int count = 0; count < matchCollection.Count; count++)
            {
                int selectionStart = matchCollection[count].Index;
                int selectionLength = matchCollection[count].Value.Length;
                this.richTextBox.SelectionStart = selectionStart;
                this.richTextBox.SelectionLength = selectionLength;
                this.richTextBox.SelectionFont = new Font(
                         currentFont.FontFamily,
                         currentFont.Size,
                         boldFont
                );
            }

            //Apply Italic pattern
            matchCollection = italicRg.Matches(this.richTextBox.Text);
            for (int count = 0; count < matchCollection.Count; count++)
            {
                int selectionStart = matchCollection[count].Index;
                int selectionLength = matchCollection[count].Value.Length;
                this.richTextBox.SelectionStart = selectionStart;
                this.richTextBox.SelectionLength = selectionLength;
                this.richTextBox.SelectionFont = new Font(
                         currentFont.FontFamily,
                         currentFont.Size,
                         italicFont
                );
            }


            //Cleanup
            while(Regex.Matches(this.richTextBox.Text, @"\*{2}").Count != 0)
            {
                this.richTextBox.SelectionStart = Regex.Match(this.richTextBox.Text, @"\*{2}").Index;
                this.richTextBox.SelectionLength = 2;
                this.richTextBox.SelectedText = "";
            }

            while (Regex.Matches(this.richTextBox.Text, @"_{2}").Count != 0)
            {
                this.richTextBox.SelectionStart = Regex.Match(this.richTextBox.Text, @"_{2}").Index;
                this.richTextBox.SelectionLength = 2;
                this.richTextBox.SelectedText = "";
            }
        }

    }
}
