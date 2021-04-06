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
    public partial class Find : Form
    {
        public Find(bool secondRound)
        {
            InitializeComponent();
            button3.Enabled = secondRound;
            button2.Enabled = !secondRound;
            textBox1.Enabled = !secondRound;
        }

    }
}
