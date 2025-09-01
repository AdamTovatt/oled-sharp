#nullable enable
namespace OledSharp
{
    /// <summary>
    /// Represents a character with its bitmap data and dimensions
    /// Uses compact storage - only stores actual character pixels
    /// </summary>
    public readonly struct CharacterData
    {
        /// <summary>
        /// Bitmap data - only contains actual character pixels (width × height)
        /// Each byte represents one pixel (0 = off, 1 = on)
        /// </summary>
        public readonly byte[] Bitmap;

        /// <summary>
        /// Width of the character in pixels
        /// </summary>
        public readonly int Width;

        /// <summary>
        /// Height of the character in pixels
        /// </summary>
        public readonly int Height;

        /// <summary>
        /// Vertical offset in pixels from the baseline (positive = down, negative = up)
        /// Default is 0 (no offset)
        /// </summary>
        public readonly int VerticalOffset;

        /// <summary>
        /// Creates a new CharacterData instance
        /// </summary>
        /// <param name="bitmap">Bitmap data (width × height bytes)</param>
        /// <param name="width">Character width in pixels</param>
        /// <param name="height">Character height in pixels</param>
        /// <param name="verticalOffset">Vertical offset in pixels from baseline (positive = down, negative = up, default: 0)</param>
        public CharacterData(byte[] bitmap, int width, int height, int verticalOffset = 0)
        {
            Bitmap = bitmap ?? throw new ArgumentNullException(nameof(bitmap));
            Width = Math.Max(1, width); // Ensure width is at least 1
            Height = Math.Max(1, height); // Ensure height is at least 1
            VerticalOffset = verticalOffset;

            // Validate bitmap size matches dimensions
            if (bitmap.Length != width * height)
            {
                throw new ArgumentException($"Bitmap size ({bitmap.Length}) must equal width × height ({width * height})", nameof(bitmap));
            }
        }

        /// <summary>
        /// Gets a pixel value at the specified position
        /// </summary>
        /// <param name="x">X coordinate (0 to Width-1)</param>
        /// <param name="y">Y coordinate (0 to Height-1)</param>
        /// <returns>True if pixel is on, false if off</returns>
        public bool GetPixel(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return false;

            int index = (y * Width) + x;
            return Bitmap[index] == 1;
        }
    }
}
