using MySql.Data.MySqlClient;

namespace UniCatalog
{
    public partial class Form1 : Form
    {
        private Form? currentForm;
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
                    Form2 form2 = new Form2();
                    form2.Number = found;
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
    }
}
