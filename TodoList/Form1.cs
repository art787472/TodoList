using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TodoList.Controller;
using TodoList.Model;
using TodoList.Utility;

namespace TodoList
{
    public partial class Form1 : Form
    {
        private TodoController _controller;
        private BindingSource _bindingSource = new BindingSource();
        public TodoController Controller
        { 
            get { return _controller; }
            set 
            { 
                _controller = value;
                
            }
        
        }
        public Form1()
        {
            InitializeComponent();
            this._controller = new TodoController(this);
            this._controller.GetTodos();
            this._bindingSource.DataSource = _controller.list;
            dataGridView1.DataSource = this._bindingSource;

            comboBox1.DataSource = new string[]
            {
                "全部",
                "完成",
                "未完成"
            };
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.AddDeleteColumn();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TodoItem newTodo = new TodoItem();
            newTodo.ID = Guid.NewGuid().ToString();
            newTodo.Title = textBox1.Text;
            newTodo.IsDone = false;

           

            this._controller.AddTodo(newTodo);
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue is string value)
            {
                switch (value)
                {
                    case "全部":
                        _controller.SwitchDisplay(DisplayCategory.DisplayAll);
                        break;
                    case "完成":
                        _controller.SwitchDisplay(DisplayCategory.DisplayDone);
                        break;
                    case "未完成":
                        _controller.SwitchDisplay(DisplayCategory.DisplayUndone);
                        break;
                }
            }
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (dataGridView.Columns[e.ColumnIndex].Name == "DeleteColumn")
            {
                string id = (string)dataGridView.Rows[e.RowIndex].Cells[1].Value;
                _controller.DeleteTodo(id);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            string editedData = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            _controller.EditTodo();
        }

        

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (e.ColumnIndex < 0 || e.ColumnIndex > dataGridView.ColumnCount)
                return;
            if (dataGridView.Columns[e.ColumnIndex].Name == "DeleteColumn")
            {
                Cursor = Cursors.Hand;
            }
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (e.ColumnIndex < 0 || e.ColumnIndex > dataGridView.ColumnCount)
                return;
            if (dataGridView.Columns[e.ColumnIndex].Name == "DeleteColumn")
            {
                Cursor = Cursors.Arrow;
            }
        }
    }
}
