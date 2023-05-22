using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace UniCatalog
{
    public partial class Form2 : Form
    {
        private DataTable dataTable;
        private string connectionString = "Server=localhost;Database=unicatalog;Uid=root;";
        private string query;
        private int currentTable;

        public Form2()
        {
            InitializeComponent();
            dataTable = new DataTable();
        }

        private void utilizatoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDataFromDatabase(1);
            currentTable = 1;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns["ID"].Visible = false;
            button2.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button2.Hide();
        }

        private void LoadDataFromDatabase(int operatie)
        {
            dataTable = new DataTable();
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connected to the database.");
                    query = operatie == 1 ? "SELECT * FROM conturi;" : "SELECT * FROM ciclurideinvatamant;";
                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                    dataGridView1.DataSource = dataTable;
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
                if (currentTable == 1)
                {
                    // Get the modified values from the DataGridView
                    string id = row["ID"].ToString();
                    string username = row["User"].ToString();
                    string password = row["Password"].ToString();
                    int userType = Convert.ToInt32(row["User Type"]);
                    UpdateRowInDatabase(id, username, password, userType);
                }
                else
                {
                    // Get the modified values from the DataGridView
                    string programdestudii = row["Program de studii"].ToString();
                    string definire = row["Definire"].ToString();
                    UpdateRowInDatabase(programdestudii, definire, definire, 0);
                }
            }
        }

        private void UpdateRowInDatabase(string id, string username, string password, int userType)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    if (userType != 0)
                        query = "UPDATE conturi SET User = @username, Password = @password, `User Type` = @userType WHERE ID = @id;";
                    else
                        query = "UPDATE ciclurideinvatamant SET `Program de studii` = @id, Definire = @username WHERE `Program de studii` = @id OR Definire = @username;";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        if (userType != 0)
                        {
                            command.Parameters.AddWithValue("@password", password);
                            command.Parameters.AddWithValue("@userType", userType);
                        }
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

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            DataRow newRow = dataTable.NewRow();

            if (currentTable == 1)
            {
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
            }
            else
            {
                string programdestudii = "Un Program Nou";
                string definire = "Descriere noua";
                newRow["Program de studii"] = programdestudii;
                newRow["Definire"] = definire;
            }

            dataTable.Rows.Add(newRow);

            // Insert the new row into the database
            InsertRowIntoDatabase(newRow);
        }

        private int GetMaxIDFromDatabase()
        {
            int maxID = 0;

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT MAX(ID) FROM conturi;";
                    using (var command = new MySqlCommand(query, connection))
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
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    query = currentTable == 1 ? "INSERT INTO conturi (ID, User, Password, `User Type`) VALUES (@id, @username, @password, @userType);" : "INSERT INTO ciclurideinvatamant (`Program de studii`, Definire) VALUES (@programdestudii, @definire);";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        if (currentTable == 1)
                        {
                            command.Parameters.AddWithValue("@id", row["ID"]);
                            command.Parameters.AddWithValue("@username", row["User"]);
                            command.Parameters.AddWithValue("@password", row["Password"]);
                            command.Parameters.AddWithValue("@userType", row["User Type"]);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@programdestudii", row["Program de studii"]);
                            command.Parameters.AddWithValue("@definire", row["Definire"]);
                        }

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
                string id = currentTable == 1 ? selectedDataRow["ID"].ToString() : selectedDataRow["Program de studii"].ToString();

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
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    query = currentTable == 1 ? "DELETE FROM conturi WHERE ID = @id;" : "DELETE FROM ciclurideinvatamant  WHERE `Program de studii` = @id;";
                    using (var command = new MySqlCommand(query, connection))
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
            MessageBox.Show("If you want to Edit a row you can double click on the field and edit it. If you want to add a row, click on the blank space at the bottom and enter the values (a new one will be created which you can edit). If you want to remove a row, select the row and press Remove.", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cicluDeInvatamantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDataFromDatabase(2);
            currentTable = 2;
            button2.Show();
        }
    }
}
