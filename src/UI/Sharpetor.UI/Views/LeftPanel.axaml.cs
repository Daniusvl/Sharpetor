using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Autofac;
using Sharpetor.UILib;
using System.Linq;

namespace Sharpetor.UI.Views
{
    public partial class LeftPanel : UserControl
    {
        public LeftPanel()
        {
            InitializeComponent();
            ContextMenuEvents.NewFileClick = NewFileClick;
            ContextMenuEvents.NewFolderClick = NewFolderClick;
            ContextMenuEvents.NewProjectClick = NewProjectClick;
            ContextMenuEvents.RenameFolderProjectSolutionClick = RenameFolderProjectSolutionClick;
            ContextMenuEvents.RenameFileClick = RenameFileClick;
        }

        private void NewProjectClick(string path)
        {
            NewProjectView view = ContainerFactory.Container.Resolve<NewProjectView>();
            if(view.DataContext is NewProjectViewModel ctx) ctx.Path = path;
            view.ShowDialog((DataContext as LeftPanelViewModel)?.MainWindow);
        }

        private void NewFileClick(string path)
        {
            AddFileView view = new();
            view.SetGetName(fileName => (DataContext as LeftPanelViewModel)?.NewFile(path, fileName));
            view.ShowDialog((DataContext as LeftPanelViewModel)?.MainWindow);
        }

        private void NewFolderClick(string path)
        {
            AddFileView view = new();
            view.SetGetName(fileName => (DataContext as LeftPanelViewModel)?.NewFolder(path, fileName));
            view.ShowDialog((DataContext as LeftPanelViewModel)?.MainWindow);
        }

        private void RenameFolderProjectSolutionClick(string path)
        {
            AddFileView view = new();
            if (view.DataContext is AddFileViewModel ctx) ctx.Name = path.Split('\\').Last();
            view.SetGetName(fileName => (DataContext as LeftPanelViewModel)?.RenameFolderProjectSolution(path, fileName));
            view.ShowDialog((DataContext as LeftPanelViewModel)?.MainWindow);
        }

        private void RenameFileClick(string path)
        {
            AddFileView view = new();
            if(view.DataContext is AddFileViewModel ctx) ctx.Name = path.Split('\\').Last();
            view.SetGetName(fileName => (DataContext as LeftPanelViewModel)?.RenameFile(path, fileName));
            view.ShowDialog((DataContext as LeftPanelViewModel)?.MainWindow);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
