using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UniCatalog
{
    public partial class Form1 : Form
    {
        private Form currentForm;
        private List<Account> accountList;

        public Form1()
        {
            accountList = new List<Account>();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label3.Hide();
            comboBox1.Hide();
            textBox2.PasswordChar = '*';
            string connectionString = "Server=localhost;Database=unicatalog;Uid=root;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connected to the database.");

                    string query = "SELECT * FROM conturi;";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Account account = new Account
                                {
                                    User = reader.GetString("User"),
                                    Password = reader.GetString("Password"),
                                    UserType = reader.GetInt32("User Type")
                                };
                                accountList.Add(account);
                            }
                        }
                    }

                    Console.WriteLine("Disconnected from the database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            int found = 0;

            foreach (Account account in accountList)
            {
                if (account.User == username && account.Password == password)
                {
                    found = account.UserType;
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    break;
                }
            }

            if (comboBox1.Items.Count != 0)
            {
                switch (comboBox1.SelectedItem.ToString())
                {
                    case "Admin":
                        found = 1;
                        break;
                    case "Secretar":
                        found = 10;
                        break;
                    default:
                        found = 100;
                        break;
                }
            }

            if (found == 1 || found == 10 || found == 100)
            {
                switch (found)
                {
                    case 1:
                        currentForm = new Form2();
                        break;
                    case 10:
                        currentForm = new Form3();
                        break;
                    case 100:
                        currentForm = new Form4();
                        break;
                }
                Hide();
                currentForm.FormClosed += CurrentForm_FormClosed;
                currentForm.Show();
            }
            else if (found != 0)
            {
                label3.Show();
                comboBox1.Show();
                switch (found)
                {
                    case 011:
                        comboBox1.Items.Add("Admin");
                        comboBox1.Items.Add("Secretar");
                        break;
                    case 110:
                        comboBox1.Items.Add("Cadru Didactic");
                        comboBox1.Items.Add("Secretar");
                        break;
                    case 101:
                        comboBox1.Items.Add("Admin");
                        comboBox1.Items.Add("Cadru Didactic");
                        break;
                    case 111:
                        comboBox1.Items.Add("Admin");
                        comboBox1.Items.Add("Cadru Didactic");
                        comboBox1.Items.Add("Secretar");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void CurrentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender == currentForm)
            {
                if (currentForm is Form1)
                {
                    Application.Exit();
                }
                else
                {
                    Show();
                }
            }
        }
    }
}
