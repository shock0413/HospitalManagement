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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            label3.Text = DataManager.Patients.Count().ToString() + "명";
            label4.Text = DataManager.Employees.Count().ToString() + "명";
            label6.Text = DataManager.Patients.Where((x) => x.Hospital_Room_ID != null && x.Hospital_Ward_ID != null).Count().ToString() + "명";
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DataManager.Patients;
            dataGridView1.CurrentCellChanged += DataGridView1_CurrentCellChanged;
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = DataManager.Medical_Records;
            dataGridView2.CurrentCellChanged += DataGridView2_CurrentCellChanged;

            foreach (var department in DataManager.Departments)
                if (department.Is_Medical_Department.Equals("예"))
                    comboBox1.Items.Add(department.Name);

            comboBox1.TextChanged += ComboBox1_TextChanged;
            button1.Click += Button1_Click;
        }

        private void 환자등록ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.FormClosed += Form2_FormClosed;
            form2.ShowDialog();
        }

        private void Form2_FormClosed(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DataManager.Patients;
            label3.Text = DataManager.Patients.Count().ToString() + "명";
            label6.Text = DataManager.Patients.Where((x) => x.Hospital_Room_ID != null && x.Hospital_Ward_ID != null).Count().ToString() + "명";
        }

        private void 사원등록ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.FormClosed += Form3_FormClosed;
            form3.ShowDialog();
        }

        private void Form3_FormClosed(object sender, EventArgs e)
        {
            label4.Text = DataManager.Employees.Count().ToString() + "명";
        }

        private void 부서관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.FormClosed += Form4_FormClosed;
            form4.ShowDialog();
        }

        private void Form4_FormClosed(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (var department in DataManager.Departments)
                if (department.Is_Medical_Department.Equals("예"))
                    comboBox1.Items.Add(department.Name);
        }

        private void 병동관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.ShowDialog();
        }

        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;
            Patient patient = dataGridView1.CurrentRow.DataBoundItem as Patient;
            textBox1.Text = patient.Name;
        }

        private void DataGridView2_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
                return;
            Medical_Record record = dataGridView2.CurrentRow.DataBoundItem as Medical_Record;
            Patient patient = DataManager.Patients.Single((x) => x.ID == record.Patient_ID);
            label13.Text = patient.Name;
            Employee doctor = DataManager.Employees.Single((x) => x.ID == record.Employee_ID);
            label15.Text = doctor.Name;
            Department department = DataManager.Departments.Single((x) => x.ID == doctor.Department_ID);
            label14.Text = department.Name;
            label17.Text = record.ReceiptAt.ToString();
        }

        private void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            foreach (var department in DataManager.Departments)
                if (department.Name.Equals(comboBox1.Text))
                    foreach (var doctor in DataManager.Employees)
                        if (doctor.Department_ID.Equals(department.ID))
                            comboBox2.Items.Add(doctor.Name);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Medical_Record medical_Record = new Medical_Record();
            medical_Record.ID = DataManager.record_id++ + "";
            Patient patient = dataGridView1.CurrentRow.DataBoundItem as Patient;
            medical_Record.Patient_ID = patient.ID;

            Employee doctor = null;
            foreach (var department in DataManager.Departments)
                if (department.Name.Equals(comboBox1.Text))
                    foreach (var employee in DataManager.Employees)
                        if (department.ID.Equals(employee.Department_ID))
                            if (employee.Name.Equals(comboBox2.Text))
                                doctor = employee;
            medical_Record.Employee_ID = doctor.ID;
            medical_Record.ReceiptAt = DateTime.Now;
            DataManager.Medical_Records.Add(medical_Record);

            DataManager.Save();
            DataManager.Load();
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = DataManager.Medical_Records;
        }
    }
}
