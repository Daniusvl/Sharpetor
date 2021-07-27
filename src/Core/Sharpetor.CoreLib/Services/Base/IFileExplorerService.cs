namespace Sharpetor.CoreLib
{
    public interface IFileExplorerService
    {
        void NewFile(string path, string fileName);

        void NewFolder(string path, string folderName);

        void RenameFolderProjectSolution(string path, string newName);

        void RenameFile(string path, string newName);

        void DeleteFile(string path);

        void DeleteFolder(string path);
    }
}
