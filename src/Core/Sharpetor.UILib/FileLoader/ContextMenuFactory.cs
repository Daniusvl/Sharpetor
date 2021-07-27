using System;
using System.Collections.Generic;
using Avalonia.Controls;

namespace Sharpetor.UILib
{
    public static class ContextMenuFactory
    {
        private static MenuItem Create(string header, Action<string> click, string path)
        {
            MenuItem item = new() { Header = header };
            item.Click += (o, e) => click?.Invoke(path);
            return item;
        }

        public static ContextMenu FolderSolutionContextMenu(string path)
        {
            List<MenuItem> items = new List<MenuItem>();

            items.Add(Create("New File", ContextMenuEvents.NewFileClick, path));
            items.Add(Create("New Folder", ContextMenuEvents.NewFolderClick, path));
            items.Add(Create("New Project", ContextMenuEvents.NewProjectClick, path));
            items.Add(Create("Rename", ContextMenuEvents.RenameFolderProjectSolutionClick, path));
            items.Add(Create("Delete", ContextMenuEvents.DeleteFolderClick, path));

            return new() { Items = items };
        }

        public static ContextMenu ProjectFolderContextMenu(string path)
        {
            List<MenuItem> items = new List<MenuItem>();

            items.Add(Create("New File", ContextMenuEvents.NewFileClick, path));
            items.Add(Create("New Folder", ContextMenuEvents.NewFolderClick, path));
            items.Add(Create("Rename", ContextMenuEvents.RenameFolderProjectSolutionClick, path));
            items.Add(Create("Delete", ContextMenuEvents.DeleteFolderClick, path));

            return new() { Items = items };
        }

        public static ContextMenu FileContextMenu(string path) 
        {
            List<MenuItem> items = new List<MenuItem>();

            items.Add(Create("Rename", ContextMenuEvents.RenameFileClick, path));
            items.Add(Create("Delete", ContextMenuEvents.DeleteFileClick, path));

            return new() { Items = items };
        }
    }
}
