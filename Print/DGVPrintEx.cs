using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;

namespace Huffman.Common.Print
{
    public class DGVPrintEx
    {
        protected DataGridViewPrinter _thePrinter = null;

        private DGVPrintEx() { }

        protected void _printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.HasMorePages = _thePrinter.DrawDataGridView(e.Graphics);
        }

        public static void Print_DataGridView(DataGridView aDataGridView, bool CenterOnPage, bool WithTitle, string aTitleText, Font aTitleFont, Color aTitleColor, bool WithPaging)
        {
            try
            {
                DGVPrintEx instance = new DGVPrintEx();

                PrintDocument printDoc = new PrintDocument();
                printDoc.DocumentName = aTitleText;
                printDoc.PrintPage += new PrintPageEventHandler(instance._printDoc_PrintPage);

                instance._thePrinter = new DataGridViewPrinter(aDataGridView, printDoc, CenterOnPage, WithTitle, aTitleText, aTitleFont, aTitleColor, WithPaging);

                PrintPreviewDialog dlg = new PrintPreviewDialog();
                dlg.Document = printDoc;
                dlg.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, aTitleText, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
