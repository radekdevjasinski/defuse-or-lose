using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderToText : MonoBehaviour
{
    private TMP_Text text;
    public Slider slider;
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }
    public void UpdateValue()
    {
        text.text = slider.value.ToString();
    }
}
