using Avalonia.Controls;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Sharpetor.UILib
{
    public class FileExplorerLoader
    {
        public string Path { get; set; }

        public ObservableCollection<FileExplorerItemViewModel> Reload(ObservableCollection<FileExplorerItemViewModel> previous)
        {
            return Load().SyncIsExpanded(previous);
        }

        private FileExplorerItemViewModel LoadFile(string path)
        {
            FileExplorerItemViewModel.ItemType type;
            string imageSource;
            if (path.EndsWith(".cs"))
            {
                type = FileExplorerItemViewModel.ItemType.CSharpFile;
                imageSource = ImagePathConstants.CSharpFile;
            }
            else
            {
                type = FileExplorerItemViewModel.ItemType.File;
                imageSource = ImagePathConstants.File;
            }

            FileExplorerItemViewModel item = new(type, path, path.Split('\\').Last(), imageSource);
            item.ContextMenu = ContextMenuFactory.FileContextMenu(path);
            return item;
        }

        private FileExplorerItemViewModel LoadDirectory(string path)
        {
            FileExplorerItemViewModel.ItemType type;
            string imageSource;
            ContextMenu contextMenu;

            if (FileExplorerExtensions.IsSolution(path))
            {
                type = FileExplorerItemViewModel.ItemType.Solution;
                imageSource = ImagePathConstants.Solution;
                contextMenu = ContextMenuFactory.FolderSolutionContextMenu(path);
            }
            else if (FileExplorerExtensions.IsProject(path, Path))
            {
                type = FileExplorerItemViewModel.ItemType.Project;
                imageSource = ImagePathConstants.Project;
                contextMenu = ContextMenuFactory.ProjectFolderContextMenu(path);
            }
            else if (FileExplorerExtensions.IsInProject(path, Path))
            {
                type = FileExplorerItemViewModel.ItemType.Folder;
                imageSource = ImagePathConstants.Folder;
                contextMenu = ContextMenuFactory.ProjectFolderContextMenu(path);
            }
            else
            {
                type = FileExplorerItemViewModel.ItemType.Folder;
                imageSource = ImagePathConstants.Folder;
                contextMenu = ContextMenuFactory.FolderSolutionContextMenu(path);
            }

            FileExplorerItemViewModel item = new(type, path, path.Split('\\').Last(), imageSource);
            item.ContextMenu = contextMenu;
            item.Children = LoadSubDirectoriesAndFiles(path);
            return item;
        }

        private ObservableCollection<FileExplorerItemViewModel> LoadSubDirectoriesAndFiles(string path)
        {
            ObservableCollection<FileExplorerItemViewModel> items = new();

            foreach (string name in Directory.GetDirectories(path))
            {
                string folerName = name.Split('\\').Last();

                // filter .vs, .gitignore etc
                if (folerName.StartsWith('.'))
                    continue;

                if (folerName == "obj" || folerName == "bin")
                    continue;

                items.Add(LoadDirectory(name));
            }

            foreach (string name in Directory.GetFiles(path))
            {
                items.Add(LoadFile(name));
            }

            return items;
        }

        public ObservableCollection<FileExplorerItemViewModel> Load()
        {
            if (string.IsNullOrWhiteSpace(Path) || !Directory.Exists(Path))
                return new();

            // If ends with \, remove that \
            string path = Path.EndsWith('\\') ? new string(Path.SkipLast(1).ToArray()) : Path;

            return new() { LoadDirectory(path) };
        }
    }
}
