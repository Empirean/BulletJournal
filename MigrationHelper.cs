using System.Data;
using System.Data.SqlClient;

namespace BulletJournal
{
    public static class MigrationHelper
    {

        public static void MigrateDailyToDaily(int _sourceId, int _targetId)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            string command = "insert into dailydetail " +
                "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                "from dailydetail " +
                "where maintaskforeignkey = @id)";


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
            };

            db.GenericNonQueryAction(command, parameters);

        }

        public static void MigrateDailyToMonthly(int _sourceId, int _targetId)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            string command = "insert into monthlydetail " +
                "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                "from dailydetail " +
                "where maintaskforeignkey = @id)";


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
            };

            db.GenericNonQueryAction(command, parameters);

        }

        public static void MigrateDailyToFuture(int _sourceId, int _targetId)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            string command = "insert into futuredetail " +
                "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                "from dailydetail " +
                "where maintaskforeignkey = @id)";


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
            };

            db.GenericNonQueryAction(command, parameters);

        }

        public static void MigrateMonthlyToDaily(int _sourceId, int _targetId)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            string command = "insert into dailydetail " +
                "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                "from monthlydetail " +
                "where maintaskforeignkey = @id)";


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
            };

            db.GenericNonQueryAction(command, parameters);

        }

        public static void MigrateMonthlyToMonthly(int _sourceId, int _targetId)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            string command = "insert into monthlydetail " +
                "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                "from monthlydetail " +
                "where maintaskforeignkey = @id)";


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
            };

            db.GenericNonQueryAction(command, parameters);

        }

        public static void MigrateMonthlyToFuture(int _sourceId, int _targetId)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            string command = "insert into futuredetail " +
                "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                "from monthlydetail " +
                "where maintaskforeignkey = @id)";


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
            };

            db.GenericNonQueryAction(command, parameters);

        }

        public static void MigrateFutureToDaily(int _sourceId, int _targetId)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            string command = "insert into dailydetail " +
                "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                "from futuredetail " +
                "where maintaskforeignkey = @id)";


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
            };

            db.GenericNonQueryAction(command, parameters);

        }

        public static void MigrateFutureToMonthly(int _sourceId, int _targetId)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            string command = "insert into monthlydetail " +
                "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                "from futuredetail " +
                "where maintaskforeignkey = @id)";


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
            };

            db.GenericNonQueryAction(command, parameters);

        }

        public static void MigrateFutureToFuture(int _sourceId, int _targetId)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            string command = "insert into futuredetail " +
                "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                "(select tasktype, taskdescription, taskisimportant, " + _targetId.ToString() + " " +
                "from futuredetail " +
                "where maintaskforeignkey = @id)";


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId}
            };

            db.GenericNonQueryAction(command, parameters);

        }
    }
}
