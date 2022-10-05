using FluentAssertions;
using RecruitmentTests;

namespace XUnitTestProject
{
    public class DiamondKataServiceUnitTests
    {
        private const bool IsUpperInputCharacter = true;
        private const bool IsNotUpperInputCharacter = false;

        public const char CharSeparator = '_';

        [Theory]
        [InlineData('\0')]
        [InlineData('1')]
        [InlineData('-')]
        [InlineData('.')]
        public void GetAlphabetLetterPosition_InvalidLetter_Returns0(char invalidLetter)
        {
            var diamondKataService = new DiamondKataService();

            var result = diamondKataService.GetAlphabetLetterPosition(invalidLetter);

            result.Should().Be(0);
        }

        [Theory]
        [InlineData('a', 1)]
        [InlineData('c', 3)]
        [InlineData('z', 26)]
        public void GetAlphabetLetterPosition_ValidLetter_ReturnsPosition(char letter, int position)
        {
            var diamondKataService = new DiamondKataService();

            var result = diamondKataService.GetAlphabetLetterPosition(letter);

            result.Should().Be(position);
        }

        [Theory]

        [InlineData('c', IsNotUpperInputCharacter, 5)]
        [InlineData('D', IsUpperInputCharacter, 7)]
        [InlineData('a', IsNotUpperInputCharacter, 4)]
        [InlineData('z', IsNotUpperInputCharacter, 26)]
        public void GenerateDiamondKataCharactersRow_ValidData_CorrectOutput(char character, bool isUpperInputCharacter, int totalLetters)
        {
            var diamondKataService = new DiamondKataService();
            var totalColumns = 2 * totalLetters - 1;
            var characterPosition = diamondKataService.GetAlphabetLetterPosition(character);
            var positionToFill1 = totalLetters - 1 - (characterPosition - 1);
            var positionToFill2 = totalLetters - 1 + (characterPosition - 1);

            var result = diamondKataService.GenerateDiamondKataCharactersRow(character, isUpperInputCharacter, totalLetters, totalColumns);

            result.Should().HaveCount(totalColumns);
            result.Where((_, i) => i != positionToFill1 && i != positionToFill2).Should()
                .OnlyContain(c => c == CharSeparator);
            result[positionToFill1].Should().Be(result[positionToFill2]);
            result[positionToFill1].Should().Be(isUpperInputCharacter ? char.ToUpper(character) : character);
        }

        [Theory]
        [InlineData('c', IsNotUpperInputCharacter, 1)]
        [InlineData('D', IsUpperInputCharacter, 3)]
        [InlineData('\0', IsUpperInputCharacter, 6)]
        [InlineData('1', IsUpperInputCharacter, 1)]
        [InlineData('5', IsUpperInputCharacter, 6)]
        [InlineData('-', IsUpperInputCharacter, 1)]
        [InlineData('.', IsUpperInputCharacter, 1)]
        public void GenerateDiamondKataCharactersRow_InvalidData_ReturnsArrayWithCharSeparator(char character, bool isUpperInputCharacter, int totalLetters)
        {
            var diamondKataService = new DiamondKataService();
            var totalColumns = 2 * totalLetters - 1;

            var result = diamondKataService.GenerateDiamondKataCharactersRow(character, isUpperInputCharacter, totalLetters, totalColumns);

            result.Should().HaveCount(totalColumns);
            result.Should().OnlyContain(c => c == CharSeparator);
        }

        [Theory]
        [InlineData('\0')]
        [InlineData('1')]
        [InlineData('-')]
        [InlineData('.')]
        public void GenerateDiamondKata_InvalidData_EmptyOutput(char character)
        {
            var diamondKataService = new DiamondKataService();

            var result = diamondKataService.GenerateDiamondKata(character);

            result.Should().BeEmpty();
        }

        [Theory]
        [InlineData('a')]
        [InlineData('B')]
        [InlineData('c')]
        [InlineData('z')]
        public void GenerateDiamondKata_ValidData_CorrectOutput(char character)
        {
            var diamondKataService = new DiamondKataService();
            var totalLetters = diamondKataService.GetAlphabetLetterPosition(char.ToLower(character));
            var totalRows = 2 * totalLetters - 1;

            var result = diamondKataService.GenerateDiamondKata(character);

            result.Should().HaveCount(totalRows);
        }

        [Theory]
        [InlineData('ă', 'a')]
        [InlineData('â', 'a')]
        [InlineData('ä', 'a')]
        [InlineData('ą', 'a')]
        [InlineData('î', 'i')]
        [InlineData('ö', 'o')]
        [InlineData('ø', 'o')]
        [InlineData('ü', 'u')]
        public void ConvertDiacriticToNormalLetter_Diacritic_NormalLetter(char diacriticCharacter, char normalLetter)
        {
            var diamondKataService = new DiamondKataService();

            var result = diamondKataService.ConvertDiacriticToNormalLetter(diacriticCharacter);

            result.Should().Be(normalLetter);
        }
    }
}