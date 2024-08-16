using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoList.Utility
{
    internal static class Extension
    {
        public static void AddDeleteColumn(this DataGridView dataGridView)
        {
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "DeleteColumn";
            imageColumn.HeaderText = "Delete";
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;

            

            string assetsPath = ConfigurationManager.AppSettings["assetsPath"];
            var trashCanImagePath = Path.Combine(assetsPath, @"trash-can.png");

            dataGridView.Columns.Add(imageColumn);
            for (int row = 0; row < dataGridView.Rows.Count; row++)
            {
                var cell = ((DataGridViewImageCell)dataGridView.Rows[row].Cells[dataGridView.ColumnCount - 1]);
                cell.Value = Image.FromFile(trashCanImagePath);
                
            }
        }
    }
}
