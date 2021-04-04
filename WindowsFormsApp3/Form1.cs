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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isUnSaved)
            {
                DialogResult confirm = MessageBox.Show("Discard Previous Changes ?", "Prompt", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.No) return;
            }
            richTextBox1.Text = "";
            this.Text = "Untitled";

        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (isUnSaved)
            {
                DialogResult confirm = MessageBox.Show("Discard Previous Changes ?", "Prompt", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.No) return;
            }
            Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult save = saveFileDialog1.ShowDialog();
            if (save == DialogResult.OK)
            {
                this.Text = saveFileDialog1.FileName.ToString();
                richTextBox1.SaveFile(saveFileDialog1.FileName);
                this.isUnSaved = false;
            }
            else
            {
                this.Text += " - Unsaved";
            }
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult open = openFileDialog1.ShowDialog();
            if (open == DialogResult.OK)
            {
                if (isUnSaved)
                {
                    DialogResult confirm = MessageBox.Show("Discard Previous Changes ?", "Prompt", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.No) return;
                }
                richTextBox1.LoadFile(openFileDialog1.FileName);
                this.Text = openFileDialog1.FileName.ToString();
                this.isUnSaved = false;
            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.isUnSaved = true;
        }
    }
}
