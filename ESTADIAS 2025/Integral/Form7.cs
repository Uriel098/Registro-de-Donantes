using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Integral
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        string connectionString = "datasource=localhost;port=3307;username=root;password=;database=integral;";
        private void Form7_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("id_Instructor", "id_Instructor");
            dataGridView1.Columns.Add("Instructor", "Instructor");
            dataGridView1.Columns.Add("Celular", "Celular");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "Select * from instrcutor";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader;
            if (textBox1.Text.Contains('%') || textBox1.Text.Contains(';'))
            {
                MessageBox.Show("Proteccion contra Sql Injection.\nNo se permiten caracteres especiales: % ;");
                return;
            }
            dataGridView1.Rows.Clear();
            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0),
                            reader.GetString(1),
                            reader.GetString(2)
                         );
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron datos.");
                }
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "insert into instructores values(null,'" + textBox3.Text + "','" + textBox4.Text + "')";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader;
            databaseConnection.Open();
            reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
            button1_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "delete from actividades where id_instrcutor=" + textBox2.Text;
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader;
            databaseConnection.Open();
            reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
            button1_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string query = "update actividades set instrcutor='"
              + textBox3.Text.Trim() +
              "', celular='"
              + textBox4.Text.Trim() 
              + "' where id_instrcutor=" + textBox2.Text;
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader;
            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            button1_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
