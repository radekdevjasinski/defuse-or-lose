using UnityEngine;
using System.Collections.Generic;

public class CablePuzzleManager : PuzzleBase
{
    public static CablePuzzleManager Instance { get; private set; }

    [Header("Bomb Data")]
    public BombController bombController;

    private int batteryLevel;
    private string serialCode;

    private Dictionary<int, int> expectedConnections = new Dictionary<int, int>();
    private Dictionary<int, int> actualConnections = new Dictionary<int, int>();


    void Awake()
    {
        Instance = this;
    }
    
    public override void Initialize()
    {
        if (bombController == null)
        {
            GameObject bombObj = GameObject.FindGameObjectWithTag("Bomb");
            if (bombObj != null)
            {
                bombController = bombObj.GetComponent<BombController>();
                if (bombController == null)
                    Debug.LogError("CablePuzzleManager: Obiekt z tagiem 'Bomb' nie posiada BombController.");
            }
            else
            {
                Debug.LogError("CablePuzzleManager: Nie znaleziono obiektu z tagiem 'Bomb'.");
                return;
            }
        }

        batteryLevel = bombController.batteryBars;
        serialCode = bombController.serialCode;
        CalculateExpectedConnections();
        PrintExpectedConnections();
    }
    void Start()
    {
        Initialize();
    }

    public bool IsLeftSlotLocked(int leftIndex)
    {
        return actualConnections.ContainsKey(leftIndex);
    }

void CalculateExpectedConnections()
{
    expectedConnections.Clear();
    HashSet<int> usedRightSlots = new HashSet<int>();

    // Rule 1: Niska bateria – jeśli batteryLevel == 1,
    // połącz lewy slot 1 (indeks 0) -> prawy slot 2 (indeks 1)
    if (batteryLevel == 1)
    {
        expectedConnections[0] = 1;
        usedRightSlots.Add(1);
        Debug.Log("Rule 1 (Low battery): Left slot 1 -> Right slot 2");
    }

    // Rule 2: Wysoka bateria – jeśli batteryLevel == 4,
    // połącz lewy slot 2 (indeks 1) -> prawy slot 5 (indeks 4)
    if (batteryLevel == 4)
    {
        expectedConnections[1] = 4;
        usedRightSlots.Add(4);
        Debug.Log("Rule 2 (High battery): Left slot 2 -> Right slot 5");
    }

    // Rule 3: Ostatni znak seryjny – jeśli ostatni znak jest cyfrą i parzystą,
    // połącz lewy slot 3 (indeks 2) -> prawy slot 3 (indeks 2)
    char lastChar = serialCode[serialCode.Length - 1];
    if (char.IsDigit(lastChar))
    {
        int digit = lastChar - '0';
        if (digit % 2 == 0)
        {
            expectedConnections[2] = 2;
            usedRightSlots.Add(2);
            Debug.Log("Rule 3 (Last digit even): Left slot 3 -> Right slot 3");
        }
    }

    // Rule 4: Suma kodu seryjnego – oblicz sumę znaków (cyfry jako liczby, litery: A=1, B=2, itd.),
    // weź sumę modulo 5 (jeśli wynik = 0, traktuj jako 5) i przypisz lewy slot 4 (indeks 3) do odpowiedniego slotu.
    int sum = 0;
    foreach (char c in serialCode)
    {
        if (char.IsDigit(c))
            sum += c - '0';
        else if (char.IsLetter(c))
            sum += (char.ToUpper(c) - 'A' + 1);
    }
    int mod = sum % 5;
    if (mod == 0) mod = 5;
    int rightSlotForRule4 = mod - 1;
    expectedConnections[3] = rightSlotForRule4;
    usedRightSlots.Add(rightSlotForRule4);
    Debug.Log($"Rule 4 (Sum rule): Left slot 4 -> Right slot {rightSlotForRule4 + 1} (sum = {sum})");

    // Rule 5: Liczba samogłosek – jeśli kod seryjny zawiera co najmniej 3 samogłoski,
    // nadpisz (jeśli jeszcze nie przypisano) lewy slot 1 (indeks 0) -> prawy slot 4 (indeks 3)
    string vowels = "AEIOU";
    int vowelCount = 0;
    foreach (char c in serialCode.ToUpper())
    {
        if (vowels.Contains(c))
            vowelCount++;
    }
    if (vowelCount >= 3 && !expectedConnections.ContainsKey(0))
    {
        if (!usedRightSlots.Contains(3))
        {
            expectedConnections[0] = 3;
            usedRightSlots.Add(3);
            Debug.Log("Rule 5 (Vowel count): Left slot 1 -> Right slot 4");
        }
    }

    // Rule 6: Pierwszy znak – jeśli pierwszy znak kodu seryjnego jest spółgłoską,
    // i lewy slot 2 (indeks 1) nie został przypisany, połącz go z prawym slotem 3 (indeks 2)
    char firstChar = serialCode[0];
    if (char.IsLetter(firstChar) && !"AEIOU".Contains(char.ToUpper(firstChar).ToString()))
    {
        if (!expectedConnections.ContainsKey(1))
        {
            if (!usedRightSlots.Contains(2))
            {
                expectedConnections[1] = 2;
                usedRightSlots.Add(2);
                Debug.Log("Rule 6 (First char consonant): Left slot 2 -> Right slot 3");
            }
        }
    }

    // Rule 7: Domyślna – dla wszystkich lewych slotów (0 do 3), które nie zostały przypisane,
    // połącz z najmniejszym wolnym prawym slotem
    for (int i = 0; i < 4; i++)
    {
        if (!expectedConnections.ContainsKey(i))
        {
            for (int r = 0; r < 5; r++)
            {
                if (!usedRightSlots.Contains(r))
                {
                    expectedConnections[i] = r;
                    usedRightSlots.Add(r);
                    Debug.Log($"Rule 7 (Default): Left slot {i + 1} -> Right slot {r + 1}");
                    break;
                }
            }
        }
    }
}

    void PrintExpectedConnections()
    {
        Debug.Log("Expected cable connections:");
        foreach (var kvp in expectedConnections)
        {
            Debug.Log($"Left slot {kvp.Key + 1} -> Right slot {kvp.Value + 1}");
        }
    }

    public void RegisterConnection(int leftIndex, int rightIndex)
    {
        if (IsLeftSlotLocked(leftIndex))
        {
            Debug.LogWarning($"Left slot {leftIndex + 1} jest już zablokowany. Nie można ponownie przeciągać.");
            return;
        }

        if (expectedConnections.ContainsKey(leftIndex))
        {
            if (expectedConnections[leftIndex] != rightIndex)
            {
                Debug.LogError($"Incorrect connection for left slot {leftIndex + 1}: expected right slot {expectedConnections[leftIndex] + 1} but got {rightIndex + 1}");
                ResetPuzzle();
                return;
            }
        }
        else
        {
            if (leftIndex != rightIndex)
            {
                Debug.LogError($"Incorrect default connection for left slot {leftIndex + 1}: expected right slot {leftIndex + 1} but got {rightIndex + 1}");
                ResetPuzzle();
                return;
            }
            else
            {
                expectedConnections[leftIndex] = leftIndex;
            }
        }

        actualConnections[leftIndex] = rightIndex;
        Debug.Log($"Registered connection: Left slot {leftIndex + 1} -> Right slot {rightIndex + 1}");

        if (actualConnections.Count == 4)
        {
            ValidateConnections();
        }
    }

    void ValidateConnections()
    {
        bool correct = true;
        foreach (var kvp in expectedConnections)
        {
            if (!actualConnections.ContainsKey(kvp.Key) || actualConnections[kvp.Key] != kvp.Value)
            {
                correct = false;
                break;
            }
        }
        if (correct)
        {
            OnComplete();
        }
        else
        {
            OnFail();
        }
    }

    void ResetPuzzle()
    {
        OnFail();
        /*
        Cable[] cables = FindObjectsByType<Cable>(FindObjectsSortMode.None);
        foreach (Cable cable in cables)
        {
            Destroy(cable.gameObject);
        }

        actualConnections.Clear();
        expectedConnections.Clear();

        RightCableSlot[] rightSlots = FindObjectsByType<RightCableSlot>(FindObjectsSortMode.None);
        foreach (RightCableSlot slot in rightSlots)
        {
            slot.isUsed = false;
        }

        Debug.Log("Puzzle reset. Wszystkie sloty zostały wyczyszczone.");
        CalculateExpectedConnections();
        PrintExpectedConnections();
        */
    }

}
