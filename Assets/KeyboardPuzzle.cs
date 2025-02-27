using UnityEngine;
using System;

public class KeyboardPuzzle : PuzzleBase
{
    private string userName = "Unknown User";
    private int day;
    private int month;
    private int year;
    public override void Initialize()
    {
        try
        {
            userName = Environment.UserName;
        }
        catch
        {
            userName = "Unknown User";
        }
        day = DateTime.Now.Day;
        month = DateTime.Now.Month;
        year = DateTime.Now.Year;
    }

    void Start()
    {
        Initialize();
    }
}
