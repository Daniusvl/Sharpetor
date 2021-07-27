using System.IO;
using System.Linq;

namespace Sharpetor.CoreLib
{
    public class FileExplorerService : IFileExplorerService
    {
        public void DeleteFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        public void DeleteFolder(string path)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, true);
        }

        public void NewFile(string path, string fileName)
        {
            void AddFile()
            {
                if (Directory.Exists(path) && !File.Exists(fileName) && !string.IsNullOrEmpty(fileName))
                {
                    string separator = path.EndsWith('\\') || fileName.StartsWith('\\') ? string.Empty : @"\";
                    File.Create($@"{path}{separator}{fileName}");
                }
            }

            if (fileName.Contains('\\') && Directory.Exists(path))
            {
                string separator = path.EndsWith('\\') || fileName.StartsWith('\\') ? string.Empty : @"\";
                path += @$"{separator}{string.Join('\\', fileName.Split('\\').SkipLast(1))}";
                Directory.CreateDirectory(path);
                fileName = fileName.Split('\\').Last();
                AddFile();
            }
            else AddFile();
        }

        public void NewFolder(string path, string folderName)
        {
            string separator = path.EndsWith('\\') || folderName.StartsWith('\\') ? string.Empty : @"\";

            Directory.CreateDirectory($@"{path}{separator}{folderName}");
        }

        public void RenameFile(string path, string newName)
        {
            string separator = newName.StartsWith('\\') ? string.Empty : @"\";
            string newPath = @$"{string.Join('\\', path.Split('\\').SkipLast(1))}{separator}{newName}";
            if (!File.Exists(newPath))
                File.Move(path, newPath);
        }

        public void RenameFolderProjectSolution(string path, string newName)
        {
            string separator = newName.StartsWith('\\') ? string.Empty : @"\";
            string newPath = @$"{string.Join('\\', path.Split('\\').SkipLast(1))}{separator}{newName}";
            if (!Directory.Exists(newPath))
                Directory.Move(path, newPath);
        }
    }
}
