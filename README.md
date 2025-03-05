# Jk

**Defuse or Lose** - Radosław Jasiński, Jakub Kotwica

## 1. Opis gry
**Defuse or Lose** to gra komputerowa, w której gracz wciela się w rolę sapera, a jego zadaniem jest rozbrojenie bomby. Po podłączeniu urządzenia rozbrajającego do bomby gracz musi przejść przez serię modułów, z których każdy zawiera unikalną zagadkę. Nie poradziłby sobie jednak sam, dlatego potrzebuje drugiego gracza, który otrzymuje pełny manual z podpowiedziami i instrukcjami. Saper myli się tylko raz, ale w naszej grze może popełnić błąd aż trzy razy, zanim bomba wybuchnie lub skończy mu się czas. Całość utrzymana jest w klimacie retro pixel art.

### 1.1 Ogólny opis rozgrywki
**Defuse or Lose** to logiczna gra 2D stworzona z myślą o trybie kooperacji. Gra przeznaczona jest dla dwóch graczy – jeden kontroluje interfejs rozbrajania bomby, a drugi dysponuje pełnym manualem zawierającym kluczowe wskazówki i instrukcje. Choć gra przewiduje możliwość rozgrywki solo, doświadczenie jednoosobowe jest znacznie gorsze, gdyż wiele zagadek opiera się na współpracy i komunikacji między partnerami.

Rozgrywka składa się z 8 poziomów o rosnącym poziomie trudności. Każdy poziom zawiera od 2 do 8 unikalnych modułów z zagadkami. Każdy moduł wymaga precyzyjnego rozwiązania zagadek, a najmniejszy błąd powoduje reset zagadki lub nawet wybuch bomby.

Sama gra zmienia się dynamicznie dzięki wpływowi trzech różnych środowisk, które oddziałują na funkcjonowanie urządzenia. Warunki, takie jak jazda pociągiem czy skażenie, sprawiają, że każda rozgrywka jest nieprzewidywalna.

### 1.2 Zaimplementowane mechaniki
- **Moduły** – wyświetlają zagadki, których rozwiązanie jest wymagane do rozbrojenia bomby. Każdy poziom zawiera kilka modułów, które z czasem stają się trudniejsze. Poziomy decydują o puli modułów, ale same są losowane, co oznacza, że ten sam poziom może zawierać inne moduły. Zagadki w modułach zmieniają się przy każdym ponownym podejściu, np. generują inny kod i kolejność elementów, co wpływa na inną odpowiedź. Na ich zmienność wpływają również elementy bomby.
- **Elementy bomby** – zmienne składniki, które mają inne wartości przy każdym uruchomieniu gry. Obejmują one baterię, kod seryjny bomby, liczbę dopuszczalnych pomyłek i zegar – wszystkie te elementy są wykorzystywane w zagadkach.
- **Zagrożenia** – zaimplementowano trzy rodzaje środowisk, które wpływają na proces rozbrajania bomby. Zagrożenia są opisane w manualu:
  - pociąg trzęsie kamerą i czasami wygasza ekran, wjeżdżając do tunelu,
  - burza zaburza wyświetlanie elementów bomby,
  - skażenie odbiera graczowi możliwość interakcji z bombą.
- **Poziomy** – zaimplementowano 8 poziomów o rosnącym poziomie trudności. Wraz z postępem rozgrywki rośnie skomplikowanie modułów i ich liczba, a czas na zegarze oraz liczba dopuszczalnych pomyłek maleją.

Poza wymienionymi elementami zaimplementowaliśmy funkcje wymagane w grze tego typu:
- menu główne, menu wyboru poziomów,
- ekran wygranej, ekran przegranej,
- generowanie i wyświetlanie modułów.

### 1.3 Tło fabularne
Gra osadzona jest w kryzysowej sytuacji. Jeden z graczy, wracając z ciężkiej podróży, znalazł się w pułapce, z której jedynym wyjściem jest szybkie i precyzyjne rozbrojenie bomby podłożonej w pociągu. Na szczęście nie jest sam – jego przyjaciel, znajdujący się w centrali, po usłyszeniu o sytuacji postanowił jak najszybciej sięgnąć po manual *How Not to Explode (Maybe)* i poprowadzić kompana, udzielając mu podpowiedzi oraz konkretnych instrukcji.

## 2. Sterowanie
Podczas całej gry gracz korzysta wyłącznie z myszki. W trakcie rozbrajania bomby może przybliżyć ekran spacją, a klawiszem *Escape* powrócić do menu.

## 3. Jak gra nawiązuje do tematu?
Temat *"Zegar tyka"* wiąże się bezpośrednio z głównym celem gry – rozbrojeniem bomby przed upływem czasu. Dodatkowo podczas rozbrajania bomby gracz słyszy faktyczne tykanie zegara.

## 4. Proces twórczy
- **Grafiki** zostały wykonane w stylu pixel art 16x16 w programach Aseprite i Adobe Photoshop. Wyjątkiem jest tło za bombą – rozpikselowane zdjęcie stockowe.
- **Audio** to dźwięki stockowe, przepuszczone przez filtr *Time Machine* w Audacity, nadający im oldschoolowy charakter.
- **Inspiracja** – gra *Keep Talking and Nobody Explodes*.

## 5. Linki do assetów
Niektóre grafiki i dźwięki pochodzą z pixabay.com (licencja CC0). Dźwięki 8-bitowe wygenerowano na sfxr.me. Użyty font to *Joystix Monospace*, a grafiki kursora pochodzą z *kenney_cursor-pixel-pack*.

## 6. Instrukcja uruchomienia
Grę uruchamia się przez *Defuse or Lose.exe* w katalogu *Game*. *Manual Defuse or Lose.pdf* jest wymagany do rozgrywki.

