using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Model
{
    public class TodoItem : INotifyPropertyChanged
    {
        private string _id;
        public string ID
        {
            get => _id;
            set
            {
                _id = value;
            }
        }

        private string _title;
        [DisplayName("標題")]
        public string Title         
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        private bool _isDone;
        [DisplayName("完成")]
        public bool IsDone
        {
            get => _isDone;
            set
            {
                _isDone = value;
                OnPropertyChanged("IsDone");
           }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}
