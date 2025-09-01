#nullable enable

namespace OledSharp.Fonts
{
    /// <summary>
    /// Interface for bitmap fonts used by text renderers
    /// </summary>
    public interface IFont
    {
        /// <summary>
        /// Gets the character data for a specific character
        /// </summary>
        /// <param name="character">Character to get data for</param>
        /// <returns>CharacterData containing bitmap and dimensions</returns>
        CharacterData GetCharacter(char character);

        /// <summary>
        /// Gets the recommended line height for this font in pixels
        /// This should accommodate the tallest character including ascenders and descenders
        /// </summary>
        int LineHeight { get; }

        /// <summary>
        /// Gets the baseline offset from the bottom of the line height
        /// This is the distance from the bottom of the line to where characters should be positioned
        /// For bottom-left origin fonts, this is typically 0
        /// </summary>
        int BaselineOffset { get; }

        /// <summary>
        /// Checks if a character is supported by this font
        /// </summary>
        /// <param name="character">Character to check</param>
        /// <returns>True if character is supported, false otherwise</returns>
        bool IsCharacterSupported(char character);

        /// <summary>
        /// Gets the default character data to display when an unsupported character is encountered
        /// </summary>
        CharacterData DefaultUnsupportedCharacter { get; }
    }
}
