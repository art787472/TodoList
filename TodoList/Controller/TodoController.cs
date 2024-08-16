using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Model;
using TodoList.Repository;

namespace TodoList.Controller
{
    public class TodoController
    {
        public Form1 View;
        
        private TodoRepository repository;
        public BindingList<TodoItem> list = new BindingList<TodoItem>();

        public TodoController(Form1 view)
        {
            repository = new TodoRepository();
            
            this.View = view;

            this.View.Controller = this;
        }

        public List<TodoItem> GetTodos()
        {
            var todos = repository.GetTodoItems();
            if (todos != null)
            {
                todos.ForEach(x =>
                {
                    list.Add(x);
                });

            }
            return todos;
        }

        public void AddTodo(TodoItem todoItem)
        {
            list.Add(todoItem);
            
            repository.AddTodoItem(todoItem);
        }

        public void SwitchDisplay(DisplayCategory displayCategory)
        {
            var allItems = repository.GetTodoItems();
            if (allItems == null)
                return;
            switch (displayCategory) 
            {
                case DisplayCategory.DisplayAll:
                    list.Clear();
                    allItems.ForEach(x =>
                    {
                        list.Add(x);
                    });
                    break;
                case DisplayCategory.DisplayDone:
                    list.Clear();
                    allItems.Where(x => x.IsDone).ToList().ForEach(x =>
                    {
                        list.Add(x);
                    });
                    break;
                case DisplayCategory.DisplayUndone:
                    list.Clear();
                    allItems.Where(x => !x.IsDone).ToList().ForEach(x =>
                    {
                        list.Add(x);
                    });
                    break;
            }
        }

        public void DeleteTodo(string id)
        {
            var todoItem = list.FirstOrDefault(x => x.ID == id);
            list.Remove(todoItem);
            
            repository.DeleteTodoItem(todoItem);

        }

        public void EditTodo()
        {
            repository.EditTodoItem(list.ToList());
        }
    }
}
