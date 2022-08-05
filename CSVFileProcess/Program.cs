using System;
using System.Collections.Generic;
using System.Data;
using System.IO;


namespace CSVFileProcess
{
    internal class Program
    {
        static void Main(string[] args)
        {



            string csvFile1 = GetFileFolderPath("addresses.csv");
            string csvFile2 = GetFileFolderPath("grades.csv");
            string csvFile3 = GetFileFolderPath("airtravel.csv");


            //Pushing dependency for CsvProcessor Class 

            CsvProcessor csvProc = new CsvProcessor(csvFile1);
            CsvHandler csvHand1 = new CsvHandler(csvProc);

            //Output for CsvProcessor class methods
            DataTable dtCsv1 = csvHand1.ReadCsvFile();
            int rc1 = csvHand1.GetRowCount();
            List<string> ls1 = csvHand1.GetColumnCollection();
            string celVal1 = csvHand1.GetValueByRowAndColumn(2, "LastName");



            //Pushing dependency for CsvProcessorV2 Class 

            CsvProcessorV2 csvProcV2 = new CsvProcessorV2(csvFile2);
            CsvHandler csvHand2 = new CsvHandler(csvProcV2);

            //Output for CsvProcessorV2 class methods
            DataTable dtCsv2 = csvHand2.ReadCsvFile();
            int rc2 = csvHand2.GetRowCount();
            List<string> ls2 = csvHand2.GetColumnCollection();
            string celVal2 = csvHand2.GetValueByRowAndColumn(7, "SSN");



            Console.ReadLine();
        }


        /// <summary>
        /// Returns the full path for the file name, as stored in the  CSVFiles folder
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string GetFileFolderPath(string fileName)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string csvFileFolder = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "\\CSVFiles\\";
            string csvFileName = csvFileFolder + fileName;
            return csvFileName;
        }
    }

    /// <summary>
    /// This Class will be use to pass and call the dependency for ICsvFile 
    /// </summary>
    class CsvHandler
    {
        ICsvFile _action = null;

        public CsvHandler(ICsvFile csvImp)
        {
            _action = csvImp;
        }
        
        public DataTable ReadCsvFile()
        {
            DataTable dtCsv = _action.ReadCsvFile();
            return dtCsv;
        }

        public int GetRowCount()
        {
            int rowCount = _action.GetRowCount();
            return rowCount;
        }

        public List<string> GetColumnCollection()
        {
            List<string> listCol = _action.GetColumnCollection();
            return listCol;
        }

        public string GetValueByRowAndColumn(int rowIndex, string columnName)
        {
            string cellVal = _action.GetValueByRowAndColumn(rowIndex, columnName);
            return cellVal;
        }
    }

   

}