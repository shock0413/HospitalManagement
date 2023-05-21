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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;

            DataGridViewRefresh();
            dataGridView1.CurrentCellChanged += DataGridView1_CurrentCellChanged;

            foreach (var ward in DataManager.Hospital_Wards)
                comboBox1.Items.Add(ward.Name);
            comboBox1.TextChanged += ComboBox1_TextChanged;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Patient patient = new Patient();
            patient.ID = (DataManager.patient_id++) + "";
            patient.Name = textBox1.Text;
            if (radioButton1.Checked)
                patient.Sex = "남";
            if (radioButton2.Checked)
                patient.Sex = "여";
            patient.Date_Of_Birth = dateTimePicker1.Value.Date;
            patient.Zip = textBox2.Text;
            patient.Addr = textBox3.Text;
            patient.Contact = textBox4.Text;
            foreach (var ward in DataManager.Hospital_Wards)
                if (ward.Name.Equals(comboBox1.Text))
                    patient.Hospital_Ward_ID = ward.ID;
            foreach (var room in DataManager.Hospital_Rooms)
                if (room.Number.Equals(comboBox2.Text))
                    patient.Hospital_Room_ID = room.ID;
            DataManager.Patients.Add(patient);
            DataGridViewRefresh();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Patient patient = dataGridView1.CurrentRow.DataBoundItem as Patient;
            patient.Name = textBox1.Text;
            if (radioButton1.Checked)
                patient.Sex = "남";
            if (radioButton2.Checked)
                patient.Sex = "여";
            patient.Date_Of_Birth = dateTimePicker1.Value.Date;
            patient.Zip = textBox2.Text;
            patient.Addr = textBox3.Text;
            patient.Contact = textBox4.Text;

            foreach (var ward in DataManager.Hospital_Wards)
            {
                if (ward.Name.Equals(comboBox1.Text))
                {
                    patient.Hospital_Ward_ID = ward.ID;
                    break;
                }
            }

            foreach (var room in DataManager.Hospital_Rooms)
            {
                if (room.Number.Equals(comboBox2.Text))
                {
                    patient.Hospital_Room_ID = room.ID;
                    break;
                }
            }
            DataGridViewRefresh();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Patient patient = dataGridView1.CurrentRow.DataBoundItem as Patient;
            DataManager.Patients.Remove(patient);
            DataGridViewRefresh();
        }

        private void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            comboBox2.Text = "";
            comboBox2.Items.Clear();

            foreach (var ward in DataManager.Hospital_Wards)
            {
                if (ward.Name.Equals(comboBox1.Text))
                {
                    string id = ward.ID;
                    foreach (var room in DataManager.Hospital_Rooms)
                    {
                        if (id.Equals(room.Hospital_Ward_ID))
                        {
                            comboBox2.Items.Add(room.Number);
                        }
                    }
                    break;
                }
            }
        }

        private void DataGridViewRefresh()
        {
            DataManager.Save();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DataManager.Patients;
        }

        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;
            Patient patient = dataGridView1.CurrentRow.DataBoundItem as Patient;
            textBox1.Text = patient.Name;
            if (patient.Sex.Equals("남"))
                radioButton1.Checked = true;
            if (patient.Sex.Equals("여"))
                radioButton2.Checked = true;
            dateTimePicker1.Value = patient.Date_Of_Birth.Date;
            textBox2.Text = patient.Zip;
            textBox3.Text = patient.Addr;
            textBox4.Text = patient.Contact;

            foreach (var ward in DataManager.Hospital_Wards)
            {
                string id = patient.Hospital_Ward_ID;
                if (ward.ID.Equals(id))
                {
                    comboBox1.Text = ward.Name;
                    break;
                }
            }

            foreach (var room in DataManager.Hospital_Rooms)
            {
                string id = patient.Hospital_Room_ID;
                if (room.ID.Equals(id))
                {
                    comboBox2.Text = room.Number;
                    break;
                }
            }
        }
    }
}
