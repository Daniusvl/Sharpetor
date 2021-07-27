using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.IO;

namespace Sharpetor.UILib
{
    public static class ImagePathConstants
    {
        private const string PathToPhotos = @"Configuration\FileExplorer\Images";

        public static readonly string Solution = @$"{Directory.GetCurrentDirectory()}\{PathToPhotos}\solution.png";

        public static readonly string Project = @$"{Directory.GetCurrentDirectory()}\{PathToPhotos}\project.png";

        public static readonly string Folder = @$"{Directory.GetCurrentDirectory()}\{PathToPhotos}\folder.png";

        public static readonly string CSharpFile = @$"{Directory.GetCurrentDirectory()}\{PathToPhotos}\c#.png";

        public static readonly string File = @$"{Directory.GetCurrentDirectory()}\{PathToPhotos}\file.png";

        public static readonly Bitmap SolutionBitmap;

        public static readonly Bitmap ProjectBitmap;

        public static readonly Bitmap FolderBitmap;

        public static readonly Bitmap CSharpFileBitmap;

        public static readonly Bitmap FileBitmap;

        static ImagePathConstants()
        {
            IAssetLoader assets = AvaloniaLocator.Current.GetService<IAssetLoader>();

            if (System.IO.File.Exists(Solution))
            {
                SolutionBitmap = new(Solution);
            }
            else
            {
                Solution = @$"avares://Sharpetor.UI/Assets/solution.png";
                SolutionBitmap = new(assets.Open(new(Solution)));
            }

            if (System.IO.File.Exists(Project))
            {
                ProjectBitmap = new(Project);
            }
            else
            {
                Project = @$"avares://Sharpetor.UI/Assets/project.png";
                ProjectBitmap = new(assets.Open(new(Project)));
            }
        

            if (System.IO.File.Exists(Folder))
            {
                FolderBitmap = new(Folder);
            }
            else
            {
                Folder = @$"avares://Sharpetor.UI/Assets/folder.png";
                FolderBitmap = new(assets.Open(new(Folder)));
            }

            if (System.IO.File.Exists(CSharpFile))
            {
                CSharpFileBitmap = new(CSharpFile);
            }
            else
            {
                CSharpFile = @$"avares://Sharpetor.UI/Assets/cs.png";
                CSharpFileBitmap = new(assets.Open(new(CSharpFile)));
            }

            if (System.IO.File.Exists(File))
            {
                FileBitmap = new(File);
            }
            else
            {
                File = @$"avares://Sharpetor.UI/Assets/file.png";
                FileBitmap = new(assets.Open(new(File)));
            }
        }
    }
}
