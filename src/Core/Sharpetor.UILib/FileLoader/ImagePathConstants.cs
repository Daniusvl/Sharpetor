using Avalonia.Media.Imaging;
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

        public static readonly Bitmap SolutionBitmap = new(Solution);
        public static readonly Bitmap ProjectBitmap = new(Project);
        public static readonly Bitmap FolderBitmap = new(Folder);
        public static readonly Bitmap CSharpFileBitmap = new(CSharpFile);
        public static readonly Bitmap FileBitmap = new(File);

        static ImagePathConstants()
        {
            if (!System.IO.File.Exists(Solution))
                throw new FileNotFoundException(nameof(Solution));

            if (!System.IO.File.Exists(Project))
                throw new FileNotFoundException(nameof(Project));

            if (!System.IO.File.Exists(Folder))
                throw new FileNotFoundException(nameof(Folder));

            if (!System.IO.File.Exists(CSharpFile))
                throw new FileNotFoundException(nameof(CSharpFile));

            if (!System.IO.File.Exists(File))
                throw new FileNotFoundException(nameof(File));
        }
    }
}
