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
        RichEditor editor;
        public Form1()
        {
            InitializeComponent();
            editor = new RichEditor(richTextBox1, rftSaveDialog, rtfOpenDialog, imageOpenDialog, statusLabel, fontDialog1, colorDialog1, forColorIndicator, backColorIndicator, this);
        }

        private void newAction(object sender, EventArgs e)
        {
            editor.newAction();
        }

        private void exitAction(object sender, EventArgs e)
        {
            Close();
        }

        private void saveAction(object sender, EventArgs e)
        {
            editor.saveAction();
        }

        private void openAction(object sender, EventArgs e)
        {
            editor.openAction();

        }

        private void copyAction(object sender, EventArgs e)
        {
            editor.copyAction();
        }

        private void cutAction(object sender, EventArgs e)
        {
            editor.cutAction();
        }

        private void pasteAction(object sender, EventArgs e)
        {
            editor.pasteAction();
        }

        private void undoAction(object sender, EventArgs e)
        {
            editor.undoAction();
        }

        private void redoAction(object sender, EventArgs e)
        {
            editor.redoAction();
        }

        private void selectAllAction(object sender, EventArgs e)
        {
            editor.selectAllAction();
        }

        private void leftAlignAction(object sender, EventArgs e)
        {
            editor.leftAlignAction();
        }

        private void centerAlignAction(object sender, EventArgs e)
        {
            editor.centerAlignAction();
        }

        private void rightAlignAction(object sender, EventArgs e)
        {
            editor.rightAlignAction();
        }

        private void fontAction(object sender, EventArgs e)
        {
            editor.fontAction();
        }

        private void colorAction(object sender, EventArgs e)
        {
            editor.colorAction();
        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            editor.isUnSaved = true;
            statusLabel.Text = "Unsaved Changes";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (editor.isUnSaved)
            {
                DialogResult confirm = MessageBox.Show("Discard Previous Changes ?", "Text Editor", MessageBoxButtons.YesNoCancel);
                if (confirm == DialogResult.No)
                    editor.saveAction();
                if (confirm == DialogResult.Cancel)
                    e.Cancel = true;

            }
        }


        //todo
        private void findAction(object sender, EventArgs e)
        {
            this.findNextToolStripMenuItem.Enabled = editor.findAction();
        }


        private void findNextAction(object sender, EventArgs e)
        {
            this.findNextToolStripMenuItem.Enabled = editor.findNextAction();
        }

        private void insertImageAction(object sender, EventArgs e)
        {
            editor.insertImageAction();
        }

        private void LTRAction(object sender, EventArgs e)
        {
            editor.LTRAction();
        }

        private void RTLAction(object sender, EventArgs e)
        {
            editor.RTLAction();
        }

        private void bulletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.bulletListAction();
        }

        private void boldAction(object sender, EventArgs e)
        {
            editor.boldAction();
        }

        private void italicAction(object sender, EventArgs e)
        {
            editor.italicAction();
        }

        private void underlineAction(object sender, EventArgs e)
        {
            editor.underlineAction();
        }

        private void strikeAction(object sender, EventArgs e)
        {
            editor.strikeAction();
        }

        private void wordWrapAction(object sender, EventArgs e)
        {
            wordWrapMenu.Checked = editor.wordWrapAction();
        }

        private void zoomToolStripMenuItem_KeyUp(object sender, KeyEventArgs e)
        {
            zoomToolStripMenuItem.Text = editor.zoomAction(zoomToolStripMenuItem.Text);
        }

        private void zoomAction(object sender, EventArgs e)
        {
            editor.zoomAction(sender);
        }

        private void highlightAction(object sender, EventArgs e)
        {
            editor.highlightAction();
        }

        private void indentIncrease(object sender, EventArgs e)
        {
            editor.indentIncrease();
        }

        private void indentDecrease(object sender, EventArgs e)
        {
            editor.indentDecrease();
        }

        private void swapColors(object sender, EventArgs e)
        {
            editor.swapColor();
        }
    }
}
