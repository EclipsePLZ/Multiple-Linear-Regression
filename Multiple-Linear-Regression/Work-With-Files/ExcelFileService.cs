using DeepParameters.Work_WIth_Files.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using System.Data;

namespace DeepParameters.Work_WIth_Files {
    internal class ExcelFileService : IFileService{
        /// <summary>
        /// Read accident information from excel file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Array of all accidents</returns>
        public List<List<string>> Open(string filePath) {
            List<List<string>> allRows = new List<List<string>>();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read)) {
                using (var reader = ExcelReaderFactory.CreateReader(stream)) {
                    // Read data form excel sheet to dataTable
                    DataTable dt = reader.AsDataSet().Tables[0];

                    for (int row = 0; row < dt.Rows.Count; row++) {
                        // Add row values from dataTable to list
                        List<string> nextRow = new List<string>();
                        for (int col = 0; col < dt.Columns.Count; col++) {
                            nextRow.Add(dt.Rows[row][col].ToString());
                        }
                        allRows.Add(nextRow);
                    }
                }
            }
            return allRows;
        }
    }
}
