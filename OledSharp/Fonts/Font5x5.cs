#nullable enable
namespace OledSharp.Fonts
{
    /// <summary>
    /// Variable-width and variable-height bitmap font containing numbers, letters, and special characters
    /// Characters can be 1-5 pixels wide and 1-6 pixels tall
    /// Uses compact storage - only stores actual character pixels
    /// Baseline is positioned at the bottom of the standard character height
    /// </summary>
    public class Font5x5 : IFont
    {
        // Standard character height (most characters are 5 pixels tall)
        private const int StandardCharacterHeight = 5;

        // Line height accommodates tallest character (6 pixels for characters like 'g', 'j', 'p', 'q', 'y')
        public int LineHeight => 6;

        // Baseline offset is 0 since we use bottom-left origin
        public int BaselineOffset => 0;

        // Character bitmap definitions - compact storage (width × height bytes per character)
        private static readonly byte[] Number0 =
        {
            0, 1, 1, 0,
            1, 0, 0, 1,
            1, 0, 0, 1,
            1, 0, 0, 1,
            0, 1, 1, 0,
        };

        private static readonly byte[] Number1 =
        {
            0, 1, 0,
            1, 1, 0,
            0, 1, 0,
            0, 1, 0,
            1, 1, 1,
        };

        private static readonly byte[] Number2 =
        {
            1, 1, 1, 0,
            0, 0, 0, 1,
            0, 1, 1, 0,
            1, 0, 0, 0,
            1, 1, 1, 1,
        };

        private static readonly byte[] Number3 =
        {
            1, 1, 1, 0,
            0, 0, 0, 1,
            0, 1, 1, 0,
            0, 0, 0, 1,
            1, 1, 1, 0,
        };

        private static readonly byte[] Number4 =
        {
            1, 0, 0, 1,
            1, 0, 0, 1,
            1, 1, 1, 1,
            0, 0, 0, 1,
            0, 0, 0, 1,
        };

        private static readonly byte[] Number5 =
        {
            1, 1, 1, 1,
            1, 0, 0, 0,
            1, 1, 1, 0,
            0, 0, 0, 1,
            1, 1, 1, 0,
        };

        private static readonly byte[] Number6 =
        {
            0, 1, 1, 0,
            1, 0, 0, 0,
            1, 1, 1, 0,
            1, 0, 0, 1,
            0, 1, 1, 0,
        };

        private static readonly byte[] Number7 =
        {
            1, 1, 1, 1,
            0, 0, 0, 1,
            0, 0, 1, 0,
            0, 1, 0, 0,
            0, 1, 0, 0,
        };

        private static readonly byte[] Number8 =
        {
            0, 1, 1, 0,
            1, 0, 0, 1,
            0, 1, 1, 0,
            1, 0, 0, 1,
            0, 1, 1, 0,
        };

        private static readonly byte[] Number9 =
        {
            0, 1, 1, 0,
            1, 0, 0, 1,
            0, 1, 1, 1,
            0, 0, 0, 1,
            0, 1, 1, 0,
        };

        private static readonly byte[] WhiteSpace =
        {
            0, 0, 0,
            0, 0, 0,
            0, 0, 0,
            0, 0, 0,
            0, 0, 0,
        };

        private static readonly byte[] Unsupported =
        {
            1, 1, 1, 1,
            1, 1, 1, 1,
            1, 1, 1, 1,
            1, 1, 1, 1,
            1, 1, 1, 1,
        };

        private static readonly CharacterData DefaultUnsupportedCharacterData = new CharacterData(Unsupported, 4, StandardCharacterHeight);

        private static readonly byte[] LetterA =
        {
            0, 1, 1, 0,
            1, 0, 0, 1,
            1, 1, 1, 1,
            1, 0, 0, 1,
            1, 0, 0, 1,
        };

        private static readonly byte[] LetterB =
        {
            1, 1, 1, 0,
            1, 0, 0, 1,
            1, 1, 1, 0,
            1, 0, 0, 1,
            1, 1, 1, 0,
        };

        private static readonly byte[] LetterC =
        {
            0, 1, 1, 1,
            1, 0, 0, 0,
            1, 0, 0, 0,
            1, 0, 0, 0,
            0, 1, 1, 1,
        };

        private static readonly byte[] LetterD =
        {
            1, 1, 1, 0,
            1, 0, 0, 1,
            1, 0, 0, 1,
            1, 0, 0, 1,
            1, 1, 1, 0,
        };

        private static readonly byte[] LetterE =
        {
            1, 1, 1, 1,
            1, 0, 0, 0,
            1, 1, 1, 0,
            1, 0, 0, 0,
            1, 1, 1, 1,
        };

        private static readonly byte[] LetterF =
        {
            1, 1, 1, 1,
            1, 0, 0, 0,
            1, 1, 1, 0,
            1, 0, 0, 0,
            1, 0, 0, 0,
        };

        private static readonly byte[] LetterG =
        {
            0, 1, 1, 1,
            1, 0, 0, 0,
            1, 0, 1, 1,
            1, 0, 0, 1,
            0, 1, 1, 1,
        };

        private static readonly byte[] LetterH =
        {
            1, 0, 0, 1,
            1, 0, 0, 1,
            1, 1, 1, 1,
            1, 0, 0, 1,
            1, 0, 0, 1,
        };

        private static readonly byte[] LetterI =
        {
            1, 1, 1,
            0, 1, 0,
            0, 1, 0,
            0, 1, 0,
            1, 1, 1,
        };

        private static readonly byte[] LetterJ =
        {
            1, 1, 1, 1,
            0, 0, 0, 1,
            0, 0, 0, 1,
            1, 0, 0, 1,
            0, 1, 1, 0,
        };

        private static readonly byte[] LetterK =
        {
            1, 0, 0, 1,
            1, 0, 1, 0,
            1, 1, 0, 0,
            1, 0, 1, 0,
            1, 0, 0, 1,
        };

        private static readonly byte[] LetterL =
        {
            1, 0, 0, 0,
            1, 0, 0, 0,
            1, 0, 0, 0,
            1, 0, 0, 0,
            1, 1, 1, 1,
        };

        private static readonly byte[] LetterM =
        {
            1, 0, 0, 0, 1,
            1, 1, 0, 1, 1,
            1, 0, 1, 0, 1,
            1, 0, 0, 0, 1,
            1, 0, 0, 0, 1,
        };

        private static readonly byte[] LetterN =
        {
            1, 0, 0, 1,
            1, 1, 0, 1,
            1, 0, 1, 1,
            1, 0, 0, 1,
            1, 0, 0, 1,
        };

        private static readonly byte[] LetterO =
        {
            0, 1, 1, 0,
            1, 0, 0, 1,
            1, 0, 0, 1,
            1, 0, 0, 1,
            0, 1, 1, 0,
        };

        private static readonly byte[] LetterP =
        {
            1, 1, 1, 0,
            1, 0, 0, 1,
            1, 1, 1, 0,
            1, 0, 0, 0,
            1, 0, 0, 0,
        };

        // Letter Q with descender (6 pixels tall)
        private static readonly byte[] LetterQ =
        {
            0, 1, 1, 1, 0,
            1, 0, 0, 0, 1,
            1, 0, 0, 0, 1,
            1, 0, 0, 0, 1,
            0, 1, 1, 1, 0,
            0, 0, 0, 1, 1,
        };

        private static readonly byte[] LetterR =
        {
            1, 1, 1, 0,
            1, 0, 0, 1,
            1, 1, 1, 0,
            1, 0, 1, 0,
            1, 0, 0, 1,
        };

        private static readonly byte[] LetterS =
        {
            0, 1, 1, 1,
            1, 0, 0, 0,
            0, 1, 1, 0,
            0, 0, 0, 1,
            1, 1, 1, 0,
        };

        private static readonly byte[] LetterT =
        {
            1, 1, 1,
            0, 1, 0,
            0, 1, 0,
            0, 1, 0,
            0, 1, 0,
        };

        private static readonly byte[] LetterU =
        {
            1, 0, 0, 1,
            1, 0, 0, 1,
            1, 0, 0, 1,
            1, 0, 0, 1,
            0, 1, 1, 0,
        };

        private static readonly byte[] LetterV =
        {
            1, 0, 1,
            1, 0, 1,
            1, 0, 1,
            1, 0, 1,
            0, 1, 0,
        };

        private static readonly byte[] LetterW =
        {
            1, 0, 0, 0, 1,
            1, 0, 1, 0, 1,
            1, 0, 1, 0, 1,
            1, 0, 1, 0, 1,
            0, 1, 0, 1, 0,
        };

        private static readonly byte[] LetterX =
        {
            1, 0, 0, 1,
            1, 0, 0, 1,
            0, 1, 1, 0,
            1, 0, 0, 1,
            1, 0, 0, 1,
        };

        private static readonly byte[] LetterY =
        {
            1, 0, 1,
            1, 0, 1,
            0, 1, 0,
            0, 1, 0,
            0, 1, 0,
        };

        private static readonly byte[] LetterZ =
        {
            1, 1, 1, 1, 1,
            0, 0, 0, 0, 1,
            0, 1, 1, 1, 0,
            1, 0, 0, 0, 0,
            1, 1, 1, 1, 1,
        };

        private static readonly byte[] LetterÅ =
        {
            0, 1, 1, 0,
            0, 0, 0, 0,
            0, 1, 1, 0,
            1, 0, 0, 1,
            1, 1, 1, 1,
            1, 0, 0, 1,
        };

        private static readonly byte[] LetterÄ =
        {
            1, 0, 0, 1,
            0, 0, 0, 0,
            0, 1, 1, 0,
            1, 0, 0, 1,
            1, 1, 1, 1,
            1, 0, 0, 1,
        };

        private static readonly byte[] LetterÖ =
        {
            1, 0, 0, 1,
            0, 0, 0, 0,
            0, 1, 1, 0,
            1, 0, 0, 1,
            1, 0, 0, 1,
            0, 1, 1, 0,
        };

        private static readonly byte[] LetterQuestion =
        {
            1, 1, 1, 0,
            0, 0, 0, 1,
            0, 1, 1, 0,
            0, 0, 0, 0,
            0, 1, 0, 0,
        };

        private static readonly byte[] LetterLine =
        {
            1, 1, 1,
        };

        private static readonly byte[] Percentage =
        {
            0, 1, 0, 0, 1,
            0, 0, 0, 1, 0,
            0, 0, 1, 0, 0,
            0, 1, 0, 0, 0,
            1, 0, 0, 1, 0,
        };

        private static readonly byte[] Quote =
        {
            1, 0, 1,
            1, 0, 1,
        };

        private static readonly byte[] Underscore =
        {
            1, 1, 1,
        };

        private static readonly byte[] Slash =
        {
            0, 0, 0, 0, 1,
            0, 0, 0, 1, 0,
            0, 0, 1, 0, 0,
            0, 1, 0, 0, 0,
            1, 0, 0, 0, 0,
        };

        private static readonly byte[] BackSlash =
        {
            1, 0, 0, 0, 0,
            0, 1, 0, 0, 0,
            0, 0, 1, 0, 0,
            0, 0, 0, 1, 0,
            0, 0, 0, 0, 1,
        };

        // ============ NARROW PUNCTUATION CHARACTERS ============

        private static readonly byte[] Period =
        {
            1,
        };

        // Comma with descender (2 pixels tall)
        private static readonly byte[] Comma =
        {
            0, 1,
            1, 0,
        };

        private static readonly byte[] Colon =
        {
            1,
            0,
            0,
            1,
        };

        // Semicolon with descender (6 pixels tall)
        private static readonly byte[] Semicolon =
        {
            0, 1,
            0, 0,
            0, 0,
            0, 1,
            1, 0,
        };

        private static readonly byte[] ExclamationMark =
        {
            1,
            1,
            1,
            0,
            1,
        };

        private static readonly byte[] Apostrophe =
        {
            1,
            1,
        };

        private static readonly byte[] OpenParenthesis =
        {
            0, 1,
            1, 0,
            1, 0,
            1, 0,
            0, 1,
        };

        private static readonly byte[] CloseParenthesis =
        {
            1, 0,
            0, 1,
            0, 1,
            0, 1,
            1, 0,
        };

        private static readonly byte[] Asterisk =
        {
            0, 1, 0,
            1, 1, 1,
            0, 1, 0,
            1, 0, 1,
            0, 0, 0,
        };

        private static readonly byte[] OpenSquareBracket =
        {
            1, 1,
            1, 0,
            1, 0,
            1, 0,
            1, 1,
        };

        private static readonly byte[] CloseSquareBracket =
        {
            1, 1,
            0, 1,
            0, 1,
            0, 1,
            1, 1,
        };

        private static readonly byte[] OpenCurlyBracket =
        {
            0, 1, 1,
            0, 1, 0,
            1, 0, 0,
            0, 1, 0,
            0, 1, 1,
        };

        private static readonly byte[] CloseCurlyBracket =
        {
            1, 1, 0,
            0, 1, 0,
            0, 0, 1,
            0, 1, 0,
            1, 1, 0,
        };

        private static readonly byte[] Plus =
        {
            0, 1, 0,
            1, 1, 1,
            0, 1, 0,
        };

        private static readonly byte[] EqualsSign =
        {
            1, 1, 1,
            0, 0, 0,
            1, 1, 1,
        };

        private static readonly byte[] LessThan =
        {
            0, 0, 1,
            0, 1, 0,
            1, 0, 0,
            0, 1, 0,
            0, 0, 1,
        };

        private static readonly byte[] GreaterThan =
        {
            1, 0, 0,
            0, 1, 0,
            0, 0, 1,
            0, 1, 0,
            1, 0, 0,
        };

        private static readonly byte[] Pipe =
        {
            1,
            1,
            1,
            1,
            1,
        };

        // Character lookup dictionary with width and height information
        private static readonly Dictionary<char, CharacterData> CharacterMap = new Dictionary<char, CharacterData>
        {
            // Numbers (4 pixels wide, 5 pixels tall)
            { '0', new CharacterData(Number0, 4, StandardCharacterHeight) },
            { '1', new CharacterData(Number1, 3, StandardCharacterHeight) },
            { '2', new CharacterData(Number2, 4, StandardCharacterHeight) },
            { '3', new CharacterData(Number3, 4, StandardCharacterHeight) },
            { '4', new CharacterData(Number4, 4, StandardCharacterHeight) },
            { '5', new CharacterData(Number5, 4, StandardCharacterHeight) },
            { '6', new CharacterData(Number6, 4, StandardCharacterHeight) },
            { '7', new CharacterData(Number7, 4, StandardCharacterHeight) },
            { '8', new CharacterData(Number8, 4, StandardCharacterHeight) },
            { '9', new CharacterData(Number9, 4, StandardCharacterHeight) },

            // Letters (lowercase) - variable widths, mostly 5 pixels tall
            { 'a', new CharacterData(LetterA, 4, StandardCharacterHeight) },
            { 'b', new CharacterData(LetterB, 4, StandardCharacterHeight) },
            { 'c', new CharacterData(LetterC, 4, StandardCharacterHeight) },
            { 'd', new CharacterData(LetterD, 4, StandardCharacterHeight) },
            { 'e', new CharacterData(LetterE, 4, StandardCharacterHeight) },
            { 'f', new CharacterData(LetterF, 4, StandardCharacterHeight) },
            { 'g', new CharacterData(LetterG, 4, StandardCharacterHeight) },
            { 'h', new CharacterData(LetterH, 4, StandardCharacterHeight) },
            { 'i', new CharacterData(LetterI, 3, StandardCharacterHeight) },
            { 'j', new CharacterData(LetterJ, 4, StandardCharacterHeight) },
            { 'k', new CharacterData(LetterK, 4, StandardCharacterHeight) },
            { 'l', new CharacterData(LetterL, 4, StandardCharacterHeight) },
            { 'm', new CharacterData(LetterM, 5, StandardCharacterHeight) },
            { 'n', new CharacterData(LetterN, 4, StandardCharacterHeight) },
            { 'o', new CharacterData(LetterO, 4, StandardCharacterHeight) },
            { 'p', new CharacterData(LetterP, 4, StandardCharacterHeight) },
            { 'q', new CharacterData(LetterQ, 5, 6, 0) }, // 6 pixels tall with 1 pixel descender
            { 'r', new CharacterData(LetterR, 4, StandardCharacterHeight) },
            { 's', new CharacterData(LetterS, 4, StandardCharacterHeight) },
            { 't', new CharacterData(LetterT, 3, StandardCharacterHeight) },
            { 'u', new CharacterData(LetterU, 4, StandardCharacterHeight) },
            { 'v', new CharacterData(LetterV, 3, StandardCharacterHeight) },
            { 'w', new CharacterData(LetterW, 5, StandardCharacterHeight) },
            { 'x', new CharacterData(LetterX, 4, StandardCharacterHeight) },
            { 'y', new CharacterData(LetterY, 3, StandardCharacterHeight) },
            { 'z', new CharacterData(LetterZ, 5, StandardCharacterHeight) },

            // Letters (uppercase - same as lowercase)
            { 'A', new CharacterData(LetterA, 4, StandardCharacterHeight) },
            { 'B', new CharacterData(LetterB, 4, StandardCharacterHeight) },
            { 'C', new CharacterData(LetterC, 4, StandardCharacterHeight) },
            { 'D', new CharacterData(LetterD, 4, StandardCharacterHeight) },
            { 'E', new CharacterData(LetterE, 4, StandardCharacterHeight) },
            { 'F', new CharacterData(LetterF, 4, StandardCharacterHeight) },
            { 'G', new CharacterData(LetterG, 4, StandardCharacterHeight) },
            { 'H', new CharacterData(LetterH, 4, StandardCharacterHeight) },
            { 'I', new CharacterData(LetterI, 3, StandardCharacterHeight) },
            { 'J', new CharacterData(LetterJ, 4, StandardCharacterHeight) },
            { 'K', new CharacterData(LetterK, 4, StandardCharacterHeight) },
            { 'L', new CharacterData(LetterL, 4, StandardCharacterHeight) },
            { 'M', new CharacterData(LetterM, 5, StandardCharacterHeight) },
            { 'N', new CharacterData(LetterN, 4, StandardCharacterHeight) },
            { 'O', new CharacterData(LetterO, 4, StandardCharacterHeight) },
            { 'P', new CharacterData(LetterP, 4, StandardCharacterHeight) },
            { 'Q', new CharacterData(LetterQ, 5, 6, 0) }, // 6 pixels tall with 1 pixel descender
            { 'R', new CharacterData(LetterR, 4, StandardCharacterHeight) },
            { 'S', new CharacterData(LetterS, 4, StandardCharacterHeight) },
            { 'T', new CharacterData(LetterT, 3, StandardCharacterHeight) },
            { 'U', new CharacterData(LetterU, 4, StandardCharacterHeight) },
            { 'V', new CharacterData(LetterV, 3, StandardCharacterHeight) },
            { 'W', new CharacterData(LetterW, 5, StandardCharacterHeight) },
            { 'X', new CharacterData(LetterX, 4, StandardCharacterHeight) },
            { 'Y', new CharacterData(LetterY, 3, StandardCharacterHeight) },
            { 'Z', new CharacterData(LetterZ, 5, StandardCharacterHeight) },

            { 'å', new CharacterData(LetterÅ, 4, 6, -1) }, // 6 pixels tall with 1 pixel above standard height
            { 'ä', new CharacterData(LetterÄ, 4, 6, -1) }, // 6 pixels tall with 1 pixel above standard height
            { 'ö', new CharacterData(LetterÖ, 4, 6, -1) }, // 6 pixels tall with 1 pixel above standard height
            { 'Å', new CharacterData(LetterÅ, 4, 6, -1) }, // 6 pixels tall with 1 pixel above standard height
            { 'Ä', new CharacterData(LetterÄ, 4, 6, -1) }, // 6 pixels tall with 1 pixel above standard height
            { 'Ö', new CharacterData(LetterÖ, 4, 6, -1) }, // 6 pixels tall with 1 pixel above standard height

            // Special characters and punctuation
            { ' ', new CharacterData(WhiteSpace, 3, StandardCharacterHeight) },
            { '.', new CharacterData(Period, 1, 1, 4) },
            { ',', new CharacterData(Comma, 2, 2, 4) }, // 2 pixels tall, positioned 1 pixel below baseline
            { ':', new CharacterData(Colon, 1, 4, 1) },
            { ';', new CharacterData(Semicolon, 2, 5, 1) },
            { '!', new CharacterData(ExclamationMark, 1, StandardCharacterHeight) },
            { '\'', new CharacterData(Apostrophe, 1, 2) },
            { '?', new CharacterData(LetterQuestion, 4, StandardCharacterHeight) },
            { '-', new CharacterData(LetterLine, 3, 1, 2) },
            { '%', new CharacterData(Percentage, 5, StandardCharacterHeight) },
            { '"', new CharacterData(Quote, 3, 2, 0) },
            { '_', new CharacterData(Underscore, 3, 1, 5) },
            { '/', new CharacterData(Slash, 5, StandardCharacterHeight) },
            { '\\', new CharacterData(BackSlash, 5, StandardCharacterHeight) },
            { '(', new CharacterData(OpenParenthesis, 2, StandardCharacterHeight) },
            { ')', new CharacterData(CloseParenthesis, 2, StandardCharacterHeight) },
            { '*', new CharacterData(Asterisk, 3, StandardCharacterHeight) },
            { '[', new CharacterData(OpenSquareBracket, 2, StandardCharacterHeight) },
            { ']', new CharacterData(CloseSquareBracket, 2, StandardCharacterHeight) },
            { '{', new CharacterData(OpenCurlyBracket, 3, StandardCharacterHeight) },
            { '}', new CharacterData(CloseCurlyBracket, 3, StandardCharacterHeight) },
            { '+', new CharacterData(Plus, 3, 3, 1) },
            { '=', new CharacterData(EqualsSign, 3, 3, 1) },
            { '<', new CharacterData(LessThan, 3, StandardCharacterHeight) },
            { '>', new CharacterData(GreaterThan, 3, StandardCharacterHeight) },
            { '|', new CharacterData(Pipe, 1, StandardCharacterHeight) },
        };

        /// <summary>
        /// Gets the character data for a character
        /// </summary>
        /// <param name="character">Character to get data for</param>
        /// <returns>CharacterData containing bitmap and dimensions, or unsupported character data if not found</returns>
        public CharacterData GetCharacter(char character)
        {
            return CharacterMap.TryGetValue(character, out CharacterData data) ? data : DefaultUnsupportedCharacter;
        }

        /// <summary>
        /// Gets the recommended line height for this font in pixels
        /// This accommodates the tallest character including ascenders and descenders
        /// </summary>
        public int GetLineHeight()
        {
            return LineHeight;
        }

        /// <summary>
        /// Gets the baseline offset within the line height in pixels
        /// This is the Y coordinate where the baseline should be positioned within the line height
        /// </summary>
        public int GetBaselineOffset()
        {
            return BaselineOffset;
        }

        /// <summary>
        /// Checks if a character is supported by this font
        /// </summary>
        /// <param name="character">Character to check</param>
        /// <returns>True if character is supported, false otherwise</returns>
        public bool IsCharacterSupported(char character)
        {
            return CharacterMap.ContainsKey(character);
        }

        /// <summary>
        /// Gets the default character data to display when an unsupported character is encountered
        /// </summary>
        public CharacterData DefaultUnsupportedCharacter => DefaultUnsupportedCharacterData;
    }
}
