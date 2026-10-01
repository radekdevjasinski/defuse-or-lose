using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefuseOrLose
{
    public class BombController : MonoBehaviour
    {
        private const string SerialCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string TimerFormat = "{0:00}:{1:00}";
        private const int SecondsPerMinute = 60;
        private const int MaxGlitchedStrikes = 5;
        private const int MinGlitchedTimerValue = 10;
        private const int MaxGlitchedTimerValue = 60;

        public static BombController Instance { get; private set; }

        [Header("Serial Code")]
        [FormerlySerializedAs("serialCodeLenght")]
        [SerializeField] private int serialCodeLength;
        [SerializeField] private TMP_Text serialcodeText;

        [Header("Battery")]
        [SerializeField] private Image batteryImage;
        [SerializeField] private List<Sprite> batterySprites;

        [Header("Timer")]
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private float timeRemaining;
        [SerializeField] private bool timerRunning = true;
        [SerializeField] private GameObject timerSound;

        [Header("Strikes")]
        [SerializeField] private GameObject strikeUI;
        [SerializeField] private Transform strikeParent;

        private readonly List<GameObject> strikeIcons = new List<GameObject>();
        private int lastDisplayedSecond;
        private bool isDisplayGlitched = false;

        public string SerialCode { get; private set; }
        public int BatteryBars { get; private set; }
        public int Strikes { get; private set; }

        public string FormattedTime
        {
            get
            {
                int wholeSeconds = Mathf.FloorToInt(timeRemaining);
                return string.Format(TimerFormat, wholeSeconds / SecondsPerMinute, wholeSeconds % SecondsPerMinute);
            }
        }

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            BatteryBars = Random.Range(0, batterySprites.Count) + 1;
            SerialCode = GenerateDefusableSerialCode();
        }

        void Start()
        {
            foreach (Transform placeholderStrike in strikeParent)
            {
                Destroy(placeholderStrike.gameObject);
            }

            serialcodeText.text = SerialCode;
            lastDisplayedSecond = Mathf.FloorToInt(timeRemaining);
            RestoreDisplay();
        }

        void Update()
        {
            if (!timerRunning)
                return;

            timeRemaining = Mathf.Max(0f, timeRemaining - Time.deltaTime);
            RefreshTimerOnSecondChange();

            if (timeRemaining <= 0f)
            {
                timerRunning = false;
                GameController.Instance.LoseGame();
            }
        }

        public void SetTimeLimit(float seconds)
        {
            timeRemaining = seconds;
        }

        public void StopTimer()
        {
            timerRunning = false;
        }

        public void AddStrike()
        {
            Strikes++;
            if (!isDisplayGlitched)
            {
                ShowStrikeIcons(Strikes);
            }
        }

        public void ShowGlitchedDisplay()
        {
            isDisplayGlitched = true;
            batteryImage.sprite = batterySprites[Random.Range(0, batterySprites.Count)];
            ShowStrikeIcons(Random.Range(0, MaxGlitchedStrikes));
            timerText.text = Random.Range(MinGlitchedTimerValue, MaxGlitchedTimerValue) + ":"
                + Random.Range(MinGlitchedTimerValue, MaxGlitchedTimerValue);
        }

        public void RestoreDisplay()
        {
            isDisplayGlitched = false;
            batteryImage.sprite = batterySprites[BatteryBars - 1];
            ShowStrikeIcons(Strikes);
            timerText.text = FormattedTime;
        }

        void RefreshTimerOnSecondChange()
        {
            int wholeSeconds = Mathf.FloorToInt(timeRemaining);
            if (wholeSeconds == lastDisplayedSecond)
                return;

            lastDisplayedSecond = wholeSeconds;
            if (!isDisplayGlitched)
            {
                timerText.text = FormattedTime;
            }
            AudioManager.Instance.PlaySound(timerSound);
        }

        void ShowStrikeIcons(int visibleCount)
        {
            while (strikeIcons.Count < visibleCount)
            {
                strikeIcons.Add(Instantiate(strikeUI, strikeParent));
            }
            for (int i = 0; i < strikeIcons.Count; i++)
            {
                strikeIcons[i].SetActive(i < visibleCount);
            }
        }

        string GenerateDefusableSerialCode()
        {
            string candidate;
            do
            {
                candidate = GenerateSerialCode();
            } while (!CableRuleBook.TryCalculateConnections(candidate, BatteryBars, out _));

            return candidate;
        }

        string GenerateSerialCode()
        {
            StringBuilder serialKey = new StringBuilder(serialCodeLength);

            for (int i = 0; i < serialCodeLength; i++)
            {
                serialKey.Append(SerialCharacters[Random.Range(0, SerialCharacters.Length)]);
            }

            return serialKey.ToString();
        }
    }
}
