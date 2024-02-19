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
            FileStream f, g;
            char fn, gn;
            int nomer, o1, o2, o3, i = 0;
            //char gr[9], fam[13];
            float sr;
            OpenFileDialog openFile1 = new OpenFileDialog();
            SaveFileDialog saveFile1 = new SaveFileDialog();
            if (openFile1.ShowDialog() == DialogResult.OK && openFile1.FileName.Length > 0) {
                fn = (char)(void)MarshalAs.StringToHGlobalAnsi(
                openFile1.FileName.ToString());
                if ((f = File.OpenRead()) == NULL)
                    MessageBox::Show("Ошибка открытия файла ");
                else
                {
                    if (saveFile1->ShowDialog()
                    == System::Windows::Forms::DialogResult::OK
                    && saveFile1->FileName->Length > 0)
                    {
                        gn = (char*)(void*)Marshal::StringToHGlobalAnsi(
                        saveFile1->FileName->ToString());
                        if ((g = fopen(gn, "w")) == NULL)
                            MessageBox::Show("Ошибка открытия файла");
                        else
                        {
                            richTextBox1->Clear();
                            while (fscanf(f, "%d %s %s %d %d %d"
                            , &nomer, gr, fam, &o1, &o2, &o3) > 0)
                            {
                                sr = (o1 + o2 + o3) / 3.0f;
                                if (sr > 4)
                                {
                                    for (int j = strlen(fam); j < 10; j++)
                                        //добавьте # include "string.h"
                                        strcat(fam, " ");
                                    String ^ s = gcnew String("");
                                    //Преобразование строки с 0
                                    //в конце к типу String
                                    String ^ q = gcnew String(gr);
                                    String ^ p = gcnew String(fam);
                                    //nomer автоматически преобразуется
                                    //к типу String
                                    s = s + nomer;
                                    if (s->Length == 1)
                                        s = " " + s;
                                    s = s + " " + q + " " + p + " " + o1 + " " + o2
                                    //"\n" переводят строку в richtextBox
                                    + " " + o3 + " " + sr + "\n";
                                    //Добавить строку в richtextBox
                                    this->richTextBox1->AppendText(s);
                                    i++;
                                    fprintf(g, "%2d %8s %10s%2d%2d%2d
                                    8.2f\n”
, i, gr, fam, o1, o2, o3, sr);
                                }// if (sr > 4)
                            }// while (fscanf ///
                        }// else
                    }// if( saveFile1->...
                    fclose(f);
                    fclose(g);
                }// else
            }// if ( openFile1->
        }// private: System::Void работа1ToolStripMenuItem_Click(...)
    }

        private void работа2ToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        

        private void выходToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) {
            flag = true;
        }
    }
}
