#nullable enable

using OledSharp;

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
        /// Height of all characters in this font (fixed height)
        /// </summary>
        int CharacterHeight { get; }
        
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
