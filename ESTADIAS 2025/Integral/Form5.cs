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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        string connectionString = "datasource=localhost;port=3307;username=root;password=;database=integral;";
        private void Form5_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("id_carrera", "id_carrera");
            dataGridView1.Columns.Add("carrera", "carrera");
            dataGridView1.Columns.Add("carr", "carr");
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
            string query = "Select * from carreras";
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
            string query = "insert into carreras values(null,'" + textBox3.Text + "','" + textBox4.Text + "')";
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
            string query = "delete from carreras where id_inscrito=" + textBox2.Text;
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
            string query = "update carreras set carrera='"
                 + textBox3.Text.Trim() +
                 "', carr='"
                 + textBox4.Text.Trim()
                 + "' where id_carrera=" + textBox2.Text;
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

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                String t; try
                {
                    t = Convert.ToString(textBox3.Text);
                }
                catch
                {
                    MessageBox.Show("El formato no es correcto", "Error deformato");
                    textBox3.Focus();
                }
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                String t; try
                {
                    t = Convert.ToString(textBox4.Text);
                }
                catch
                {
                    MessageBox.Show("El formato no es correcto", "Error deformato");
                    textBox4.Focus();
                }
            }
        }
    }
}
