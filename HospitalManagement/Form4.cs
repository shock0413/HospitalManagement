using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KDJ_HospitalManager
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();

            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;

            dataGridView1.CellClick += DataGridView1_CurrentCellChanged;
            DataGridViewRefresh();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Department department = new Department();
            department.ID = DataManager.department_id++ + "";
            department.Name = textBox1.Text;
            if (radioButton1.Checked)
                department.Is_Medical_Department = "예";
            if (radioButton2.Checked)
                department.Is_Medical_Department = "아니오";
            DataManager.Departments.Add(department);

            DataGridViewRefresh();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Department department = dataGridView1.CurrentRow.DataBoundItem as Department;
            department.Name = textBox1.Text;
            if (radioButton1.Checked)
                department.Is_Medical_Department = "예";
            if (radioButton2.Checked)
                department.Is_Medical_Department = "아니오";
            DataGridViewRefresh();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Department department = dataGridView1.CurrentRow.DataBoundItem as Department;
            DataManager.Departments.Remove(department);

            DataGridViewRefresh();
        }

        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;

            Department department = dataGridView1.CurrentRow.DataBoundItem as Department;
            textBox1.Text = department.Name;
        }

        private void DataGridViewRefresh()
        {
            DataManager.Save();
            dataGridView1.DataSource = null;
            DataManager.Load();
            dataGridView1.DataSource = DataManager.Departments;
        }
    }
}
