using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using static System.Windows.Forms.LinkLabel;

namespace lb2
{
    public partial class Form1 : Form {
        bool flag = false;
        static bool coinc(string line, string mask)
        {
            string[] msk = mask.Split('*');
            string s1 = line.Substring(0, msk[0].Length);
            string s2 = line.Substring(line.Length - msk[1].Length, msk[1].Length);
            if ((String.Equals(msk[0].ToLower(), s1.ToLower())) && (String.Equals(msk[1].ToLower(), s2.ToLower())))
            {
                return true;
            }
            return false;
        }


        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            richTextBox1.Clear();
            richTextBox1.Location = new Point(0, 24);
            richTextBox1.Size = ClientSize;
            flag = false;
        }

        private void Form1_Resize(object sender, EventArgs e) {
            richTextBox1.Size = ClientSize;
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e) {
            if (flag) {
                DialogResult result;
                result = MessageBox.Show(this, "Файл изменен. Сохранить?", "Ошибка", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Yes) {
                    сохранитьToolStripMenuItem_Click(sender, e);
                    flag = false;
                }
                richTextBox1.Clear();
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog openFile1 = new OpenFileDialog();
            if (openFile1.ShowDialog() == DialogResult.OK && openFile1.FileName.Length > 0) {
                richTextBox1.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
            } else MessageBox.Show("Ошибка открытия файла");
            flag = false;
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog saveFile1 = new SaveFileDialog();
            saveFile1.DefaultExt = "*.rtf";
            saveFile1.Filter = "RTF Files|*.rtf";
            if (saveFile1.ShowDialog() == DialogResult.OK && saveFile1.FileName.Length > 0) {
                richTextBox1.SaveFile(saveFile1.FileName, RichTextBoxStreamType.PlainText);
            } else MessageBox.Show("Ошибка открытия файла");
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e) {
            richTextBox1.Clear();
            flag = false;
        }

        private void работа1ToolStripMenuItem_Click(object sender, EventArgs e) {
            StreamReader reader = new StreamReader("list.txt");
            List<string> badStudents = new List<string>();
            while (!reader.EndOfStream) {
                string line = reader.ReadLine();
                string studentNum = line.Substring(0, 4).Trim();
                string groupNum = line.Substring(6, 8).Trim();
                string surname = line.Substring(14, 12).Trim();
                int grade1 = int.Parse(line.Substring(26, 3).Trim());
                int grade2 = int.Parse(line.Substring(29, 3).Trim());
                int grade3 = int.Parse(line.Substring(32).Trim());
                if ((grade1 == grade2 || grade1 == grade3 || grade2 == grade3) && (grade1 == 2 || grade2 == 2 && grade3 == 2)) {
                    badStudents.Add(line);
                }
                StreamWriter groupWriter = new StreamWriter($"{groupNum}.txt", true);
                groupWriter.WriteLine(line);
                groupWriter.Close();
            }

            reader.Close();
            richTextBox1.Text = "Студенты с плохими оценками: \n";
            for (int i = 0; i < badStudents.Count; i++) {
                richTextBox1.Text = richTextBox1.Text + badStudents[i] + "\n";
            }
        }


        private void работа2ToolStripMenuItem_Click(object sender, EventArgs e) {
            string mask = richTextBox1.Text.Trim();
            if (string.IsNullOrEmpty(mask)) {
                MessageBox.Show("Введите маску", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            StreamReader file = new StreamReader("list.txt");
            string line;
            richTextBox1.Clear();
            while ((line = file.ReadLine()) != null) {
                string surname = line.Substring(14, 12).Trim();
                if (coinc(surname, mask)) {
                    richTextBox1.AppendText(surname + Environment.NewLine);
                }
            }
        }



        private void выходToolStripMenuItem_Click(object sender, EventArgs e) {
            if (flag) {
                DialogResult result;
                result = MessageBox.Show(this, "Файл изменен. Сохранить?", "Ошибка", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Yes) сохранитьToolStripMenuItem_Click(sender, e);
                flag = false;
            }
            Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) {
            flag = true;
        }
    }
}
