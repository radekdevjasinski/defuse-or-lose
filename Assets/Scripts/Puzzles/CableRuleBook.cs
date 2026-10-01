using System.Collections.Generic;
using System.Linq;

namespace DefuseOrLose
{
    public static class CableRuleBook
    {
        public const int LeftSlotCount = 4;
        public const int RightSlotCount = 5;

        private const string Vowels = "AEIOU";
        private const int LowBatteryLevel = 1;
        private const int HighBatteryLevel = 4;
        private const int VowelCountThreshold = 3;

        public static bool TryCalculateConnections(string serialCode, int batteryLevel, out Dictionary<int, int> connections)
        {
            CableWiring wiring = new CableWiring();

            ApplyLowBatteryRule(wiring, batteryLevel);
            ApplyHighBatteryRule(wiring, batteryLevel);
            ApplyLastDigitRule(wiring, serialCode);
            bool isSolvable = TryApplySerialSumRule(wiring, serialCode);
            ApplyVowelCountRule(wiring, serialCode);
            ApplyFirstConsonantRule(wiring, serialCode);
            ApplyDefaultRule(wiring);

            connections = wiring.Connections;
            return isSolvable;
        }

        private static void ApplyLowBatteryRule(CableWiring wiring, int batteryLevel)
        {
            if (batteryLevel == LowBatteryLevel)
            {
                wiring.Connect(0, 1);
            }
        }

        private static void ApplyHighBatteryRule(CableWiring wiring, int batteryLevel)
        {
            if (batteryLevel == HighBatteryLevel)
            {
                wiring.Connect(1, 4);
            }
        }

        private static void ApplyLastDigitRule(CableWiring wiring, string serialCode)
        {
            char lastCharacter = serialCode[serialCode.Length - 1];
            if (char.IsDigit(lastCharacter) && (lastCharacter - '0') % 2 == 0)
            {
                wiring.Connect(2, 2);
            }
        }

        private static bool TryApplySerialSumRule(CableWiring wiring, string serialCode)
        {
            int remainder = SumSerialCharacters(serialCode) % RightSlotCount;
            int rightSlot = remainder == 0 ? RightSlotCount - 1 : remainder - 1;
            bool isRightSlotFree = !wiring.IsRightSlotUsed(rightSlot);

            wiring.Connect(3, rightSlot);
            return isRightSlotFree;
        }

        private static int SumSerialCharacters(string serialCode)
        {
            int sum = 0;
            foreach (char character in serialCode)
            {
                if (char.IsDigit(character))
                    sum += character - '0';
                else if (char.IsLetter(character))
                    sum += char.ToUpperInvariant(character) - 'A' + 1;
            }
            return sum;
        }

        private static void ApplyVowelCountRule(CableWiring wiring, string serialCode)
        {
            int vowelCount = serialCode.Count(IsVowel);
            if (vowelCount >= VowelCountThreshold && !wiring.IsLeftSlotAssigned(0) && !wiring.IsRightSlotUsed(3))
            {
                wiring.Connect(0, 3);
            }
        }

        private static void ApplyFirstConsonantRule(CableWiring wiring, string serialCode)
        {
            char firstCharacter = serialCode[0];
            bool isConsonant = char.IsLetter(firstCharacter) && !IsVowel(firstCharacter);
            if (isConsonant && !wiring.IsLeftSlotAssigned(1) && !wiring.IsRightSlotUsed(2))
            {
                wiring.Connect(1, 2);
            }
        }

        private static void ApplyDefaultRule(CableWiring wiring)
        {
            for (int leftSlot = 0; leftSlot < LeftSlotCount; leftSlot++)
            {
                if (wiring.IsLeftSlotAssigned(leftSlot))
                    continue;

                int freeRightSlot = Enumerable.Range(0, RightSlotCount).First(rightSlot => !wiring.IsRightSlotUsed(rightSlot));
                wiring.Connect(leftSlot, freeRightSlot);
            }
        }

        private static bool IsVowel(char character)
        {
            return Vowels.IndexOf(char.ToUpperInvariant(character)) >= 0;
        }
    }
}
