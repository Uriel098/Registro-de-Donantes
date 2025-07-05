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
    public partial class Form2 : Form
    {
        int contador = 2;
        string connectionString = "datasource=localhost;port=3307;username=root;password=;database=integral;";
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string query = "Select nivel from usuarios where cuenta='" + textBox1.Text + "'and clave =md5('" + textBox2.Text + "')";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader;
            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Form1.cuenta = textBox1.Text;
                        Form1.nivel = Convert.ToInt32(reader.GetString(0));
                        Close();

                    }
                }
                else
                {
                    MessageBox.Show("Cuenta o contraseña inocrrecta intentelo de nuevo.\n Tienes " + contador + " intentos.");
                    contador--;
                    if (contador == -1) Application.Exit();
                }
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Form1.cuenta == "") Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }

        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}