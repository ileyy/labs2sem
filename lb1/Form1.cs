using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lb1 {
    public partial class Form1 : Form {
        double a, b, eps;
        int n;

        double f(double x) {
            return x * x * Math.Sin(x) * Math.Sin(x) * Math.Sin(x) * Math.Cos(x);
        }

        double Rect(double a, double b, int n) {
            double dx, sum, x, integ;
            int i;
            dx = (b - a) / n;
            a = a + dx / 2;
            sum = 0;
            for (i = 0; i < n; i++) {
                x = a + i * dx;
                sum += f(x);
            }
            integ = dx * sum;
            return Math.Round(integ, 4);
        }

        double Trap(double a, double b, int n) {
            double dx = (b - a) / n;
            double integ = 0;
            for (int i = 0; i < n; i++) {
                double x1 = a + i * dx;
                double x2 = a + (i + 1) * dx;
                integ += 0.5 * (x2 - x1) * (f(x1) + f(x2));
            }
            return Math.Round(integ, 4);
        }

        double Par(double a, double b, int n) {
            double dx = (b - a) / n;
            double integ = 0;
            for (int i = 0; i < n; i++) {
                double x1 = a + i * dx;
                double x2 = a + (i + 1) * dx;
                integ += (x2 - x1) / 6.0 * (f(x1) + 4.0 * f(0.5 * (x1 + x2)) + f(x2));
            }
            return Math.Round(integ, 4);
        }

        public Form1() {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e) {

        }

        private void Button1Click(object sender, EventArgs e) {
            a = double.Parse(textBox1.Text); //LLOI
            b = double.Parse(textBox2.Text); //ULOI
            n = int.Parse(textBox3.Text); //Number of splits
        }

        private void Button2Click(object sender, EventArgs e) {
            textBox4.Text = Rect(a, b, n).ToString();
        }

        private void Button3Click(object sender, EventArgs e) {
            textBox5.Text = Trap(a, b, n).ToString();
        }

        private void Button4Click(object sender, EventArgs e) {
            textBox6.Text = Par(a, b, n).ToString();
        }

        private void Button5Click(object sender, EventArgs e) { //EXIT BUTTON
            Application.Exit();
        }

        private void ButtonClick6(object sender, EventArgs e) { //epsilon parsing
            eps = double.Parse(textBox7.Text);
        }

        private void Button7Click(object sender, EventArgs e) {
            double integ1, integ2;
            int n1;
            integ2 = Rect(a, b, n);
            n1 = n;
            do {
                integ1 = integ2;
                n1 *= 2;
                integ2 = Rect(a, b, n1);
            } while (Math.Abs(integ1 - integ2) > eps);
            textBox8.Text = integ2.ToString();
        }
            

        private void Button8Click(object sender, EventArgs e) {
            double integ1, integ2;
            int n1;
            integ2 = Trap(a, b, n);
            n1 = n;
            do {
                integ1 = integ2;
                n1 *= 2;
                integ2 = Trap(a, b, n1);
            } while (Math.Abs(integ1 - integ2) > eps);
            textBox9.Text = integ2.ToString();
        }

        private void Button9Click(object sender, EventArgs e) {
            double integ1, integ2;
            int n1;
            integ2 = Par(a, b, n);
            n1 = n;
            do {
                integ1 = integ2;
                n1 *= 2;
                integ2 = Par(a, b, n1);
            } while (Math.Abs(integ1 - integ2) > eps);
            textBox10.Text = integ2.ToString();
        }
    }
}
