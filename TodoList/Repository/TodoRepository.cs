using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Model;

namespace TodoList.Repository
{
    internal class TodoRepository
    {
        public List<TodoItem> GetTodoItems()
        {
            var path = ConfigurationManager.AppSettings["dataPath"];
            
            if(!File.Exists(path))
            {
                return null;
            }
            var list = CSVLibrary.CSVHelper.Read<TodoItem>(path);
            return list;
        }

        public void AddTodoItem(TodoItem newTodo)
        {
            var path = ConfigurationManager.AppSettings["dataPath"];
            CSVLibrary.CSVHelper.Write<TodoItem>(newTodo, path);
        }

        public void DeleteTodoItem(TodoItem todo) 
        {
            var path = ConfigurationManager.AppSettings["dataPath"];
            var list = CSVLibrary.CSVHelper.Read<TodoItem>(path);

            var todoItem = list.FirstOrDefault(x => x.ID == todo.ID);
            list.Remove(todoItem);

            File.Delete(path);
            CSVLibrary.CSVHelper.Write<TodoItem>(list, path);
        }

        public void EditTodoItem(List<TodoItem> list)
        {
            var path = ConfigurationManager.AppSettings["dataPath"];
            File.Delete(path);

            CSVLibrary.CSVHelper.Write<TodoItem>(list, path);
        }


    }
}
