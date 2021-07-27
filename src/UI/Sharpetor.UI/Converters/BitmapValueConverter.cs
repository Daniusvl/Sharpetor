using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Sharpetor.UILib;
using System;
using System.Globalization;

namespace Sharpetor.UI
{
    public class BitmapValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string path && targetType == typeof(IImage))
            {
                if (path == ImagePathConstants.Solution)
                    return ImagePathConstants.SolutionBitmap;

                if (path == ImagePathConstants.Project)
                    return ImagePathConstants.ProjectBitmap;

                if (path == ImagePathConstants.Folder)
                    return ImagePathConstants.FolderBitmap;

                if (path == ImagePathConstants.CSharpFile)
                    return ImagePathConstants.CSharpFileBitmap;

                if (path == ImagePathConstants.File)
                    return ImagePathConstants.FileBitmap;

                return new Bitmap(path);
            }

            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
