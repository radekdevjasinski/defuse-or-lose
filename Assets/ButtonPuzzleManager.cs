using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonType
{
    Play = 0,
    Stop = 1,
    Record = 2,
    Heart = 3
}

public class InitializeButton : MonoBehaviour
{
    public static System.Random random = new System.Random();
    public static void Init(ButtonType buttonType, Button button)
    {
        switch(buttonType)
        {
            case (ButtonType)0:
                button.AddComponent<ButtonPuzzlePlay>();
                break;
            case (ButtonType)1:
                button.AddComponent<ButtonPuzzleStop>();
                break;
            case (ButtonType)2:
                button.AddComponent<ButtonPuzzleRecord>();
                break;
            case (ButtonType)3:
                button.AddComponent<ButtonPuzzleHeart>();
                break;
            default: break;
        }
    }
    public static T GetRandomButton<T>() where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(random.Next(values.Length));
    }
}
public class ButtonPuzzleManager : PuzzleBase
{
    public List<Sprite> buttonSprites;
    private Button button;

    [Header("Refs")]
    public BombController bombController;

    void Start()
    {
        button = GetComponent<Button>();
        bombController = GameObject.Find("Bomb").GetComponent<BombController>();
        Initialize();
    }
    public override void Initialize()
    {
        SetUpRandomButton();
    }

    void SetUpRandomButton()
    {
        ButtonType randomButton = InitializeButton.GetRandomButton<ButtonType>();
        Sprite selectedSprite = buttonSprites[(int)randomButton];
        button.image.sprite = selectedSprite;
        InitializeButton.Init(randomButton, button);

    }
    public void ButtonAnswer(bool answer)
    {
        if(answer)
        {
            OnComplete();
        }
        else
        {
            Debug.Log("Wrong answer");
        }
    }
}
