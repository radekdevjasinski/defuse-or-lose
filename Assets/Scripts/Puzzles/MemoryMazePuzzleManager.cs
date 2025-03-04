using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum ButtonPosition { TL = 0, TR = 1, BL = 2, BR = 3 }

public class MemoryMazePuzzleManager : PuzzleBase
{
    [Header("UI References")]
    public Button[] buttons;

    [Header("Blinking Settings")]
    public Color normalColor = new Color(0f, 0.91f, 0f);
    public Color blinkColor = Color.black;
    public float blinkDuration = 0.5f;

    [Header("Puzzle Settings")]
    private int currentPhase = 0;
    private ButtonPosition[] blinkedPositions = new ButtonPosition[5];
    private ButtonPosition[] pressedPositions = new ButtonPosition[5];

    private bool phaseActive = false;

    public override void Initialize()
    {
        Debug.Log("[MemoryMazePuzzleManager] Initialize() called.");
        currentPhase = 0;
        StartCoroutine(StartPhaseCoroutine());
    }

    void Start()
    {
        Debug.Log("[MemoryMazePuzzleManager] Start() called.");
        Initialize();
    }

    IEnumerator StartPhaseCoroutine()
    {
        phaseActive = false;
        yield return new WaitForSeconds(1f);
        
        if (buttons == null || buttons.Length < 4)
        {
            Debug.LogError("[MemoryMazePuzzleManager] Brak przypisanych przycisków w tablicy 'buttons'.");
            yield break;
        }

        ButtonPosition chosen = (ButtonPosition)Random.Range(0, 4);
        blinkedPositions[currentPhase] = chosen;
        Debug.Log("[MemoryMazePuzzleManager] Etap " + (currentPhase + 1) + " – Miganie: " + chosen.ToString());
        yield return StartCoroutine(BlinkButton(chosen));
        phaseActive = true;
    }

IEnumerator BlinkButton(ButtonPosition pos)
{
    Button btn = buttons[(int)pos];
    if (btn == null)
    {
        Debug.LogError("[MemoryMazePuzzleManager] Brak przycisku dla pozycji " + pos.ToString());
        yield break;
    }
    btn.interactable = false;
    Debug.Log("[MemoryMazePuzzleManager] Miganie (blokada) przycisku " + pos.ToString() + " rozpoczęte.");
    yield return new WaitForSeconds(blinkDuration);
    btn.interactable = true;
    Debug.Log("[MemoryMazePuzzleManager] Miganie zakończone, przycisk " + pos.ToString() + " ponownie interaktywny.");
}


    public void OnButtonPressed(int buttonIndex)
    {
        if (!phaseActive)
        {
            Debug.LogWarning("[MemoryMazePuzzleManager] Ignoruję naciśnięcie, etap jeszcze nie aktywny.");
            return;
        }

        ButtonPosition pressed = (ButtonPosition)buttonIndex;
        Debug.Log("[MemoryMazePuzzleManager] Naciśnięto przycisk: " + pressed.ToString());

        ButtonPosition expected = GetExpectedPosition(currentPhase);
        Debug.Log("[MemoryMazePuzzleManager] Etap " + (currentPhase + 1) + " – Oczekiwany przycisk: " + expected.ToString());

        if (pressed == expected)
        {
            pressedPositions[currentPhase] = pressed;
            Debug.Log("[MemoryMazePuzzleManager] Etap " + (currentPhase + 1) + " poprawny: naciśnięto " + pressed.ToString());
            phaseActive = false;
            currentPhase++;
            if (currentPhase >= 5)
            {
                Debug.Log("[MemoryMazePuzzleManager] Puzzle rozwiązane!");
                OnComplete();
            }
            else
            {
                StartCoroutine(StartPhaseCoroutine());
            }
        }
        else
        {
            Debug.LogError("[MemoryMazePuzzleManager] Błąd w etapie " + (currentPhase + 1) + "! Oczekiwano: " + expected.ToString() + " – Resetowanie zagadki.");
            OnFail();
            ResetPuzzle();
        }
    }

    ButtonPosition GetExpectedPosition(int phase)
    {
        int globalStrikes = BombController.instance != null ? BombController.instance.strikes : 0;

        switch (phase)
        {
            case 0:
                switch (blinkedPositions[0])
                {
                    case ButtonPosition.TL: return ButtonPosition.TR;
                    case ButtonPosition.TR: return ButtonPosition.BL;
                    case ButtonPosition.BL: return ButtonPosition.BR;
                    case ButtonPosition.BR: return ButtonPosition.TL;
                }
                break;
            case 1:
                switch (blinkedPositions[1])
                {
                    case ButtonPosition.TL: return blinkedPositions[0];
                    case ButtonPosition.TR: return pressedPositions[0];
                    case ButtonPosition.BL: return GetOpposite(blinkedPositions[0]);
                    case ButtonPosition.BR: return blinkedPositions[0];
                }
                break;
            case 2:
                switch (blinkedPositions[2])
                {
                    case ButtonPosition.TL: return (globalStrikes >= 1) ? blinkedPositions[1] : blinkedPositions[0];
                    case ButtonPosition.TR: return pressedPositions[0];
                    case ButtonPosition.BL: return blinkedPositions[1];
                    case ButtonPosition.BR: return GetOpposite(pressedPositions[0]);
                }
                break;
            case 3:
                switch (blinkedPositions[3])
                {
                    case ButtonPosition.TL: return pressedPositions[0];
                    case ButtonPosition.TR: return pressedPositions[1];
                    case ButtonPosition.BL: return blinkedPositions[2];
                    case ButtonPosition.BR: return GetOpposite(pressedPositions[1]);
                }
                break;
            case 4:
                switch (blinkedPositions[4])
                {
                    case ButtonPosition.TL: return blinkedPositions[0];
                    case ButtonPosition.TR: return blinkedPositions[1];
                    case ButtonPosition.BL: return blinkedPositions[3];
                    case ButtonPosition.BR: return blinkedPositions[2];
                }
                break;
        }
        return ButtonPosition.TL;
    }

    ButtonPosition GetOpposite(ButtonPosition pos)
    {
        switch (pos)
        {
            case ButtonPosition.TL: return ButtonPosition.BR;
            case ButtonPosition.TR: return ButtonPosition.BL;
            case ButtonPosition.BL: return ButtonPosition.TR;
            case ButtonPosition.BR: return ButtonPosition.TL;
        }
        return ButtonPosition.TL;
    }

    void ResetPuzzle()
    {
        currentPhase = 0;
        phaseActive = false;
        Debug.Log("[MemoryMazePuzzleManager] Puzzle reset – wracamy do etapu 1.");
        StartCoroutine(StartPhaseCoroutine());
    }
}
