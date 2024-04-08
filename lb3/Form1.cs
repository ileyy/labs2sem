using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lb3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public struct Data
        {
            public int Number;
            public string Surname;
            public string Group;
            public int[] Grades;
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data student = new Data();
            SaveFileDialog saveFile1 = new SaveFileDialog();
            if (saveFile1.ShowDialog() == DialogResult.OK && saveFile1.FileName.Length > 0)
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(saveFile1.FileName, FileMode.Create)))
                {
                    int i = 0;
                    while (i < (dataGridView1.RowCount - 1))
                    {
                        student.Number = Convert.ToInt32(dataGridView1[0, i].Value);
                        string number = student.Number.ToString();
                        writer.Write(Encoding.UTF8.GetBytes(number.PadLeft(4)));
                        student.Group = Convert.ToString(dataGridView1[1, i].Value);
                        writer.Write(Encoding.UTF8.GetBytes(student.Group.PadLeft(8)));
                        writer.Write(Encoding.UTF8.GetBytes("  "));
                        student.Surname = Convert.ToString(dataGridView1[2, i].Value);
                        writer.Write(Encoding.UTF8.GetBytes(student.Surname.PadRight(12)));
                        string grade1 = Convert.ToString(dataGridView1[3, i].Value);
                        string grade2 = Convert.ToString(dataGridView1[4, i].Value);
                        string grade3 = Convert.ToString(dataGridView1[5, i].Value);
                        writer.Write(Encoding.UTF8.GetBytes(grade1.PadLeft(3)));
                        writer.Write(Encoding.UTF8.GetBytes(grade2.PadLeft(3)));
                        writer.Write(Encoding.UTF8.GetBytes(grade3.PadLeft(3)));
                        i++;
                    }
                }
                if (File.Exists(saveFile1.FileName))
                {
                    MessageBox.Show("Файл создан", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.Rows.Clear();
                }
                else
                    MessageBox.Show("Ошибка при создании файла", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            OpenFileDialog openFile1 = new OpenFileDialog();
            if (openFile1.ShowDialog() == DialogResult.OK && openFile1.FileName.Length > 0)
            {
                using (BinaryReader reader = new BinaryReader(File.Open(openFile1.FileName, FileMode.Open)))
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        if (reader.BaseStream.Position + 4 > reader.BaseStream.Length)
                            break;
                        byte[] NumberBytes = reader.ReadBytes(4);
                        string Number = Encoding.UTF8.GetString(NumberBytes).Trim();
                        byte[] GroupBytes = reader.ReadBytes(10);
                        string Group = Encoding.ASCII.GetString(GroupBytes).Trim();
                        byte[] SurnameBytes = reader.ReadBytes(12);
                        string Surname = Encoding.ASCII.GetString(SurnameBytes).Trim();
                        string[] Grades = new string[3];
                        for (int i = 0; i < 3; i++)
                        {
                            if (reader.BaseStream.Position + 3 > reader.BaseStream.Length)
                                break;
                            byte[] GradeBytes = reader.ReadBytes(3);
                            Grades[i] = Encoding.ASCII.GetString(GradeBytes).Trim();
                        }
                        dataGridView1.Rows.Add(Number, Group, Surname, Grades[0], Grades[1], Grades[2]);
                    }

                }
            }
            else
                MessageBox.Show("Ошибка открытия файла");
        }
        private bool StudentsGroups(string inputFile)
        {
            if (!File.Exists(inputFile))
            {
                MessageBox.Show("Исходный файл не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            using (BinaryReader reader = new BinaryReader(File.Open(inputFile, FileMode.Open)))
            {

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    if (reader.BaseStream.Position + 4 > reader.BaseStream.Length)
                        break;
                    byte[] NumberBytes = reader.ReadBytes(4);
                    string Number = Encoding.UTF8.GetString(NumberBytes).Trim();
                    byte[] GroupBytes = reader.ReadBytes(10);
                    string Group = Encoding.ASCII.GetString(GroupBytes).Trim();
                    byte[] SurnameBytes = reader.ReadBytes(12);
                    string Surname = Encoding.ASCII.GetString(SurnameBytes).Trim();
                    string[] Grades = new string[3];
                    for (int i = 0; i < 3; i++)
                    {
                        if (reader.BaseStream.Position + 3 > reader.BaseStream.Length)
                            break;
                        byte[] GradeBytes = reader.ReadBytes(3);
                        Grades[i] = Encoding.ASCII.GetString(GradeBytes).Trim();
                    }
                    string line = $"{Number} {Group} {Surname} {Grades[0]} {Grades[1]} {Grades[2]}";
                    string outFileName = $"{Group}.txt";
                    Write(outFileName, line);
                }
                return true;
            }
        }

        private void ClearFile(string inputFile)
        {
            File.WriteAllText(inputFile, string.Empty);
        }

        private void Write(string file, string line)
        {
            StreamWriter writer = new StreamWriter(file, true);
            writer.WriteLine(line);
            writer.Close();
        }



        static bool maska(string line, string mask)
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
        private void maskToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string mask = dataGridView1[2, 0].Value.ToString();
            string inputFile = "students.bin";
            dataGridView1.Rows.Clear();
            if (!File.Exists(inputFile))
            {
                MessageBox.Show("Исходный файл не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (BinaryReader reader = new BinaryReader(File.Open(inputFile, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    byte[] NumberBytes = reader.ReadBytes(4);
                    string Number = Encoding.UTF8.GetString(NumberBytes).Trim();
                    byte[] GroupBytes = reader.ReadBytes(10);
                    string Group = Encoding.ASCII.GetString(GroupBytes).Trim();
                    byte[] SurnameBytes = reader.ReadBytes(12);
                    string Surname = Encoding.ASCII.GetString(SurnameBytes).Trim();
                    string[] Grades = new string[3];
                    for (int i = 0; i < 3; i++)
                    {
                        if (reader.BaseStream.Position + 3 > reader.BaseStream.Length)
                            break;
                        byte[] GradeBytes = reader.ReadBytes(3);
                        Grades[i] = Encoding.ASCII.GetString(GradeBytes).Trim();
                    }
                    if (maska(Surname, mask))
                    {
                        dataGridView1.Rows.Add(Number, Group, Surname, Grades[0], Grades[1], Grades[2]);
                    }
                }
            }
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("В файле нет студентов с этой маской", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Информация выведена на экран", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i, j;
            for (i = 0; i < dataGridView1.ColumnCount; i++)
                for (j = 0; j < dataGridView1.RowCount; j++)
                    dataGridView1[i, j].Value = "";
        }

        private bool convertTxtToBin(string inputFile, string outputFile)
        {
            ClearFile(outputFile);
            if (!File.Exists(inputFile))
            {
                MessageBox.Show("Исходный файл не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            using (StreamReader reader = new StreamReader(inputFile))
            using (BinaryWriter writer = new BinaryWriter(File.Open(outputFile, FileMode.Create)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Data student;
                    if (TryParseData(line, out student))
                        WriteData(student, writer);
                    else
                    {
                        MessageBox.Show("Ошибка в данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void WriteData(Data student, BinaryWriter writer)
        {
            string number = student.Number.ToString();
            writer.Write(Encoding.UTF8.GetBytes(number.PadLeft(4)));
            writer.Write(Encoding.UTF8.GetBytes(student.Group.PadLeft(8)));
            writer.Write(Encoding.UTF8.GetBytes("  "));
            writer.Write(Encoding.UTF8.GetBytes(student.Surname.PadRight(12)));
            for (int i = 0; i < 3; i++)
            {
                writer.Write(Encoding.UTF8.GetBytes(student.Grades[i].ToString().PadLeft(3)));
            }
        }
        private bool TryParseData(string line, out Data student)
        {
            student = new Data();
            string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 6)
            {
                if (!int.TryParse(parts[0], out student.Number) || student.Number < 1)
                    return false;
                student.Group = parts[1];
                student.Surname = parts[2];
                student.Grades = new int[3];
                for (int i = 0; i < 3; i++)
                {
                    if (!int.TryParse(parts[3 + i], out student.Grades[i]))
                        return false;
                }
            }
            else
                return false;
            return true;
        }

        private void convertTxtToBinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string inputFile = "C:\\Users\\iley\\source\\repos\\lb\\lb3\bin\\Debug\\students.txt";
            string outputFile = "students.bin";
            bool success = convertTxtToBin(inputFile, outputFile);
            if (success)
                MessageBox.Show("Файл записан!", "Ура", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Ошибка :(", "Грустно", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TabClear()
        {
            int i, j;
            for (i = 0; i < dataGridView1.ColumnCount; i++)
                for (j = 0; j < dataGridView1.RowCount; j++)
                    dataGridView1[i, j].Value = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 6;
            dataGridView1.RowCount = 2;
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 60;
            dataGridView1.Columns[4].Width = 60;
            dataGridView1.Columns[5].Width = 60;
            dataGridView1.Columns[0].HeaderText = "Номер";
            dataGridView1.Columns[1].HeaderText = "Группа";
            dataGridView1.Columns[2].HeaderText = "Фамилия";
            dataGridView1.Columns[3].HeaderText = "Оценка 1";
            dataGridView1.Columns[4].HeaderText = "Оценка 2";
            dataGridView1.Columns[5].HeaderText = "Оценка 3";
            TabClear();
        }

        private void разбитьПоГруппамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string inputFile = "students.bin";
            bool success = StudentsGroups(inputFile);
            if (success)
                MessageBox.Show("Группировка завершена", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Ошибка при группировке", "-", MessageBoxButtons.OK, MessageBoxIcon.Error);
            dataGridView1.Rows.Clear();
            string inputFile1 = "students.bin";
            if (!File.Exists(inputFile1))
            {
                MessageBox.Show("Исходный файл не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (BinaryReader reader = new BinaryReader(File.Open(inputFile, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    byte[] NumberBytes = reader.ReadBytes(4);
                    string Number = Encoding.UTF8.GetString(NumberBytes).Trim();
                    byte[] GroupBytes = reader.ReadBytes(10);
                    string Group = Encoding.ASCII.GetString(GroupBytes).Trim();
                    byte[] SurnameBytes = reader.ReadBytes(12);
                    string Surname = Encoding.ASCII.GetString(SurnameBytes).Trim();
                    string[] Grades = new string[3];
                    for (int i = 0; i < 3; i++)
                    {
                        if (reader.BaseStream.Position + 3 > reader.BaseStream.Length)
                            break;
                        byte[] GradeBytes = reader.ReadBytes(3);
                        Grades[i] = Encoding.ASCII.GetString(GradeBytes).Trim();
                    }
                    int count2 = Grades.Count(grade => grade == "2");
                    if (count2 >= 2)
                    {
                        dataGridView1.Rows.Add(Number, Group, Surname, Grades[0], Grades[1], Grades[2]);
                    }
                }
            }
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("В файле нет студентов с двумя и более оценками 2", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
