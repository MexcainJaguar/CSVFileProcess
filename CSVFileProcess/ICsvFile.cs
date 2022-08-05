using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVFileProcess
{
    internal interface ICsvFile
    {
        DataTable ReadCsvFile();
        int GetRowCount();
        List<string> GetColumnCollection();
        string GetValueByRowAndColumn(int rowIndex, string columnName);
    }
}
