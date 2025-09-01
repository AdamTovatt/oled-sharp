namespace OledSharp.Png.Tests
{
    [TestClass]
    public sealed class PngOutputDisplayTests
    {
        private string _outputDirectory = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            // Ensure the output directory exists
            _outputDirectory = Path.Combine("..", "..", "PngOutput");
            Directory.CreateDirectory(_outputDirectory);
        }

        [TestMethod]
        public void TestSimplePixelOutput()
        {
            // Arrange
            string filePath = Path.Combine(_outputDirectory, "simple_pixel_test.png");

            // Act
            using (PngOutputDisplay display = new PngOutputDisplay(filePath))
            {
                display.Initialize();
                display.ClearBuffer();

                // Draw a simple pattern - a few pixels in a line
                display.SetBufferPixel(10, 10, true);
                display.SetBufferPixel(11, 10, true);
                display.SetBufferPixel(12, 10, true);
                display.SetBufferPixel(13, 10, true);

                display.PushBuffer();
            }

            // Assert
            Assert.IsTrue(File.Exists(filePath), "PNG file should be created");
            FileInfo fileInfo = new FileInfo(filePath);
            Assert.IsTrue(fileInfo.Length > 0, "PNG file should not be empty");
        }

        [TestMethod]
        public void TestClearBuffer()
        {
            // Arrange
            string filePath = Path.Combine(_outputDirectory, "clear_buffer_test.png");

            // Act
            using (PngOutputDisplay display = new PngOutputDisplay(filePath))
            {
                display.Initialize();

                // Set some pixels
                display.SetBufferPixel(5, 5, true);
                display.SetBufferPixel(10, 10, true);
                display.SetBufferPixel(15, 15, true);

                // Clear the buffer
                display.ClearBuffer();

                // Verify all pixels are off
                Assert.IsFalse(display.GetBufferPixel(5, 5), "Pixel should be off after clear");
                Assert.IsFalse(display.GetBufferPixel(10, 10), "Pixel should be off after clear");
                Assert.IsFalse(display.GetBufferPixel(15, 15), "Pixel should be off after clear");

                display.PushBuffer();
            }

            // Assert
            Assert.IsTrue(File.Exists(filePath), "PNG file should be created");
        }

        [TestMethod]
        public void TestCornerPixels()
        {
            // Arrange
            string filePath = Path.Combine(_outputDirectory, "corner_pixels_test.png");

            // Act
            using (PngOutputDisplay display = new PngOutputDisplay(filePath))
            {
                display.Initialize();
                display.ClearBuffer();

                // Draw one pixel in each corner of the display
                display.SetBufferPixel(0, 0, true);        // Top-left corner
                display.SetBufferPixel(127, 0, true);      // Top-right corner
                display.SetBufferPixel(0, 63, true);       // Bottom-left corner
                display.SetBufferPixel(127, 63, true);     // Bottom-right corner

                display.PushBuffer();
            }

            // Assert
            Assert.IsTrue(File.Exists(filePath), "PNG file should be created");
            FileInfo fileInfo = new FileInfo(filePath);
            Assert.IsTrue(fileInfo.Length > 0, "PNG file should not be empty");
        }

        [TestMethod]
        public void TestBoundaryHandling()
        {
            // Arrange
            string filePath = Path.Combine(_outputDirectory, "boundary_handling_test.png");

            // Act
            using (PngOutputDisplay display = new PngOutputDisplay(filePath))
            {
                display.Initialize();
                display.ClearBuffer();

                // Draw a horizontal line that starts outside and goes outside
                // Line from (-10, 20) to (150, 20) - should only draw from (0, 20) to (127, 20)
                for (int x = -10; x <= 150; x++)
                {
                    display.SetBufferPixel(x, 20, true);
                }

                // Draw a vertical line that starts outside and goes outside
                // Line from (50, -10) to (50, 80) - should only draw from (50, 0) to (50, 63)
                for (int y = -10; y <= 80; y++)
                {
                    display.SetBufferPixel(50, y, true);
                }

                display.PushBuffer();
            }

            // Assert
            Assert.IsTrue(File.Exists(filePath), "PNG file should be created");
            FileInfo fileInfo = new FileInfo(filePath);
            Assert.IsTrue(fileInfo.Length > 0, "PNG file should not be empty");
        }
    }
}
