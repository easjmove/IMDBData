using IMDBData.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBData
{
    public class NormalInserter
    {
        public NormalInserter() { }

        public void Insert(List<Title> titles, SqlConnection sqlConn, SqlTransaction transAction)
        {
            foreach (Title title in titles)
            {
                string SQL = "INSERT INTO [Titles]([TConst]," +
                    "[PrimaryTitle],[OriginalTitle],[IsAdult],[StartYear]," +
                    "[EndYear],[RuntimeMinutes]) " +
                    "VALUES('" + title.TConst + "'" +
                    ",'" + title.PrimaryTitle.Replace("'","''") + "'" +
                    ",'" + title.OriginalTitle.Replace("'", "''") + "'" +
                    ",'" + title.IsAdult + "'" +
                    "," + CheckIntForNull(title.StartYear) +
                    "," + CheckIntForNull(title.EndYear) +
                    "," + CheckIntForNull(title.RuntimeMinutes) + ")";

                //throw new Exception(SQL);

                SqlCommand sqlComm = new SqlCommand(SQL, sqlConn, transAction);
                sqlComm.ExecuteNonQuery();
            }
        }

        public string CheckIntForNull(int? value)
        {
            if (value == null)
            {
                return "NULL";
            }
            return value.ToString();
        }
    }
}
