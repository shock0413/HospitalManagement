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
    public partial class Form6 : Form
    {
        string hospitalization_id = "";     // 인스턴스 멤버

        public Form6()
        {
            InitializeComponent();

            foreach (var ward in DataManager.Hospital_Wards)
                comboBox1.Items.Add(ward.Name);

            dataGridView1.CurrentCellChanged += DataGridView1_CurrentCellChanged;
            dataGridView2.CurrentCellChanged += DataGridView2_CurrentCellChanged;
            comboBox1.TextChanged += ComboBox1_TextChanged;
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
            comboBox2.Items.Clear();

            Hospital_Ward ward = DataManager.Hospital_Wards.Single((x) => x.Name.Equals(comboBox1.Text));
            foreach (Hospital_Room item in DataManager.Hospital_Rooms)
                if (ward.ID.Equals(item.Hospital_Ward_ID))
                    comboBox2.Items.Add(item.Number);
            label8.Text = DataManager.Hospital_Rooms.Where((x) => x.Hospital_Ward_ID.Equals(ward.ID)).Count() + "개";

            Hospital_Room room = DataManager.Hospital_Rooms.Single((x) => x.Hospital_Ward_ID.Equals(ward.ID) &&
                x.Number.Equals(comboBox2.Text));
            label9.Text = room.Count + "명";
            label10.Text = DataManager.Hospitalizations.Where((x) => x.Hospital_Ward_ID.Equals(ward.ID) &&
                x.Hospital_Room_ID.Equals(room.ID) && x.Exit_Date >= DateTime.Now).Count() + "명";
            var hospitalization_list = DataManager.Hospitalizations.Where((x) => x.Hospital_Room_ID.Equals(room.ID));
            var temp = hospitalization_list.Join(DataManager.Patients,
                hospitalization => hospitalization.Patient_ID,
                patient => patient.ID,
                (hospitalization, patient) =>
                new
                {
                    입원번호 = hospitalization.ID,
                    성명 = patient.Name,
                    성별 = patient.Sex,
                    생년월일 = patient.Date_Of_Birth,
                    입원일 = hospitalization.Join_Date,
                    퇴원일 = hospitalization.Exit_Date
                }).ToList();
            dataGridView2.DataSource = temp;
        }

        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;
            
            Patient patient = dataGridView1.CurrentRow.DataBoundItem as Patient;
            if (DataManager.Hospitalizations.Where((x) => x.Patient_ID.Equals(patient.ID) &&
                x.Exit_Date >= DateTime.Now).Count() == 0)
            {
                return;
            }
            Hospitalization hospitalization = DataManager.Hospitalizations.Single((x) => x.Patient_ID.Equals(patient.ID) &&
                x.Exit_Date >= DateTime.Now);
            comboBox1.Text = DataManager.Hospital_Wards.Single((x) => x.ID.Equals(hospitalization.Hospital_Ward_ID)).Name;
            comboBox2.Text = DataManager.Hospital_Rooms.Single((x) => x.ID.Equals(hospitalization.Hospital_Room_ID)).Number;
            dateTimePicker2.Value = hospitalization.Join_Date;
            dateTimePicker3.Value = hospitalization.Exit_Date;
        }

        private void DataGridView2_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
                return;
            DataGridView temp = (DataGridView)sender;
            hospitalization_id = temp.CurrentRow.Cells[0].Value.ToString();
        }

        private void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            comboBox2.Text = "";
            comboBox2.Items.Clear();

            Hospital_Ward ward = DataManager.Hospital_Wards.Single((x) => x.Name.Equals(comboBox1.Text));
            foreach (var room in DataManager.Hospital_Rooms)
                if (ward.ID.Equals(room.Hospital_Ward_ID))
                    comboBox2.Items.Add(room.Number);
            label8.Text = DataManager.Hospital_Rooms.Where((x) => x.Hospital_Ward_ID.Equals(ward.ID)).Count() + "개";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hospitalization hospitalization = new Hospitalization();
            hospitalization.ID = DataManager.hospitalization_id++ + "";
            Patient patient = dataGridView1.CurrentRow.DataBoundItem as Patient;
            hospitalization.Patient_ID = patient.ID;
            Hospital_Ward ward = DataManager.Hospital_Wards.Single((x) => x.Name.Equals(comboBox1.Text));
            Hospital_Room room = DataManager.Hospital_Rooms.Single((x) => x.Number.Equals(comboBox2.Text));
            hospitalization.Hospital_Ward_ID = ward.ID;
            hospitalization.Hospital_Room_ID = room.ID;
            hospitalization.Join_Date = DateTime.Now;
            hospitalization.Join_Date = dateTimePicker2.Value;
            hospitalization.Exit_Date = dateTimePicker3.Value;
            DataManager.Hospitalizations.Add(hospitalization);

            var hospitalization_list = DataManager.Hospitalizations.Where((x) => x.Hospital_Room_ID.Equals(room.ID));
            var temp = hospitalization_list.Join(DataManager.Patients,
                hospitalization_temp => hospitalization_temp.Patient_ID,
                patient_temp => patient_temp.ID,
                (hospitalization_temp, patient_temp) =>
                new
                {
                    입원번호 = hospitalization_temp.ID,
                    성명 = patient_temp.Name,
                    성별 = patient_temp.Sex,
                    생년월일 = patient_temp.Date_Of_Birth,
                    입원일 = hospitalization_temp.Join_Date,
                    퇴원일 = hospitalization_temp.Exit_Date
                }).ToList();
            DataManager.Save();
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = temp;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hospitalization hospitalization = DataManager.Hospitalizations.Single((x) => x.ID.Equals(hospitalization_id));
            Patient patient = dataGridView1.CurrentRow.DataBoundItem as Patient;
            hospitalization.Patient_ID = patient.ID;
            Hospital_Ward ward = DataManager.Hospital_Wards.Single((x) => x.Name.Equals(comboBox1.Text));
            Hospital_Room room = DataManager.Hospital_Rooms.Single((x) => x.Number.Equals(comboBox2.Text));
            hospitalization.Hospital_Ward_ID = ward.ID;
            hospitalization.Hospital_Room_ID = room.ID;
            hospitalization.Join_Date = DateTime.Now;
            hospitalization.Join_Date = dateTimePicker2.Value;
            hospitalization.Exit_Date = dateTimePicker3.Value;
            var hospitalization_list = DataManager.Hospitalizations.Where((x) => x.Hospital_Room_ID.Equals(room.ID));
            var temp = hospitalization_list.Join(DataManager.Patients,
                hospitalization_temp => hospitalization_temp.Patient_ID,
                patient_temp => patient_temp.ID,
                (hospitalization_temp, patient_temp) =>
                new
                {
                    입원번호 = hospitalization_temp.ID,
                    성명 = patient_temp.Name,
                    성별 = patient_temp.Sex,
                    생년월일 = patient_temp.Date_Of_Birth,
                    입원일 = hospitalization_temp.Join_Date,
                    퇴원일 = hospitalization_temp.Exit_Date
                }).ToList();
            DataManager.Save();
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = temp;
            label10.Text = DataManager.Hospitalizations.Where((x) => x.Hospital_Ward_ID.Equals(ward.ID) &&
                x.Hospital_Room_ID.Equals(room.ID) && x.Exit_Date >= DateTime.Now).Count() + "명";
        }
    }
}
