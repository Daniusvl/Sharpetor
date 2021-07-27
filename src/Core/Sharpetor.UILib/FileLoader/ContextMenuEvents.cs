using System;

namespace Sharpetor.UILib
{
    public static class ContextMenuEvents
    {
        public static Action<string> NewFileClick { get; set; } = s => { };

        public static Action<string> NewFolderClick { get; set; } = s => { };

        public static Action<string> NewProjectClick { get; set; } = s => { };

        public static Action<string> RenameFolderProjectSolutionClick { get; set; } = s => { };

        public static Action<string> RenameFileClick { get; set; } = s => { };

        public static Action<string> DeleteFileClick { get; set; } = s => { };

        public static Action<string> DeleteFolderClick { get; set; } = s => { };

    }
}
