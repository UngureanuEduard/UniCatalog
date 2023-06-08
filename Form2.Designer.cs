namespace UniCatalog
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            label1 = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            menuStrip1 = new MenuStrip();
            optiuniToolStripMenuItem = new ToolStripMenuItem();
            utilizatoriToolStripMenuItem = new ToolStripMenuItem();
            cicluDeInvatamantToolStripMenuItem = new ToolStripMenuItem();
            programeDeStudiiToolStripMenuItem = new ToolStripMenuItem();
            planuriDeInvatamantToolStripMenuItem = new ToolStripMenuItem();
            disciplineToolStripMenuItem = new ToolStripMenuItem();
            vizualizareToolStripMenuItem = new ToolStripMenuItem();
            studentiToolStripMenuItem = new ToolStripMenuItem();
            grupeToolStripMenuItem = new ToolStripMenuItem();
            asociereGrupaToolStripMenuItem = new ToolStripMenuItem();
            creareGrupaToolStripMenuItem = new ToolStripMenuItem();
            vizualizareGrupaToolStripMenuItem = new ToolStripMenuItem();
            divizareAutomataToolStripMenuItem = new ToolStripMenuItem();
            noteToolStripMenuItem = new ToolStripMenuItem();
            catalogToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            dataGridView1 = new DataGridView();
            button2 = new Button();
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            button1 = new Button();
            comboBox2 = new ComboBox();
            comboBox3 = new ComboBox();
            comboBox4 = new ComboBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(350, 188);
            label1.Name = "label1";
            label1.Size = new Size(0, 20);
            label1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { optiuniToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(6, 3, 0, 3);
            menuStrip1.Size = new Size(800, 30);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // optiuniToolStripMenuItem
            // 
            optiuniToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { utilizatoriToolStripMenuItem, cicluDeInvatamantToolStripMenuItem, programeDeStudiiToolStripMenuItem, planuriDeInvatamantToolStripMenuItem, studentiToolStripMenuItem, grupeToolStripMenuItem, noteToolStripMenuItem, catalogToolStripMenuItem });
            optiuniToolStripMenuItem.Name = "optiuniToolStripMenuItem";
            optiuniToolStripMenuItem.Size = new Size(75, 24);
            optiuniToolStripMenuItem.Text = "Options";
            // 
            // utilizatoriToolStripMenuItem
            // 
            utilizatoriToolStripMenuItem.Image = (Image)resources.GetObject("utilizatoriToolStripMenuItem.Image");
            utilizatoriToolStripMenuItem.Name = "utilizatoriToolStripMenuItem";
            utilizatoriToolStripMenuItem.Size = new Size(236, 26);
            utilizatoriToolStripMenuItem.Text = "Users";
            utilizatoriToolStripMenuItem.Click += UtilizatoriToolStripMenuItem_Click;
            // 
            // cicluDeInvatamantToolStripMenuItem
            // 
            cicluDeInvatamantToolStripMenuItem.Image = (Image)resources.GetObject("cicluDeInvatamantToolStripMenuItem.Image");
            cicluDeInvatamantToolStripMenuItem.Name = "cicluDeInvatamantToolStripMenuItem";
            cicluDeInvatamantToolStripMenuItem.Size = new Size(236, 26);
            cicluDeInvatamantToolStripMenuItem.Text = "Ciclu De Invatamant";
            cicluDeInvatamantToolStripMenuItem.Click += CicluDeInvatamantToolStripMenuItem_Click;
            // 
            // programeDeStudiiToolStripMenuItem
            // 
            programeDeStudiiToolStripMenuItem.Image = (Image)resources.GetObject("programeDeStudiiToolStripMenuItem.Image");
            programeDeStudiiToolStripMenuItem.Name = "programeDeStudiiToolStripMenuItem";
            programeDeStudiiToolStripMenuItem.Size = new Size(236, 26);
            programeDeStudiiToolStripMenuItem.Text = "Programe De Studii";
            programeDeStudiiToolStripMenuItem.Click += ProgrameDeStudiiToolStripMenuItem_Click;
            // 
            // planuriDeInvatamantToolStripMenuItem
            // 
            planuriDeInvatamantToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { disciplineToolStripMenuItem, vizualizareToolStripMenuItem });
            planuriDeInvatamantToolStripMenuItem.Image = (Image)resources.GetObject("planuriDeInvatamantToolStripMenuItem.Image");
            planuriDeInvatamantToolStripMenuItem.Name = "planuriDeInvatamantToolStripMenuItem";
            planuriDeInvatamantToolStripMenuItem.Size = new Size(236, 26);
            planuriDeInvatamantToolStripMenuItem.Text = "Planuri de Invatamant";
            // 
            // disciplineToolStripMenuItem
            // 
            disciplineToolStripMenuItem.Image = (Image)resources.GetObject("disciplineToolStripMenuItem.Image");
            disciplineToolStripMenuItem.Name = "disciplineToolStripMenuItem";
            disciplineToolStripMenuItem.Size = new Size(164, 26);
            disciplineToolStripMenuItem.Text = "Discipline";
            disciplineToolStripMenuItem.Click += DisciplineToolStripMenuItem_Click;
            // 
            // vizualizareToolStripMenuItem
            // 
            vizualizareToolStripMenuItem.Image = (Image)resources.GetObject("vizualizareToolStripMenuItem.Image");
            vizualizareToolStripMenuItem.Name = "vizualizareToolStripMenuItem";
            vizualizareToolStripMenuItem.Size = new Size(164, 26);
            vizualizareToolStripMenuItem.Text = "Vizualizare";
            vizualizareToolStripMenuItem.Click += VizualizareToolStripMenuItem_Click;
            // 
            // studentiToolStripMenuItem
            // 
            studentiToolStripMenuItem.Image = (Image)resources.GetObject("studentiToolStripMenuItem.Image");
            studentiToolStripMenuItem.Name = "studentiToolStripMenuItem";
            studentiToolStripMenuItem.Size = new Size(236, 26);
            studentiToolStripMenuItem.Text = "Studenti";
            studentiToolStripMenuItem.Click += StudentiToolStripMenuItem_Click;
            // 
            // grupeToolStripMenuItem
            // 
            grupeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { asociereGrupaToolStripMenuItem, creareGrupaToolStripMenuItem, vizualizareGrupaToolStripMenuItem, divizareAutomataToolStripMenuItem });
            grupeToolStripMenuItem.Image = (Image)resources.GetObject("grupeToolStripMenuItem.Image");
            grupeToolStripMenuItem.Name = "grupeToolStripMenuItem";
            grupeToolStripMenuItem.Size = new Size(236, 26);
            grupeToolStripMenuItem.Text = "Grupe";
            // 
            // asociereGrupaToolStripMenuItem
            // 
            asociereGrupaToolStripMenuItem.Image = (Image)resources.GetObject("asociereGrupaToolStripMenuItem.Image");
            asociereGrupaToolStripMenuItem.Name = "asociereGrupaToolStripMenuItem";
            asociereGrupaToolStripMenuItem.Size = new Size(212, 26);
            asociereGrupaToolStripMenuItem.Text = "Asociere Grupa";
            asociereGrupaToolStripMenuItem.Click += AsociereGrupaToolStripMenuItem_Click;
            // 
            // creareGrupaToolStripMenuItem
            // 
            creareGrupaToolStripMenuItem.Image = (Image)resources.GetObject("creareGrupaToolStripMenuItem.Image");
            creareGrupaToolStripMenuItem.Name = "creareGrupaToolStripMenuItem";
            creareGrupaToolStripMenuItem.Size = new Size(212, 26);
            creareGrupaToolStripMenuItem.Text = "Creare Grupa";
            creareGrupaToolStripMenuItem.Click += CreareGrupaToolStripMenuItem_Click;
            // 
            // vizualizareGrupaToolStripMenuItem
            // 
            vizualizareGrupaToolStripMenuItem.Image = (Image)resources.GetObject("vizualizareGrupaToolStripMenuItem.Image");
            vizualizareGrupaToolStripMenuItem.Name = "vizualizareGrupaToolStripMenuItem";
            vizualizareGrupaToolStripMenuItem.Size = new Size(212, 26);
            vizualizareGrupaToolStripMenuItem.Text = "Vizualizare Grupa";
            vizualizareGrupaToolStripMenuItem.Click += VizualizareGrupaToolStripMenuItem_Click;
            // 
            // divizareAutomataToolStripMenuItem
            // 
            divizareAutomataToolStripMenuItem.Image = (Image)resources.GetObject("divizareAutomataToolStripMenuItem.Image");
            divizareAutomataToolStripMenuItem.Name = "divizareAutomataToolStripMenuItem";
            divizareAutomataToolStripMenuItem.Size = new Size(212, 26);
            divizareAutomataToolStripMenuItem.Text = "DivizareAutomata";
            divizareAutomataToolStripMenuItem.Click += DivizareAutomataToolStripMenuItem_Click;
            // 
            // noteToolStripMenuItem
            // 
            noteToolStripMenuItem.Image = (Image)resources.GetObject("noteToolStripMenuItem.Image");
            noteToolStripMenuItem.Name = "noteToolStripMenuItem";
            noteToolStripMenuItem.Size = new Size(236, 26);
            noteToolStripMenuItem.Text = "Note";
            noteToolStripMenuItem.Click += NoteToolStripMenuItem_Click;
            // 
            // catalogToolStripMenuItem
            // 
            catalogToolStripMenuItem.Image = (Image)resources.GetObject("catalogToolStripMenuItem.Image");
            catalogToolStripMenuItem.Name = "catalogToolStripMenuItem";
            catalogToolStripMenuItem.Size = new Size(236, 26);
            catalogToolStripMenuItem.Text = "Catalog";
            catalogToolStripMenuItem.Click += CatalogToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(55, 24);
            helpToolStripMenuItem.Text = "Help";
            helpToolStripMenuItem.Click += HelpToolStripMenuItem_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 31);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(601, 416);
            dataGridView1.TabIndex = 3;
            dataGridView1.CellClick += DataGridView1_CellClick_1;
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            dataGridView1.UserAddedRow += DataGridView1_UserAddedRow;
            // 
            // button2
            // 
            button2.Location = new Point(656, 409);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 5;
            button2.Text = "Remove";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(627, 31);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 6;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(627, 65);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(151, 27);
            textBox1.TabIndex = 7;
            // 
            // button1
            // 
            button1.Location = new Point(656, 167);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 8;
            button1.Text = "Insert";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(627, 99);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(151, 28);
            comboBox2.TabIndex = 9;
            // 
            // comboBox3
            // 
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(627, 65);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(151, 28);
            comboBox3.TabIndex = 10;
            comboBox3.SelectedIndexChanged += ComboBox3_SelectedIndexChanged;
            // 
            // comboBox4
            // 
            comboBox4.DisplayMember = "A,B";
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(627, 133);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(151, 28);
            comboBox4.TabIndex = 11;
            comboBox4.SelectedIndexChanged += ComboBox4_SelectedIndexChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(641, 219);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(125, 27);
            textBox2.TabIndex = 12;
            textBox2.Click += TextBox2_Click;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(641, 252);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(125, 27);
            textBox3.TabIndex = 13;
            textBox3.Click += TextBox2_Click;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(641, 285);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(125, 27);
            textBox4.TabIndex = 14;
            textBox4.Click += TextBox2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(656, 317);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 15;
            button3.Text = "Insert";
            button3.UseVisualStyleBackColor = true;
            button3.Click += Button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(608, 353);
            button4.Margin = new Padding(3, 4, 3, 4);
            button4.Name = "button4";
            button4.Size = new Size(94, 31);
            button4.TabIndex = 16;
            button4.Text = "Adaugare";
            button4.UseVisualStyleBackColor = true;
            button4.Click += Button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(709, 353);
            button5.Margin = new Padding(3, 4, 3, 4);
            button5.Name = "button5";
            button5.Size = new Size(86, 31);
            button5.TabIndex = 17;
            button5.Text = "Stergere";
            button5.UseVisualStyleBackColor = true;
            button5.Click += Button5_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 451);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(comboBox4);
            Controls.Add(comboBox3);
            Controls.Add(comboBox2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(comboBox1);
            Controls.Add(button2);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            Controls.Add(label1);
            MainMenuStrip = menuStrip1;
            Name = "Form2";
            Text = "UniCatalog";
            Load += Form2_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ContextMenuStrip contextMenuStrip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem optiuniToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem utilizatoriToolStripMenuItem;
        private DataGridView dataGridView1;
        private Button button2;
        private ToolStripMenuItem cicluDeInvatamantToolStripMenuItem;
        private ComboBox comboBox1;
        private TextBox textBox1;
        private Button button1;
        private ToolStripMenuItem programeDeStudiiToolStripMenuItem;
        private ComboBox comboBox2;
        private ToolStripMenuItem planuriDeInvatamantToolStripMenuItem;
        private ToolStripMenuItem disciplineToolStripMenuItem;
        private ToolStripMenuItem vizualizareToolStripMenuItem;
        private ComboBox comboBox3;
        private ComboBox comboBox4;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private Button button3;
        private ToolStripMenuItem studentiToolStripMenuItem;
        private ToolStripMenuItem grupeToolStripMenuItem;
        private ToolStripMenuItem asociereGrupaToolStripMenuItem;
        private ToolStripMenuItem creareGrupaToolStripMenuItem;
        private ToolStripMenuItem vizualizareGrupaToolStripMenuItem;
        private ToolStripMenuItem divizareAutomataToolStripMenuItem;
        private ToolStripMenuItem noteToolStripMenuItem;
        private Button button4;
        private Button button5;
        private ToolStripMenuItem catalogToolStripMenuItem;
    }
}