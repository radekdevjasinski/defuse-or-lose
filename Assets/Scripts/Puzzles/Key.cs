using UnityEngine;
using UnityEngine.UI;

namespace DefuseOrLose
{
    public class Key : MonoBehaviour
    {
        private char keyChar;
        private KeyboardPuzzle keyboardPuzzle;

        public void SetKey(char keyChar, KeyboardPuzzle keyboardPuzzle)
        {
            this.keyboardPuzzle = keyboardPuzzle;
            SetCharacter(keyChar);
            GetComponent<Button>().onClick.AddListener(ClickKey);
        }

        public void SetCharacter(char keyChar)
        {
            this.keyChar = keyChar;
            name = keyChar + " key";
            GetComponentInChildren<TMPro.TMP_Text>().text = keyChar.ToString();
        }

        void ClickKey()
        {
            keyboardPuzzle.KeyClicked(keyChar);
        }
    }
}
