using BigGustave;

namespace OledSharp.Png
{
    public class PngOutputDisplay : IOledDisplay
    {
        private readonly string _filePath;
        private readonly int _width;
        private readonly int _height;
        private readonly bool[,] _buffer;
        private bool _disposed = false;

        public int Width => _width;
        public int Height => _height;

        public PngOutputDisplay(string filePath, int width = 128, int height = 64)
        {
            _filePath = filePath;
            _width = width;
            _height = height;
            _buffer = new bool[width, height];
        }

        public void Initialize()
        {
            // No initialization needed for PNG output
        }

        public void SetBufferPixel(int x, int y, bool isOn)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return;

            _buffer[x, y] = isOn;
        }

        public bool GetBufferPixel(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return false;

            return _buffer[x, y];
        }

        public void ClearBuffer()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _buffer[x, y] = false;
                }
            }
        }

        public void PushBuffer()
        {
            // Create a PNG builder with the same dimensions as the buffer
            // Using grayscale (false for no alpha channel)
            PngBuilder builder = PngBuilder.Create(Width, Height, false);

            // Convert buffer to PNG pixels
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    // Convert OLED pixel (true = on/white, false = off/black) to PNG pixel
                    byte pixelValue = _buffer[x, y] ? (byte)255 : (byte)0;
                    Pixel pixel = new Pixel(pixelValue, pixelValue, pixelValue);
                    builder.SetPixel(pixel, x, y);
                }
            }

            // Save to file
            using (FileStream fileStream = File.Create(_filePath))
            {
                builder.Save(fileStream);
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
            }
        }
    }
}
