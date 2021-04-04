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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length != 0)
            {
                DialogResult confirm = MessageBox.Show("Discard Previous Changes ?", "Prompt", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.No) return;
            }
            richTextBox1.Text = "";
            this.Text = "Untitled";

        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length != 0)
            {
                DialogResult confirm = MessageBox.Show("Discard Previous Changes ?", "Prompt", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.No) return;
            }
            Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create Save Dialog
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Rich Text | *.rtf";
            saveFileDialog1.Title = "Save an Image File";
            DialogResult save = saveFileDialog1.ShowDialog();

            //Save the file
            if (save == DialogResult.OK)
            {
                this.Text = saveFileDialog1.FileName.ToString();
                richTextBox1.SaveFile(Convert.ToString(saveFileDialog1.FileName));
            }
        }
    }
}
