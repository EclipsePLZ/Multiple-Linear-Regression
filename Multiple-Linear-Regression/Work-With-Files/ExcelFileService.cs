using Multiple_Linear_Regression.Work_WIth_Files.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using OfficeOpenXml;
using System.Data;

namespace Multiple_Linear_Regression.Work_WIth_Files {
    internal class ExcelFileService : IFileService{
        /// <summary>
        /// Read accident information from excel file
        /// </summary>
        /// <param name="filePath">Path to file</param>
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

        /// <summary>
        /// Save rows as excel file
        /// </summary>
        /// <param name="filename">Path to file</param>
        /// <param name="rows">List of rows</param>
        public void Save(string filename, List<List<string>> rows) {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage()) {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Результат управления");
                for (int row = 0; row < rows.Count; row++) { 
                    for (int col = 0; col < rows[row].Count; col++) {

                        // Check if it is a number
                        if (double.TryParse(rows[row][col], out _)) {
                            ws.Cells[row + 1, col + 1].Value = Convert.ToDouble(rows[row][col]);
                        }
                        else {
                            ws.Cells[row + 1, col + 1].Value = rows[row][col];
                        }
                        
                    }
                }

                // Rewrite file if it's already exist
                if (File.Exists(filename)) {
                    File.Delete(filename);
                }
                File.WriteAllBytes(filename, package.GetAsByteArray());
            }
        }
    }
}
