#nullable enable
using System;

namespace OledSharp
{
    /// <summary>
    /// Example class demonstrating vertical offset functionality
    /// </summary>
    public class Class1
    {
        /// <summary>
        /// Demonstrates the vertical offset functionality
        /// </summary>
        public static void DemonstrateVerticalOffsets()
        {
            // Create a mock display for testing
            var mockDisplay = new MockOledDisplay(128, 64);
            var textRenderer = new TextRenderer(mockDisplay);
            var font = new Fonts.Font5x5();

            Console.WriteLine("Vertical Offset Demonstration:");
            Console.WriteLine("=============================");
            
            // Test characters with different vertical offsets
            string testText = "Hello, World!";
            Console.WriteLine($"Text: {testText}");
            
            // Show which characters have vertical offsets
            foreach (char character in testText)
            {
                CharacterData data = font.GetCharacter(character);
                string offsetInfo = data.VerticalOffset > 0 ? $" (offset: +{data.VerticalOffset})" : "";
                Console.WriteLine($"  '{character}': width={data.Width}, height={data.Height}{offsetInfo}");
            }
            
            // Characters that should have vertical offsets:
            // - ',' (comma): offset +1
            // - ';' (semicolon): offset +1
            // - 'j', 'p', 'q' (lowercase): offset +1 (for descenders)
            // - 'J', 'P', 'Q' (uppercase): offset +1 (for descenders)
        }
    }

    /// <summary>
    /// Mock OLED display for testing purposes
    /// </summary>
    public class MockOledDisplay : IOledDisplay
    {
        public int Width { get; }
        public int Height { get; }
        private readonly bool[,] pixels;

        public MockOledDisplay(int width, int height)
        {
            Width = width;
            Height = height;
            pixels = new bool[width, height];
        }

        public void SetBufferPixel(int x, int y, bool isOn)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                pixels[x, y] = isOn;
            }
        }

        public bool GetBufferPixel(int x, int y)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                return pixels[x, y];
            }
            return false;
        }

        public void ClearBuffer()
        {
            Array.Clear(pixels, 0, pixels.Length);
        }

        public void PushBuffer()
        {
            // Mock implementation - no actual display update
        }

        public void Initialize()
        {
            // Mock implementation - no initialization needed
        }

        public void Dispose()
        {
            // Mock implementation - no resources to dispose
        }
    }
}
