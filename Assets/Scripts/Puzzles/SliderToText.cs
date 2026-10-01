using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefuseOrLose
{
    public class SliderToText : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        private TMP_Text text;

        void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        public void UpdateValue()
        {
            text.text = slider.value.ToString();
        }
    }
}
