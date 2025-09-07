using OledSharp.Fonts;

namespace OledSharp
{
    /// <summary>
    /// Text rendering class for OLED displays using bitmap fonts
    /// </summary>
    public class TextRenderer
    {
        private readonly IOledDisplay _display;
        private readonly IFont _font;

        /// <summary>
        /// Spacing between characters in pixels (default: 1 pixel)
        /// </summary>
        public int CharacterSpacing { get; set; } = 1;

        /// <summary>
        /// Spacing between lines in pixels (default: 2 pixels)
        /// </summary>
        public int LineSpacing { get; set; } = 2;

        /// <summary>
        /// Creates a new text renderer for the specified display with default font
        /// </summary>
        /// <param name="display">The OLED display to render text on</param>
        public TextRenderer(IOledDisplay display) : this(display, new Font5x5())
        {
        }

        /// <summary>
        /// Creates a new text renderer for the specified display with custom font
        /// </summary>
        /// <param name="display">The OLED display to render text on</param>
        /// <param name="font">The font to use for rendering</param>
        public TextRenderer(IOledDisplay display, IFont font)
        {
            this._display = display ?? throw new ArgumentNullException(nameof(display));
            this._font = font ?? throw new ArgumentNullException(nameof(font));
        }

        /// <summary>
        /// Draws text at the specified position
        /// </summary>
        /// <param name="text">Text to draw</param>
        /// <param name="x">X coordinate (left edge)</param>
        /// <param name="y">Y coordinate (baseline position)</param>
        public void DrawText(string text, int x, int y)
        {
            if (string.IsNullOrEmpty(text))
                return;

            int currentX = x;
            int baselineY = y - _font.BaselineOffset;

            foreach (char c in text)
            {
                if (c == '\n')
                {
                    currentX = x;
                    baselineY += _font.LineHeight + LineSpacing;
                    continue;
                }

                CharacterData characterData = _font.GetCharacter(c);
                // Calculate Y position for this character (bottom-left origin)
                int characterY = baselineY + (_font.LineHeight - characterData.Height) + characterData.VerticalOffset;

                DrawCharacter(currentX, characterY, c);
                currentX += characterData.Width + CharacterSpacing;
            }
        }

        /// <summary>
        /// Draws a single character at the specified position
        /// </summary>
        /// <param name="x">X coordinate for top-left of character</param>
        /// <param name="y">Y coordinate for top-left of character</param>
        /// <param name="character">Character to draw</param>
        /// <param name="isOn">True to draw character pixels on, false to draw them off (erase)</param>
        /// <returns>The actual width of the drawn character in pixels</returns>
        public int DrawCharacter(int x, int y, char character, bool isOn = true)
        {
            CharacterData characterData = _font.GetCharacter(character);

            // Apply vertical offset to the y position
            int adjustedY = y + characterData.VerticalOffset;

            for (int row = 0; row < characterData.Height; row++)
            {
                for (int col = 0; col < characterData.Width; col++)
                {
                    bool pixelOn = characterData.GetPixel(col, row);

                    // Only draw the pixel if it should be on (for regular text)
                    // or if we're erasing (isOn = false), draw the opposite
                    if (pixelOn)
                    {
                        _display.SetBufferPixel(x + col, adjustedY + row, isOn);
                    }
                    else if (!isOn)
                    {
                        // When erasing, clear all pixels in character area
                        _display.SetBufferPixel(x + col, adjustedY + row, false);
                    }
                }
            }

            return characterData.Width;
        }

        /// <summary>
        /// Draws a string of text starting at the specified position
        /// </summary>
        /// <param name="x">X coordinate for start of text</param>
        /// <param name="y">Y coordinate for baseline of text</param>
        /// <param name="text">Text to draw</param>
        /// <param name="isOn">True to draw text pixels on, false to erase text area</param>
        /// <returns>The width in pixels of the rendered text</returns>
        public int DrawString(int x, int y, string text, bool isOn = true)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            int currentX = x;
            int totalWidth = 0;

            foreach (char character in text)
            {
                CharacterData characterData = _font.GetCharacter(character);

                // Check if character would fit on display
                if (currentX + characterData.Width > _display.Width)
                    break; // Stop drawing if we run out of horizontal space

                // Check if character would fit vertically (accounting for vertical offset)
                int maxCharacterHeight = characterData.Height + characterData.VerticalOffset;
                if (y + maxCharacterHeight > _display.Height)
                    break; // Stop drawing if we run out of vertical space

                int actualWidth = DrawCharacter(currentX, y, character, isOn);

                currentX += actualWidth + CharacterSpacing;
                totalWidth += actualWidth + CharacterSpacing;
            }

            // Remove the last character spacing from total width
            if (totalWidth > 0)
                totalWidth -= CharacterSpacing;

            return totalWidth;
        }

        /// <summary>
        /// Draws multi-line text with automatic line wrapping
        /// </summary>
        /// <param name="x">X coordinate for start of text</param>
        /// <param name="y">Y coordinate for baseline of first line</param>
        /// <param name="text">Text to draw (can contain \n for manual line breaks)</param>
        /// <param name="isOn">True to draw text pixels on, false to erase text area</param>
        /// <returns>Total height in pixels of the rendered text</returns>
        public int DrawMultiLineString(int x, int y, string text, bool isOn = true)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            string[] lines = text.Split('\n');
            int currentY = y;
            int totalHeight = 0;
            int lineHeight = _font.LineHeight;

            foreach (string line in lines)
            {
                // Check if this line would fit vertically
                if (currentY + lineHeight > _display.Height)
                    break;

                DrawString(x, currentY, line, isOn);

                currentY += lineHeight + LineSpacing;
                totalHeight += lineHeight + LineSpacing;
            }

            // Remove the last line spacing from total height
            if (totalHeight > 0)
                totalHeight -= LineSpacing;

            return totalHeight;
        }

        /// <summary>
        /// Draws text with automatic word wrapping within specified width
        /// </summary>
        /// <param name="x">X coordinate for start of text</param>
        /// <param name="y">Y coordinate for baseline of first line</param>
        /// <param name="text">Text to draw</param>
        /// <param name="maxWidth">Maximum width for text wrapping</param>
        /// <param name="isOn">True to draw text pixels on, false to erase text area</param>
        /// <returns>Total height in pixels of the rendered text</returns>
        public int DrawWrappedString(int x, int y, string text, int maxWidth, bool isOn = true)
        {
            if (string.IsNullOrEmpty(text) || maxWidth <= 0)
                return 0;

            // First split by line breaks to handle explicit \n characters
            string[] lines = text.Split('\n');
            int currentY = y;
            int lineHeight = _font.LineHeight;
            int totalHeight = 0;

            foreach (string line in lines)
            {
                // Check if we've run out of vertical space
                if (currentY + lineHeight > _display.Height)
                    break;

                // Process each line for word wrapping
                int lineWrapHeight = DrawWrappedLine(x, currentY, line, maxWidth, isOn);
                
                currentY += lineWrapHeight + LineSpacing;
                totalHeight += lineWrapHeight + LineSpacing;
            }

            // Remove the last line spacing from total height
            if (totalHeight > 0)
                totalHeight -= LineSpacing;

            return totalHeight;
        }

        /// <summary>
        /// Draws a single line of text with word wrapping within specified width
        /// </summary>
        /// <param name="x">X coordinate for start of text</param>
        /// <param name="y">Y coordinate for baseline of first line</param>
        /// <param name="line">Single line of text to draw (should not contain \n)</param>
        /// <param name="maxWidth">Maximum width for text wrapping</param>
        /// <param name="isOn">True to draw text pixels on, false to erase text area</param>
        /// <returns>Total height in pixels of the rendered line(s)</returns>
        private int DrawWrappedLine(int x, int y, string line, int maxWidth, bool isOn)
        {
            if (string.IsNullOrEmpty(line))
                return _font.LineHeight;

            string[] words = line.Split(' ');
            int currentX = x;
            int currentY = y;
            int lineHeight = _font.LineHeight;
            int totalHeight = lineHeight;

            foreach (string word in words)
            {
                int wordWidth = CalculateStringWidth(word);

                // Check if word fits on current line
                if (currentX > x && currentX + wordWidth > x + maxWidth)
                {
                    // Move to next line
                    currentX = x;
                    currentY += lineHeight + LineSpacing;
                    totalHeight += lineHeight + LineSpacing;

                    // Check if we've run out of vertical space
                    if (currentY + lineHeight > _display.Height)
                        break;
                }

                // Draw the word
                int wordPixelWidth = DrawString(currentX, currentY, word, isOn);
                currentX += wordPixelWidth;

                // Add space after word (if there's room and it's not the last word)
                CharacterData spaceData = _font.GetCharacter(' ');
                if (currentX + spaceData.Width + CharacterSpacing <= x + maxWidth)
                {
                    currentX += spaceData.Width + CharacterSpacing;
                }
            }

            return totalHeight;
        }

        /// <summary>
        /// Calculates the width in pixels that a string would occupy when rendered
        /// </summary>
        /// <param name="text">Text to measure</param>
        /// <returns>Width in pixels</returns>
        public int CalculateStringWidth(string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            int width = 0;
            foreach (char character in text)
            {
                CharacterData characterData = _font.GetCharacter(character);
                width += characterData.Width;
            }

            // Add character spacing between characters
            if (text.Length > 1)
                width += (text.Length - 1) * CharacterSpacing;

            return width;
        }

        /// <summary>
        /// Calculates the height in pixels that multi-line text would occupy
        /// </summary>
        /// <param name="text">Text to measure (can contain \n)</param>
        /// <returns>Height in pixels</returns>
        public int CalculateMultiLineStringHeight(string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            string[] lines = text.Split('\n');
            int lineHeight = _font.LineHeight;
            int height = lines.Length * lineHeight;
            if (lines.Length > 1)
                height += (lines.Length - 1) * LineSpacing;

            return height;
        }
    }
}
