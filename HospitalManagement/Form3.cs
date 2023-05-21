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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;

            foreach (var department in DataManager.Departments)
                comboBox1.Items.Add(department.Name);

            comboBox2.Items.Add("부장");
            comboBox2.Items.Add("차장");
            comboBox2.Items.Add("과장");
            comboBox2.Items.Add("대리");
            comboBox2.Items.Add("사원");
            comboBox2.Items.Add("임상강사");
            comboBox2.Items.Add("조교수");
            comboBox2.Items.Add("부교수");
            comboBox2.Items.Add("교수");

            dataGridView1.CurrentCellChanged += DataGridView1_CurrentCellChanged;
            DataGridViewRefresh();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            employee.ID = (DataManager.employee_id++) + "";
            employee.Name = textBox1.Text;
            if (radioButton1.Checked)
                employee.Sex = "남";
            if (radioButton2.Checked)
                employee.Sex = "여";
            employee.Date_Of_Birth = dateTimePicker1.Value.Date;
            employee.Join_Date = dateTimePicker2.Value.Date;
            employee.Zip = textBox2.Text;
            employee.Addr = textBox3.Text;
            employee.Contact = textBox4.Text;
            employee.Email = textBox5.Text;
            foreach (var department in DataManager.Departments)
            {
                if (department.Name.Equals(comboBox1.Text))
                {
                    employee.Department_ID = department.ID;
                    break;
                }
            }
            employee.Position = comboBox2.Text;
            DataManager.Employees.Add(employee);
            DataGridViewRefresh();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Employee employee = dataGridView1.CurrentRow.DataBoundItem as Employee;
            employee.Name = textBox1.Text;
            if (radioButton1.Checked)
                employee.Sex = "남";
            if (radioButton2.Checked)
                employee.Sex = "여";
            employee.Date_Of_Birth = dateTimePicker1.Value.Date;
            employee.Join_Date = dateTimePicker2.Value.Date;
            employee.Zip = textBox2.Text;
            employee.Addr = textBox3.Text;
            employee.Contact = textBox4.Text;
            employee.Email = textBox5.Text;
            foreach (var department in DataManager.Departments)
            {
                if (department.Name.Equals(comboBox1.Text))
                {
                    employee.Department_ID = department.ID;
                    break;
                }
            }
            employee.Position = comboBox2.Text;
            DataGridViewRefresh();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Employee employee = dataGridView1.CurrentRow.DataBoundItem as Employee;
            DataManager.Employees.Remove(employee);
            DataGridViewRefresh();
        }

        private void DataGridViewRefresh()
        {
            DataManager.Save();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DataManager.Employees;
        }

        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;
            Employee employee = dataGridView1.CurrentRow.DataBoundItem as Employee;
            textBox1.Text = employee.Name;
            if (employee.Sex.Equals("남"))
                radioButton1.Checked = true;
            if (employee.Sex.Equals("여"))
                radioButton2.Checked = true;
            dateTimePicker1.Value = employee.Date_Of_Birth.Date;
            dateTimePicker2.Value = employee.Join_Date.Date;
            textBox2.Text = employee.Zip;
            textBox3.Text = employee.Addr;
            textBox4.Text = employee.Contact;
            textBox5.Text = employee.Email;
            comboBox1.Text = DataManager.Departments.Single((x) => x.ID == employee.Department_ID).Name;
            comboBox2.Text = employee.Position;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DataManager.Employees.Where((x) => x.Name == textBox1.Text).ToList<Employee>();
        }
    }
}
