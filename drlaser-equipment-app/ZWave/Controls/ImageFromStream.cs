namespace ZWave.Controls
{
    public class ImageFromStream : Image
    {
        public Stream ImageStream { get; private set; }

        public ImageFromStream(Stream stream) 
        {
            if (!stream.CanRead)
            {
                throw new ArgumentException();
            }
            ImageStream = new MemoryStream();
            stream.CopyTo(ImageStream);
            stream.Position = 0;
            ImageStream.Position = 0;
            Source = ImageSource.FromStream(() => new BufferedStream(stream));
        }
    }
}
