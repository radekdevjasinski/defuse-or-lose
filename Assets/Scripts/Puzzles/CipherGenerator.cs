using System.Collections.Generic;
using System.Text;

namespace DefuseOrLose
{
    public class CipherGenerator
    {
        private const int WordLength = 5;
        private const int DigitCount = 10;
        private const int HighDigitThreshold = 5;

        private static readonly List<char> evenLetters = new List<char> { 'A', 'B', 'D', 'G', 'F', 'H' };
        private static readonly List<char> oddLetters = new List<char> { 'C', 'J', 'K', 'L', 'Z' };

        private static readonly Dictionary<char, char> evenMappingLow = new Dictionary<char, char>
        {
            { 'A', 'X' }, { 'B', 'Y' }, { 'D', 'W' }, { 'G', 'T' }, { 'F', 'M' }, { 'H', 'P' }
        };
        private static readonly Dictionary<char, char> oddMappingLow = new Dictionary<char, char>
        {
            { 'C', 'J' }, { 'J', 'K' }, { 'K', 'L' }, { 'L', 'Z' }, { 'Z', 'R' }
        };
        private static readonly Dictionary<char, char> evenMappingHigh = new Dictionary<char, char>
        {
            { 'A', 'N' }, { 'B', 'O' }, { 'D', 'U' }, { 'G', 'V' }, { 'F', 'S' }, { 'H', 'E' }
        };
        private static readonly Dictionary<char, char> oddMappingHigh = new Dictionary<char, char>
        {
            { 'C', 'Q' }, { 'J', 'B' }, { 'K', 'C' }, { 'L', 'D' }, { 'Z', 'A' }
        };

        private readonly System.Random random = new System.Random();

        public string GenerateCipher(out string decryptedWord)
        {
            StringBuilder encrypted = new StringBuilder();
            StringBuilder decrypted = new StringBuilder();

            for (int i = 0; i < WordLength; i++)
            {
                int digit = random.Next(DigitCount);
                bool isEvenDigit = digit % 2 == 0;
                List<char> letters = isEvenDigit ? evenLetters : oddLetters;
                char letter = letters[random.Next(letters.Count)];

                encrypted.Append(digit).Append(letter);
                decrypted.Append(SelectMapping(digit, isEvenDigit)[letter]);
            }

            decryptedWord = decrypted.ToString();
            return encrypted.ToString();
        }

        private static Dictionary<char, char> SelectMapping(int digit, bool isEvenDigit)
        {
            if (digit < HighDigitThreshold)
            {
                return isEvenDigit ? evenMappingLow : oddMappingLow;
            }
            return isEvenDigit ? evenMappingHigh : oddMappingHigh;
        }
    }
}
