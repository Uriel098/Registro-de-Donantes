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
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Debes proporcionar la contraseña actual");
                textBox1.Focus();
            }
            else
            {
                if (textBox2.Text == textBox3.Text)
                {
                    if (textBox2.Text == "")
                    {
                        textBox2.Text = textBox1.Text;
                    }
                    string connectionString = "datasource=localhost;port=3307;username=root;password=;database=techdy;";
                    string query = "update usuarios set Clave=md5('" + textBox2.Text + "'), " + " where cuenta='" + Form1.cuenta + "' and clave=md5('" + textBox1.Text + "')";
                    MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                    MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                    int reader;
                    try
                    {
                        databaseConnection.Open();
                        reader = commandDatabase.ExecuteNonQuery();
                        if (reader == 0)
                        {
                            MessageBox.Show("La contraseña actual es incorrecta.");
                        }
                        else
                        {
                            MessageBox.Show("Cambio de la contraseña se realizó con éxito\nEs necesario reinciar para visualizar los cambios");

                            Close();
                        }
                        databaseConnection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Confirmación de la contraseña no válida");
                    textBox3.Focus();
                }
            }
        }
            

        private void Form14_Load(object sender, EventArgs e)
        {
         
          
           
        }
    }
}
