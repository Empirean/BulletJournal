
namespace BulletJournal
{
    partial class ControlPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlPanel));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_noteDateAdded = new System.Windows.Forms.CheckBox();
            this.chk_noteDateChanged = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk_dailyTaskType = new System.Windows.Forms.CheckBox();
            this.chk_dailyIsImportant = new System.Windows.Forms.CheckBox();
            this.chk_dailyDateAdded = new System.Windows.Forms.CheckBox();
            this.chk_dailyDateChanged = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_noteDateAdded);
            this.groupBox1.Controls.Add(this.chk_noteDateChanged);
            this.groupBox1.Location = new System.Drawing.Point(30, 217);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 119);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Notes";
            // 
            // chk_noteDateAdded
            // 
            this.chk_noteDateAdded.AutoSize = true;
            this.chk_noteDateAdded.Location = new System.Drawing.Point(6, 58);
            this.chk_noteDateAdded.Name = "chk_noteDateAdded";
            this.chk_noteDateAdded.Size = new System.Drawing.Size(194, 21);
            this.chk_noteDateAdded.TabIndex = 1;
            this.chk_noteDateAdded.Text = "Show Date Added Column";
            this.chk_noteDateAdded.UseVisualStyleBackColor = true;
            this.chk_noteDateAdded.CheckedChanged += new System.EventHandler(this.chk_notedateadded_CheckedChanged);
            // 
            // chk_noteDateChanged
            // 
            this.chk_noteDateChanged.AutoSize = true;
            this.chk_noteDateChanged.Location = new System.Drawing.Point(6, 30);
            this.chk_noteDateChanged.Name = "chk_noteDateChanged";
            this.chk_noteDateChanged.Size = new System.Drawing.Size(210, 21);
            this.chk_noteDateChanged.TabIndex = 0;
            this.chk_noteDateChanged.Text = "Show Date Changed Column";
            this.chk_noteDateChanged.UseVisualStyleBackColor = true;
            this.chk_noteDateChanged.CheckedChanged += new System.EventHandler(this.chk_notedate_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_dailyTaskType);
            this.groupBox2.Controls.Add(this.chk_dailyIsImportant);
            this.groupBox2.Controls.Add(this.chk_dailyDateAdded);
            this.groupBox2.Controls.Add(this.chk_dailyDateChanged);
            this.groupBox2.Location = new System.Drawing.Point(30, 26);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(257, 154);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Daily Tasks";
            // 
            // chk_dailyTaskType
            // 
            this.chk_dailyTaskType.AutoSize = true;
            this.chk_dailyTaskType.Location = new System.Drawing.Point(7, 123);
            this.chk_dailyTaskType.Name = "chk_dailyTaskType";
            this.chk_dailyTaskType.Size = new System.Drawing.Size(186, 21);
            this.chk_dailyTaskType.TabIndex = 3;
            this.chk_dailyTaskType.Text = "Show Task Type Column";
            this.chk_dailyTaskType.UseVisualStyleBackColor = true;
            this.chk_dailyTaskType.CheckedChanged += new System.EventHandler(this.chk_dailyTaskType_CheckedChanged);
            // 
            // chk_dailyIsImportant
            // 
            this.chk_dailyIsImportant.AutoSize = true;
            this.chk_dailyIsImportant.Location = new System.Drawing.Point(7, 95);
            this.chk_dailyIsImportant.Name = "chk_dailyIsImportant";
            this.chk_dailyIsImportant.Size = new System.Drawing.Size(162, 21);
            this.chk_dailyIsImportant.TabIndex = 2;
            this.chk_dailyIsImportant.Text = "Show Important Task";
            this.chk_dailyIsImportant.UseVisualStyleBackColor = true;
            this.chk_dailyIsImportant.CheckedChanged += new System.EventHandler(this.chk_dailyIsImportant_CheckedChanged);
            // 
            // chk_dailyDateAdded
            // 
            this.chk_dailyDateAdded.AutoSize = true;
            this.chk_dailyDateAdded.Location = new System.Drawing.Point(7, 67);
            this.chk_dailyDateAdded.Name = "chk_dailyDateAdded";
            this.chk_dailyDateAdded.Size = new System.Drawing.Size(194, 21);
            this.chk_dailyDateAdded.TabIndex = 1;
            this.chk_dailyDateAdded.Text = "Show Date Added Column";
            this.chk_dailyDateAdded.UseVisualStyleBackColor = true;
            this.chk_dailyDateAdded.CheckedChanged += new System.EventHandler(this.chk_dailyDateAdded_CheckedChanged);
            // 
            // chk_dailyDateChanged
            // 
            this.chk_dailyDateChanged.AutoSize = true;
            this.chk_dailyDateChanged.Location = new System.Drawing.Point(7, 39);
            this.chk_dailyDateChanged.Name = "chk_dailyDateChanged";
            this.chk_dailyDateChanged.Size = new System.Drawing.Size(210, 21);
            this.chk_dailyDateChanged.TabIndex = 0;
            this.chk_dailyDateChanged.Text = "Show Date Changed Column";
            this.chk_dailyDateChanged.UseVisualStyleBackColor = true;
            this.chk_dailyDateChanged.CheckedChanged += new System.EventHandler(this.chk_dailyDateChanged_CheckedChanged);
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 449);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ControlPanel";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Control Panel";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chk_noteDateAdded;
        private System.Windows.Forms.CheckBox chk_noteDateChanged;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chk_dailyTaskType;
        private System.Windows.Forms.CheckBox chk_dailyIsImportant;
        private System.Windows.Forms.CheckBox chk_dailyDateAdded;
        private System.Windows.Forms.CheckBox chk_dailyDateChanged;
    }
}