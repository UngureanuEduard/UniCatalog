using MySql.Data.MySqlClient;
using System.Data;

namespace UniCatalog
{
    public partial class Form2 : Form
    {
        public int Number { get; set; }
        private DataTable dataTable;
        private string connectionString = "Server=localhost;Database=unicatalog;Uid=root;";
        private string? query;
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
            dataGridView1.Columns["ID"].Visible = false;
            button2.Show();
            hidestudii();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox3.Hide();
            comboBox4.Hide();
            button2.Hide();
            hidestudii();
           
            foreach (ToolStripItem item in optiuniToolStripMenuItem.DropDownItems)
            {
                item.Visible = false;
            }
            if (Number % 10 == 1)
            {
                utilizatoriToolStripMenuItem.Visible = true;
                cicluDeInvatamantToolStripMenuItem.Visible = true;
                programeDeStudiiToolStripMenuItem.Visible = true;
                planuriDeInvatamantToolStripMenuItem.Visible = true;
                studentiToolStripMenuItem.Visible = true;
                grupeToolStripMenuItem.Visible = true;
            }
        }

        private void hidestudii()
        {
            comboBox1.Hide();
            textBox1.Hide();
            button1.Hide();
            comboBox2.Hide();
            comboBox4.Hide();
            comboBox3.Hide();
            if (textBox2.Visible == true)
            {
                textBox2.Hide();
                textBox3.Hide();
                textBox4.Hide();
                button3.Hide();
            }
        }

        private void LoadDataFromDatabase(int operatie)
        {
            dataGridView1.Enabled = true;
            dataTable = new DataTable();
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connected to the database.");

                    query = operatie switch
                    {
                        1 => "SELECT * FROM conturi;",
                        2 => "SELECT * FROM ciclurideinvatamant;",
                        3 => "SELECT * FROM programedestudii;",
                        4 => "SELECT * FROM discipline;",
                        7 => "SELECT * FROM student;",
                        8 => "Select * FROM grupe",
                        _ => query
                    };

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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (currentTable != 3 && currentTable != 4)
            {
                int rowIndex = e.RowIndex;
                if (rowIndex >= 0 && rowIndex < dataTable.Rows.Count)
                {
                    DataRow row = dataTable.Rows[rowIndex];
                    if (currentTable == 1)
                    {
                        // Get the modified values from the DataGridView
                        string? id = row["ID"].ToString();
                        string? username = row["User"].ToString();
                        string? password = row["Password"].ToString();
                        int userType = Convert.ToInt32(row["User Type"]);
                        string? materie = row["MaterieTitular"].ToString();
                        UpdateRowInDatabase(id, username, password, userType, materie);
                    }
                    else if (currentTable == 7)
                    {
                        string? nrmat = row["Nr. Matricol"].ToString();
                        string? nume = row["Nume"].ToString();
                        string? prenume = row["Prenume"].ToString();
                        string? initialaTata = row["Initiala Tatalui"].ToString();
                        long cnp = Convert.ToInt64(row["CNP"]);
                        DateTime dataInscrierii = (DateTime)row["Data Inscrierii"];
                        string? cicluInvatamant = row["Ciclu de invatamant"].ToString();
                        double medieInscriere = Convert.ToDouble(row["Media Inscriere"]);
                        string grupa = row["Grupa"].ToString();
                        UpdateRowInTableStudent(nrmat, nume, prenume, initialaTata, cnp, dataInscrierii, cicluInvatamant, medieInscriere, grupa);
                    }
                    else
                    {
                        // Get the modified values from the DataGridView
                        string ciclu = row["Ciclu"].ToString();
                        string acronim = row["Acronim"].ToString();
                        UpdateRowInDatabase(ciclu, acronim, acronim, 0, null);
                    }
                }
            }
            else
            {
                MessageBox.Show("You can only remove and insert in this table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateRowInDatabase(string id, string username, string password, int userType, string materie)
        {

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    if (userType != 0)
                        query = "UPDATE conturi SET User = @username, Password = @password, `User Type` = @userType, `MaterieTitular`=@materie WHERE ID = @id;";
                    else
                        query = "UPDATE ciclurideinvatamant SET Ciclu = @id, Acronim = @username WHERE Ciclu = @id OR Acronim = @username;";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        if (userType != 0)
                        {
                            command.Parameters.AddWithValue("@materie", materie);
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

        private void UpdateRowInTableStudent(string nrmat, string nume, string prenume, string initialaTata, long cnp,
            DateTime dataInscrierii, string cicluInvatamant, double medieInscriere, string grupa)
        {

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    query = "UPDATE student SET `Nr. Matricol` = @nrmat, Nume = @nume, Prenume = @prenume,`Initiala Tatalui` = @initialaTata, CNP = @cnp, `Data Inscrierii` =  @dataInscrierii,`Ciclu de invatamant` = @cicluInvatamant, `Media Inscriere`=@medieInscriere, Grupa=@grupa  WHERE `Nr. Matricol` = @nrmat OR  CNP = @cnp";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nrmat", nrmat);
                        command.Parameters.AddWithValue("@nume", nume);
                        command.Parameters.AddWithValue("@prenume", prenume);
                        command.Parameters.AddWithValue("@initialaTata", initialaTata);
                        command.Parameters.AddWithValue("@cnp", cnp);
                        command.Parameters.AddWithValue("@dataInscrierii", dataInscrierii);
                        command.Parameters.AddWithValue("@cicluInvatamant", cicluInvatamant);
                        command.Parameters.AddWithValue("@medieInscriere", medieInscriere);
                        command.Parameters.AddWithValue("@grupa", grupa);

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
            if (currentTable != 3 && currentTable != 4)
            {
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
                else if (currentTable == 7)
                {
                    newRow["Nr. Matricol"] = 0;
                    newRow["Nume"] = "Nume";
                    newRow["Prenume"] = "Prenume";
                    newRow["Initiala Tatalui"] = "A";
                    newRow["CNP"] = 1234;
                    newRow["Data Inscrierii"] = new DateTime(2023, 6, 1);
                    newRow["Ciclu de invatamant"] = "Zi";
                    newRow["Media inscriere"] = 0;
                }
                else
                {
                    string ciclu = "Ciclu Nou";
                    string acronim = "Acronim nou";
                    newRow["Ciclu"] = ciclu;
                    newRow["Acronim"] = acronim;
                }

                dataTable.Rows.Add(newRow);

                // Insert the new row into the database
                if (currentTable != 7)
                    InsertRowIntoDatabase(newRow);
                else
                    InsertRowIntoTableStudent(newRow);
            }
            else
            {
                MessageBox.Show("You can only remove and insert in this table , use the buttons", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    query = currentTable == 1 ? "INSERT INTO conturi (ID, User, Password, `User Type`) VALUES (@id, @username, @password, @userType);" : "INSERT INTO ciclurideinvatamant (Ciclu, Acronim) VALUES (@ciclu, @acronim);";
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
                            command.Parameters.AddWithValue("@ciclu", row["Ciclu"]);
                            command.Parameters.AddWithValue("@acronim", row["Acronim"]);
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

        private void InsertRowIntoTableStudent(DataRow row)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    query = "INSERT INTO student (`Nr. Matricol`, Nume, Prenume, `Initiala Tatalui`, CNP, `Data Inscrierii`, `Ciclu de invatamant`, `Media Inscriere`) VALUES (@nrmat, @nume, @prenume, @initialaTata, @cnp, @dataInscrierii, @cicluInvatamant, @medieInscriere);";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nrmat", row["Nr. MatricoL"]);
                        command.Parameters.AddWithValue("@nume", row["Nume"]);
                        command.Parameters.AddWithValue("@prenume", row["Prenume"]);
                        command.Parameters.AddWithValue("@initialaTata", row["Initiala Tatalui"]);
                        command.Parameters.AddWithValue("@cnp", row["CNP"]);
                        command.Parameters.AddWithValue("@dataInscrierii", row["Data Inscrierii"]);
                        command.Parameters.AddWithValue("@cicluInvatamant", row["Ciclu de invatamant"]);
                        command.Parameters.AddWithValue("@medieInscriere", row["Media Inscriere"]);

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
            if (dataGridView1.SelectedRows.Count > 0 && currentTable != 5)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                DataRowView selectedRowView = (DataRowView)selectedRow.DataBoundItem;
                DataRow selectedDataRow = selectedRowView.Row;
                string id;
                switch (currentTable)
                {
                    case 3:
                        id = selectedDataRow["Programul"].ToString();
                        break;
                    case 4:
                        id = selectedDataRow["Cod"].ToString();
                        break;
                    case 7:
                        id = selectedDataRow["Nr. Matricol"].ToString();
                        break;
                    case 8:
                        id = selectedDataRow["Cod"].ToString();
                        break;
                    case 10:
                        id = selectedDataRow["Nr. Matricol"].ToString();
                        break;
                    default:
                        id = currentTable == 1 ? selectedDataRow["ID"].ToString() : selectedDataRow["Ciclu"].ToString();
                        break;
                }
                // Delete the row from the database
                DeleteRowFromDatabase(id);

                // Delete the row from the DataTable
                dataTable.Rows.Remove(selectedDataRow);
            }
            else
            {
                MessageBox.Show("You can only view , not delete!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteRowFromDatabase(string id)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    switch (currentTable)
                    {
                        case 3:
                            query = "DELETE FROM programedestudii WHERE Programul = @id;";
                            break;
                        case 4:
                            query = "DELETE FROM discipline WHERE Cod = @id;";
                            break;
                        case 7:
                            query = "DELETE FROM student WHERE `Nr. Matricol` = @id;";
                            break;
                        case 8:
                            query = "DELETE FROM grupe WHERE Cod = @id;";
                            break;
                        case 10:
                            query = "UPDATE student SET Grupa = NULL WHERE `Nr. Matricol` = @id;";
                            break;
                        default:
                            query = currentTable == 1 ? "DELETE FROM conturi WHERE ID = @id;" : "DELETE FROM ciclurideinvatamant  WHERE Ciclu = @id;";
                            break;
                    }
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
            hidestudii();
        }

        private void programeDeStudiiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Text = "Insert";
            LoadDataFromDatabase(3);
            currentTable = 3;
            button2.Show();
            comboBox4.Show();
            button1.Show();
            comboBox1.Show();
            textBox1.Show();
            comboBox2.Show();
            loadComboBox();
            comboBox3.Hide();
            comboBox4.Items.Clear();
            for (int i = 0; i <= 9; i++)
            {
                comboBox4.Items.Add(i);
            }
        }

        private void loadComboBox()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connected to the database.");

                    string query = "SELECT Ciclu FROM ciclurideinvatamant;";

                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ciclu = reader.GetString("Ciclu");
                            comboBox1.Items.Add(ciclu);
                        }
                    }

                    Console.WriteLine("Disconnected from the database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            int[] numbers = { 3, 4 };

            foreach (int number in numbers)
            {
                comboBox2.Items.Add(number);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentTable == 3)
            {
                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO programedestudii (Ciclu, Programul,Durata,Cod) VALUES (@ciclu, @programul,@durata,@cod);";
                        using (var command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ciclu", comboBox1.SelectedItem.ToString());
                            command.Parameters.AddWithValue("@programul", textBox1.Text);
                            command.Parameters.AddWithValue("@durata", comboBox2.SelectedIndex + 2);
                            command.Parameters.AddWithValue("@cod", comboBox4.SelectedItem);
                            command.ExecuteNonQuery();
                        }

                        Console.WriteLine("New row inserted into the database.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                LoadDataFromDatabase(3);

            }
            else if (currentTable == 4)
            {
                if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox3.SelectedItem != null && comboBox4.SelectedItem != null && button1.Text == "Check")
                {
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    comboBox3.Enabled = false;
                    comboBox4.Enabled = false;
                    textBox2.Show();
                    textBox3.Show();
                    textBox4.Show();
                    button3.Show();
                    button1.Text = "Uncheck";
                    textboxload();
                }
                else if (button1.Text == "Uncheck")
                {
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                    comboBox3.Enabled = true;
                    comboBox4.Enabled = true;
                    textBox2.Hide();
                    textBox3.Hide();
                    textBox4.Hide();
                    button3.Hide();
                    button1.Text = "Check";
                }
                else
                {
                    MessageBox.Show("Select every field", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (currentTable == 5)
            {
                if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox3.SelectedItem != null && comboBox4.SelectedItem != null)
                {
                    String Ciclu = comboBox1.SelectedItem.ToString();
                    String Programul = comboBox3.SelectedItem.ToString();
                    int An = int.Parse(comboBox2.SelectedItem.ToString());
                    String Semestru = comboBox4.SelectedItem.ToString();
                    String cod = String.Concat(String.Concat(Ciclu.Substring(0, 1)[0], Programul.Substring(0, 2)) + An, Semestru);
                    query = $"SELECT * FROM discipline WHERE Cod like '{cod}%';";
                    LoadDataFromDatabase(5);
                    dataGridView1.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Select Every Field", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (currentTable == 8)
            {
                String cod = 4.ToString();
                String Ciclu = comboBox1.SelectedItem.ToString();
                String Programul = comboBox3.SelectedItem.ToString();
                cod = cod + String.Concat(Ciclu.Substring(0, 1)[0], 'F') + ProgramCheck(Programul) + comboBox2.SelectedIndex.ToString();
                InsertRowIntoTableGrupe(cod);
                LoadDataFromDatabase(currentTable);
            }
            else if (currentTable == 9)
            {
                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        query = "UPDATE student SET Grupa = @grupa WHERE `Nr. Matricol` = @nr;";

                        using (var command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@grupa", comboBox3.SelectedItem);
                            command.Parameters.AddWithValue("@nr", comboBox1.SelectedItem);
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
            else if (currentTable == 11)
            {
                double rowCount = (double)(dataGridView1.RowCount - 1) / Convert.ToInt32(comboBox3.SelectedItem);
                rowCount = (int)Math.Ceiling(rowCount);
                int nr = 1;
                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            if (i % rowCount == 0 && i != 0)
                                nr++;
                            DataGridViewRow row = dataGridView1.Rows[i];
                            string cellValue = row.Cells["Nr. Matricol"].Value?.ToString();
                            query = "UPDATE student SET Grupa = @grupa WHERE `Nr. Matricol` = @nr ;";
                            using (var command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@grupa", comboBox1.SelectedItem + nr.ToString());
                                command.Parameters.AddWithValue("@nr", cellValue);
                                command.ExecuteNonQuery();
                            }
                            Console.WriteLine("Database updated successfully.");

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                comboBox1_SelectedIndexChanged(null, null);
            }
        }
        private void InsertRowIntoTableGrupe(String Cod)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    query = "INSERT INTO grupe (Cod) VALUES (@cod);";
                    using (var command = new MySqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@cod", Cod);
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
        private int ProgramCheck(string Programul)
        {
            int nr = 0;
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Cod FROM programedestudii WHERE Programul = @programul;";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@programul", Programul);
                        var codValue = command.ExecuteScalar();

                        if (codValue != null)
                        {
                            nr = Convert.ToInt32(codValue);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return nr;
        }

        private void vizualizareToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DisciplineCheck();
            currentTable = 5;
            SetControlsVisibility(true, true, true, true, false);
            LoadComboDiscipline();
            button1.Text = "Check";
            dataGridView1.Enabled = false;
        }

        private void disciplineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisciplineCheck();
            currentTable = 4;
            SetControlsVisibility(true, true, true, true, true);
            LoadDataFromDatabase(currentTable);
            LoadComboDiscipline();
            button1.Text = "Check";
        }

        private void LoadComboDiscipline()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox4.Items.AddRange(new string[] { "A", "B" });
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connected to the database.");

                    string query = "SELECT DISTINCT Ciclu FROM programedestudii;";

                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ciclu = reader.GetString("Ciclu");
                            comboBox1.Items.Add(ciclu);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentTable == 4 || currentTable == 5 || currentTable == 8)
            {
                comboBox3.Items.Clear();
                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        Console.WriteLine("Connected to the database.");

                        string query = "SELECT Programul FROM programedestudii WHERE Ciclu = @ciclu;";

                        using (var command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ciclu", comboBox1.SelectedItem.ToString());
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string program = reader.GetString("Programul");
                                    comboBox3.Items.Add(program);
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
            else if (currentTable == 10 || currentTable == 11)
            {
                dataGridView1.Enabled = true;
                dataTable = new DataTable();
                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        Console.WriteLine("Connected to the database.");

                        query = $"SELECT * FROM student WHERE Grupa LIKE '{comboBox1.SelectedItem.ToString()}%' ORDER BY `Media Inscriere` DESC;";

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
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentTable != 8 && currentTable != 10)
            {

                comboBox2.Items.Clear();
                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        Console.WriteLine("Connected to the database.");

                        string query = "SELECT Durata FROM programedestudii WHERE Ciclu = @ciclu AND Programul = @program;";

                        using (var command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ciclu", comboBox1.SelectedItem.ToString());
                            command.Parameters.AddWithValue("@program", comboBox3.SelectedItem.ToString());
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int an = reader.GetInt16("Durata");
                                    for (int i = 1; i <= an; i++)
                                        comboBox2.Items.Add(i);
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
            else if (currentTable == 10)
            {
                dataGridView1.Enabled = true;
                dataTable = new DataTable();
                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        Console.WriteLine("Connected to the database.");

                        query = "SELECT * FROM student WHERE Grupa = @grupa;";

                        using (var command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@grupa", comboBox1.SelectedItem.ToString() + comboBox3.SelectedItem.ToString());
                            using (var reader = command.ExecuteReader())
                            {
                                dataTable.Load(reader);
                            }
                            dataGridView1.DataSource = dataTable;
                            Console.WriteLine("Disconnected from the database.");
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            }
            else
            {
                for (int i = 0; i <= 9; i++)
                {
                    comboBox2.Items.Add(i);
                }
            }
        }

        private void textboxload()
        {
            textBox2.Text = "Nume";
            textBox2.ForeColor = System.Drawing.SystemColors.GrayText;
            textBox3.Text = "Acronim";
            textBox3.ForeColor = System.Drawing.SystemColors.GrayText;
            textBox4.Text = "Credite";
            textBox4.ForeColor = System.Drawing.SystemColors.GrayText;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.ForeColor = System.Drawing.SystemColors.WindowText;
            textBox.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text) && textBox2.Text != "Nume" && textBox3.Text != "Acronim" && textBox4.Text != "Credite")
            {

                String Ciclu = comboBox1.SelectedItem.ToString();
                String Programul = comboBox3.SelectedItem.ToString();
                int An = int.Parse(comboBox2.SelectedItem.ToString());
                String Semestru = comboBox4.SelectedItem.ToString();
                String cod = String.Concat(String.Concat(Ciclu.Substring(0, 1)[0], Programul.Substring(0, 2)) + An, Semestru);
                cod = cod + codCheck(cod);  // check the last number for the last disciplina
                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO discipline (Cod, Nume, Acronim, Credite) VALUES (@cod, @nume, @acronim, @credite);";
                        using (var command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@cod", cod);
                            command.Parameters.AddWithValue("@nume", textBox2.Text);
                            command.Parameters.AddWithValue("@acronim", textBox3.Text);
                            command.Parameters.AddWithValue("@credite", textBox4.Text);
                            command.ExecuteNonQuery();
                        }

                        Console.WriteLine("New row inserted into the database.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                LoadDataFromDatabase(4);
            }
            else
            {
                MessageBox.Show("Complete the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int codCheck(string prefix)
        {
            int cod = 0;

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT MAX(CAST(SUBSTRING(Cod, CHAR_LENGTH(Cod)) AS UNSIGNED)) FROM discipline WHERE Cod LIKE '{prefix}%';";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                            cod = Convert.ToInt32(result);
                    }

                    Console.WriteLine("Max ID retrieved from the database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return cod + 1;
        }

        private void SetControlsVisibility(bool textBox1Visible, bool comboBox3Visible, bool comboBox1Visible, bool comboBox4Visible, bool button2Visible)
        {
            textBox1.Visible = textBox1Visible;
            comboBox3.Visible = comboBox3Visible;
            comboBox1.Visible = comboBox1Visible;
            comboBox4.Visible = comboBox4Visible;
            comboBox2.Visible = true;
            button1.Visible = true;
            button2.Visible = button2Visible;
            dataGridView1.Enabled = false;
        }
        private void DisciplineCheck()
        {
            if (textBox2.Visible == true)
            {
                textBox2.Hide();
                textBox3.Hide();
                textBox4.Hide();
                button3.Hide();
            }
            if (comboBox1.Enabled == false)
            {
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
            }

        }

        private void studentiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlsVisibility(false, false, false, false, false);
            comboBox2.Hide();
            button1.Hide();
            currentTable = 7;
            LoadDataFromDatabase(7);
            button2.Show();
        }

        private void creareGrupaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlsVisibility(false, true, true, false, true);
            button1.Text = "Creare";
            currentTable = 8;
            LoadDataFromDatabase(8);
            comboBox3.Show();
            button1.Show();
            LoadComboDiscipline();
        }

        private void asociereGrupaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDataFromDatabase(7);
            currentTable = 9;
            hidestudii();
            button2.Hide();
            comboBox1.Show();
            comboBox3.Show();
            button1.Show();
            button1.Text = "Asociaza";
            LoadStudenti();
            LoadGrupe();
        }
        private void LoadGrupe()
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connected to the database.");

                    string query = "SELECT Cod FROM grupe";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            comboBox3.Items.Clear();
                            while (reader.Read())
                            {
                                string cod = reader.GetString("Cod");
                                comboBox3.Items.Add(cod);
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

        private void LoadStudenti()
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connected to the database.");

                    string query = "SELECT `Nr. Matricol` FROM student WHERE Grupa IS NULL ORDER BY `Media Inscriere` DESC;";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            comboBox1.Items.Clear();
                            while (reader.Read())
                            {
                                string nr = reader.GetString("Nr. Matricol");
                                comboBox1.Items.Add(nr);
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

        private void AdaugaGrupa(ComboBox comboBox)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connected to the database.");

                    string query = "SELECT Cod FROM grupe";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            comboBox.Items.Clear();
                            while (reader.Read())
                            {
                                string cod = reader.GetString("Cod");
                                comboBox.Items.Add(cod);
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

        private void vizualizareGrupaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = 10;
            hidestudii();
            comboBox1.Show();
            comboBox3.Show();
            button2.Show();
            AdaugaGrupa(comboBox1);
            comboBox3.Items.Clear();
            for (int i = 1; i <= 4; i++)
            {
                comboBox3.Items.Add(i);
            }
        }

        private void divizareAutomataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDataFromDatabase(7);
            currentTable = 11;
            hidestudii();
            comboBox1.Show();
            comboBox3.Show();
            button1.Show();
            button1.Text = "Imparte";
            AdaugaGrupa(comboBox1);
            comboBox3.Items.Clear();
            for (int i = 1; i <= 4; i++)
            {
                comboBox3.Items.Add(i);
            }
        }
    }
}