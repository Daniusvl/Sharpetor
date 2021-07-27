using Prism.Events;
using System;
using Avalonia.Controls;
using System.IO;
using System.Linq;
using System.Collections.ObjectModel;

namespace Sharpetor.UILib
{
    public class LeftPanelViewModel : BaseViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly FileExplorerLoader _fileExplorerLoader;

        private Window mainWindow;
        public Window MainWindow { get => mainWindow; set 
            {
                mainWindow = value;
                MainWindow.PropertyChanged += MainWindow_PropertyChanged;
                FileExplorerHeight = MainWindow.Height - 100;
            } }

        public LeftPanelViewModel(IEventAggregator eventAggregator)
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
            void AddFile()
            {
                if (Directory.Exists(path) && !File.Exists(name) && !string.IsNullOrEmpty(name))
                {
                    if (Path.EndsWith('\\'))
                        path = new string(path.SkipLast(1).ToArray());
                    File.Create($@"{path}\{name}");
                    ProjectFiles = _fileExplorerLoader.Reload(ProjectFiles);
                }
            }

            if (name.Contains('\\') && Directory.Exists(path))
            {
                path += @$"\{string.Join('\\', name.Split('\\').SkipLast(1))}";
                Directory.CreateDirectory(path);
                name = name.Split('\\').Last();
                AddFile();
            }
            else AddFile();
        }

        public void NewFolder(string path, string name)
        {
            Directory.CreateDirectory($@"{path}\{name}");
            ProjectFiles = _fileExplorerLoader.Reload(ProjectFiles);
        }

        public void RenameFolderProjectSolution(string path, string newName)
        {
            string newPath = @$"{string.Join('\\', path.Split('\\').SkipLast(1))}\{newName}";
            if (!Directory.Exists(newPath))
                Directory.Move(path, newPath);
            ProjectFiles = _fileExplorerLoader.Reload(ProjectFiles);
        }

        public void RenameFile(string path, string newName)
        {
            string newPath = @$"{string.Join('\\', path.Split('\\').SkipLast(1))}\{newName}";
            if (!File.Exists(newPath))
                File.Move(path, newPath);
            ProjectFiles = _fileExplorerLoader.Reload(ProjectFiles);
        }

        private void DeleteFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
            ProjectFiles = _fileExplorerLoader.Reload(ProjectFiles);
        }

        private void DeleteFolder(string path)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, true);

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
