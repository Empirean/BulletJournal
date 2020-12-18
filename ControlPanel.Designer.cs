
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
            this.grp_notes = new System.Windows.Forms.GroupBox();
            this.chk_noteDateAdded = new System.Windows.Forms.CheckBox();
            this.chk_noteDateChanged = new System.Windows.Forms.CheckBox();
            this.grp_dailyTask = new System.Windows.Forms.GroupBox();
            this.chk_dailyTaskType = new System.Windows.Forms.CheckBox();
            this.chk_dailyIsImportant = new System.Windows.Forms.CheckBox();
            this.chk_dailyDateAdded = new System.Windows.Forms.CheckBox();
            this.chk_dailyDateChanged = new System.Windows.Forms.CheckBox();
            this.grp_monthlyTasks = new System.Windows.Forms.GroupBox();
            this.chk_monthlyTaskType = new System.Windows.Forms.CheckBox();
            this.chk_monthlyIsImportant = new System.Windows.Forms.CheckBox();
            this.chk_monthlyDateAdded = new System.Windows.Forms.CheckBox();
            this.chk_monthlyDateChanged = new System.Windows.Forms.CheckBox();
            this.grp_notes.SuspendLayout();
            this.grp_dailyTask.SuspendLayout();
            this.grp_monthlyTasks.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_notes
            // 
            this.grp_notes.Controls.Add(this.chk_noteDateAdded);
            this.grp_notes.Controls.Add(this.chk_noteDateChanged);
            this.grp_notes.Location = new System.Drawing.Point(30, 217);
            this.grp_notes.Name = "grp_notes";
            this.grp_notes.Size = new System.Drawing.Size(257, 119);
            this.grp_notes.TabIndex = 0;
            this.grp_notes.TabStop = false;
            this.grp_notes.Text = "Notes";
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
            // grp_dailyTask
            // 
            this.grp_dailyTask.Controls.Add(this.chk_dailyTaskType);
            this.grp_dailyTask.Controls.Add(this.chk_dailyIsImportant);
            this.grp_dailyTask.Controls.Add(this.chk_dailyDateAdded);
            this.grp_dailyTask.Controls.Add(this.chk_dailyDateChanged);
            this.grp_dailyTask.Location = new System.Drawing.Point(30, 26);
            this.grp_dailyTask.Name = "grp_dailyTask";
            this.grp_dailyTask.Size = new System.Drawing.Size(257, 154);
            this.grp_dailyTask.TabIndex = 1;
            this.grp_dailyTask.TabStop = false;
            this.grp_dailyTask.Text = "Daily Tasks";
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
            // grp_monthlyTasks
            // 
            this.grp_monthlyTasks.Controls.Add(this.chk_monthlyTaskType);
            this.grp_monthlyTasks.Controls.Add(this.chk_monthlyIsImportant);
            this.grp_monthlyTasks.Controls.Add(this.chk_monthlyDateAdded);
            this.grp_monthlyTasks.Controls.Add(this.chk_monthlyDateChanged);
            this.grp_monthlyTasks.Location = new System.Drawing.Point(324, 26);
            this.grp_monthlyTasks.Name = "grp_monthlyTasks";
            this.grp_monthlyTasks.Size = new System.Drawing.Size(257, 154);
            this.grp_monthlyTasks.TabIndex = 4;
            this.grp_monthlyTasks.TabStop = false;
            this.grp_monthlyTasks.Text = "Monthly Tasks";
            // 
            // chk_monthlyTaskType
            // 
            this.chk_monthlyTaskType.AutoSize = true;
            this.chk_monthlyTaskType.Location = new System.Drawing.Point(7, 123);
            this.chk_monthlyTaskType.Name = "chk_monthlyTaskType";
            this.chk_monthlyTaskType.Size = new System.Drawing.Size(186, 21);
            this.chk_monthlyTaskType.TabIndex = 3;
            this.chk_monthlyTaskType.Text = "Show Task Type Column";
            this.chk_monthlyTaskType.UseVisualStyleBackColor = true;
            this.chk_monthlyTaskType.CheckedChanged += new System.EventHandler(this.chk_monthlyTaskType_CheckedChanged);
            // 
            // chk_monthlyIsImportant
            // 
            this.chk_monthlyIsImportant.AutoSize = true;
            this.chk_monthlyIsImportant.Location = new System.Drawing.Point(7, 95);
            this.chk_monthlyIsImportant.Name = "chk_monthlyIsImportant";
            this.chk_monthlyIsImportant.Size = new System.Drawing.Size(162, 21);
            this.chk_monthlyIsImportant.TabIndex = 2;
            this.chk_monthlyIsImportant.Text = "Show Important Task";
            this.chk_monthlyIsImportant.UseVisualStyleBackColor = true;
            this.chk_monthlyIsImportant.CheckedChanged += new System.EventHandler(this.chk_monthlyIsImportant_CheckedChanged);
            // 
            // chk_monthlyDateAdded
            // 
            this.chk_monthlyDateAdded.AutoSize = true;
            this.chk_monthlyDateAdded.Location = new System.Drawing.Point(7, 67);
            this.chk_monthlyDateAdded.Name = "chk_monthlyDateAdded";
            this.chk_monthlyDateAdded.Size = new System.Drawing.Size(194, 21);
            this.chk_monthlyDateAdded.TabIndex = 1;
            this.chk_monthlyDateAdded.Text = "Show Date Added Column";
            this.chk_monthlyDateAdded.UseVisualStyleBackColor = true;
            this.chk_monthlyDateAdded.CheckedChanged += new System.EventHandler(this.chk_monthlyDateAdded_CheckedChanged);
            // 
            // chk_monthlyDateChanged
            // 
            this.chk_monthlyDateChanged.AutoSize = true;
            this.chk_monthlyDateChanged.Location = new System.Drawing.Point(7, 39);
            this.chk_monthlyDateChanged.Name = "chk_monthlyDateChanged";
            this.chk_monthlyDateChanged.Size = new System.Drawing.Size(210, 21);
            this.chk_monthlyDateChanged.TabIndex = 0;
            this.chk_monthlyDateChanged.Text = "Show Date Changed Column";
            this.chk_monthlyDateChanged.UseVisualStyleBackColor = true;
            this.chk_monthlyDateChanged.CheckedChanged += new System.EventHandler(this.chk_monthlyDateChanged_CheckedChanged);
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 449);
            this.Controls.Add(this.grp_monthlyTasks);
            this.Controls.Add(this.grp_dailyTask);
            this.Controls.Add(this.grp_notes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ControlPanel";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Control Panel";
            this.grp_notes.ResumeLayout(false);
            this.grp_notes.PerformLayout();
            this.grp_dailyTask.ResumeLayout(false);
            this.grp_dailyTask.PerformLayout();
            this.grp_monthlyTasks.ResumeLayout(false);
            this.grp_monthlyTasks.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_notes;
        private System.Windows.Forms.CheckBox chk_noteDateAdded;
        private System.Windows.Forms.CheckBox chk_noteDateChanged;
        private System.Windows.Forms.GroupBox grp_dailyTask;
        private System.Windows.Forms.CheckBox chk_dailyTaskType;
        private System.Windows.Forms.CheckBox chk_dailyIsImportant;
        private System.Windows.Forms.CheckBox chk_dailyDateAdded;
        private System.Windows.Forms.CheckBox chk_dailyDateChanged;
        private System.Windows.Forms.GroupBox grp_monthlyTasks;
        private System.Windows.Forms.CheckBox chk_monthlyTaskType;
        private System.Windows.Forms.CheckBox chk_monthlyIsImportant;
        private System.Windows.Forms.CheckBox chk_monthlyDateAdded;
        private System.Windows.Forms.CheckBox chk_monthlyDateChanged;
    }
}