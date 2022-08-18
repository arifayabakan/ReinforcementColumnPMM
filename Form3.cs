using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {

        private void button2_Click(object sender, EventArgs e)
        {
            StaticLabels.excellData.Rows.Clear();
            StaticLabels.CreateTitleRowForExcellData();

            for (int i = 0; i < StaticLabels.pLabels.Count; i++)
            {
                StaticLabels.excellData.Rows[0][i + 1] = StaticLabels.pLabels[i].Text;
            }

            for (int i = 0; i < StaticLabels.myLabels.Count; i++)
            {
                StaticLabels.excellData.Rows[1][i + 1] = StaticLabels.myLabels[i].Text;
            }

            for (int i = 0; i < StaticLabels.mzLabels.Count; i++)
            {
                StaticLabels.excellData.Rows[2][i + 1] = StaticLabels.mzLabels[i].Text;
            }

            for (int i = 0; i < StaticLabels.mbLabels.Count; i++)
            {
                StaticLabels.excellData.Rows[3][i + 1] = StaticLabels.mbLabels[i].Text;
            }

            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            try
            {
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "ExportedFromDatGrid";
                int cellRowIndex = 1;
                int cellColumnIndex = 1;

                //Loop through each row and read value from each column. 
                for (int i = 0; i < StaticLabels.excellData.Rows.Count; i++)
                {
                    for (int j = StaticLabels.excellData.Columns.Count; j > 0; j--)
                    {
                        worksheet.Cells[cellRowIndex, cellColumnIndex] = StaticLabels.excellData.Rows[i][j-1].ToString();
                        cellColumnIndex++;
                    }
                    cellColumnIndex = 1;
                    cellRowIndex++;
                }

                //Getting the location and file name of the excel to save from user. 
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FilterIndex = 2;

                if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    MessageBox.Show("Export Successful");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                excel.Quit();
                workbook = null;
                excel = null;
            }
        }


    }
}
