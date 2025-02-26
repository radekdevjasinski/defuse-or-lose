using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;


public class ButtonPuzzle : MonoBehaviour
{
    protected Button button;
    protected ButtonPuzzleManager buttonPuzzleManager;
    public virtual void ExecutePuzzle(){}
    public virtual void Start()
    {
        button = GetComponent<Button>();
        buttonPuzzleManager = GetComponent<ButtonPuzzleManager>();
        button.onClick.AddListener(ExecutePuzzle);
    }
}
public class ButtonPuzzlePlay : ButtonPuzzle, IPointerDownHandler, IPointerUpHandler
{
    private float pressStartTime;
    private bool isPressed;
    private float holdDuration;

    private int secondsToHold;

    public override void Start()
    {
        base.Start();
        secondsToHold = buttonPuzzleManager.bombController.batteryBars;
    }

    void Update()
    {
        if (isPressed)
        {
            holdDuration = Time.time - pressStartTime;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressStartTime = Time.time;
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
    public static bool IsInRange(float value, float min, float max)
    {
        return value >= min && value <= max;
    }
    public override void ExecutePuzzle()
    {
        if ( IsInRange(holdDuration, secondsToHold-1,secondsToHold+1))
        {
            buttonPuzzleManager.ButtonAnswer(true);
        }
        else
        {
            buttonPuzzleManager.ButtonAnswer(false);
        }
    }
}
public class ButtonPuzzleStop : ButtonPuzzle
{
    private int clickAmount, clicked = 0;
    private float secondsToHold = 2f;
    public override void Start()
    {
        base.Start();
        int sum = GetSumOfNumbersInString
        (buttonPuzzleManager.bombController.serialCode);

        if(sum == 0)
        {
            clickAmount = 1;
        }
        else
        {
            clickAmount = sum;
        }
        
    }
    int GetSumOfNumbersInString(string input)
    {
        int sum = 0;

        foreach (char c in input)
        {
            if (Char.IsDigit(c))
            {
                sum += int.Parse(c.ToString());
            }
        }

        return sum;
    }
    public override void ExecutePuzzle()
    {
        clicked++;
        StopAllCoroutines();
        StartCoroutine(waitTimer());
        
    }
    IEnumerator waitTimer()
    {
        yield return new WaitForSeconds(secondsToHold);
        if (clicked == clickAmount)
        {
            buttonPuzzleManager.ButtonAnswer(true);
        }
        else
        {
            buttonPuzzleManager.ButtonAnswer(false);
        }
    }
}