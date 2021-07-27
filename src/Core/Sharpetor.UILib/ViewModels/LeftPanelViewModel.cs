using Prism.Events;
using System;
using Avalonia.Controls;
using System.IO;
using System.Linq;
using System.Collections.ObjectModel;
using Sharpetor.CoreLib;

namespace Sharpetor.UILib
{
    public class LeftPanelViewModel : BaseViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IFileExplorerService _fileExplorerService;

        private readonly FileExplorerLoader _fileExplorerLoader;

        private Window mainWindow;
        public Window MainWindow { get => mainWindow; set 
            {
                mainWindow = value;
                MainWindow.PropertyChanged += MainWindow_PropertyChanged;
                FileExplorerHeight = MainWindow.Height - 100;
            } }

        public LeftPanelViewModel(IEventAggregator eventAggregator, IFileExplorerService fileExplorerService)
        {
            _eventAggregator = eventAggregator ?? throw new NullReferenceException(nameof(eventAggregator));
            _eventAggregator.GetEvent<ProjectCreatedEvent>().Subscribe(path =>
            {
                // If StartsWith, it means project is created in an already opened solution and path should not be changed.
                if (!string.IsNullOrEmpty(path) && path.StartsWith(Path) && Directory.Exists(Path))
                {
                    ProjectFiles = _fileExplorerLoader?.Reload(ProjectFiles);
                }
                else
                {
                    Path = path;
                }
            });

            _fileExplorerService = fileExplorerService ?? throw new NullReferenceException(nameof(fileExplorerService));

            ContextMenuEvents.DeleteFileClick = DeleteFile;
            ContextMenuEvents.DeleteFolderClick = DeleteFolder;

            _fileExplorerLoader = new FileExplorerLoader();
            _fileExplorerLoader.Path = Path;

            Path = string.Empty;

            ProjectFiles = _fileExplorerLoader.Load();
        }

        private void MainWindow_PropertyChanged(object sender, Avalonia.AvaloniaPropertyChangedEventArgs e)
        {
            if(e.Property.Name == "Height")
            {
                FileExplorerHeight = (double)e.NewValue - 100;
            }
        }

        public void NewFile(string path, string name)
        {
            _fileExplorerService.NewFile(path, name);

            ProjectFiles = _fileExplorerLoader.Reload(ProjectFiles);
        }

        public void NewFolder(string path, string name)
        {
            _fileExplorerService.NewFolder(path, name);

            ProjectFiles = _fileExplorerLoader.Reload(ProjectFiles);
        }

        public void RenameFolderProjectSolution(string path, string newName)
        {
            _fileExplorerService.RenameFolderProjectSolution(path, newName);

            ProjectFiles = _fileExplorerLoader.Reload(ProjectFiles);
        }

        public void RenameFile(string path, string newName)
        {
            _fileExplorerService.RenameFile(path, newName);

            ProjectFiles = _fileExplorerLoader.Reload(ProjectFiles);
        }

        private void DeleteFile(string path)
        {
            _fileExplorerService.DeleteFile(path);

            ProjectFiles = _fileExplorerLoader.Reload(ProjectFiles);
        }

        private void DeleteFolder(string path)
        {
            _fileExplorerService.DeleteFolder(path);

            ProjectFiles = _fileExplorerLoader.Reload(ProjectFiles);
        }

        private double fileExplorerHeight;

        public double FileExplorerHeight 
        { 
            get => fileExplorerHeight; 
            set
            {
                fileExplorerHeight = value;
                OnPropertyChanged();
            }
        }

        private string path;

        public string Path
        {
            get => path;
            set {
                path = value;
                OnPropertyChanged();
                _fileExplorerLoader.Path = Path;
                ProjectFiles = _fileExplorerLoader.Load();
            }
        }

        private ObservableCollection<FileExplorerItemViewModel> projectFiles;

        public ObservableCollection<FileExplorerItemViewModel> ProjectFiles
        {
            get => projectFiles;
            set { 
                projectFiles = value;
                OnPropertyChanged();
            }
        }

    }
}
