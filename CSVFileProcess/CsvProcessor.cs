using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CSVFileProcess
{

    /// <summary>
    /// This implemetations of the ICsvFile interface Reads the CSV file once, stores the resulting data table 
    /// in the field _dtCsv and uses it to return the values for each nethod (Read CSV, Row Count, List of Columns
    /// and Value by Row Index/Column Name
    /// </summary>
    internal class CsvProcessor : ICsvFile
    {
        private DataTable _dtCsv;
        public CsvProcessor(string csvFileName)
        {
            _dtCsv = GetDSFromCSV(csvFileName);
        }

        /// <summary>
        /// Returns list of column names in the CSV file
        /// </summary>
        /// <returns></returns>
        public List<string> GetColumnCollection()
        {
            List<string> listColumns = new List<string>();
            foreach (DataColumn column in _dtCsv.Columns)
            {
                listColumns.Add(column.ColumnName.ToString());
            }

            return listColumns;
        }

        /// <summary>
        /// Returns the number of rows in the CSV file
        /// </summary>
        /// <returns></returns>
        public int GetRowCount()
        {
            int rowCount = 0;
            rowCount = _dtCsv.Rows.Count;
            return rowCount;

        }

        /// <summary>
        /// Returns the value based on the row index and the column name
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string GetValueByRowAndColumn(int rowIndex, string columnName)
        {
            string fieldContent = string.Empty;


            try
            {
                Object cellValue = _dtCsv.Rows[rowIndex][columnName];
                fieldContent = (string)cellValue;
            }
            catch (Exception ex)
            {
                //If row index or column name don't exist
                fieldContent = "Exception: " + ex.Message;
            }


            return fieldContent;
        }

        /// <summary>
        /// Returns the data table containing all the values, rows and columns in the CSV
        /// </summary>
        /// <returns></returns>
        public DataTable ReadCsvFile()
        {
            return _dtCsv;
        }


        /// <summary>
        /// Returns the DataSet from the CSV file
        /// </summary>
        /// <param name="csvFile"></param>
        /// <returns></returns>
        private DataTable GetDSFromCSV(string csvFile)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(csvFile))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(new DataColumn(header));
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }

            }

            return dt;
        }


    }
}
