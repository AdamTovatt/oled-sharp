using OledSharp.Png;

namespace OledSharp.Tests
{
    [TestClass]
    public sealed class TextRendererTests
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
        public void TestSimpleTextRendering()
        {
            // Arrange
            string filePath = Path.Combine(_outputDirectory, "simple_text_test.png");

            // Act
            using (PngOutputDisplay display = new PngOutputDisplay(filePath))
            {
                display.Initialize();
                display.ClearBuffer();

                TextRenderer textRenderer = new TextRenderer(display);

                // Draw some simple text
                textRenderer.DrawString(2, 2, "Hello");
                textRenderer.DrawString(2, 12, "World!");

                display.PushBuffer();
            }

            // Assert
            Assert.IsTrue(File.Exists(filePath), "PNG file should be created");
            FileInfo fileInfo = new FileInfo(filePath);
            Assert.IsTrue(fileInfo.Length > 0, "PNG file should not be empty");
        }

        [TestMethod]
        public void TestMultiLineTextRendering()
        {
            // Arrange
            string filePath = Path.Combine(_outputDirectory, "multiline_text_test.png");

            // Act
            using (PngOutputDisplay display = new PngOutputDisplay(filePath))
            {
                display.Initialize();
                display.ClearBuffer();

                TextRenderer textRenderer = new TextRenderer(display);

                // Draw multi-line text with manual line breaks
                string multiLineText = "Line 1\nLine 2\nLine 3";
                textRenderer.DrawMultiLineString(2, 2, multiLineText);

                display.PushBuffer();
            }

            // Assert
            Assert.IsTrue(File.Exists(filePath), "PNG file should be created");
            FileInfo fileInfo = new FileInfo(filePath);
            Assert.IsTrue(fileInfo.Length > 0, "PNG file should not be empty");
        }

        [TestMethod]
        public void TestTextWrapping()
        {
            // Arrange
            string filePath = Path.Combine(_outputDirectory, "text_wrapping_test.png");

            // Act
            using (PngOutputDisplay display = new PngOutputDisplay(filePath))
            {
                display.Initialize();
                display.ClearBuffer();

                TextRenderer textRenderer = new TextRenderer(display);

                // Draw text with automatic word wrapping
                string longText = "This is a very long text that should wrap to multiple lines when it reaches the maximum width";
                textRenderer.DrawWrappedString(2, 2, longText, 80);

                display.PushBuffer();
            }

            // Assert
            Assert.IsTrue(File.Exists(filePath), "PNG file should be created");
            FileInfo fileInfo = new FileInfo(filePath);
            Assert.IsTrue(fileInfo.Length > 0, "PNG file should not be empty");
        }

        [TestMethod]
        public void TestTextErasing()
        {
            // Arrange
            string filePath = Path.Combine(_outputDirectory, "text_erasing_test.png");

            // Act
            using (PngOutputDisplay display = new PngOutputDisplay(filePath))
            {
                display.Initialize();
                display.ClearBuffer();

                TextRenderer textRenderer = new TextRenderer(display);

                // Draw some text
                textRenderer.DrawString(2, 2, "Original Text");

                // Draw the same text in reverse (erase it)
                textRenderer.DrawString(2, 2, "Original Text", false);

                // Draw new text in the same area
                textRenderer.DrawString(2, 2, "New Text");

                display.PushBuffer();
            }

            // Assert
            Assert.IsTrue(File.Exists(filePath), "PNG file should be created");
            FileInfo fileInfo = new FileInfo(filePath);
            Assert.IsTrue(fileInfo.Length > 0, "PNG file should not be empty");
        }

        [TestMethod]
        public void TestTextBoundaryHandling()
        {
            // Arrange
            string filePath = Path.Combine(_outputDirectory, "text_boundary_test.png");

            // Act
            using (PngOutputDisplay display = new PngOutputDisplay(filePath))
            {
                display.Initialize();
                display.ClearBuffer();

                TextRenderer textRenderer = new TextRenderer(display);

                // Try to draw text that goes off the right edge
                textRenderer.DrawString(120, 2, "This should be clipped");

                // Try to draw text that goes off the bottom edge
                textRenderer.DrawString(2, 60, "Bottom text");

                // Try to draw text that goes off both edges
                textRenderer.DrawString(120, 60, "Corner text");

                display.PushBuffer();
            }

            // Assert
            Assert.IsTrue(File.Exists(filePath), "PNG file should be created");
            FileInfo fileInfo = new FileInfo(filePath);
            Assert.IsTrue(fileInfo.Length > 0, "PNG file should not be empty");
        }

        [TestMethod]
        public void TestTextWidthCalculation()
        {
            // Arrange
            using (PngOutputDisplay display = new PngOutputDisplay("dummy.png"))
            {
                TextRenderer textRenderer = new TextRenderer(display);

                // Act & Assert
                int width1 = textRenderer.CalculateStringWidth("Hello");
                Assert.IsTrue(width1 > 0, "Width should be positive");

                int width2 = textRenderer.CalculateStringWidth("Hello World");
                Assert.IsTrue(width2 > width1, "Longer text should have greater width");

                int width3 = textRenderer.CalculateStringWidth("");
                Assert.AreEqual(0, width3, "Empty string should have zero width");
            }
        }

        [TestMethod]
        public void TestFullAlphabet()
        {
            // Arrange
            string filePath = Path.Combine(_outputDirectory, "full_alphabet_test.png");

            // Act
            using (PngOutputDisplay display = new PngOutputDisplay(filePath))
            {
                display.Initialize();
                display.ClearBuffer();

                TextRenderer textRenderer = new TextRenderer(display);

                // Write the full alphabet (uppercase and lowercase)
                string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ\nabcdefghijklmnopqrstuvwxyz";
                textRenderer.DrawMultiLineString(2, 2, alphabet);

                display.PushBuffer();
            }

            // Assert
            Assert.IsTrue(File.Exists(filePath), "PNG file should be created");
            FileInfo fileInfo = new FileInfo(filePath);
            Assert.IsTrue(fileInfo.Length > 0, "PNG file should not be empty");
        }

        [TestMethod]
        public void TestAllSpecialCharacters()
        {
            // Arrange
            string filePath = Path.Combine(_outputDirectory, "special_characters_test.png");

            // Act
            using (PngOutputDisplay display = new PngOutputDisplay(filePath))
            {
                display.Initialize();
                display.ClearBuffer();

                TextRenderer textRenderer = new TextRenderer(display);

                // Write all supported special characters and punctuation
                string specialChars = "0123456789\n.,:;!?'-\nSpace Test";
                textRenderer.DrawMultiLineString(2, 2, specialChars);

                display.PushBuffer();
            }

            // Assert
            Assert.IsTrue(File.Exists(filePath), "PNG file should be created");
            FileInfo fileInfo = new FileInfo(filePath);
            Assert.IsTrue(fileInfo.Length > 0, "PNG file should not be empty");
        }
    }
}
