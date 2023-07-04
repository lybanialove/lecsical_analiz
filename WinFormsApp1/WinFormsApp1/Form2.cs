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
    public partial class Form2 : Form
    {
        Form1 form1;
        public Form2(funcion function, Form1 form1)
        {
            this.function = function;
            this.form1 = form1;
            InitializeComponent();
        }
        funcion function;
        private void Form2_Load(object sender, EventArgs e)
        {
            function.Keywords.Sort();
            function.Separators.Sort();
            for (int i = 0;i< function.Separators.Count();i++)
            {
                dataGridView3.Rows.Add();
                dataGridView3[1, i].Value = function.Separators[i];
                dataGridView3[0, i].Value = i;               
            }
            for (int i = 0; i < function.Keywords.Count(); i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1[1, i].Value = function.Keywords[i];
                dataGridView1[0, i].Value = i;
            }
        }
        public void Change()
        {
            if (function.Numbers.Count != 0)
            {
                for (int i = 0; i < function.Numbers.Count(); i++)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2[1, i].Value = function.Numbers[i];
                    dataGridView2[0, i].Value = i;
                }
            }
            if (function.Variebles.Count != 0)
            {
                for (int i = 0; i < function.Variebles.Count(); i++)
                {
                    dataGridView4.Rows.Add();
                    dataGridView4[1, i].Value = function.Variebles[i];
                    dataGridView4[0, i].Value = i;
                }
            }
        }
    }
}
