using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace CSVFileProcess
{
    /// <summary>
    /// This implementation of ICsvFile does not store the data table in a field;
    /// It reads the CSV file for each operation (Read, RowCount, List of Columns and Value by Row Index/Column Name
    /// </summary>
    internal class CsvProcessorV2 : ICsvFile
    {
        private string _csvFileName;
        public CsvProcessorV2(string csvFileName)
        {
            _csvFileName = csvFileName;
        }

        /// <summary>
        /// Returns list of column names in the CSV file
        /// </summary>
        /// <returns></returns>
        public List<string> GetColumnCollection()
        {
            DataTable dt = GetDTFromCSV(_csvFileName);
            List<string> listColumns = new List<string>();
            foreach (DataColumn column in dt.Columns)
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
            DataTable dataTable = GetDTFromCSV(_csvFileName);
            int rowCount = 0;
            rowCount = dataTable.Rows.Count;
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

            DataTable dataTable = GetDTFromCSV(_csvFileName);
            string fieldContent = string.Empty;


            try
            {
                Object cellValue = dataTable.Rows[rowIndex][columnName];
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
            DataTable dataTable = GetDTFromCSV(_csvFileName);
            return dataTable;
        }


        /// <summary>
        /// Returns the DataSet from the CSV file
        /// </summary>
        /// <param name="csvFile"></param>
        /// <returns></returns>
        private DataTable GetDTFromCSV(string csvFile)
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
