using MySql.Data.MySqlClient;
using System.Data;

namespace UniCatalog
{
    public partial class Form2 : Form
    {
        public int Number { get; set; }
        private DataTable dataTable;
        private readonly string connectionString = "Server=localhost;Database=unicatalog;Uid=root;";
        private string? query;
        private int currentTable;
        readonly List<string> subjects = new();
        public string? disciplina, matricol;
        public DataTable? grades;

        public Form2(string receivedData)
        {
            InitializeComponent();
            dataTable = new DataTable();
            disciplina = receivedData;
        }

        private void UtilizatoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlsVisibility(false, false, false, false, false, true, false, false, false, false, false, false, false);
            LoadDataFromDatabase(1);
            currentTable = 1;
            dataGridView1.Columns["ID"].Visible = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            SetControlsVisibility(false, false, false, false, false, false, false, false, false, false, false, false, false);
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
            if ((Number % 100) / 10 >= 1)
            {
                cicluDeInvatamantToolStripMenuItem.Visible = true;
                programeDeStudiiToolStripMenuItem.Visible = true;
                planuriDeInvatamantToolStripMenuItem.Visible = true;
                studentiToolStripMenuItem.Visible = true;
                grupeToolStripMenuItem.Visible = true;
                catalogToolStripMenuItem.Visible = true;
            }
            if (Number / 100 >= 1)
            {
                noteToolStripMenuItem.Visible = true;

            }
        }

        private void LoadDataFromDatabase(int operatie)
        {
            dataGridView1.Enabled = true;
            dataTable = new DataTable();
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Connected to the database.");

                query = operatie switch
                {
                    1 => "SELECT * FROM conturi;",
                    2 => "SELECT * FROM ciclurideinvatamant;",
                    3 => "SELECT * FROM programedestudii;",
                    4 => "SELECT * FROM discipline;",
                    7 => "SELECT * FROM student;",
                    8 => "SELECT * FROM grupe",
                    13 => "SELECT * FROM catalog",
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
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
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
                        UpdateRowInDatabase(id ?? string.Empty, username ?? string.Empty, password ?? string.Empty, userType, materie);
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
                        UpdateRowInTableStudent(nrmat ?? string.Empty, nume ?? string.Empty, prenume ?? string.Empty, initialaTata ?? string.Empty, cnp, dataInscrierii, cicluInvatamant ?? string.Empty, medieInscriere);
                    }
                    else
                    {
                        // Get the modified values from the DataGridView
                        string ciclu = row["Ciclu"].ToString();
                        string acronim = row["Acronim"].ToString();
                        UpdateRowInDatabase(ciclu ?? string.Empty, acronim ?? string.Empty, acronim ?? string.Empty, 0, null);
                    }
                }
            }
            else
            {
                MessageBox.Show("You can only remove and insert in this table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateRowInDatabase(string id, string username, string password, int userType, string? materie)
        {

            try
            {
                using var connection = new MySqlConnection(connectionString);
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
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }

        private void UpdateRowInTableStudent(string nrmat, string nume, string prenume, string initialaTata, long cnp,
            DateTime dataInscrierii, string cicluInvatamant, double medieInscriere)
        {

            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();

                query = "UPDATE student SET `Nr. Matricol` = @nrmat, Nume = @nume, Prenume = @prenume,`Initiala Tatalui` = @initialaTata, CNP = @cnp, `Data Inscrierii` =  @dataInscrierii,`Ciclu de invatamant` = @cicluInvatamant, `Media Inscriere`=@medieInscriere WHERE `Nr. Matricol` = @nrmat OR  CNP = @cnp";

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
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Database updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }

        private void DataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
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
                using var connection = new MySqlConnection(connectionString);
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
                using var connection = new MySqlConnection(connectionString);
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
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void InsertRowIntoTableStudent(DataRow row)
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();
                query = "INSERT INTO student (`Nr. Matricol`, Nume, Prenume, `Initiala Tatalui`, CNP, `Data Inscrierii`, `Ciclu de invatamant`, `Media Inscriere`,Grupa) VALUES (@nrmat, @nume, @prenume, @initialaTata, @cnp, @dataInscrierii, @cicluInvatamant, @medieInscriere,null);";
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
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && currentTable != 5)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                DataRowView selectedRowView = (DataRowView)selectedRow.DataBoundItem;
                DataRow selectedDataRow = selectedRowView.Row;
                string id = currentTable switch
                {
                    3 => selectedDataRow["Programul"].ToString(),
                    4 => selectedDataRow["Cod"].ToString(),
                    7 => selectedDataRow["Nr. Matricol"].ToString(),
                    8 => selectedDataRow["Cod"].ToString(),
                    10 => selectedDataRow["Nr. Matricol"].ToString(),
                    _ => currentTable == 1 ? selectedDataRow["ID"].ToString() : selectedDataRow["Ciclu"].ToString(),
                };

                // Delete the row from the database
                DeleteRowFromDatabase(id ?? string.Empty);

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
                using var connection = new MySqlConnection(connectionString);
                connection.Open();

                query = currentTable switch
                {
                    3 => "DELETE FROM programedestudii WHERE Programul = @id;",
                    4 => "DELETE FROM discipline WHERE Cod = @id;",
                    7 => "DELETE FROM student WHERE `Nr. Matricol` = @id;",
                    8 => "DELETE FROM grupe WHERE Cod = @id;",
                    10 => "UPDATE student SET Grupa = NULL WHERE `Nr. Matricol` = @id;",
                    _ => currentTable == 1 ? "DELETE FROM conturi WHERE ID = @id;" : "DELETE FROM ciclurideinvatamant WHERE Ciclu = @id;",
                };
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Row deleted from the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("If you want to Edit a row you can double click on the field and edit it. If you want to add a row, click on the blank space at the bottom and enter the values (a new one will be created which you can edit). If you want to remove a row, select the row and press Remove.", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CicluDeInvatamantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlsVisibility(false, false, false, false, false, true, false, false, false, false, false, false, false);
            currentTable = 2;
            LoadDataFromDatabase(2);
        }

        private void ProgrameDeStudiiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlsVisibility(true, false, false, false, true, true, false, false, false, true, true, false, true);
            button1.Text = "Insert";
            LoadDataFromDatabase(3);
            currentTable = 3;
            LoadComboBox();
            comboBox4.Items.Clear();
            for (int i = 0; i <= 9; i++)
            {
                comboBox4.Items.Add(i);
            }
        }

        private void LoadComboBox()
        {
            comboBox1.Items.Clear();
            try
            {
                using var connection = new MySqlConnection(connectionString);
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
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            int[] numbers = { 3, 4 };
            comboBox2.Items.Clear();
            foreach (int number in numbers)
            {
                comboBox2.Items.Add(number);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (currentTable == 3)
            {
                try
                {
                    using var connection = new MySqlConnection(connectionString);
                    connection.Open();
                    string query = "INSERT INTO programedestudii (Ciclu, Programul,Durata,Cod) VALUES (@ciclu, @programul,@durata,@cod);";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ciclu", comboBox1.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@programul", textBox1.Text);
                        command.Parameters.AddWithValue("@durata", int.Parse(comboBox2.SelectedItem.ToString()));
                        command.Parameters.AddWithValue("@cod", comboBox4.SelectedItem);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("New row inserted into the database.");
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
                    SetControlsVisibility(false, true, true, true, true, true, true, false, false, true, true, true, true);
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    comboBox3.Enabled = false;
                    comboBox4.Enabled = false;
                    button1.Text = "Uncheck";
                    Textboxload();
                }
                else if (button1.Text == "Uncheck")
                {
                    SetControlsVisibility(false, false, false, false, true, true, false, false, false, true, true, true, true);
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
                    String Programul = (comboBox3.SelectedIndex + 1).ToString();
                    int An = int.Parse(comboBox2.SelectedItem.ToString());
                    String Semestru = comboBox4.SelectedItem.ToString();
                    string cod = string.Concat(string.Concat(Ciclu[..1][0], Programul) + An, Semestru);
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
                String Ciclu = comboBox1.SelectedItem?.ToString();
                String Programul = comboBox3.SelectedItem.ToString();
                cod = cod + string.Concat(Ciclu[..1][0], 'F') + ProgramCheck(Programul) + comboBox2.SelectedIndex.ToString();
                InsertRowIntoTableGrupe(cod);
                LoadDataFromDatabase(currentTable);
            }
            else if (currentTable == 9)
            {
                try
                {
                    using var connection = new MySqlConnection(connectionString);
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
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                LoadDataFromDatabase(7);
            }
            else if (currentTable == 11)
            {
                if (comboBox3.SelectedIndex != -1)
                {
                    double rowCount = (double)(dataGridView1.RowCount - 1) / Convert.ToInt32(comboBox3.SelectedItem);
                    rowCount = (int)Math.Ceiling(rowCount);
                    int nr = 1;
                    try
                    {
                        using var connection = new MySqlConnection(connectionString);
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
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                    ComboBox1_SelectedIndexChanged(null, null);


                    AsociereMaterie();
                }
                else
                {
                    MessageBox.Show("Selecteaza in cate vrei sa imparti ");
                }

            }
            else if (currentTable == 13)
            {
                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Trebuie sa selectezi grupa si semestrul");
                }
                else
                {
                    string grupa = comboBox1.SelectedItem.ToString();
                    List<string> matricol = new();
                    List<string> nume = new();
                    List<string> prenume = new();
                    try
                    {
                        using var connection = new MySqlConnection(connectionString);
                        connection.Open();
                        Console.WriteLine("Connected to the database.");

                        query = $"SELECT `Nr. Matricol`,Nume,Prenume  FROM student WHERE Grupa like '{grupa}%';";

                        using (var command = new MySqlCommand(query, connection))
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string value = reader.GetString(0);
                                matricol.Add(value);
                                value = reader.GetString(1);
                                nume.Add(value);
                                value = reader.GetString(2);
                                prenume.Add(value);
                            }

                        }
                        Console.WriteLine("Disconnected from the database.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                    List<float> medii1 = new();
                    List<float> medii2 = new();
                    if (grupa != null)
                    {
                        medii1 = MediePeSemenstru("A", grupa, matricol.Count);
                        medii2 = MediePeSemenstru("B", grupa, matricol.Count);

                    }
                    List<float> mediaF = new();
                    for (int i = 0; i < medii1.Count; i++)
                    {
                        mediaF.Add((medii1[i] + medii2[i]) / 2);
                    }
                    List<string> Restantieri = this.Restantieri(matricol);



                    try
                    {
                        using var connection = new MySqlConnection(connectionString);
                        connection.Open();

                        query = "INSERT INTO catalog (Matricol, Nume, Prenume, MS1,MS2,MF,Promovat) VALUES (@matricol, @nume, @prenume, @ms1,@ms2,@mf,@promovat);";
                        for (int i = 0; i < mediaF.Count; i++)
                        {
                            try
                            {
                                using var command = new MySqlCommand(query, connection);
                                command.Parameters.AddWithValue("@matricol", matricol[i]);
                                command.Parameters.AddWithValue("@nume", nume[i]);
                                command.Parameters.AddWithValue("@prenume", prenume[i]);
                                command.Parameters.AddWithValue("@ms1", medii1[i]);
                                command.Parameters.AddWithValue("@ms2", medii2[i]);
                                command.Parameters.AddWithValue("@mf", mediaF[i]);
                                if (Restantieri.Contains(matricol[i]))
                                {
                                    command.Parameters.AddWithValue("@promovat", "Restantier");
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@promovat", "Promovat");
                                }
                                command.ExecuteNonQuery();

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                            }

                        }

                        Console.WriteLine("New row inserted into the database.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                    LoadDataFromDatabase(13);

                }

            }
        }

        List<string> Restantieri(List<String> Matricol)
        {
            List<string> Restantieri = new();
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Connected to the database.");
                foreach (String mat in Matricol)
                {
                    query = $"SELECT DISTINCT Matricol FROM note WHERE Nota<5 AND Matricol= '{mat}';";

                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string value = reader.GetString(0);
                            Restantieri.Add(value);
                        }

                    }
                    Console.WriteLine("Disconnected from the database.");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return Restantieri;
        }


        List<float> MediePeSemenstru(string sem, string grupa, int n)
        {
            List<string> acronim = new();
            List<int> credite = new();
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Connected to the database.");

                query = $"SELECT Acronim ,Credite  FROM discipline WHERE Cod like '{string.Concat(grupa[1].ToString(), grupa.AsSpan(grupa.Length - 2))}{sem}%';";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string value = reader.GetString(0);
                        acronim.Add(value);
                        value = reader.GetString(1);
                        credite.Add(int.Parse(value));
                    }

                }
                Console.WriteLine("Disconnected from the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            List<float> note = new();
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Connected to the database.");

                foreach (string acron in acronim)
                {
                    query = $"SELECT Nota FROM note WHERE Disciplina ='{acron}';";

                    using var command = new MySqlCommand(query, connection);
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        float value = reader.GetFloat(0);
                        note.Add(value);
                    }

                }

                Console.WriteLine("Disconnected from the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            List<float> medii = new();
            int nrMedii = n;
            int i = 0;
            float sumacredite = 0;
            for (int j = 0; j < credite.Count; j++)
                sumacredite += credite[j];

            while (i < nrMedii)
            {
                float medie = 0;
                int noteParcurse = 0;
                int index = i;
                while (noteParcurse < acronim.Count)
                {
                    medie += note[index] * credite[noteParcurse];
                    index += nrMedii;
                    noteParcurse++;
                }
                medii.Add(medie / sumacredite);
                i++;
            }
            return medii;
        }

        private void AsociereMaterie()
        {
            string cod = comboBox1.SelectedItem.ToString();
            char secondLetter = cod[1];  // Extract the second letter (index 1)
            string lastTwoNumbers = cod[^2..];  // Extract the last two numbers
            string combinedString = secondLetter.ToString() + lastTwoNumbers;
            List<string> values = new();
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Connected to the database.");

                query = $"SELECT Acronim FROM discipline WHERE Cod like '{combinedString}%';";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string value = reader.GetString(0);
                        values.Add(value);

                    }

                }
                Console.WriteLine("Disconnected from the database.");
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            List<int> valuesList = new();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (!row.IsNewRow && row.Cells["Nr. Matricol"].Value != null)
                {
                    if (int.TryParse(row.Cells["Nr. Matricol"].Value.ToString(), out int value))
                    {
                        valuesList.Add(value);
                    }
                }
            }
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Connected to the database.");

                foreach (int matricol in valuesList)
                {
                    foreach (string disciplina in values)
                    {
                        try
                        {   
                            query = $"INSERT INTO note (Matricol, Disciplina, Nota) VALUES ({matricol}, '{disciplina}', 0);";
                            using var command = new MySqlCommand(query, connection);
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }

                    }
                }

                Console.WriteLine("Entries added to the database.");
                Console.WriteLine("Disconnected from the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }
        private void InsertRowIntoTableGrupe(String Cod)
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();
                query = "INSERT INTO grupe (Cod) VALUES (@cod);";
                using (var command = new MySqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@cod", Cod);
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("New row inserted into the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        private int ProgramCheck(string Programul)
        {
            if (Programul != null)
            {
                int nr = 0;
                try
                {
                    using var connection = new MySqlConnection(connectionString);
                    connection.Open();
                    string query = "SELECT Cod FROM programedestudii WHERE Programul = @programul;";
                    using var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@programul", Programul);
                    var codValue = command.ExecuteScalar();

                    if (codValue != null)
                    {
                        nr = Convert.ToInt32(codValue);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                return nr;

            }
            else return 0;

        }

        private void VizualizareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = 5;
            SetControlsVisibility(false, false, false, false, true, false, false, false, false, true, true, true, true);
            LoadComboDiscipline();
            button1.Text = "Check";
            dataGridView1.Enabled = false;
            comboBox4.Items.Clear();
            comboBox4.Items.AddRange(new string[] { "A", "B" });

        }

        private void DisciplineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = 4;
            SetControlsVisibility(false, false, false, false, true, true, false, false, false, true, true, true, true);
            LoadDataFromDatabase(currentTable);
            LoadComboDiscipline();
            button1.Text = "Check";
            comboBox4.Items.Clear();
            comboBox4.Items.AddRange(new string[] { "A", "B" });
        }

        private void LoadComboDiscipline()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();

            try
            {
                using var connection = new MySqlConnection(connectionString);
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
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void ComboBox1_SelectedIndexChanged(object? sender, EventArgs? e)
        {
            if (currentTable == 4 || currentTable == 5 || currentTable == 8)
            {
                comboBox3.Items.Clear();
                try
                {
                    using var connection = new MySqlConnection(connectionString);
                    connection.Open();
                    Console.WriteLine("Connected to the database.");

                    string query = "SELECT Programul FROM programedestudii WHERE Ciclu = @ciclu;";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ciclu", comboBox1.SelectedItem.ToString());
                        using var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            string program = reader.GetString("Programul");
                            comboBox3.Items.Add(program);
                        }
                    }

                    Console.WriteLine("Disconnected from the database.");
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
                    using var connection = new MySqlConnection(connectionString);
                    connection.Open();
                    Console.WriteLine("Connected to the database.");

                    query = $"SELECT * FROM student WHERE Grupa LIKE '{comboBox1.SelectedItem}%' ORDER BY `Media Inscriere` DESC;";

                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                    dataGridView1.DataSource = dataTable;
                    Console.WriteLine("Disconnected from the database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else if (currentTable == 13)
            {
                try
                {
                    using var connection = new MySqlConnection(connectionString);
                    connection.Open();
                    query = "DELETE FROM catalog";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("Row deleted from the database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                LoadDataFromDatabase(13);

            }
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentTable != 8 && currentTable != 10)
            {

                comboBox2.Items.Clear();
                try
                {
                    using var connection = new MySqlConnection(connectionString);
                    connection.Open();
                    Console.WriteLine("Connected to the database.");

                    string query = "SELECT Durata FROM programedestudii WHERE Ciclu = @ciclu AND Programul = @program;";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ciclu", comboBox1.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@program", comboBox3.SelectedItem.ToString());
                        using var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int an = reader.GetInt16("Durata");
                            for (int i = 1; i <= an; i++)
                                comboBox2.Items.Add(i);
                        }
                    }

                    Console.WriteLine("Disconnected from the database.");
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
                    using var connection = new MySqlConnection(connectionString);
                    connection.Open();
                    Console.WriteLine("Connected to the database.");

                    query = "SELECT * FROM student WHERE Grupa = @grupa;";

                    using var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@grupa", comboBox1.SelectedItem.ToString() + comboBox3.SelectedItem.ToString());
                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                    dataGridView1.DataSource = dataTable;
                    Console.WriteLine("Disconnected from the database.");
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

        private void Textboxload()
        {
            textBox2.Text = "Nume";
            textBox2.ForeColor = System.Drawing.SystemColors.GrayText;
            textBox3.Text = "Acronim";
            textBox3.ForeColor = System.Drawing.SystemColors.GrayText;
            textBox4.Text = "Credite";
            textBox4.ForeColor = System.Drawing.SystemColors.GrayText;
        }

        private void TextBox2_Click(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.ForeColor = System.Drawing.SystemColors.WindowText;
            textBox.Clear();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (currentTable != 13)
            {
                if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text) && textBox2.Text != "Nume" && textBox3.Text != "Acronim" && textBox4.Text != "Credite")
                {

                    String Ciclu = comboBox1.SelectedItem.ToString();
                    String Programul = (comboBox3.SelectedIndex + 1).ToString();
                    int An = int.Parse(comboBox2.SelectedItem.ToString());
                    String Semestru = comboBox4.SelectedItem.ToString();
                    string cod = string.Concat(string.Concat(Ciclu[..1][0], Programul) + An, Semestru);
                    cod += CodCheck(cod);  // check the last number for the last disciplina
                    try
                    {
                        using var connection = new MySqlConnection(connectionString);
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
            else
            {
                ExportToExcel_Click(this, EventArgs.Empty);
            }

        }
        private void ExportToExcel_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectAll();
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject copyData = dataGridView1.GetClipboardContent();

            if (copyData != null)
            {
                Clipboard.SetDataObject(copyData);
                Microsoft.Office.Interop.Excel.Application xlApp = new()
                {
                    Visible = true
                };
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Add();
                Microsoft.Office.Interop.Excel.Worksheet xlWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Worksheets[1];
                Microsoft.Office.Interop.Excel.Range xlRange = (Microsoft.Office.Interop.Excel.Range)xlWorksheet.Cells[1, 1];
                xlRange.Select();
                xlWorksheet.PasteSpecial(xlRange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
            }
        }

        private int CodCheck(string prefix)
        {
            int cod = 0;

            try
            {
                using var connection = new MySqlConnection(connectionString);
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
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return cod + 1;
        }

        private void SetControlsVisibility(bool textBox1Visible, bool textBox2Visible, bool textBox3Visible, bool textBox4Visible, bool button1Visible, bool button2Visible, bool button3Visible, bool button4Visible, bool button5Visible, bool comboBox1Visible, bool comboBox2Visible, bool comboBox3Visible, bool comboBox4Visible)
        {
            textBox1.Visible = textBox1Visible;
            textBox2.Visible = textBox2Visible;
            textBox3.Visible = textBox3Visible;
            textBox4.Visible = textBox4Visible;
            button1.Visible = button1Visible;
            button2.Visible = button2Visible;
            button3.Visible = button3Visible;
            button4.Visible = button4Visible;
            button5.Visible = button5Visible;
            comboBox1.Visible = comboBox1Visible;
            comboBox2.Visible = comboBox2Visible;
            comboBox3.Visible = comboBox3Visible;
            comboBox4.Visible = comboBox4Visible;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            comboBox4.Enabled = true;
        }

        private void StudentiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = 7;
            LoadDataFromDatabase(7);
            SetControlsVisibility(false, false, false, false, false, true, false, false, false, false, false, false, false);
        }

        private void CreareGrupaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlsVisibility(false, false, false, false, true, true, false, false, false, true, true, true, false);
            button1.Text = "Creare";
            currentTable = 8;
            LoadDataFromDatabase(8);
            LoadComboDiscipline();
        }

        private void AsociereGrupaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDataFromDatabase(7);
            currentTable = 9;
            SetControlsVisibility(false, false, false, false, true, false, false, false, false, true, false, true, false);
            button1.Text = "Asociaza";
            LoadStudenti();
            LoadGrupe();
        }
        private void LoadGrupe()
        {
            if (currentTable != 13)
                comboBox3.Items.Clear();
            else
                comboBox1.Items.Clear();
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Connected to the database.");

                string query = "SELECT Cod FROM grupe";

                using (var command = new MySqlCommand(query, connection))
                {
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string cod = reader.GetString("Cod");
                        if (currentTable != 13)
                            comboBox3.Items.Add(cod);
                        else
                            comboBox1.Items.Add(cod);
                    }
                }

                Console.WriteLine("Disconnected from the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }
        private void LoadStudenti()
        {
            comboBox1.Items.Clear();
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Connected to the database.");

                string query = "SELECT `Nr. Matricol` FROM student WHERE Grupa IS NULL ORDER BY `Media Inscriere` DESC ;";

                using (var command = new MySqlCommand(query, connection))
                {
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string nr = reader.GetString("Nr. Matricol");
                        comboBox1.Items.Add(nr);
                    }
                }

                Console.WriteLine("Disconnected from the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void Adaugagrupa()
        {
            comboBox1.Items.Clear();
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Connected to the database.");

                string query = "SELECT Cod FROM grupe";

                using (var command = new MySqlCommand(query, connection))
                {
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string cod = reader.GetString("Cod");
                        comboBox1.Items.Add(cod);
                    }
                }
                Console.WriteLine("Disconnected from the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        private void VizualizareGrupaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = 10;
            SetControlsVisibility(false, false, false, false, false, true, false, false, false, true, false, true, false);
            Adaugagrupa();
            comboBox3.Items.Clear();
            for (int i = 1; i <= 5; i++)
            {
                comboBox3.Items.Add(i);
            }
        }

        private void DivizareAutomataToolStripMenuItem_Click(object sender, EventArgs e)
        {

            LoadDataFromDatabase(7);
            currentTable = 11;
            SetControlsVisibility(false, false, false, false, true, false, false, false, false, true, false, true, false);
            button1.Text = "Imparte";
            Adaugagrupa();
            comboBox3.Items.Clear();
            for (int i = 1; i <= 4; i++)
            {
                comboBox3.Items.Add(i);
            }
        }

        //creaza o lista cu acronimele disciplinlor
        private void GetSubjects(string subject)
        {
            subjects.Clear();
            string[] sub = subject.Split(',');
            string ac = "SELECT Cod, Acronim FROM discipline";
            DataTable acronime = GetDataFromDatabase(ac);
            foreach (DataRow row in acronime.Rows)
            {
                int pos = Array.IndexOf(sub, row["Cod"]);
                if (pos > -1)
                {
                    subjects.Add(row["Acronim"].ToString());
                }

            }
        }
        //afiseaza data grid-ul
        private void ShowDataGrid(DataTable date)
        {
            dataGridView1.Enabled = true;
            dataGridView1.DataSource = date;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        //preia datele din baza de date
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
        private static string GetGrades(string subject)
        {
            return "SELECT student.`Nr. Matricol`, student.Nume, student.Prenume,note.Disciplina, note.Nota FROM `student` JOIN `note` ON student.`Nr. Matricol`=note.Matricol WHERE note.Disciplina='" + subject + "';";
        }
        private void NoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = 12;
            SetControlsVisibility(false, false, false, true, false, false, false, true, true, false, false, false, true);
            subjects.Clear();
            GetSubjects(disciplina);
            foreach (string subject in subjects)
            {
                comboBox4.Items.Add(subject);
            }
            query = "SELECT student.`Nr. Matricol`, student.Nume, student.Prenume,note.Disciplina, note.Nota FROM `student` JOIN `note` ON student.`Nr. Matricol`=note.Matricol WHERE note.Disciplina='" + subjects[0] + "';";
            ShowDataGrid(GetDataFromDatabase(query));

        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentTable == 12)
            {
                grades = GetDataFromDatabase(GetGrades(comboBox4.SelectedItem.ToString()));
                ShowDataGrid(grades);

            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex != -1)
            {
                string nota = textBox4.Text;
                if (string.IsNullOrEmpty(nota))
                {
                    MessageBox.Show("Please enter a grade.");
                    return;
                }
                query = "UPDATE note SET Nota='" + nota + "' WHERE Matricol='" + matricol + "' AND Disciplina='" + comboBox4.SelectedItem.ToString() + "';";
                using (MySqlConnection connection = new(connectionString))
                {
                    using MySqlCommand command = new(query, connection);
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Grade inserted into the database successfully.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
                grades = GetDataFromDatabase(GetGrades(comboBox4.SelectedItem.ToString()));
                ShowDataGrid(grades);
            }
            else
            {
                MessageBox.Show("Selecteaza materia");
            }

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            string nota = "0";
            textBox4.Text = nota;
            string query = "UPDATE note SET Nota='" + nota + "' WHERE Matricol='" + matricol + "' AND Disciplina='" + comboBox4.SelectedItem.ToString() + "';";
            using (MySqlConnection connection = new(connectionString))
            {
                using MySqlCommand command = new(query, connection);
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Grade inserted into the database successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            grades = GetDataFromDatabase(GetGrades(comboBox4.SelectedItem.ToString()));
            ShowDataGrid(grades);
        }

        private void DataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            matricol = String.Empty;
            // Check if the click is on a valid cell within the second column (grades column)
            if (e.ColumnIndex >= 1 && e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                // Get the registration number from the first column (registration number column)
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                matricol = selectedRow.Cells[0].Value.ToString();
            }
        }

        private void CatalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = 13;
            SetControlsVisibility(false, false, false, false, true, false, true, false, false, true, false, false, false);
            LoadDataFromDatabase(13);
            LoadGrupe();
            button3.Text = "Exporta";
            button1.Text = "Genereaza";
        }
    }
}