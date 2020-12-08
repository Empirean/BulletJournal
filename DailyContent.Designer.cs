
namespace BulletJournal
{
    partial class DailyContent
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DailyContent));
            this.dataGrid_content = new System.Windows.Forms.DataGridView();
            this.btn_addCollection = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.migrateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toExistingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dailyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monthlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.futureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dailyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.monthlyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.futureToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_dailySearch = new System.Windows.Forms.TextBox();
            this.lbl_title = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_content)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid_content
            // 
            this.dataGrid_content.AllowUserToAddRows = false;
            this.dataGrid_content.AllowUserToDeleteRows = false;
            this.dataGrid_content.AllowUserToOrderColumns = true;
            this.dataGrid_content.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_content.Location = new System.Drawing.Point(12, 57);
            this.dataGrid_content.MultiSelect = false;
            this.dataGrid_content.Name = "dataGrid_content";
            this.dataGrid_content.ReadOnly = true;
            this.dataGrid_content.RowHeadersWidth = 51;
            this.dataGrid_content.RowTemplate.Height = 24;
            this.dataGrid_content.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_content.Size = new System.Drawing.Size(550, 398);
            this.dataGrid_content.TabIndex = 10;
            this.dataGrid_content.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_content_CellMouseUp);
            // 
            // btn_addCollection
            // 
            this.btn_addCollection.Location = new System.Drawing.Point(487, 461);
            this.btn_addCollection.Name = "btn_addCollection";
            this.btn_addCollection.Size = new System.Drawing.Size(75, 42);
            this.btn_addCollection.TabIndex = 1;
            this.btn_addCollection.Text = "Add";
            this.btn_addCollection.UseVisualStyleBackColor = true;
            this.btn_addCollection.Click += new System.EventHandler(this.btn_addCollection_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.migrateToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(131, 76);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // migrateToolStripMenuItem
            // 
            this.migrateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toExistingToolStripMenuItem,
            this.asNewToolStripMenuItem});
            this.migrateToolStripMenuItem.Name = "migrateToolStripMenuItem";
            this.migrateToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.migrateToolStripMenuItem.Text = "Migrate";
            // 
            // toExistingToolStripMenuItem
            // 
            this.toExistingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dailyToolStripMenuItem,
            this.monthlyToolStripMenuItem,
            this.futureToolStripMenuItem});
            this.toExistingToolStripMenuItem.Name = "toExistingToolStripMenuItem";
            this.toExistingToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.toExistingToolStripMenuItem.Text = "To Existing";
            // 
            // dailyToolStripMenuItem
            // 
            this.dailyToolStripMenuItem.Name = "dailyToolStripMenuItem";
            this.dailyToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.dailyToolStripMenuItem.Text = "Daily Task";
            this.dailyToolStripMenuItem.Click += new System.EventHandler(this.dailyToolStripMenuItem_Click);
            // 
            // monthlyToolStripMenuItem
            // 
            this.monthlyToolStripMenuItem.Name = "monthlyToolStripMenuItem";
            this.monthlyToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.monthlyToolStripMenuItem.Text = "Monthly Task";
            this.monthlyToolStripMenuItem.Click += new System.EventHandler(this.monthlyToolStripMenuItem_Click);
            // 
            // futureToolStripMenuItem
            // 
            this.futureToolStripMenuItem.Name = "futureToolStripMenuItem";
            this.futureToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.futureToolStripMenuItem.Text = "Future Log";
            this.futureToolStripMenuItem.Click += new System.EventHandler(this.futureToolStripMenuItem_Click);
            // 
            // asNewToolStripMenuItem
            // 
            this.asNewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dailyToolStripMenuItem1,
            this.monthlyToolStripMenuItem1,
            this.futureToolStripMenuItem1});
            this.asNewToolStripMenuItem.Name = "asNewToolStripMenuItem";
            this.asNewToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.asNewToolStripMenuItem.Text = "As New";
            // 
            // dailyToolStripMenuItem1
            // 
            this.dailyToolStripMenuItem1.Name = "dailyToolStripMenuItem1";
            this.dailyToolStripMenuItem1.Size = new System.Drawing.Size(177, 26);
            this.dailyToolStripMenuItem1.Text = "Daily Task";
            this.dailyToolStripMenuItem1.Click += new System.EventHandler(this.dailyToolStripMenuItem1_Click);
            // 
            // monthlyToolStripMenuItem1
            // 
            this.monthlyToolStripMenuItem1.Name = "monthlyToolStripMenuItem1";
            this.monthlyToolStripMenuItem1.Size = new System.Drawing.Size(177, 26);
            this.monthlyToolStripMenuItem1.Text = "Monthly Task";
            this.monthlyToolStripMenuItem1.Click += new System.EventHandler(this.monthlyToolStripMenuItem1_Click);
            // 
            // futureToolStripMenuItem1
            // 
            this.futureToolStripMenuItem1.Name = "futureToolStripMenuItem1";
            this.futureToolStripMenuItem1.Size = new System.Drawing.Size(177, 26);
            this.futureToolStripMenuItem1.Text = "Future Log";
            this.futureToolStripMenuItem1.Click += new System.EventHandler(this.futureToolStripMenuItem1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(335, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "Seach:";
            // 
            // txt_dailySearch
            // 
            this.txt_dailySearch.Location = new System.Drawing.Point(399, 13);
            this.txt_dailySearch.Name = "txt_dailySearch";
            this.txt_dailySearch.Size = new System.Drawing.Size(163, 22);
            this.txt_dailySearch.TabIndex = 0;
            this.txt_dailySearch.TextChanged += new System.EventHandler(this.txt_dailySearch_TextChanged);
            // 
            // lbl_title
            // 
            this.lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.Location = new System.Drawing.Point(12, 9);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(323, 41);
            this.lbl_title.TabIndex = 16;
            this.lbl_title.Text = "label1";
            // 
            // DailyContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 524);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_dailySearch);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.btn_addCollection);
            this.Controls.Add(this.dataGrid_content);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DailyContent";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Daily Tasks";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_content)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid_content;
        private System.Windows.Forms.Button btn_addCollection;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem migrateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toExistingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dailyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monthlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem futureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dailyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem monthlyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem futureToolStripMenuItem1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_dailySearch;
        private System.Windows.Forms.Label lbl_title;
    }
}