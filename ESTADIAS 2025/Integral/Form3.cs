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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        string connectionString = "datasource=localhost;port=3307;username=root;password=;database=integral;";

        private void Form3_Load(object sender, EventArgs e)
        {
            //datagrid de inscritos
            dataGridView1.Columns.Add("id_Inscrito", "id_Inscrito");
            dataGridView1.Columns.Add("Id_Estudiante", "Id_Estudiante");
            dataGridView1.Columns.Add("Id_Grupo", "Id_Grupo");
            //Barra de busqueda estudiantes
            dataGridView2.Columns.Add("id_Estudiante", "id_Estudiante");
            dataGridView2.Columns.Add("Estudiante", "Estudiante");
            dataGridView2.Columns.Add("Matricula", "Matricula");
            dataGridView2.Columns.Add("Carr", "Carr");
            dataGridView2.Columns.Add("Id_Carrera", "Id_Carrera");
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
            string query = "select * from inscritos where id_estudiante like '%" + textBox1.Text + "%' order by id_inscrito limit 10";
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
            string query = "insert into Inscritos values(null,'" + textBox3.Text + "','" + textBox4.Text + "')";
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
            string query = "delete from inscritos where id_inscrito=" + textBox2.Text;
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
            string query = "update inscritos set Id_Estudiante='"
                    + textBox3.Text.Trim() +
                    "', Id_Grupo='"
                    + textBox4.Text.Trim()
                    + "' where id_inscrito=" + textBox2.Text;
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
                double t;
                try
                {
                    t = Convert.ToInt32(textBox3.Text);
                }
                catch
                {
                    MessageBox.Show("El formato no es correcto", "Error de formato");
                    textBox3.Focus();
                }
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                double t;
                try
                {
                    t = Convert.ToInt32(textBox4.Text);
                }
                catch
                {
                    MessageBox.Show("El formato no es correcto", "Error de formato");
                    textBox4.Focus();
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //barra de busqueda 
                string query = "select* from estudiantes where estudiante LIKE '%" + textBox5.Text + " %' limit 300";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlDataReader reader;
                if (textBox5.Text.Contains('%') || textBox5.Text.Contains(';'))
                {
                    MessageBox.Show("Proteccion contra Sql Injection.\nNo se permiten caracteres especiales: %  ;");
                    return;
                }
                dataGridView2.Rows.Clear();
                try
                {
                    databaseConnection.Open();
                    reader = commandDatabase.ExecuteReader();

                    if (textBox5.Text != "")
                    {

                        while (reader.Read())
                        {
                            dataGridView2.Rows.Add(reader.GetString(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4)
                             );
                        }
                        if (dataGridView2.Rows.Count > 299)
                            MessageBox.Show("Demasiados registros encontrados.\n Favor de depurar la busqueda");
                    }
                    else
                    {
                        dataGridView2.Rows.Clear();
                    }
                    databaseConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                textBox3.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();

            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string query = "select * from inscritos where id_estudiante like '%" + textBox1.Text + "%' order by id_inscrito limit 10";
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
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                int t; try
                {
                    t = Convert.ToInt32(textBox1.Text);
                }
                catch
                {
                    MessageBox.Show("El formato no es correcto", "Error deformato");
                    textBox1.Focus();
                }
            }
        }


        private void textBox5_Leave(object sender, EventArgs e)
        {

        }

        private void textBox5_Enter(object sender, EventArgs e)
        {

        }
    }
}

     