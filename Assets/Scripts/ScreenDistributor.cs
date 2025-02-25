using UnityEngine;

public class ScreenDistributor : MonoBehaviour
{
    [Header("Refs")]
    public SpriteRenderer tabletScreen;
    
    [Header("View")]
    public int columns; // Liczba kolumn
    private int rows;   // Liczba wierszy (obliczana na podstawie ilości obiektów)
    
    private float spacingX;
    private float spacingY;

    public void DistributeChildren()
    {
        int count = transform.childCount;
        if (count == 0 || tabletScreen == null) return;

        Vector2 screenSize = tabletScreen.bounds.size;

        // Obliczenie liczby wierszy na podstawie ilości obiektów
        rows = Mathf.CeilToInt((float)count / columns);

        // Pobranie rozmiaru pierwszego dziecka (zakładamy, że wszystkie są podobne)
        Vector2 size = transform.GetChild(0).GetComponentInChildren<RectTransform>().sizeDelta;

        // Obliczenie odstępów (zabezpieczenie przed dzieleniem przez 0)
        spacingX = (columns > 1) ? (screenSize.x - (columns * size.x)) / (columns - 1) : 0;
        spacingY = (rows > 1) ? (screenSize.y - (rows * size.y)) / (rows - 1) : 0;

        // Pobranie pozycji lewego górnego rogu ekranu
        Vector3 topLeft = tabletScreen.bounds.min + new Vector3(size.x / 2, screenSize.y - size.y / 2, 0);

        for (int i = 0; i < count; i++)
        {
            int row = i / columns;  // Numer wiersza
            int col = i % columns;  // Numer kolumny

            // Obliczanie pozycji każdego elementu
            Vector3 newPosition = topLeft + new Vector3(col * (size.x + spacingX), -row * (size.y + spacingY), 0);
            transform.GetChild(i).position = newPosition; // Ustawienie pozycji globalnej
        }
    }
}
