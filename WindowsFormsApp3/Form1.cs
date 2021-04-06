using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool isUnSaved = false;

        private void newAction(object sender, EventArgs e)
        {
            if (isUnSaved)
            {
                DialogResult confirm = MessageBox.Show("Discard Previous Changes ?", "Text Editor", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.No) return;
            }
            richTextBox1.Text = "";
            this.Text = "Untitled";
            statusLabel.Text = "Open";

        }

        private void exitAction(object sender, EventArgs e)
        {
            Close();
        }

        private void saveAction(object sender, EventArgs e)
        {
            DialogResult save = rftSaveDialog.ShowDialog();
            if (save == DialogResult.OK)
            {
                this.Text = rftSaveDialog.FileName.ToString();
                richTextBox1.SaveFile(rftSaveDialog.FileName);
                this.isUnSaved = false;
                statusLabel.Text = "Saved Changes";
            }
            else
            {
                this.Text += " - Unsaved";
                statusLabel.Text = "Unsaved Changes";
            }
        }

        private void openAction(object sender, EventArgs e)
        {
            DialogResult open = rtfOpenDialog.ShowDialog();
            if (open == DialogResult.OK)
            {
                if (isUnSaved)
                {
                    DialogResult confirm = MessageBox.Show("Discard Previous Changes ?", "Text Editor", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.No) return;
                }
                richTextBox1.LoadFile(rtfOpenDialog.FileName);
                this.Text = rtfOpenDialog.FileName.ToString();
                this.isUnSaved = false;
            }

        }

        private void copyAction(object sender, EventArgs e)
        {
            richTextBox1.Copy();
            statusLabel.Text = "Copied";
        }

        private void cutAction(object sender, EventArgs e)
        {
            richTextBox1.Cut();
            statusLabel.Text = "Cut";
        }

        private void pasteAction(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void undoAction(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void redoAction(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void selectAllAction(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void leftAlignAction(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void centerAlignAction(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void rightAlignAction(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void fontAction(object sender, EventArgs e)
        {
            DialogResult fontRes = fontDialog1.ShowDialog();
            if (fontRes == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fontDialog1.Font;
                statusLabel.Text = "Font Changed";
            }

        }

        private void colorAction(object sender, EventArgs e)
        {
            DialogResult colorRes = colorDialog1.ShowDialog();
            if (colorRes == DialogResult.OK)
            {
                richTextBox1.SelectionColor = colorDialog1.Color;
                forColorIndicator.BackColor = colorDialog1.Color;
                statusLabel.Text = "Color Changed";
            }
        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.isUnSaved = true;
            statusLabel.Text = "Unsaved Changes";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
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
        private void findAction(object sender, EventArgs e)
        {
            Find find = new Find();
            DialogResult findDiaRes = find.ShowDialog();

            int matches = 0;
            string word = find.textBox1.Text;

            if (findDiaRes == DialogResult.Cancel) return;
            if (word == string.Empty)
                return;

            int s_start = richTextBox1.SelectionStart, startIndex = 0, index;

            while ((index = richTextBox1.Text.IndexOf(word, startIndex)) != -1)
            {
                richTextBox1.Select(index, word.Length);
                richTextBox1.SelectionColor = Color.Red;
                startIndex = index + word.Length;
                matches++;
            }

            richTextBox1.SelectionStart = s_start;
            richTextBox1.SelectionLength = 0;
            richTextBox1.SelectionColor = Color.Black;

            MessageBox.Show($"found {matches} matches.", "Search");
        }

        private void insertImageAction(object sender, EventArgs e)
        {
            DialogResult insertImageDialog = imageOpenDialog.ShowDialog();

            if (insertImageDialog == DialogResult.OK)
            {
                IDataObject before = Clipboard.GetDataObject();
                Clipboard.SetImage(Image.FromFile(imageOpenDialog.FileName));
                richTextBox1.Paste();
                Clipboard.SetDataObject(before);
            }
        }

        private void LTRAction(object sender, EventArgs e)
        {
            richTextBox1.RightToLeft = RightToLeft.No;
        }

        private void RTLAction(object sender, EventArgs e)
        {
            richTextBox1.RightToLeft = RightToLeft.Yes;
        }

        private void bulletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionBullet = !richTextBox1.SelectionBullet;
        }

        private void boldAction(object sender, EventArgs e)
        {
            Font currentFont = richTextBox1.Font;
            FontStyle newFontStyle;
            if (richTextBox1.SelectionFont.Bold == true)
            {
                newFontStyle = FontStyle.Regular;
            }
            else
            {
                newFontStyle = FontStyle.Bold;
            }

            richTextBox1.SelectionFont = new Font(
               currentFont.FontFamily,
               currentFont.Size,
               newFontStyle
            );
        }

        private void italicAction(object sender, EventArgs e)
        {
            Font currentFont = richTextBox1.Font;
            FontStyle newFontStyle;
            if (richTextBox1.SelectionFont.Italic == true)
            {
                newFontStyle = FontStyle.Regular;
            }
            else
            {
                newFontStyle = FontStyle.Italic;
            }

            richTextBox1.SelectionFont = new Font(
               currentFont.FontFamily,
               currentFont.Size,
               newFontStyle
            );
        }

        private void underlineAction(object sender, EventArgs e)
        {
            Font currentFont = richTextBox1.Font;
            FontStyle newFontStyle;
            if (richTextBox1.SelectionFont.Underline == true)
            {
                newFontStyle = FontStyle.Regular;
            }
            else
            {
                newFontStyle = FontStyle.Underline;
            }

            richTextBox1.SelectionFont = new Font(
               currentFont.FontFamily,
               currentFont.Size,
               newFontStyle
            );
        }

        private void strikeAction(object sender, EventArgs e)
        {
            Font currentFont = richTextBox1.Font;
            FontStyle newFontStyle;
            if (richTextBox1.SelectionFont.Strikeout == true)
            {
                newFontStyle = FontStyle.Regular;
            }
            else
            {
                newFontStyle = FontStyle.Strikeout;
            }

            richTextBox1.SelectionFont = new Font(
               currentFont.FontFamily,
               currentFont.Size,
               newFontStyle
            );
        }

        private void wordWrapAction(object sender, EventArgs e)
        {
            richTextBox1.WordWrap = wordWrapMenu.Checked;
        }

        private void zoomToolStripMenuItem_KeyUp(object sender, KeyEventArgs e)
        {
            float zoom;
            float.TryParse(zoomToolStripMenuItem.Text, out zoom);
            zoom /= 100;
            if (zoom >= 1)
            {
                richTextBox1.ZoomFactor = zoom;
            }
        }

        private void zoomAction(object sender, EventArgs e)
        {
            float zoom;
            string value = (sender as ToolStripMenuItem).Text;
            value = value.Replace("%", "");
            float.TryParse(value, out zoom);
            zoomToolStripMenuItem.Text = Convert.ToString((int)zoom);
            richTextBox1.ZoomFactor = zoom / 100;
        }

        private void highlightAction(object sender, EventArgs e)
        {
            DialogResult colorRes = colorDialog1.ShowDialog();
            if (colorRes == DialogResult.OK)
            {
                richTextBox1.SelectionBackColor = colorDialog1.Color;
                backColorIndicator.BackColor = colorDialog1.Color;
                statusLabel.Text = "Back Color Changed";
            }
        }

        private void indentIncrease(object sender, EventArgs e)
        {
            richTextBox1.SelectionIndent += 4;
        }

        private void indentDecrease(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionIndent >= 4)
                richTextBox1.SelectionIndent -= 4;
        }
    }
}
