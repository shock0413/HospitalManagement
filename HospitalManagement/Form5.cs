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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();

            dataGridView1.CurrentCellChanged += DataGridView1_CurrentCellChanged;
            dataGridView1.DataSource = DataManager.Hospital_Wards;
            dataGridView2.CurrentCellChanged += DataGridView2_CurrentCellChanged;

            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;
            button4.Click += Button4_Click;
            button5.Click += Button5_Click;
            button6.Click += Button6_Click;

            comboBox1.TextChanged += ComboBox1_TextChanged;
            foreach (var ward in DataManager.Hospital_Wards)
                comboBox1.Items.Add(ward.Name);
        }

        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                    return;
                Hospital_Ward ward = dataGridView1.CurrentRow.DataBoundItem as Hospital_Ward;
                textBox1.Text = ward.Name;
                dataGridView2.DataSource = DataManager.Hospital_Rooms.Where((x) => x.Hospital_Ward_ID == ward.ID).ToList<Hospital_Room>();
                comboBox1.Text = ward.Name;
            }
            catch (Exception expt)
            {
                
            }
        }

        private void DataGridView2_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.CurrentRow == null)
                    return;
                Hospital_Room room = dataGridView2.CurrentRow.DataBoundItem as Hospital_Room;
                comboBox1.Text = DataManager.Hospital_Wards.Single((x) => x.ID == room.Hospital_Ward_ID).Name;
                textBox2.Text = room.Number;
                textBox3.Text = room.Count;
            }
            catch (Exception expt)
            {

            }
        }

        private void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (comboBox1.Text.Equals(cell.Value.ToString()))
                        {
                            dataGridView1.CurrentCell = cell;
                            break;
                        }
                    }
                }
            }
            catch (Exception expt)
            {

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Hospital_Ward ward = new Hospital_Ward();
            ward.ID = DataManager.ward_id++ + "";
            ward.Name = textBox1.Text;
            ward.Count = DataManager.Hospital_Rooms.Where((x) => x.Hospital_Ward_ID == ward.ID).Count().ToString();
            DataManager.Hospital_Wards.Add(ward);

            WardDataGridViewRefresh();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Hospital_Ward ward = dataGridView1.CurrentRow.DataBoundItem as Hospital_Ward;
            ward.Name = textBox1.Text;

            WardDataGridViewRefresh();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Hospital_Ward ward = dataGridView1.CurrentRow.DataBoundItem as Hospital_Ward;
            DataManager.Hospital_Wards.Remove(ward);

            WardDataGridViewRefresh();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Hospital_Ward temp = dataGridView1.CurrentRow.DataBoundItem as Hospital_Ward;
            Hospital_Room room = new Hospital_Room();
            room.ID = DataManager.room_id++ + "";
            foreach (var ward in DataManager.Hospital_Wards)
            {
                if (ward.Name.Equals(comboBox1.Text))
                {
                    room.Hospital_Ward_ID = ward.ID;
                    break;
                }
            }
            room.Number = textBox2.Text;
            room.Count = textBox3.Text;

            DataManager.Hospital_Rooms.Add(room);

            foreach (var ward in DataManager.Hospital_Wards)
            {
                if (ward.Name.Equals(comboBox1.Text))
                {
                    ward.Count = DataManager.Hospital_Rooms.Where((x) => x.Hospital_Ward_ID == ward.ID).Count().ToString();
                    break;
                }
            }

            if (temp.Name.Equals(comboBox1.Text))
            {
                DataManager.Save();
                dataGridView2.DataSource = null;
                DataManager.Load();
                dataGridView2.DataSource = DataManager.Hospital_Rooms.Where((x) => x.Hospital_Ward_ID == temp.ID).ToList<Hospital_Room>();
            }

            WardDataGridViewRefresh();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Hospital_Ward temp = dataGridView1.CurrentRow.DataBoundItem as Hospital_Ward;
            Hospital_Room room = dataGridView2.CurrentRow.DataBoundItem as Hospital_Room;

            foreach (var ward in DataManager.Hospital_Wards)
            {
                if (ward.Name.Equals(comboBox1.Text))
                {
                    room.Hospital_Ward_ID = ward.ID;
                    break;
                }
            }

            room.Number = textBox2.Text;
            room.Count = textBox3.Text;

            DataManager.Save();
            DataManager.Load();
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = DataManager.Hospital_Rooms.Where((x) => x.Hospital_Ward_ID == temp.ID).ToList<Hospital_Room>();
        }

        private void Button6_Click(object sender, EventArgs e)
        {

        }

        private void WardDataGridViewRefresh()
        {
            DataManager.Save();
            DataManager.Load();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DataManager.Hospital_Wards;
        }
    }
}