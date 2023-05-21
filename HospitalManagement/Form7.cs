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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();

            dataGridView2.CurrentCellChanged += DataGridView2_CurrentCellChanged;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            if (checkBox1.Checked)
                dataGridView1.DataSource = DataManager.Patients.Where((x) => x.Name.Equals(textBox1.Text) &&
                    x.Date_Of_Birth == dateTimePicker1.Value).ToList();
            else
                dataGridView1.DataSource = DataManager.Patients.Where((x) => x.Name.Equals(textBox1.Text)).ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Patient patient = dataGridView1.CurrentRow.DataBoundItem as Patient;
            List<Medical_Record> records = DataManager.Medical_Records.Where((x) => x.Patient_ID == patient.ID &&
                x.ReceiptAt >= dateTimePicker2.Value.Date && x.ReceiptAt <= dateTimePicker3.Value.Date).ToList();
            var temp = records.Join(DataManager.Employees,
                record => record.Employee_ID,
                employee => employee.ID,
                (record, employee) =>
                new
                {
                    접수번호 = record.ID,
                    성명 = patient.Name,
                    진료과목 = DataManager.Departments.Single((x) => x.ID.Equals(employee.Department_ID)).Name,
                    담당의사 = employee.Name,
                    접수날짜 = record.ReceiptAt,
                    진료실 = record.Medical_Room
                }).ToList();

            dataGridView2.DataSource = null;
            dataGridView2.DataSource = temp;
        }

        private void DataGridView2_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
                return;
            DataGridViewCellCollection temp = dataGridView2.CurrentRow.Cells;
            label8.Text = temp[1].Value.ToString();
            label9.Text = temp[2].Value.ToString();
            label10.Text = temp[3].Value.ToString();
            label11.Text = temp[4].Value.ToString();
        }
    }
}
