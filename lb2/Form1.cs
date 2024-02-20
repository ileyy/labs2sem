using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using System.Runtime.InteropServices;
using File = System.IO.File;

namespace lb2
{
    public partial class Form1 : Form {
        bool flag = false;

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
            FileStream f = null, g = null;
            string fn = "";
            int nomer, o1, o2, o3, i = 0;
            char[] gr = new char[9];
            char[] fam = new char[13];
            float sr;
            OpenFileDialog openFile1 = new OpenFileDialog();
            SaveFileDialog saveFile1 = new SaveFileDialog();

            if (openFile1.ShowDialog() == DialogResult.OK && openFile1.FileName.Length > 0) {
                fn = openFile1.FileName;
                try {
                    f = new FileStream(fn, FileMode.Open);
                    StreamReader reader = new StreamReader(f);
                    StreamWriter writer = new StreamWriter(g);
                    string line;
                    while ((line = reader.ReadLine()) != null) {
                        string[] parts = line.Split(' ');
                        nomer = int.Parse(parts[0]);
                        gr = parts[1].ToCharArray();
                        fam = parts[2].ToCharArray();
                        o1 = int.Parse(parts[3]);
                        o2 = int.Parse(parts[4]);
                        o3 = int.Parse(parts[5]);
                        sr = (o1 + o2 + o3) / 3.0f;

                        if (sr > 4) {
                            richTextBox1.AppendText(line + "\n");
                            i++;
                            string formattedLine = $"{i,2} {new string(gr),8} {new string(fam),10} {o1,2} {o2,2} {o3,2} {sr,8:f2}\n";
                            writer.WriteLine(formattedLine);
                        }
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show("Ошибка открытия файла: " + ex.Message);
                }
                finally {
                    if (f != null) f.Close();
                    if (g != null) g.Close();
                }
            }
        }


        private void работа2ToolStripMenuItem_Click(object sender, EventArgs e) {
            FileStream f = null, g = null;
            string fn = "";
            int nomer, o1, o2, o3, i = 0;
            char[] gr = new char[9];
            char[] fam = new char[13];
            OpenFileDialog openFile1 = new OpenFileDialog();
            SaveFileDialog saveFile1 = new SaveFileDialog();

            if (openFile1.ShowDialog() == DialogResult.OK && openFile1.FileName.Length > 0) {
                fn = openFile1.FileName;
                try {
                    f = new FileStream(fn, FileMode.Open);
                    StreamReader reader = new StreamReader(f);
                    string line;
                    StreamWriter writer = new StreamWriter(g);
                    while ((line = reader.ReadLine()) != null) {
                        string[] parts = line.Split(' ');
                        nomer = int.Parse(parts[0]);
                        gr = parts[1].ToCharArray();
                        fam = parts[2].ToCharArray();
                        o1 = int.Parse(parts[3]);
                        o2 = int.Parse(parts[4]);
                        o3 = int.Parse(parts[5]);
                        writer.WriteLine($" {new string(fam),8} {o1,2}{o2,2}{o3,2}");
                        if (o1 >= 4 && o2 >= 4 && o3 >= 4) {
                            string p = new string(fam);
                            p = p + " " + o1 + " " + o2 + " " + o3 + "\n";
                            richTextBox1.AppendText(p);
                            writer.WriteLine($" {new string(fam),8} {o1,2}{o2,2}{o3,2}");
                            i++;
                        }
                    }
                    if (i == 0) writer.WriteLine("нет\n");
                }
                catch (Exception ex) {
                    MessageBox.Show("Ошибка открытия файла: " + ex.Message);
                }
                finally{
                    if (f != null) f.Close();
                    if (g != null) g.Close();
                }
            }
        }



        private void выходToolStripMenuItem_Click(object sender, EventArgs e) {
            if (flag) {
                DialogResult result;
                result = MessageBox.Show(this, "Файл изменен.Сохранить?", "Ошибка", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
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
