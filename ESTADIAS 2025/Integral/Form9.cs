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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }
        string connectionString = "datasource=localhost;port=3307;username=root;password=;database=integral;";

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "select* from estudiantes where estudiante LIKE '%" + textBox1.Text + " %' limit 300";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader;
            if (textBox1.Text.Contains('%') || textBox1.Text.Contains(';'))
            {
                MessageBox.Show("Proteccion contra Sql Injection.\nNo se permiten caracteres especiales: %;");
                return;
            }
            dataGridView1.Rows.Clear();
            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                if (textBox1.Text != "")
                {

                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4)
                         );
                    }
                    if (dataGridView1.Rows.Count > 299)
                        MessageBox.Show("Demasiados registros encontrados.\n Favor de depurar la busqueda");
                }
                else
                {
                    dataGridView1.Rows.Clear();
                }
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("id_Estudiante", "id_Estudiante");
            dataGridView1.Columns.Add("Estudiante", "Estudiante");
            dataGridView1.Columns.Add("Matricula", "Matricula");
            dataGridView1.Columns.Add("Carrera", "Carrera");
            dataGridView1.Columns.Add("Id_Carrera", "Id_Carrera");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "insert into estudiantes values(null,'" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')";
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
            string query = "delete from grupos where id_estudiante=" + textBox2.Text;
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader;
            databaseConnection.Open();
            reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
            button1_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string query = "update inscritos set Id_Estudiante='"
                 + textBox3.Text.Trim() +
                 "', estudiante='"
                 + textBox4.Text.Trim()
                 + "' where matricula="
                 + textBox5.Text.Trim()
                 + "' where carrera="
                 + textBox6.Text.Trim()
                 + "' where id_carrera="
                 + textBox2.Text;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
        }
    }
}

