using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UniCatalog
{
    public partial class Form2 : Form
    {
        private DataTable dataTable;
        private string connectionString = "Server=localhost;Database=unicatalog;Uid=root;";

        public Form2()
        {
            InitializeComponent();
            dataTable = new DataTable();
        }

        private void utilizatoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dataTable;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns["ID"].Visible = false;
            button2.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadDataFromDatabase();
            button2.Hide();
        }

        private void LoadDataFromDatabase()
        {
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
                            dataTable.Load(reader);
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
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0 && rowIndex < dataTable.Rows.Count)
            {
                DataRow row = dataTable.Rows[rowIndex];

                // Get the modified values from the DataGridView
                string id = row["ID"].ToString();
                string username = row["User"].ToString();
                string password = row["Password"].ToString();
                int userType = Convert.ToInt32(row["User Type"]);

                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE conturi SET User = @username, Password = @password, `User Type` = @userType WHERE ID = @id;";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@username", username);
                            command.Parameters.AddWithValue("@password", password);
                            command.Parameters.AddWithValue("@userType", userType);
                            command.Parameters.AddWithValue("@id", id);

                            command.ExecuteNonQuery();
                        }

                        Console.WriteLine("Database updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            DataRow newRow = dataTable.NewRow();

            // Assign new values to the row
            string username = "NewUser";
            string password = "NewPassword";
            int userType = 1; // Replace with the desired value

            // Get the highest ID from the existing rows
            int maxID = GetMaxIDFromDatabase();
            int newID = maxID + 1;

            newRow["ID"] = newID;
            newRow["User"] = username;
            newRow["Password"] = password;
            newRow["User Type"] = userType;

            dataTable.Rows.Add(newRow);

            // Insert the new row into the database
            InsertRowIntoDatabase(newRow);
        }

        private int GetMaxIDFromDatabase()
        {
            int maxID = 0;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT MAX(ID) FROM conturi;";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                            maxID = Convert.ToInt32(result);
                    }

                    Console.WriteLine("Max ID retrieved from the database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return maxID;
        }

        private void InsertRowIntoDatabase(DataRow row)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO conturi (ID, User, Password, `User Type`) VALUES (@id, @username, @password, @userType);";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", row["ID"]);
                        command.Parameters.AddWithValue("@username", row["User"]);
                        command.Parameters.AddWithValue("@password", row["Password"]);
                        command.Parameters.AddWithValue("@userType", row["User Type"]);

                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("New row inserted into the database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                DataRowView selectedRowView = (DataRowView)selectedRow.DataBoundItem;
                DataRow selectedDataRow = selectedRowView.Row;

                // Get the ID of the selected row
                string id = selectedDataRow["ID"].ToString();

                // Delete the row from the database
                DeleteRowFromDatabase(id);

                // Delete the row from the DataTable
                dataTable.Rows.Remove(selectedDataRow);
            }
        }
        private void DeleteRowFromDatabase(string id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM conturi WHERE ID = @id;";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("Row deleted from the database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("If you want to Edit a row you can double click on the field and edit it . If you want to add a row click on the blank space at the buttom and press something ( a new one will be created witch you can edit). If you want to remove a row , select the row and press Remove", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
