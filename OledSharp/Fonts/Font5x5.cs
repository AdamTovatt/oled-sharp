#nullable enable
using OledSharp;
using System;
using System.Collections.Generic;

namespace OledSharp.Fonts
{
    /// <summary>
    /// Variable-width bitmap font containing numbers, letters, and special characters
    /// Characters can be 1-5 pixels wide, all are 5 pixels tall
    /// Uses compact storage - only stores actual character pixels
    /// </summary>
    public class Font5x5 : IFont
    {
        public const int CharacterHeight = 5;
        
        int IFont.CharacterHeight => CharacterHeight;

        // Character bitmap definitions - compact storage (width × 5 bytes per character)
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
            0, 1, 0, 1,
            1, 1, 1, 1,
            0, 1, 0, 1,
            1, 1, 1, 1,
            0, 1, 0, 1,
        };

        private static readonly CharacterData DefaultUnsupportedCharacterData = new CharacterData(Unsupported, 4, CharacterHeight);

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

        private static readonly byte[] LetterQ = 
        {
            0, 1, 1, 1, 0,
            1, 0, 0, 0, 1,
            1, 0, 0, 0, 1,
            0, 1, 1, 1, 0,
            0, 0, 0, 0, 1,
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

        private static readonly byte[] LetterQuestion = 
        {
            0, 1, 1, 1, 0,
            0, 0, 0, 0, 1,
            0, 0, 1, 1, 0,
            0, 0, 0, 0, 0,
            0, 0, 1, 0, 0,
        };

        private static readonly byte[] LetterLine = 
        {
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 1, 1, 1,
            0, 0, 0, 0,
            0, 0, 0, 0,
        };

        // ============ NARROW PUNCTUATION CHARACTERS ============
        
        private static readonly byte[] Period = 
        {
            1,
            0,
            0,
            0,
            0,
        };

        private static readonly byte[] Comma = 
        {
            0, 0,
            0, 0,
            0, 0,
            0, 1,
            1, 0,
        };

        private static readonly byte[] Colon = 
        {
            1,
            0,
            0,
            1,
            0,
        };

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
            1, 1,
            0, 0,
            0, 0,
            0, 0,
            0, 0,
        };

        // Character lookup dictionary with width information
        private static readonly Dictionary<char, CharacterData> CharacterMap = new Dictionary<char, CharacterData>
        {
            // Numbers (4 pixels wide)
            { '0', new CharacterData(Number0, 4, CharacterHeight) },
            { '1', new CharacterData(Number1, 3, CharacterHeight) },
            { '2', new CharacterData(Number2, 4, CharacterHeight) },
            { '3', new CharacterData(Number3, 4, CharacterHeight) },
            { '4', new CharacterData(Number4, 4, CharacterHeight) },
            { '5', new CharacterData(Number5, 4, CharacterHeight) },
            { '6', new CharacterData(Number6, 4, CharacterHeight) },
            { '7', new CharacterData(Number7, 4, CharacterHeight) },
            { '8', new CharacterData(Number8, 4, CharacterHeight) },
            { '9', new CharacterData(Number9, 4, CharacterHeight) },

            // Letters (lowercase) - variable widths
            { 'a', new CharacterData(LetterA, 4, CharacterHeight) },
            { 'b', new CharacterData(LetterB, 4, CharacterHeight) },
            { 'c', new CharacterData(LetterC, 4, CharacterHeight) },
            { 'd', new CharacterData(LetterD, 4, CharacterHeight) },
            { 'e', new CharacterData(LetterE, 4, CharacterHeight) },
            { 'f', new CharacterData(LetterF, 4, CharacterHeight) },
            { 'g', new CharacterData(LetterG, 4, CharacterHeight) },
            { 'h', new CharacterData(LetterH, 4, CharacterHeight) },
            { 'i', new CharacterData(LetterI, 3, CharacterHeight) },
            { 'j', new CharacterData(LetterJ, 4, CharacterHeight, 1) }, // Offset down by 1 pixel for descender
            { 'k', new CharacterData(LetterK, 4, CharacterHeight) },
            { 'l', new CharacterData(LetterL, 4, CharacterHeight) },
            { 'm', new CharacterData(LetterM, 5, CharacterHeight) },
            { 'n', new CharacterData(LetterN, 4, CharacterHeight) },
            { 'o', new CharacterData(LetterO, 4, CharacterHeight) },
            { 'p', new CharacterData(LetterP, 4, CharacterHeight, 1) }, // Offset down by 1 pixel for descender
            { 'q', new CharacterData(LetterQ, 5, CharacterHeight, 1) }, // Offset down by 1 pixel for descender
            { 'r', new CharacterData(LetterR, 4, CharacterHeight) },
            { 's', new CharacterData(LetterS, 4, CharacterHeight) },
            { 't', new CharacterData(LetterT, 3, CharacterHeight) },
            { 'u', new CharacterData(LetterU, 4, CharacterHeight) },
            { 'v', new CharacterData(LetterV, 3, CharacterHeight) },
            { 'w', new CharacterData(LetterW, 5, CharacterHeight) },
            { 'x', new CharacterData(LetterX, 4, CharacterHeight) },
            { 'y', new CharacterData(LetterY, 3, CharacterHeight) },
            { 'z', new CharacterData(LetterZ, 5, CharacterHeight) },

            // Letters (uppercase - same as lowercase)
            { 'A', new CharacterData(LetterA, 4, CharacterHeight) },
            { 'B', new CharacterData(LetterB, 4, CharacterHeight) },
            { 'C', new CharacterData(LetterC, 4, CharacterHeight) },
            { 'D', new CharacterData(LetterD, 4, CharacterHeight) },
            { 'E', new CharacterData(LetterE, 4, CharacterHeight) },
            { 'F', new CharacterData(LetterF, 4, CharacterHeight) },
            { 'G', new CharacterData(LetterG, 4, CharacterHeight) },
            { 'H', new CharacterData(LetterH, 4, CharacterHeight) },
            { 'I', new CharacterData(LetterI, 3, CharacterHeight) },
            { 'J', new CharacterData(LetterJ, 4, CharacterHeight, 1) }, // Offset down by 1 pixel for descender
            { 'K', new CharacterData(LetterK, 4, CharacterHeight) },
            { 'L', new CharacterData(LetterL, 4, CharacterHeight) },
            { 'M', new CharacterData(LetterM, 5, CharacterHeight) },
            { 'N', new CharacterData(LetterN, 4, CharacterHeight) },
            { 'O', new CharacterData(LetterO, 4, CharacterHeight) },
            { 'P', new CharacterData(LetterP, 4, CharacterHeight, 1) }, // Offset down by 1 pixel for descender
            { 'Q', new CharacterData(LetterQ, 5, CharacterHeight, 1) }, // Offset down by 1 pixel for descender
            { 'R', new CharacterData(LetterR, 4, CharacterHeight) },
            { 'S', new CharacterData(LetterS, 4, CharacterHeight) },
            { 'T', new CharacterData(LetterT, 3, CharacterHeight) },
            { 'U', new CharacterData(LetterU, 4, CharacterHeight) },
            { 'V', new CharacterData(LetterV, 3, CharacterHeight) },
            { 'W', new CharacterData(LetterW, 5, CharacterHeight) },
            { 'X', new CharacterData(LetterX, 4, CharacterHeight) },
            { 'Y', new CharacterData(LetterY, 3, CharacterHeight) },
            { 'Z', new CharacterData(LetterZ, 5, CharacterHeight) },

            // Special characters and punctuation
            { ' ', new CharacterData(WhiteSpace, 3, CharacterHeight) },
            { '.', new CharacterData(Period, 1, CharacterHeight) },
            { ',', new CharacterData(Comma, 2, CharacterHeight, 1) }, // Offset down by 1 pixel
            { ':', new CharacterData(Colon, 1, CharacterHeight) },
            { ';', new CharacterData(Semicolon, 2, CharacterHeight, 1) }, // Offset down by 1 pixel
            { '!', new CharacterData(ExclamationMark, 1, CharacterHeight) },
            { '\'', new CharacterData(Apostrophe, 2, CharacterHeight) },
            { '?', new CharacterData(LetterQuestion, 5, CharacterHeight) },
            { '-', new CharacterData(LetterLine, 4, CharacterHeight) },
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
