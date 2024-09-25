using IMDBData.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace IMDBData
{
    public class PreparedInserter : IInserter
    {
        public void Insert(List<Title> titles, SqlConnection sqlConn, SqlTransaction transAction)
        {
            string SQL = "INSERT INTO [Titles]([TConst]," +
                    "[PrimaryTitle],[OriginalTitle],[IsAdult],[StartYear]," +
                    "[EndYear],[RuntimeMinutes]) " +
                    "VALUES(@tconst" +
                    ",@primaryTitle" +
                    ",@originalTitle" +
                    ",@isAdult" +
                    ",@startYear" +
                    ",@endYear"+
                    ",@runtimeMinutes)";
            SqlCommand sqlComm = new SqlCommand(SQL, sqlConn, transAction);            

            SqlParameter tconstPar = new SqlParameter("@tconst",
                SqlDbType.VarChar, 50);
            sqlComm.Parameters.Add(tconstPar);

            SqlParameter primaryTitlePar = new SqlParameter("@primaryTitle",
                SqlDbType.VarChar, 200);
            sqlComm.Parameters.Add(primaryTitlePar);

            SqlParameter originalTitlePar = new SqlParameter("@originalTitle",
                SqlDbType.VarChar, 200);
            sqlComm.Parameters.Add(originalTitlePar);

            SqlParameter isAdultPar = new SqlParameter("@isAdult",
                SqlDbType.Bit);
            sqlComm.Parameters.Add(isAdultPar);

            SqlParameter startYearPar = new SqlParameter("@startYear",
                SqlDbType.Int);
            sqlComm.Parameters.Add(startYearPar);

            SqlParameter endYearPar = new SqlParameter("@endYear",
                SqlDbType.Int);
            sqlComm.Parameters.Add(endYearPar);

            SqlParameter runtimeMinutesPar = new SqlParameter("@runtimeMinutes",
                SqlDbType.Int);
            sqlComm.Parameters.Add(runtimeMinutesPar);

            sqlComm.Prepare();

            foreach (Title title in titles)
            {
                tconstPar.Value = title.TConst;
                primaryTitlePar.Value = CheckObjectForNull(title.PrimaryTitle);
                originalTitlePar.Value = CheckObjectForNull(title.OriginalTitle);
                isAdultPar.Value = title.IsAdult;
                startYearPar.Value = CheckObjectForNull(title.StartYear);
                endYearPar.Value = CheckObjectForNull(title.EndYear);
                runtimeMinutesPar.Value = CheckObjectForNull(title.RuntimeMinutes);

                sqlComm.ExecuteNonQuery();
            }
        }

        public Object CheckObjectForNull(Object? value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            return value;
        }
    }
}
