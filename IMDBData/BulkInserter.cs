using IMDBData.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBData
{
    public class BulkInserter : IInserter
    {
        public void Insert(List<Title> titles, SqlConnection sqlConn, SqlTransaction transAction)
        {
            DataTable titleTable = new DataTable();

            DataColumn tconstCol = new DataColumn("tconst", typeof(string));
            DataColumn primaryTitleCol = new DataColumn("primaryTitle", typeof(string));
            DataColumn originalTitleCol = new DataColumn("originalTitle", typeof(string));
            DataColumn isAdultCol = new DataColumn("isAdult", typeof(bool));
            DataColumn startYearCol = new DataColumn("startYear", typeof(int));
            DataColumn endYearCol = new DataColumn("endYear", typeof(int));
            DataColumn runtimeMinutesCol = new DataColumn("runtimeMinutes", typeof(int));

            titleTable.Columns.Add(tconstCol);
            titleTable.Columns.Add(primaryTitleCol);
            titleTable.Columns.Add(originalTitleCol);
            titleTable.Columns.Add(isAdultCol);
            titleTable.Columns.Add(startYearCol);
            titleTable.Columns.Add(endYearCol);
            titleTable.Columns.Add(runtimeMinutesCol);

            foreach (Title title in titles)
            {
                DataRow titleRow = titleTable.NewRow();
                FillParameter(titleRow, "tconst", title.TConst);
                FillParameter(titleRow, "primaryTitle", title.PrimaryTitle);
                FillParameter(titleRow, "originalTitle", title.OriginalTitle);
                FillParameter(titleRow, "isAdult", title.IsAdult);
                FillParameter(titleRow, "startYear", title.StartYear);
                FillParameter(titleRow, "endYear", title.EndYear);
                FillParameter(titleRow, "runtimeMinutes", title.RuntimeMinutes);
                titleTable.Rows.Add(titleRow);
            }
            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn, 
                SqlBulkCopyOptions.KeepNulls, transAction);
            bulkCopy.DestinationTableName = "Titles";
            bulkCopy.WriteToServer(titleTable);
        }

        public void FillParameter(DataRow row, string columnName, object? value)
        {
            if (value != null)
            {
                row[columnName] = value;
            }
            else
            {
                row[columnName] = DBNull.Value;
            }
        }
    }
}
