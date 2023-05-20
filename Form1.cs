using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UniCatalog
{
    public partial class Form1 : Form
    {
        private List<Account> accountList;
        public Form1()
        {
            accountList = new List<Account>();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            string connectionString = "Server=localhost;Database=unicatalog;Uid=root;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
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
                                Account account = new Account();
                                account.User = reader["User"].ToString();
                                account.Password = reader["Password"].ToString();
                                account.UserType = int.Parse(reader["User Type"].ToString());

                                accountList.Add(account);
                            }
                        }
                    }

                    connection.Close();
                    Console.WriteLine("Disconnected from the database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            int found = 0;

            foreach (var item in accountList)
            {
                Account account = (Account)item;
                if (account.User == username && account.Password == password)
                {
                    found = account.UserType;

                    break;
                }
            }

            if (found!=0)
            {
                MessageBox.Show("Login successful!");
                this.Hide();
                switch (found)
                {
                    case 1:
                        Form2 form2 = new Form2();
                        form2.Show();
                        break;
                    case 2:
                        Form3 form3 = new Form3();
                        form3.Show();
                        break;
                    case 3:
                        Form4 form4 = new Form4();
                        form4.Show();
                        break;

                }
                   
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
    }


}
