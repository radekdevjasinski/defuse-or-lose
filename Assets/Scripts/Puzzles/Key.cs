using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    public char keyChar;
    public KeyboardPuzzle keyboardPuzzle;
    private Button button;
    public bool isAnswerKey;

    public void SetKey(char keyChar, KeyboardPuzzle keyboardPuzzle)
    {
        this.keyChar = keyChar;
        this.keyboardPuzzle = keyboardPuzzle;
        button = GetComponent<Button>();
        button.onClick.AddListener(ClickKey);
        isAnswerKey = false;

    }

    public void ClickKey()
    {
        keyboardPuzzle.KeyClicked(keyChar);
    }

}
