using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Text;

namespace Huffman.Common.Print
{
    public static class PrintDGV
    {
        private static StringFormat _strFormat;  // Holds content of a TextBox Cell to write by DrawString
        private static StringFormat _strFormatComboBox; // Holds content of a Boolean Cell to write by DrawImage
        private static Button _cellButton;       // Holds the Contents of Button Cell
        private static CheckBox _cellCheckBox;   // Holds the Contents of CheckBox Cell 
        private static ComboBox _cellComboBox;   // Holds the Contents of ComboBox Cell

        private static int _totalWidth;          // Summation of Columns widths
        private static int _rowPos;              // Position of currently printing row 
        private static bool _newPage;            // Indicates if a new page reached
        private static int _pageNo;              // Number of pages to print
        private static ArrayList _columnLefts = new ArrayList();  // Left Coordinate of Columns
        private static ArrayList _columnWidths = new ArrayList(); // Width of Columns
        private static ArrayList _columnTypes = new ArrayList();  // DataType of Columns
        private static int _cellHeight;          // Height of DataGrid Cell
        private static int _rowsPerPage;         // Number of Rows per Page
        private static System.Drawing.Printing.PrintDocument _printDoc =
                       new System.Drawing.Printing.PrintDocument();  // PrintDocumnet Object used for printing

        private static string _printTitle = "";  // Header of pages
        private static DataGridView _dataGridView;        // Holds DataGridView Object to print its contents
        private static List<string> _selectedColumns = new List<string>();   // The Columns Selected by user to print.
        private static List<string> _availableColumns = new List<string>();  // All Columns avaiable in DataGrid 
        private static bool _printAllRows = true;   // True = print all rows,  False = print selected rows    
        private static bool _fitToPageWidth = true; // True = Fits selected columns to page width ,  False = Print columns as showed    
        private static int _headerHeight = 0;

        public static void Print_DataGridView(DataGridView dgv, string title)
        {
            PrintPreviewDialog ppvw;
            try
            {
                // Getting DataGridView object to print
                _dataGridView = dgv;

                // Getting all Coulmns Names in the DataGridView
                _availableColumns.Clear();
                foreach (DataGridViewColumn c in _dataGridView.Columns)
                {
                    if (!c.Visible) continue;
                    _availableColumns.Add(c.HeaderText);
                }

                // Showing the PrintOption Form
                PrintOptions dlg = new PrintOptions(_availableColumns, title);
                if (dlg.ShowDialog() != DialogResult.OK) return;

                _printTitle = dlg.PrintTitle;
                _printAllRows = dlg.PrintAllRows;
                _fitToPageWidth = dlg.FitToPageWidth;
                _selectedColumns = dlg.GetSelectedColumns();

                _rowsPerPage = 0;

                ppvw = new PrintPreviewDialog();
                ppvw.Document = _printDoc;

                // Showing the Print Preview Page
                _printDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                _printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
                if (ppvw.ShowDialog() != DialogResult.OK)
                {
                    _printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                    _printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
                    return;
                }

                // Printing the Documnet
                _printDoc.Print();
                _printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                _printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private static void PrintDoc_BeginPrint(object sender,
                    System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                // Formatting the Content of Text Cell to print
                _strFormat = new StringFormat();
                _strFormat.Alignment = StringAlignment.Near;
                _strFormat.LineAlignment = StringAlignment.Center;
                _strFormat.Trimming = StringTrimming.EllipsisCharacter;
                _strFormat.FormatFlags |= StringFormatFlags.LineLimit;

                // Formatting the Content of Combo Cells to print
                _strFormatComboBox = new StringFormat();
                _strFormatComboBox.LineAlignment = StringAlignment.Center;
                _strFormatComboBox.FormatFlags = StringFormatFlags.NoWrap;
                _strFormatComboBox.Trimming = StringTrimming.EllipsisCharacter;

                _columnLefts.Clear();
                _columnWidths.Clear();
                _columnTypes.Clear();
                _cellHeight = 0;
                _rowsPerPage = 0;

                // For various column types
                _cellButton = new Button();
                _cellCheckBox = new CheckBox();
                _cellComboBox = new ComboBox();

                // Calculating Total Widths
                _totalWidth = 0;
                foreach (DataGridViewColumn gridCol in _dataGridView.Columns)
                {
                    if (!gridCol.Visible) continue;
                    if (!PrintDGV._selectedColumns.Contains(gridCol.HeaderText)) continue;
                    _totalWidth += gridCol.Width;
                }
                _pageNo = 1;
                _newPage = true;
                _rowPos = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void PrintDoc_PrintPage(object sender,
                    System.Drawing.Printing.PrintPageEventArgs e)
        {
            int tmpWidth, i;
            int tmpTop = e.MarginBounds.Top;
            int tmpLeft = e.MarginBounds.Left;

            try
            {
                // Before starting first page, it saves Width & Height of Headers and CoulmnType
                if (_pageNo == 1)
                {
                    foreach (DataGridViewColumn gridCol in _dataGridView.Columns)
                    {
                        if (!gridCol.Visible) continue;
                        // Skip if the current column not selected
                        if (!PrintDGV._selectedColumns.Contains(gridCol.HeaderText)) continue;

                        // Detemining whether the columns are fitted to page or not.
                        if (_fitToPageWidth)
                            tmpWidth = (int)(Math.Floor((double)((double)gridCol.Width *
                                       ((double)e.MarginBounds.Width / (double)_totalWidth))));
                        else
                            tmpWidth = gridCol.Width;

                        _headerHeight = (int)(e.Graphics.MeasureString(gridCol.HeaderText, gridCol.InheritedStyle.Font, tmpWidth).Height);

                        // Save width & height of headres and ColumnType
                        _columnLefts.Add(tmpLeft);
                        _columnWidths.Add(tmpWidth);
                        _columnTypes.Add(gridCol.GetType());
                        tmpLeft += tmpWidth;
                    }
                }

                // Printing Current Page, Row by Row
                while (_rowPos <= _dataGridView.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = _dataGridView.Rows[_rowPos];
                    if (GridRow.IsNewRow || (!_printAllRows && !GridRow.Selected))
                    {
                        _rowPos++;
                        continue;
                    }

                    _cellHeight = GridRow.Height;

                    if (tmpTop + _cellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        DrawFooter(e, _rowsPerPage);
                        _newPage = true;
                        _pageNo++;
                        e.HasMorePages = true;
                        return;
                    }
                    else
                    {
                        if (_newPage)
                        {
                            // Draw Header
                            e.Graphics.DrawString(_printTitle, new Font(_dataGridView.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                            e.Graphics.MeasureString(_printTitle, new Font(_dataGridView.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height);

                            String s = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();

                            e.Graphics.DrawString(s, new Font(_dataGridView.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(s, new Font(_dataGridView.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString(_printTitle, new Font(new Font(_dataGridView.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height);

                            // Draw Columns
                            tmpTop = e.MarginBounds.Top;
                            i = 0;
                            foreach (DataGridViewColumn gridCol in _dataGridView.Columns)
                            {
                                if (!gridCol.Visible) continue;
                                if (!PrintDGV._selectedColumns.Contains(gridCol.HeaderText))
                                    continue;

                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new Rectangle((int)_columnLefts[i], tmpTop,
                                    (int)_columnWidths[i], _headerHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)_columnLefts[i], tmpTop,
                                    (int)_columnWidths[i], _headerHeight));

                                e.Graphics.DrawString(gridCol.HeaderText, gridCol.InheritedStyle.Font,
                                    new SolidBrush(gridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)_columnLefts[i], tmpTop,
                                    (int)_columnWidths[i], _headerHeight), _strFormat);
                                i++;
                            }
                            _newPage = false;
                            tmpTop += _headerHeight;
                        }

                        // Draw Columns Contents
                        i = 0;
                        foreach (DataGridViewCell cel in GridRow.Cells)
                        {
                            if (!cel.OwningColumn.Visible) continue;
                            if (!_selectedColumns.Contains(cel.OwningColumn.HeaderText))
                                continue;

                            // For the TextBox Column
                            if (((Type)_columnTypes[i]).Name == "DataGridViewTextBoxColumn" ||
                                ((Type)_columnTypes[i]).Name == "DataGridViewLinkColumn")
                            {
                                e.Graphics.DrawString(cel.Value.ToString(), cel.InheritedStyle.Font,
                                        new SolidBrush(cel.InheritedStyle.ForeColor),
                                        new RectangleF((int)_columnLefts[i], (float)tmpTop,
                                        (int)_columnWidths[i], (float)_cellHeight), _strFormat);
                            }
                            // For the Button Column
                            else if (((Type)_columnTypes[i]).Name == "DataGridViewButtonColumn")
                            {
                                _cellButton.Text = cel.Value.ToString();
                                _cellButton.Size = new Size((int)_columnWidths[i], _cellHeight);
                                Bitmap bmp = new Bitmap(_cellButton.Width, _cellButton.Height);
                                _cellButton.DrawToBitmap(bmp, new Rectangle(0, 0,
                                        bmp.Width, bmp.Height));
                                e.Graphics.DrawImage(bmp, new Point((int)_columnLefts[i], tmpTop));
                            }
                            // For the CheckBox Column
                            else if (((Type)_columnTypes[i]).Name == "DataGridViewCheckBoxColumn")
                            {
                                _cellCheckBox.Size = new Size(14, 14);
                                _cellCheckBox.Checked = (bool)cel.Value;
                                Bitmap bmp = new Bitmap((int)_columnWidths[i], _cellHeight);
                                Graphics tmpGraphics = Graphics.FromImage(bmp);
                                tmpGraphics.FillRectangle(Brushes.White, new Rectangle(0, 0,
                                        bmp.Width, bmp.Height));
                                _cellCheckBox.DrawToBitmap(bmp,
                                        new Rectangle((int)((bmp.Width - _cellCheckBox.Width) / 2),
                                        (int)((bmp.Height - _cellCheckBox.Height) / 2),
                                        _cellCheckBox.Width, _cellCheckBox.Height));
                                e.Graphics.DrawImage(bmp, new Point((int)_columnLefts[i], tmpTop));
                            }
                            // For the ComboBox Column
                            else if (((Type)_columnTypes[i]).Name == "DataGridViewComboBoxColumn")
                            {
                                _cellComboBox.Size = new Size((int)_columnWidths[i], _cellHeight);
                                Bitmap bmp = new Bitmap(_cellComboBox.Width, _cellComboBox.Height);
                                _cellComboBox.DrawToBitmap(bmp, new Rectangle(0, 0,
                                        bmp.Width, bmp.Height));
                                e.Graphics.DrawImage(bmp, new Point((int)_columnLefts[i], tmpTop));
                                e.Graphics.DrawString(cel.Value.ToString(), cel.InheritedStyle.Font,
                                        new SolidBrush(cel.InheritedStyle.ForeColor),
                                        new RectangleF((int)_columnLefts[i], tmpTop, (int)_columnWidths[i], _cellHeight), _strFormatComboBox);
                            }
                            // For the Image Column
                            else if (((Type)_columnTypes[i]).Name == "DataGridViewImageColumn")
                            {
                                Rectangle CelSize = new Rectangle((int)_columnLefts[i],
                                        tmpTop, (int)_columnWidths[i], _cellHeight);
                                Size ImgSize = ((Image)(cel.FormattedValue)).Size;
                                e.Graphics.DrawImage((Image)cel.FormattedValue,
                                        new Rectangle((int)_columnLefts[i] + (int)((CelSize.Width - ImgSize.Width) / 2),
                                        tmpTop + (int)((CelSize.Height - ImgSize.Height) / 2),
                                        ((Image)(cel.FormattedValue)).Width, ((Image)(cel.FormattedValue)).Height));

                            }

                            // Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)_columnLefts[i],
                                    tmpTop, (int)_columnWidths[i], _cellHeight));

                            i++;

                        }
                        tmpTop += _cellHeight;
                    }

                    _rowPos++;
                    // For the first page it calculates Rows per Page
                    if (_pageNo == 1) _rowsPerPage++;
                }

                if (_rowsPerPage == 0) return;

                // Write Footer (Page Number)
                DrawFooter(e, _rowsPerPage);

                e.HasMorePages = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void DrawFooter(System.Drawing.Printing.PrintPageEventArgs e,
                    int RowsPerPage)
        {
            double cnt = 0;

            // Detemining rows number to print
            if (_printAllRows)
            {
                if (_dataGridView.Rows[_dataGridView.Rows.Count - 1].IsNewRow)
                    cnt = _dataGridView.Rows.Count - 2; // When the DataGridView doesn't allow adding rows
                else
                    cnt = _dataGridView.Rows.Count - 1; // When the DataGridView allows adding rows
            }
            else
                cnt = _dataGridView.SelectedRows.Count;

            // Writing the Page Number on the Bottom of Page
            string PageNum = " µÚ " + _pageNo.ToString()
                           + " Ò³£¬¹² " + Math.Ceiling((double)(cnt / RowsPerPage)).ToString()
                           + " Ò³";

            e.Graphics.DrawString(PageNum, _dataGridView.Font, Brushes.Black,
                e.MarginBounds.Left + (e.MarginBounds.Width -
                e.Graphics.MeasureString(PageNum, _dataGridView.Font,
                e.MarginBounds.Width).Width) / 2, e.MarginBounds.Top +
                e.MarginBounds.Height + 31);
        }
    }
}
