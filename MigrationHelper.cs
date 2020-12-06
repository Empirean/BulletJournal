using System.Data;
using System.Data.SqlClient;

namespace BulletJournal
{
    public static class MigrationHelper
    {

        public static void MigrateDailyToDaily(int _sourceId, int _targetId, JournalTask.EntryMode _mode)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            string command;
            SqlParameter[] parameters;

            if (_mode == JournalTask.EntryMode.migrate_main)
            {
                command = "insert into dailydetail " +
                            "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                            "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                            "from dailydetail " +
                            "where maintaskforeignkey = @id)";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
                };
            }
            else
            {
                command = "insert into dailydetail " +
                            "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                            "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                            "from dailydetail " +
                            "where taskid = @id)";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
                };
            }

            db.GenericNonQueryAction(command, parameters);

        }

        public static void MigrateDailyToMonthly(int _sourceId, int _targetId, JournalTask.EntryMode _mode)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            string command;
            SqlParameter[] parameters;

            if (_mode == JournalTask.EntryMode.migrate_main)
            {
                command = "insert into monthlydetail " +
                            "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                            "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                            "from dailydetail " +
                            "where maintaskforeignkey = @id)";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
                };
            }
            else
            {
                command = "insert into monthlydetail " +
                            "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                            "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                            "from dailydetail " +
                            "where taskid = @id)";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
                };
            }

            db.GenericNonQueryAction(command, parameters);

        }

        public static void MigrateDailyToFuture(int _sourceId, int _targetId, JournalTask.EntryMode _mode)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            
            string command;
            SqlParameter[] parameters;

            if (_mode == JournalTask.EntryMode.migrate_main)
            {
                command = "insert into futuredetail " +
                            "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                            "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                            "from dailydetail " +
                            "where maintaskforeignkey = @id)";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
                };
            }
            else
            {
                command = "insert into futuredetail " +
                           "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                           "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                           "from dailydetail " +
                           "where taskid = @id)";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
                };
            }


            db.GenericNonQueryAction(command, parameters);

        }

        public static void MigrateMonthlyToDaily(int _sourceId, int _targetId, JournalTask.EntryMode _mode)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            string command;
            SqlParameter[] parameters;

            if (_mode == JournalTask.EntryMode.migrate_main)
            {

                command = "insert into dailydetail " +
                            "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                            "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                            "from monthlydetail " +
                            "where maintaskforeignkey = @id)";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
                };

            }
            else
            {
                command = "insert into dailydetail " +
                             "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                             "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                             "from monthlydetail " +
                             "where taskid = @id)";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
                };

            }


            db.GenericNonQueryAction(command, parameters);

        }

        public static void MigrateMonthlyToMonthly(int _sourceId, int _targetId, JournalTask.EntryMode _mode)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            string command;
            SqlParameter[] parameters;

            if (_mode == JournalTask.EntryMode.migrate_main)
            {
                command = "insert into monthlydetail " +
                            "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                            "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                            "from monthlydetail " +
                            "where maintaskforeignkey = @id)";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
                };
            }
            else
            {
                command = "insert into monthlydetail " +
                            "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                            "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                            "from monthlydetail " +
                            "where taskid = @id)";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
                };
            }


            db.GenericNonQueryAction(command, parameters);

        }

        public static void MigrateMonthlyToFuture(int _sourceId, int _targetId, JournalTask.EntryMode _mode)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            string command;
            SqlParameter[] parameters;

            if (_mode == JournalTask.EntryMode.migrate_main)
            {
                command = "insert into futuredetail " +
                            "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                            "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                            "from monthlydetail " +
                            "where maintaskforeignkey = @id)";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
                };
            }
            else
            {
                command = "insert into futuredetail " +
                           "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                           "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                           "from monthlydetail " +
                           "where taskid = @id)";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
                };
            }

            db.GenericNonQueryAction(command, parameters);

        }

        public static void MigrateFutureToDaily(int _sourceId, int _targetId, JournalTask.EntryMode _mode)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            string command;
            SqlParameter[] parameters;

            if (_mode == JournalTask.EntryMode.migrate_main)
            {
                command = "insert into dailydetail " +
                            "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                            "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                            "from futuredetail " +
                            "where maintaskforeignkey = @id)";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
                };
            }
            else
            {
                command = "insert into dailydetail " +
                            "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                            "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                            "from futuredetail " +
                            "where taskid = @id)";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
                };
            }



            db.GenericNonQueryAction(command, parameters);

        }

        public static void MigrateFutureToMonthly(int _sourceId, int _targetId, JournalTask.EntryMode _mode)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            string command;
            SqlParameter[] parameters;

            if (_mode == JournalTask.EntryMode.migrate_main)
            {
                command = "insert into monthlydetail " +
                            "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                            "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                            "from futuredetail " +
                            "where maintaskforeignkey = @id)";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
                };

            }
            else
            {
                command = "insert into monthlydetail " +
                         "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                         "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                         "from futuredetail " +
                         "where taskid = @id)";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
                };
            }


            db.GenericNonQueryAction(command, parameters);

        }

        public static void MigrateFutureToFuture(int _sourceId, int _targetId, JournalTask.EntryMode _mode)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            string command;
            SqlParameter[] parameters;

            if (_mode == JournalTask.EntryMode.migrate_main)
            {
                command = "insert into futuredetail " +
                            "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                            "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                            "from futuredetail " +
                            "where maintaskforeignkey = @id)";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
                };
            }
            else
            {
                command = "insert into futuredetail " +
                           "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                           "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                           "from futuredetail " +
                           "where taskid = @id)";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
                };
            }

            db.GenericNonQueryAction(command, parameters);

        }
    }
}
