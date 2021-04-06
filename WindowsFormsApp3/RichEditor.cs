using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public class RichEditor
    {
        public RichTextBox richTextBox;
        public SaveFileDialog saveDialog;
        public OpenFileDialog rtfOpen;
        public OpenFileDialog imgOpen;
        public StatusStrip statusBar;
        public FontDialog fontDialog;
        public ColorDialog colorDialog;
        public Button forColorIndicator;
        public Button backColorIndicator;
        public Form1 parrent;
        public bool isUnSaved = false;


        public RichEditor(RichTextBox richTextBox,
             SaveFileDialog saveDialog,
             OpenFileDialog rtfOpen,
             OpenFileDialog imgOpen,
             StatusStrip statusBar,
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
        }

        public void newAction(object sender, EventArgs e)
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

        public void saveAction(object sender, EventArgs e)
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

        public void openAction(object sender, EventArgs e)
        {
            DialogResult open = this.rtfOpen.ShowDialog();
            if (open == DialogResult.OK)
            {
                if (isUnSaved)
                {
                    DialogResult confirm = MessageBox.Show("Discard Previous Changes ?", "Text Editor", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.No) return;
                }
                this.richTextBox.LoadFile(this.rtfOpen.FileName);
                this.parrent.Text = this.rtfOpen.FileName.ToString();
                this.isUnSaved = false;
            }

        }

        public void copyAction(object sender, EventArgs e)
        {
            this.richTextBox.Copy();
            this.statusBar.Text = "Copied";
        }

        public void cutAction(object sender, EventArgs e)
        {
            this.richTextBox.Cut();
            this.statusBar.Text = "Cut";
        }

        public void pasteAction(object sender, EventArgs e)
        {
            this.richTextBox.Paste();
        }

        public void undoAction(object sender, EventArgs e)
        {
            this.richTextBox.Undo();
        }

        public void redoAction(object sender, EventArgs e)
        {
            this.richTextBox.Redo();
        }

        public void selectAllAction(object sender, EventArgs e)
        {
            this.richTextBox.SelectAll();
        }

        public void leftAlignAction(object sender, EventArgs e)
        {
            this.richTextBox.SelectionAlignment = HorizontalAlignment.Left;
        }

        public void centerAlignAction(object sender, EventArgs e)
        {
            this.richTextBox.SelectionAlignment = HorizontalAlignment.Center;
        }

        public void rightAlignAction(object sender, EventArgs e)
        {
            this.richTextBox.SelectionAlignment = HorizontalAlignment.Right;
        }

        public void fontAction(object sender, EventArgs e)
        {
            DialogResult fontRes = this.fontDialog.ShowDialog();
            if (fontRes == DialogResult.OK)
            {
                this.richTextBox.SelectionFont = this.fontDialog.Font;
                this.statusBar.Text = "Font Changed";
            }

        }

        public void colorAction(object sender, EventArgs e)
        {
            DialogResult colorRes = this.colorDialog.ShowDialog();
            if (colorRes == DialogResult.OK)
            {
                this.richTextBox.SelectionColor = this.colorDialog.Color;
                this.forColorIndicator.BackColor = this.colorDialog.Color;
                this.statusBar.Text = "Color Changed";
            }
        }


        public void TextChanged(object sender, EventArgs e)
        {
            this.isUnSaved = true;
            this.statusBar.Text = "Unsaved Changes";
        }

        public void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isUnSaved)
            {
                DialogResult confirm = MessageBox.Show("Discard Previous Changes ?", "Text Editor", MessageBoxButtons.YesNoCancel);
                if (confirm == DialogResult.No)
                    this.saveAction(null, null);
                if (confirm == DialogResult.Cancel)
                    e.Cancel = true;

            }
        }


        //todo
        public void findAction(object sender, EventArgs e)
        {
            Find find = new Find();
            DialogResult findDiaRes = find.ShowDialog();

            int matches = 0;
            string word = find.textBox1.Text;

            if (findDiaRes == DialogResult.Cancel) return;
            if (word == string.Empty)
                return;

            int s_start = this.richTextBox.SelectionStart, startIndex = 0, index;

            while ((index = this.richTextBox.Text.IndexOf(word, startIndex)) != -1)
            {
                this.richTextBox.Select(index, word.Length);
                this.richTextBox.SelectionColor = Color.Red;
                startIndex = index + word.Length;
                matches++;
            }

            this.richTextBox.SelectionStart = s_start;
            this.richTextBox.SelectionLength = 0;
            this.richTextBox.SelectionColor = Color.Black;

            MessageBox.Show($"found {matches} matches.", "Search");
        }

        public void insertImageAction(object sender, EventArgs e)
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

        public void LTRAction(object sender, EventArgs e)
        {
            this.richTextBox.RightToLeft = RightToLeft.No;
        }

        public void RTLAction(object sender, EventArgs e)
        {
            this.richTextBox.RightToLeft = RightToLeft.Yes;
        }

        public void bulletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox.SelectionBullet = !this.richTextBox.SelectionBullet;
        }

        public void boldAction(object sender, EventArgs e)
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

        public void italicAction(object sender, EventArgs e)
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

        public void underlineAction(object sender, EventArgs e)
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

        public void strikeAction(object sender, EventArgs e)
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

        public bool wordWrapAction(object sender, EventArgs e)
        {
            this.richTextBox.WordWrap = !this.richTextBox.WordWrap;
            return this.richTextBox.WordWrap;
        }

        public void zoomAction(object sender, EventArgs e)
        {
            float zoom;
            string value = (sender as ToolStripMenuItem).Text;
            value = value.Replace("%", "");
            float.TryParse(value, out zoom);
            this.richTextBox.ZoomFactor = zoom / 100;
        }

        public void highlightAction(object sender, EventArgs e)
        {
            DialogResult colorRes = this.colorDialog.ShowDialog();
            if (colorRes == DialogResult.OK)
            {
                this.richTextBox.SelectionBackColor = this.colorDialog.Color;
                this.backColorIndicator.BackColor = this.colorDialog.Color;
                this.statusBar.Text = "Back Color Changed";
            }
        }

        public void indentIncrease(object sender, EventArgs e)
        {
            this.richTextBox.SelectionIndent += 4;
        }

        public void indentDecrease(object sender, EventArgs e)
        {
            if (this.richTextBox.SelectionIndent >= 4)
                this.richTextBox.SelectionIndent -= 4;
        }

    }
}
