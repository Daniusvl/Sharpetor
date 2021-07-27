using Avalonia.Controls;
using System.Collections.ObjectModel;

namespace Sharpetor.UILib
{
    public class FileExplorerItemViewModel : BaseViewModel
    {
        public enum ItemType
        {
            Folder,
            File,
            CSharpFile,
            Project,
            Solution
        }

        private ObservableCollection<FileExplorerItemViewModel> children;

        public ObservableCollection<FileExplorerItemViewModel> Children
        {
            get => children;
            set
            {
                children = value;
                OnPropertyChanged();
            }
        }

        public FileExplorerItemViewModel(ItemType type, string path, string itemName, string imageSource) =>
            (Type, Path, ItemName, ImageSource) = (type, path, itemName, imageSource);

        public ItemType Type { get; set; }

        public string Path { get; set; }

        private string itemName;

        public string ItemName 
        { 
            get => itemName;
            set
            {
                itemName = value;
                OnPropertyChanged();
            }
        }

        private string imageSource;
        public string ImageSource 
        { 
            get => imageSource;
            set
            {
                imageSource = value;
                OnPropertyChanged();
            }
        }

        private bool isExpanded;

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                OnPropertyChanged();
            }
        }

        private ContextMenu contextMenu;
        public ContextMenu ContextMenu 
        {
            get => contextMenu;
            set
            {
                contextMenu = value;
                OnPropertyChanged();
            }
        }
    }
}
