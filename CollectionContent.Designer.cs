
namespace BulletJournal
{
    partial class CollectionContent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CollectionContent));
            this.dataGrid_content = new System.Windows.Forms.DataGridView();
            this.btn_addCollection = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.dataGrid_content.TabIndex = 0;
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
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 52);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // CollectionContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 512);
            this.Controls.Add(this.btn_addCollection);
            this.Controls.Add(this.dataGrid_content);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CollectionContent";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CollectionContent";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_content)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid_content;
        private System.Windows.Forms.Button btn_addCollection;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}