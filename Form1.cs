using MySql.Data.MySqlClient;
using System.Data;
using System.Net;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace UniCatalog
{
    public partial class Form1 : Form
    {
        private readonly List<Account> accountList;
        private readonly string connectionString = "Server=localhost;Database=unicatalog;Uid=root;";
        public string ?ins, dis;
        public Form1()
        {
            accountList = new List<Account>();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            try
            {
                using MySqlConnection connection = new(connectionString);
                connection.Open();
                Console.WriteLine("Connected to the database.");

                string query = "SELECT * FROM conturi;";
                using (MySqlCommand command = new(query, connection))
                {
                    using MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Account account = new()
                        {
                            User = reader.GetString("User"),
                            Password = reader.GetString("Password"),
                            UserType = reader.GetInt32("User Type")
                        };
                        accountList.Add(account);
                    }
                }

                Console.WriteLine("Disconnected from the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            ins = "SELECT MaterieTitular FROM `conturi` WHERE user = '" + username + "';";
            int found = 0;
            DataTable dt = GetDataFromDatabase(ins);
            foreach (DataRow row in dt.Rows)
            {
                dis = row["MaterieTitular"].ToString();
            }
            foreach (Account account in accountList)
            {
                if (account.User == username && account.Password == password)
                {
                    found = account.UserType;
                    Form2 form2 = new(dis)
                    {
                        Number = found
                    };
                    form2.Show();
                    this.Hide();
                    break;
                }
            }
            if (found == 0)
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
        private DataTable GetDataFromDatabase(string query)
        {
            DataTable data = new();
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Connected to the database.");
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    data.Load(reader);
                }
                Console.WriteLine("Disconnected from the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return data;
        }

    }
}
