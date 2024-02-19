using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lb2
{
    public partial class Form1 : Form {
        int flag = 0;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            richTextBox1.Clear();
            richTextBox1.Location = new Point(0, 24);
            richTextBox1.Size = ClientSize;
            flag = 0;
        }
        private void Form1_Resize(object sender, EventArgs e) {
            richTextBox1.Size = ClientSize;
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void работа1ToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void работа2ToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) {
            flag = 1;
        }
    }
}
