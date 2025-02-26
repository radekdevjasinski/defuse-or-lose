using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class BombController : MonoBehaviour
{
    [Header("Serial Code")]
    public int serialCodeLenght;
    [SerializeField] private TMP_Text serialcodeText;
    public string serialCode;


    [SerializeField] private Image batteryImage;
    public int batteryBars;
    public List<Sprite> batterySprites;


    void Start()
    {
        serialCode = GenerateSerialKey();
        serialcodeText.text = serialCode;

        batteryBars = Random.Range(0, batterySprites.Count) + 1;
        batteryImage.sprite = batterySprites[batteryBars-1];
    }
    string GenerateSerialKey()
    {
        const string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        StringBuilder serialKey = new StringBuilder(serialCodeLenght);

        for (int i = 0; i < serialCodeLenght; i++)
        {
            int index = Random.Range(0, characters.Length);
            serialKey.Append(characters[index]);
        }

        return serialKey.ToString();
    }
}

