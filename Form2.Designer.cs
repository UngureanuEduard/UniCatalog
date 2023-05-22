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
            label1 = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            menuStrip1 = new MenuStrip();
            optiuniToolStripMenuItem = new ToolStripMenuItem();
            utilizatoriToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            dataGridView1 = new DataGridView();
            button2 = new Button();
            cicluDeInvatamantToolStripMenuItem = new ToolStripMenuItem();
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
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // optiuniToolStripMenuItem
            // 
            optiuniToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { utilizatoriToolStripMenuItem, cicluDeInvatamantToolStripMenuItem });
            optiuniToolStripMenuItem.Name = "optiuniToolStripMenuItem";
            optiuniToolStripMenuItem.Size = new Size(75, 24);
            optiuniToolStripMenuItem.Text = "Options";
            // 
            // utilizatoriToolStripMenuItem
            // 
            utilizatoriToolStripMenuItem.Name = "utilizatoriToolStripMenuItem";
            utilizatoriToolStripMenuItem.Size = new Size(225, 26);
            utilizatoriToolStripMenuItem.Text = "Users";
            utilizatoriToolStripMenuItem.Click += utilizatoriToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(55, 24);
            helpToolStripMenuItem.Text = "Help";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
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
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            dataGridView1.UserAddedRow += dataGridView1_UserAddedRow;
            // 
            // button2
            // 
            button2.Location = new Point(656, 409);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 5;
            button2.Text = "Remove";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // cicluDeInvatamantToolStripMenuItem
            // 
            cicluDeInvatamantToolStripMenuItem.Name = "cicluDeInvatamantToolStripMenuItem";
            cicluDeInvatamantToolStripMenuItem.Size = new Size(225, 26);
            cicluDeInvatamantToolStripMenuItem.Text = "Ciclu De Invatamant";
            cicluDeInvatamantToolStripMenuItem.Click += cicluDeInvatamantToolStripMenuItem_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            Controls.Add(label1);
            MainMenuStrip = menuStrip1;
            Name = "Form2";
            Text = "Admin";
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
    }
}