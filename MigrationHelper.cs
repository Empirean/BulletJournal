using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BulletJournal
{
    public static class MigrationHelper
    {
        
        public static void MigrateDailyToDaily(int _sourceId, int _targetId)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            int layer = GetDailyLayer(_targetId);

            string insertCommand = "insert into currenttasks " +
                                   "(description, layerid, previouslayerid, dateadded, datechanged, taskisimportant, tasktype) " +
                                   "(select description, @layerid, @previouslayerid, dateadded, datechanged, taskisimportant, tasktype " +
                                   "from currenttasks " +
                                   "where id = @id) ";

            SqlParameter[] insertParameter = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId},
                new SqlParameter("@layerid", SqlDbType.Int) {  Value = layer + 1},
                new SqlParameter("@previouslayerid", SqlDbType.Int) {  Value = _targetId},
            };

            db.GenericNonQueryAction(insertCommand, insertParameter);
        }

        public static void MigrateDailyToMonthly(int _sourceId, int _targetId)
        {

            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            int layer = GetMonthlyLayer(_targetId);

            string insertCommand = "insert into monthlytasks " +
                                   "(description, layerid, previouslayerid, dateadded, datechanged, taskisimportant, tasktype) " +
                                   "(select description, @layerid, @previouslayerid, dateadded, datechanged, taskisimportant, tasktype " +
                                   "from currenttasks " +
                                   "where id = @id) ";

            SqlParameter[] insertParameter = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId},
                new SqlParameter("@layerid", SqlDbType.Int) {  Value = layer + 1},
                new SqlParameter("@previouslayerid", SqlDbType.Int) {  Value = _targetId},
            };

            db.GenericNonQueryAction(insertCommand, insertParameter);
        }

        public static void MigrateDailyToFuture(int _sourceId, int _targetId)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            int layer = GetFutureLayer(_targetId);

            string insertCommand = "insert into futuretasks " +
                                   "(description, layerid, previouslayerid, dateadded, datechanged, taskisimportant, tasktype) " +
                                   "(select description, @layerid, @previouslayerid, dateadded, datechanged, taskisimportant, tasktype " +
                                   "from currenttasks " +
                                   "where id = @id) ";

            SqlParameter[] insertParameter = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId},
                new SqlParameter("@layerid", SqlDbType.Int) {  Value = layer + 1},
                new SqlParameter("@previouslayerid", SqlDbType.Int) {  Value = _targetId},
            };

            db.GenericNonQueryAction(insertCommand, insertParameter);

        }

        public static void MigrateMonthlyToDaily(int _sourceId, int _targetId)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            int layer = GetDailyLayer(_targetId);

            string insertCommand = "insert into currenttasks " +
                                   "(description, layerid, previouslayerid, dateadded, datechanged, taskisimportant, tasktype) " +
                                   "(select description, @layerid, @previouslayerid, dateadded, datechanged, taskisimportant, tasktype " +
                                   "from monthlytasks " +
                                   "where id = @id) ";

            SqlParameter[] insertParameter = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId},
                new SqlParameter("@layerid", SqlDbType.Int) {  Value = layer + 1},
                new SqlParameter("@previouslayerid", SqlDbType.Int) {  Value = _targetId},
            };

            db.GenericNonQueryAction(insertCommand, insertParameter);

        }

        public static void MigrateMonthlyToMonthly(int _sourceId, int _targetId)
        {

            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            int layer = GetMonthlyLayer(_targetId);

            string insertCommand = "insert into monthlytasks " +
                                   "(description, layerid, previouslayerid, dateadded, datechanged, taskisimportant, tasktype) " +
                                   "(select description, @layerid, @previouslayerid, dateadded, datechanged, taskisimportant, tasktype " +
                                   "from monthlytasks " +
                                   "where id = @id) ";

            SqlParameter[] insertParameter = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId},
                new SqlParameter("@layerid", SqlDbType.Int) {  Value = layer + 1},
                new SqlParameter("@previouslayerid", SqlDbType.Int) {  Value = _targetId},
            };

            db.GenericNonQueryAction(insertCommand, insertParameter);
        }

        public static void MigrateMonthlyToFuture(int _sourceId, int _targetId)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            int layer = GetFutureLayer(_targetId);

            string insertCommand = "insert into futuretasks " +
                                   "(description, layerid, previouslayerid, dateadded, datechanged, taskisimportant, tasktype) " +
                                   "(select description, @layerid, @previouslayerid, dateadded, datechanged, taskisimportant, tasktype " +
                                   "from monthlytasks " +
                                   "where id = @id) ";

            SqlParameter[] insertParameter = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId},
                new SqlParameter("@layerid", SqlDbType.Int) {  Value = layer + 1},
                new SqlParameter("@previouslayerid", SqlDbType.Int) {  Value = _targetId},
            };

            db.GenericNonQueryAction(insertCommand, insertParameter);

        }

        public static void MigrateFutureToDaily(int _sourceId, int _targetId)
        {

            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            int layer = GetDailyLayer(_targetId);

            string insertCommand = "insert into currenttasks " +
                                   "(description, layerid, previouslayerid, dateadded, datechanged, taskisimportant, tasktype) " +
                                   "(select description, @layerid, @previouslayerid, dateadded, datechanged, taskisimportant, tasktype " +
                                   "from futuretasks " +
                                   "where id = @id) ";

            SqlParameter[] insertParameter = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId},
                new SqlParameter("@layerid", SqlDbType.Int) {  Value = layer + 1},
                new SqlParameter("@previouslayerid", SqlDbType.Int) {  Value = _targetId},
            };

            db.GenericNonQueryAction(insertCommand, insertParameter);
        }

        public static void MigrateFutureToMonthly(int _sourceId, int _targetId)
        {

            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            int layer = GetMonthlyLayer(_targetId);

            string insertCommand = "insert into monthlytasks " +
                                   "(description, layerid, previouslayerid, dateadded, datechanged, taskisimportant, tasktype) " +
                                   "(select description, @layerid, @previouslayerid, dateadded, datechanged, taskisimportant, tasktype " +
                                   "from futuretasks " +
                                   "where id = @id) ";

            SqlParameter[] insertParameter = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId},
                new SqlParameter("@layerid", SqlDbType.Int) {  Value = layer + 1},
                new SqlParameter("@previouslayerid", SqlDbType.Int) {  Value = _targetId},
            };

            db.GenericNonQueryAction(insertCommand, insertParameter);
        }

        public static void MigrateFutureToFuture(int _sourceId, int _targetId)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            int layer = GetFutureLayer(_targetId);

            string insertCommand = "insert into futuretasks " +
                                   "(description, layerid, previouslayerid, dateadded, datechanged, taskisimportant, tasktype) " +
                                   "(select description, @layerid, @previouslayerid, dateadded, datechanged, taskisimportant, tasktype " +
                                   "from futuretasks " +
                                   "where id = @id) ";

            SqlParameter[] insertParameter = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _sourceId},
                new SqlParameter("@layerid", SqlDbType.Int) {  Value = layer + 1},
                new SqlParameter("@previouslayerid", SqlDbType.Int) {  Value = _targetId},
            };

            db.GenericNonQueryAction(insertCommand, insertParameter);

        }

        public static int GetDailyLayer(int _id)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            string queryCommand = "select layerid " +
                             "from currenttasks " +
                             "where id = @id";

            SqlParameter[] queryParameter = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _id}
            };

            DataTable dataTable = db.GenericQueryAction(queryCommand, queryParameter);
            DataRow dataRow = dataTable.AsEnumerable().ToList()[0];

            return dataRow.Field<int>("layerid");
        }

        public static int GetMonthlyLayer(int _id)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            string queryCommand = "select layerid " +
                             "from monthlytasks " +
                             "where id = @id";

            SqlParameter[] queryParameter = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _id}
            };

            DataTable dataTable = db.GenericQueryAction(queryCommand, queryParameter);
            DataRow dataRow = dataTable.AsEnumerable().ToList()[0];

            return dataRow.Field<int>("layerid");
        }

        public static int GetFutureLayer(int _id)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            string queryCommand = "select layerid " +
                             "from futuretasks " +
                             "where id = @id";

            SqlParameter[] queryParameter = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) {  Value = _id}
            };

            DataTable dataTable = db.GenericQueryAction(queryCommand, queryParameter);
            DataRow dataRow = dataTable.AsEnumerable().ToList()[0];

            return dataRow.Field<int>("layerid");
        }
    }
}
