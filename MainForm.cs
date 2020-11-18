using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BulletJournal
{
    public partial class MainForm : Form
    {
        DBTools dbTools;

        public MainForm()
        {
            InitializeComponent();
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
            string cn = Properties.Settings.Default.DatabaseConnectionString;
            dbTools = new DBTools(cn);
            
            Populate_dailyTask();
            Populate_collection();
            Populate_futureLog();
            

        }


        private void addEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Populate_collection();
            Populate_dailyTask();
            Populate_futureLog();
        }

        private void maintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Maintenance maintenanceForm = new Maintenance())
            {
                maintenanceForm.ShowDialog();

            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            using (AddDailyTask addDailyTask = new AddDailyTask(this))
            {
                addDailyTask.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (AddCollection addCollection = new AddCollection(this))
            {
                addCollection.ShowDialog();
            }
        }

        public void Populate_dailyTask()
        {
            string commandString = "select m.taskid, " +
                                          "m.taskdate as [Date], " +
                                          "case when d.taskisimportant = 1 " +
                                          "then '*' else '' end as [I], " +
                                          "case " +
                                          "when d.tasktype = 0 then 'TASK' " +
                                          "when d.tasktype = 1 then 'EVENT' " +
                                          "when d.tasktype = 2 then 'NOTES'" +
                                          "else 'CLOSED' end as [Type], " +
                                          "d.taskdescription as [Description]" +
                                   "from dailymain as m " +
                                   "inner join dailydetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate >= @taskdate " +
                                   "order by m.taskdate";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@taskdate", SqlDbType.Date) { Value = DateTime.Now }
            };

            dataGrid_dailyTask.DataSource = dbTools.GenericQueryAction(commandString, parameters);
            dataGrid_dailyTask.Columns[0].Visible = false;
            dataGrid_dailyTask.Columns[0].Width = 1;
            dataGrid_dailyTask.Columns["Date"].Width = 70;
            dataGrid_dailyTask.Columns["I"].Width = 20;
            dataGrid_dailyTask.Columns["Type"].Width = 50;
            dataGrid_dailyTask.Columns["Description"].Width = 400;
        }

        public void Populate_futureLog()
        {
            string commandString = "select m.taskid, " +
                                          "datename(month, m.taskdate) +" + " " +
                                          "datename(year, m.taskdate)" +
                                          " as [Date], " +
                                          "case when d.taskisimportant = 1 " +
                                          "then '*' else '' end as [I], " +
                                          "case " +
                                          "when d.tasktype = 0 then 'TASK' " +
                                          "when d.tasktype = 1 then 'EVENT' " +
                                          "when d.tasktype = 2 then 'NOTES'" +
                                          "else 'CLOSED' end as [Type], " +
                                          "d.taskdescription as [Description]" +
                                   "from futuremain as m " +
                                   "inner join futuredetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate >= @taskdate " +
                                   "order by m.taskdate";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@taskdate", SqlDbType.Date) { Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) }
            };

            dataGrid_futureLog.DataSource = dbTools.GenericQueryAction(commandString, parameters);
            dataGrid_futureLog.Columns[0].Visible = false;
            dataGrid_futureLog.Columns[0].Width = 1;
            dataGrid_futureLog.Columns["Date"].Width = 100;
            dataGrid_futureLog.Columns["I"].Width = 20;
            dataGrid_futureLog.Columns["Type"].Width = 50;
            dataGrid_futureLog.Columns["Description"].Width = 400;
        }

        public void Populate_collection()
        {
            string commandString = "select TaskId,  case when TaskIsImportant = 1 then '*' else '' end as [I], " + 
                                   " case " +
                                   " when TaskType = 0 then 'TASK' " + 
                                   " when TaskType = 1 then 'EVENT' " +
                                   " when TaskType = 2 then 'NOTES' " + 
                                   " else 'CLOSE' end as [Type], " + 
                                   " TaskDescription as [Description] " +
                                   " from CollectionTable";

            SqlParameter[] parameters = new SqlParameter[]
            {
            };

            
            dataGrid_collection.DataSource = dbTools.GenericQueryAction(commandString, parameters);
            dataGrid_collection.Columns[0].Visible = false;
            dataGrid_collection.Columns[0].Width = 1;
            dataGrid_collection.Columns["I"].Width = 20;
            dataGrid_collection.Columns["Type"].Width = 50;
            dataGrid_collection.Columns["Description"].Width = 400;

        }

        private void btn_addFutureLog_Click(object sender, EventArgs e)
        {
            using (AddFutureLog addFutureLog = new AddFutureLog(this))
            {
                addFutureLog.ShowDialog();
            }
        }

    }
}