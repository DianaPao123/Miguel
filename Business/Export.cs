using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using log4net;
using log4net.Config;

namespace Business 
{
    public class Export 
    {

        protected static ILog Logger = LogManager.GetLogger(typeof(Export));

        public Export()
        {
            XmlConfigurator.Configure();
        }

        private static Type[] numericTypes = new[] {typeof(Nullable<Decimal>),
            typeof(Decimal),typeof(Double),typeof(Nullable<Double>),
            typeof(Int16),typeof(Nullable<Int16>), typeof(Int32), typeof(Nullable<Int32>), typeof(Int64),typeof(Nullable<Int64>), typeof(SByte),
            typeof(Nullable<SByte>)};


        private static bool IsNumeric(DataGridViewColumn col)
        {
            if (col == null)
                return false;
           
            return numericTypes.Contains(col.ValueType);
        }


        public void GridToExcel(DataGridView grid, string sheetName, string fileName, List<string> columnas)
        {
            try
            {
                var a = new DataGridView();
                foreach (var columna in columnas)
                {
                    a.Columns.Add(columna, columna);
                }


                for (int s = 0; s <= grid.Rows.Count - 1; s++)
                {
                    foreach (var columna in columnas)
                    {
                        a.Rows.Add();
                        a.Rows[s].Cells[columna].Value = grid.Rows[s].Cells[columna].Value;
                    }
                }

                ExcelPackage ep = new ExcelPackage();
                ep.Workbook.Worksheets.Add(sheetName);
                ExcelWorksheet ew = ep.Workbook.Worksheets[1];
                ew.Name = sheetName;
                ew.Cells.Style.Font.Name = "Arial";
                ew.Cells.Style.Font.Size = 8;
                int i = 0;
                foreach (var s  in columnas)
                {
                    ew.Cells[1, i + 1].Value = s;
                    ew.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.LightGray;
                    ew.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    i++;
                    
                }
                foreach (DataGridViewRow row in a.Rows)
                {
                    foreach (DataGridViewColumn column in a.Columns)
                    {
                        if (columnas.Any(p=>p == column.HeaderText))
                        {
                            if (column.HeaderText == "SubTotal" || column.HeaderText == "IVA" || column.HeaderText == "Total")
                            {
                                ew.Cells[row.Index + 2, column.Index + 1].Style.Numberformat.Format = "0.00";
                                ew.Cells[row.Index + 2, column.Index + 1].Value = row.Cells[column.Name].Value;
                            }
                            else
                            {
                                string valor = row.Cells[column.Index].Value == null ? "" : row.Cells[column.Index].Value.ToString();
                                ew.Cells[row.Index + 2, column.Index + 1].Value = valor;
                            }
                        }
                        
                    }
                }
                if (File.Exists(fileName))
                    File.Delete(fileName);
                ep.SaveAs(new FileStream(fileName, FileMode.Create));
                ep.Dispose();
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
            }

        }

        public void GridToExcel(DataGridView grid, string sheetName, string fileName)
        {
            try
            {
                ExcelPackage ep = new ExcelPackage();
                ep.Workbook.Worksheets.Add(sheetName);
                ExcelWorksheet ew = ep.Workbook.Worksheets[1];
                ew.Name = sheetName;
                ew.Cells.Style.Font.Name = "Arial";
                ew.Cells.Style.Font.Size = 8;
                foreach (DataGridViewColumn col in grid.Columns)
                {
                    ew.Cells[1, col.Index + 1].Value = col.HeaderText;
                    ew.Cells[1, col.Index + 1].Style.Fill.PatternType = ExcelFillStyle.LightGray;
                    ew.Cells[1, col.Index + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }
                foreach (DataGridViewRow row in grid.Rows)
                {
                    foreach (DataGridViewColumn column in grid.Columns)
                    {
                        if (IsNumeric(column))
                        {
                            ew.Cells[row.Index + 2, column.Index + 1].Style.Numberformat.Format = "0.00";
                            ew.Cells[row.Index + 2, column.Index + 1].Value = row.Cells[column.Name].Value; 
                        }
                        else
                        {
                            string valor = row.Cells[column.Index].Value == null ? "": row.Cells[column.Index].Value.ToString();
                            ew.Cells[row.Index + 2, column.Index + 1].Value = valor; 
                        }
                    }
                }
                if(File.Exists(fileName))
                    File.Delete(fileName);
                ep.SaveAs(new FileStream(fileName,FileMode.Create));
                ep.Dispose();
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
            }
            
        }

        public byte[] GridToExcel(GridView grid, string sheetName)
        {
            try
            {
                grid.AllowPaging = false;
                var ep = new ExcelPackage();
                ep.Workbook.Worksheets.Add(sheetName);
                ExcelWorksheet ew = ep.Workbook.Worksheets[1];
                ew.Name = sheetName;
                ew.Cells.Style.Font.Name = "Arial";
                ew.Cells.Style.Font.Size = 8;
                for (int x = 0; x < grid.Columns.Count; x++ )
                {
                    ew.Cells[1, x + 1].Value = grid.Columns[x].HeaderText;
                    ew.Cells[1, x + 1].Style.Fill.PatternType = ExcelFillStyle.LightGray;
                    ew.Cells[1, x + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }
                int y = 0;
                foreach (GridViewRow row in grid.Rows)
                {
                    int z = 0;
                    foreach (TableCell cell in row.Cells)
                    {
                        ew.Cells[y + 2, z + 1].Style.Numberformat.Format = "0.00";
                        ew.Cells[y + 2, z + 1].Value =
                            cell.Text.Replace("&#209;", "Ñ").Replace("&#225;", "á").Replace("&#233;", "é").Replace(
                                "&#237;", "í").Replace("&#243;", "ó").Replace("&#250;", "ú").Replace("&#193;", "Á").
                                Replace("&#201;", "É").Replace("&#205;", "Í").Replace("&#211;", "Ó").Replace("&#218;","Ú").
                                Replace("&#241;", "ñ").Replace("&nbsp;", " ");
                        z++;
                    }
                    y++;
                }
                var res = ep.GetAsByteArray();
                ep.Dispose();
                return res;
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
            }
            return null;
        }
    }
}
