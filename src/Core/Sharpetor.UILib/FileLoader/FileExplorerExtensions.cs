using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Sharpetor.UILib
{
    public static class FileExplorerExtensions
    {
        public static ObservableCollection<FileExplorerItemViewModel> SyncIsExpanded(this ObservableCollection<FileExplorerItemViewModel> loaded, ObservableCollection<FileExplorerItemViewModel> previous)
        {
            if (previous == null)
                return loaded;

            for (int i = 0; i < loaded.Count; i++)
            {
                if (loaded[i].Type == FileExplorerItemViewModel.ItemType.Folder ||
                    loaded[i].Type == FileExplorerItemViewModel.ItemType.Project ||
                    loaded[i].Type == FileExplorerItemViewModel.ItemType.Solution)
                {
                    FileExplorerItemViewModel item = previous.SingleOrDefault(it =>
                    {
                        return it.Path == loaded[i].Path;
                    });

                    if (item != null)
                        loaded[i].IsExpanded = item.IsExpanded;

                    if(previous.Count > i)
                        SyncIsExpanded(loaded[i].Children, previous.Count > i ? previous[i].Children : null);
                }
            }
            return loaded;
        }

        public static bool IsSolution(string path)
        {
            return Directory.GetFiles(path).Any(s => s.EndsWith(".sln"));
        }

        public static bool IsInProject(string thisPath, string basePath)
        {
            bool output = IsIn_Or_IsProjectFolder(thisPath, basePath, out bool? isProject, out string fromProjectToThisFilePath);
            return output == true && !isProject == true;
        }

        public static bool IsProject(string thisPath, string basePath)
        {
            bool output = IsIn_Or_IsProjectFolder(thisPath, basePath, out bool? isProject, out string fromProjectToThisFilePath);
            return output == true && isProject == true;
        }

        private static bool IsIn_Or_IsProjectFolder(string thisPath, string basePath, out bool? isProject, out string fromProjectToThisFilePath)
        {
            fromProjectToThisFilePath = string.Empty;
            isProject = null;
            string path = thisPath;
            int i = 0;
            while(path.Length > basePath.Length)
            {
                string file = Directory.EnumerateFiles(path).SingleOrDefault(file => file.EndsWith(".csproj"));
                if (file != null)
                {
                    // example: [ProjectName]\[folder]\[etc]
                    // needed for file creation templates
                    fromProjectToThisFilePath.Insert(0, path.Split('\\').Last());
                    if (i == 0) isProject = true;
                    else isProject = false;
                    return true;
                }
                fromProjectToThisFilePath.Insert(0, path.Split('\\').Last());
                path = string.Join('\\', path.Split('\\').SkipLast(1));
                i++;
            }
            return false;
        }
    }
}
