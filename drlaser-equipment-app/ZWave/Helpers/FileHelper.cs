using CommunityToolkit.Maui.Storage;

namespace ZWave.Helpers
{
    public static class FileHelper
    {
        public static async Task<FileResult> ImageFilePicker()
        {
            var customImageFileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new [] { "image/*" } },
                { DevicePlatform.WinUI, new [] { ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".tiff", ".tif" } }
            });
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                FileTypes = customImageFileTypes
            });

            return result;
        }

        public static async Task<string> ImageFileSaver(Stream stream, CancellationToken cancellationToken = new CancellationToken())
        {
            if (!stream.CanRead)
            {
                return string.Empty;
            }
            MemoryStream memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            stream.Position = 0;
            memoryStream.Position = 0;
#pragma warning disable CA1416 // Validate platform compatibility
            FileSaverResult fileSaverResult = await FileSaver.Default.SaveAsync("", stream, cancellationToken);
#pragma warning restore CA1416 // Validate platform compatibility
            if (fileSaverResult.IsSuccessful)
            {
                return fileSaverResult.FilePath;
            }

            return string.Empty;
        }

        public static async Task<string> SaveStringsToFile(IEnumerable<string> contentArray, CancellationToken cancellationToken = new CancellationToken())
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            foreach (var item in contentArray)
            {
                writer.WriteLine(item);
            }

            writer.Flush();
            stream.Position = 0;

#pragma warning disable CA1416 // Validate platform compatibility
            FileSaverResult fileSaverResult = await FileSaver.Default.SaveAsync($"Log-{DateTime.Now.ToString("yyyyMMddhhmm")}.txt", stream, cancellationToken);
#pragma warning restore CA1416 // Validate platform compatibility

            if (fileSaverResult.IsSuccessful)
            {
                return fileSaverResult.FilePath;
            }

            return string.Empty;
        }
    }
}
