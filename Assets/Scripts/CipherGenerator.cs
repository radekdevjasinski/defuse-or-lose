using UnityEngine;
using System.Collections.Generic;

public class CipherGenerator : MonoBehaviour
{
    private List<char> evenLetters = new List<char> { 'A', 'B', 'D', 'G', 'F', 'H' };
    private List<char> oddLetters = new List<char> { 'C', 'J', 'K', 'L', 'Z' };

    private Dictionary<char, char> evenMappingLow = new Dictionary<char, char>
    {
        { 'A', 'X' }, { 'B', 'Y' }, { 'D', 'W' }, { 'G', 'T' }, { 'F', 'M' }, { 'H', 'P' }
    };
    private Dictionary<char, char> oddMappingLow = new Dictionary<char, char>
    {
        { 'C', 'J' }, { 'J', 'K' }, { 'K', 'L' }, { 'L', 'Z' }, { 'Z', 'R' }
    };

    private Dictionary<char, char> evenMappingHigh = new Dictionary<char, char>
    {
        { 'A', 'N' }, { 'B', 'O' }, { 'D', 'U' }, { 'G', 'V' }, { 'F', 'S' }, { 'H', 'E' }
    };
    private Dictionary<char, char> oddMappingHigh = new Dictionary<char, char>
    {
        { 'C', 'Q' }, { 'J', 'B' }, { 'K', 'C' }, { 'L', 'D' }, { 'Z', 'A' }
    };

    public string GenerateCipher(out string decryptedWord)
    {
        System.Random random = new System.Random();
        decryptedWord = "";

        string encryptedWord = "";

        for (int i = 0; i < 5; i++)
        {
            int digit = random.Next(0, 10);
            char letter;

            if (digit % 2 == 0)
            {
                letter = evenLetters[random.Next(evenLetters.Count)];
            }
            else
            {
                letter = oddLetters[random.Next(oddLetters.Count)];
            }

            char decodedLetter;
            if (digit < 5)
            {
                decodedLetter = digit % 2 == 0 ? evenMappingLow[letter] : oddMappingLow[letter];
            }
            else
            {
                decodedLetter = digit % 2 == 0 ? evenMappingHigh[letter] : oddMappingHigh[letter];
            }

            decryptedWord += decodedLetter;
            encryptedWord += digit.ToString() + letter;
        }

        Debug.Log($"Zaszyfrowane haslo: {encryptedWord}");
        Debug.Log($"Odszyfrowane haslo: {decryptedWord}");

        return encryptedWord;
    }
}


