﻿using System;
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
            DialogResult save = saveFileDialog1.ShowDialog();
            if (save == DialogResult.OK)
            {
                this.Text = saveFileDialog1.FileName.ToString();
                richTextBox1.SaveFile(saveFileDialog1.FileName);
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
            DialogResult open = openFileDialog1.ShowDialog();
            if (open == DialogResult.OK)
            {
                if (isUnSaved)
                {
                    DialogResult confirm = MessageBox.Show("Discard Previous Changes ?", "Text Editor", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.No) return;
                }
                richTextBox1.LoadFile(openFileDialog1.FileName);
                this.Text = openFileDialog1.FileName.ToString();
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
                DialogResult confirm = MessageBox.Show("Discard Previous Changes ?", "Text Editor", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.No) e.Cancel = true;
            }
        }
    }
}
