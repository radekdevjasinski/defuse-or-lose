using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class BombController : MonoBehaviour
{
    public static BombController instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [Header("Serial Code")]
    public int serialCodeLenght;
    [SerializeField] private TMP_Text serialcodeText;
    public string serialCode;

    [Header("Battery")]
    public Image batteryImage;
    public int batteryBars;
    public List<Sprite> batterySprites;

    [Header("Timer")]
    public TMP_Text timerText; 
    public float timeRemaining;
    public bool timerRunning = true;
    public bool timerVisible = true;
    private float blinkTime = 0f;  
    private bool isColonVisible = true; 

    [Header("Strikes")]
    public GameObject strikeUI;
    public Transform strikeParent;
    public int strikes;

    void Start()
    {
        serialCode = GenerateSerialKey();
        serialcodeText.text = serialCode;

        batteryBars = Random.Range(0, batterySprites.Count) + 1;
        batteryImage.sprite = batterySprites[batteryBars-1];

        strikes = 0;
        WriteStrikes();
    }
    void Update()
    {
        if (timerRunning)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;
                timerRunning = false;
            }

            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);

            blinkTime -= Time.deltaTime;
            if (blinkTime <= 0f)
            {
                isColonVisible = !isColonVisible; 
                blinkTime = 0.5f; 
            }

            string colon = isColonVisible ? ":" : " "; 
            if (timerVisible)
            {
                timerText.text = string.Format("{0:00}{1}{2:00}", minutes, colon, seconds);
            }
        }
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
    public void AddStrike()
    {
        strikes++;
        WriteStrikes();
    }
    public void WriteStrikes()
    {
        foreach (Transform child in strikeParent)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < strikes; i++)
        {
            Instantiate(strikeUI, strikeParent);
        }
    }

}

