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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }
        string connectionString = "datasource=localhost;port=3307;username=root;password=;database=integral;";
        private void button1_Click(object sender, EventArgs e)
        {
            string query = "Select * from usuarios";
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
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4)
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

        private void Form10_Load(object sender, EventArgs e)
        {
            if (Form1.nivel!= 1 )
            {
                MessageBox.Show("Acesso denegado \nNo tienes nivel para acceder ");
                Close();
            }
           
            dataGridView1.Columns.Add("Id_Usuario", "Id_Usuario");
            dataGridView1.Columns.Add("Usuario", "Usuario");
            dataGridView1.Columns.Add("Cuenta", "Cuenta");
            dataGridView1.Columns.Add("Clave", "Clave");
            dataGridView1.Columns.Add("Nivel", "Nivel");
            
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

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
