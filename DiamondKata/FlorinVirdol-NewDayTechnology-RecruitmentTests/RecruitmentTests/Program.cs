using RecruitmentTests;

var messages = new
{
    enterEscOrLetter = "Press ESC to stop or Enter a letter to generate:",
    youPressed = "You pressed:",
    notLetterChar = "Character is not a letter!"
};

ConsoleKeyInfo inputKey;
var diamondKataService = new DiamondKataService();

do
{
    Console.WriteLine(messages.enterEscOrLetter);
    inputKey = Console.ReadKey();

    if (inputKey.Key == ConsoleKey.Escape)
    {
        continue;
    }

    var inputCharacter = diamondKataService.ConvertDiacriticToNormalLetter(inputKey.KeyChar);
    Console.WriteLine($"\n{messages.youPressed} {inputCharacter}");

    if (!char.IsLetter(inputCharacter))
    {
        Console.WriteLine(messages.notLetterChar);
        continue;
    }

    try
    {
        ConsoleOutputDiamondKata(diamondKataService.GenerateDiamondKata(inputCharacter));
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }

} while (inputKey.Key != ConsoleKey.Escape);

void ConsoleOutputDiamondKata(List<char[]> diamondKataData)
{
    foreach (var rowsChars in diamondKataData)
    {
        foreach (var c in rowsChars)
        {
            Console.Write(c);
        }
        Console.WriteLine();
    }
}