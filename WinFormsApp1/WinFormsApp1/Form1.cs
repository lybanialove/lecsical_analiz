using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        funcion funcion = new funcion();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            funcion.Keywords.Sort();
            funcion.Separators.Sort();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            funcion.Keys.Clear();
            funcion.Variebles.Clear();
            funcion.Numbers.Clear();
            textBox1.Text = funcion.sintactical_analyzer(richTextBox1.Text);
            for (int i = 0; i < funcion.Keys.Count();i++)
            {
                listBox1.Items.Add(funcion.Keys[i]);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this.funcion, this);
            form2.Change();
            if (checkBox1.Checked)
            {
                form2.Show();
            }
            else
            {
                form2.Close();
            }

        }
    }
}
