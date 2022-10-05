using System.Text;

namespace RecruitmentTests
{
    public class DiamondKataService
    {
        public const char CharSeparator = '_';

        public char ConvertDiacriticToNormalLetter(char diacriticCharacter)
        {
            // https://stackoverflow.com/a/2086575/1037190

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(diacriticCharacter.ToString());
            var conversionSucceeded = char.TryParse(Encoding.UTF8.GetString(tempBytes), out var normalLetter);

            return conversionSucceeded ? normalLetter : '\0';
        }

        public List<char[]> GenerateDiamondKata(char inputCharacter)
        {
            var rowsCharacters = new List<char[]>();

            if (!char.IsLetter(inputCharacter))
            {
                return rowsCharacters;
            }

            var isUpperInputCharacter = char.IsUpper(inputCharacter);
            var lowerInputCharacter = char.ToLower(inputCharacter);
            var totalLetters = GetAlphabetLetterPosition(lowerInputCharacter);
            var totalColumns = 2 * totalLetters - 1;

            for (var c = 'a'; c < lowerInputCharacter; c++)
            {
                rowsCharacters.Add(GenerateDiamondKataCharactersRow(c, isUpperInputCharacter, totalLetters, totalColumns));
            }

            for (var c = lowerInputCharacter; c >= 'a'; c--)
            {
                rowsCharacters.Add(GenerateDiamondKataCharactersRow(c, isUpperInputCharacter, totalLetters, totalColumns));
            }

            return rowsCharacters;
        }

        public int GetAlphabetLetterPosition(char letter) =>
            char.IsLetter(letter) ? char.ToLower(letter) - 'a' + 1 : 0;

        public char[] GenerateDiamondKataCharactersRow(char character, bool isUpperInputCharacter, int totalLetters, int totalColumns)
        {
            var columnsCharacters = new char[totalColumns];
            Array.Fill(columnsCharacters, CharSeparator);

            var characterPosition = GetAlphabetLetterPosition(character);

            if (!char.IsLetter(character) || totalLetters < characterPosition)
            {
                return columnsCharacters;
            }

            var characterPositionToAddSubtract = characterPosition - 1;

            var positionToFill1 = totalLetters - 1 - characterPositionToAddSubtract;
            var positionToFill2 = totalLetters - 1 + characterPositionToAddSubtract;

            columnsCharacters[positionToFill1] = columnsCharacters[positionToFill2] = isUpperInputCharacter ? char.ToUpper(character) : character;

            return columnsCharacters;
        }
    }
}