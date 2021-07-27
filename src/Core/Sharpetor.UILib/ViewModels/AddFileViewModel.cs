using System;

namespace Sharpetor.UILib
{
    public class AddFileViewModel : BaseViewModel
    {
        public Command AddCmd { get; }

        public Action CloseThis;

        public Action<string> GetName;

        private void ExecuteAdd(object parameter)
        {
            if (string.IsNullOrEmpty(Name))
            {
                ErrorMessage = "Enter file/folder name";
            }
            else
            {
                GetName?.Invoke(Name);
                CloseThis?.Invoke();
            }
        }

        public AddFileViewModel()
        {
            AddCmd = new(ExecuteAdd);
        }

        private string errorMessage;

        public string ErrorMessage
        {
            get => errorMessage;
            set 
            { 
                errorMessage = value;
                OnPropertyChanged();
            }
        }


        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

    }
}
