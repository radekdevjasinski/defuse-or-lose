# Defuse or Lose

Radosław Jasiński, Jakub Kotwica

[English](#english) | [Polski](#polski)

---

## English

### 1. About the game
**Defuse or Lose** is a computer game in which the player takes the role of a bomb disposal expert whose job is to defuse a bomb. Once the defusing device is connected to the bomb, the player has to work through a series of modules, each holding a unique puzzle. They would not manage alone, so they need a second player who gets the full manual with hints and instructions. A sapper only gets one mistake, but in our game you can make as many as three before the bomb goes off or the time runs out. The whole thing is done in a retro pixel art style.

#### 1.1 Gameplay overview
**Defuse or Lose** is a 2D puzzle game built for co-op. It is meant for two players: one operates the defusing interface, the other holds the full manual with the key hints and instructions. The game can be played solo, but the single-player experience is much worse, because many puzzles rely on cooperation and communication between the partners.

The game has 8 levels of increasing difficulty. Each level contains from 2 to 8 unique puzzle modules. Every module has to be solved precisely, and the smallest mistake resets the puzzle or even blows up the bomb.

The game changes dynamically under the influence of three different environments that affect how the device works. Conditions such as a train ride or radioactive fallout make every playthrough unpredictable.

#### 1.2 Implemented mechanics
- **Modules** - they display the puzzles that have to be solved to defuse the bomb. Each level contains several modules, which get harder over time. Levels decide the pool of modules, but the modules themselves are drawn at random, so the same level can contain different modules. Puzzles in the modules change on every attempt, for example by generating a different code and order of elements, which leads to a different answer. They are also affected by the bomb elements.
- **Bomb elements** - variable components that have different values on every run. They include the battery, the bomb's serial code, the number of allowed mistakes and the timer, and all of them are used in puzzles.
- **Hazards** - three kinds of environments that affect the defusal. The hazards are described in the manual:
  - the train shakes the camera and sometimes darkens the screen when entering a tunnel,
  - the storm scrambles the display of the bomb elements,
  - the fallout takes away the player's ability to interact with the bomb.
- **Levels** - 8 levels of increasing difficulty. As the game progresses, the modules get more complex and more numerous, while the time on the clock and the number of allowed mistakes go down.

Apart from the elements above, we implemented the features a game of this kind needs:
- main menu, level select menu,
- win screen, lose screen,
- generating and displaying modules.

#### 1.3 Story
The game is set in a crisis. One of the players, on their way back from a rough trip, ends up in a trap, and the only way out is to quickly and precisely defuse a bomb planted on the train. Luckily they are not alone: their friend at headquarters, having heard about the situation, grabs the manual *How Not to Explode (Maybe)* as fast as possible and guides their companion with hints and concrete instructions.

### 2. Controls
The player uses only the mouse throughout the game. While defusing the bomb, the screen can be zoomed in with the space bar, and *Escape* returns to the menu.

### 3. How does the game fit the theme?
The theme *"The clock is ticking"* ties directly to the main goal of the game: defusing the bomb before the time runs out. On top of that, the player hears an actual ticking clock while defusing.

### 4. Creative process
- **Graphics** were made in 16x16 pixel art style in Aseprite and Adobe Photoshop. The exception is the background behind the bomb, which is a pixelated stock photo.
- **Audio** consists of stock sounds run through the *Time Machine* filter in Audacity, which gives them an old-school character.
- **Inspiration** - the game *Keep Talking and Nobody Explodes*.

### 5. Asset credits
Some graphics and sounds come from pixabay.com (CC0 licence). The 8-bit sounds were generated on sfxr.me. The font is *Joystix Monospace*, and the cursor graphics come from *kenney_cursor-pixel-pack*.

### 6. How to run
Download the built game from the *Releases* tab and start it with *Defuse or Lose.exe*. The manual is required to play and sits in the root of this repository:
- [Manual Defuse or Lose (EN).pdf](<Manual Defuse or Lose (EN).pdf>) - English
- [Manual Defuse or Lose (PL).pdf](<Manual Defuse or Lose (PL).pdf>) - Polish

---

## Polski

### 1. Opis gry
**Defuse or Lose** to gra komputerowa, w której gracz wciela się w rolę sapera, a jego zadaniem jest rozbrojenie bomby. Po podłączeniu urządzenia rozbrajającego do bomby gracz musi przejść przez serię modułów, z których każdy zawiera unikalną zagadkę. Nie poradziłby sobie jednak sam, dlatego potrzebuje drugiego gracza, który otrzymuje pełny manual z podpowiedziami i instrukcjami. Saper myli się tylko raz, ale w naszej grze może popełnić błąd aż trzy razy, zanim bomba wybuchnie lub skończy mu się czas. Całość utrzymana jest w klimacie retro pixel art.

#### 1.1 Ogólny opis rozgrywki
**Defuse or Lose** to logiczna gra 2D stworzona z myślą o trybie kooperacji. Gra przeznaczona jest dla dwóch graczy – jeden kontroluje interfejs rozbrajania bomby, a drugi dysponuje pełnym manualem zawierającym kluczowe wskazówki i instrukcje. Choć gra przewiduje możliwość rozgrywki solo, doświadczenie jednoosobowe jest znacznie gorsze, gdyż wiele zagadek opiera się na współpracy i komunikacji między partnerami.

Rozgrywka składa się z 8 poziomów o rosnącym poziomie trudności. Każdy poziom zawiera od 2 do 8 unikalnych modułów z zagadkami. Każdy moduł wymaga precyzyjnego rozwiązania zagadek, a najmniejszy błąd powoduje reset zagadki lub nawet wybuch bomby.

Sama gra zmienia się dynamicznie dzięki wpływowi trzech różnych środowisk, które oddziałują na funkcjonowanie urządzenia. Warunki, takie jak jazda pociągiem czy skażenie, sprawiają, że każda rozgrywka jest nieprzewidywalna.

#### 1.2 Zaimplementowane mechaniki
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

#### 1.3 Tło fabularne
Gra osadzona jest w kryzysowej sytuacji. Jeden z graczy, wracając z ciężkiej podróży, znalazł się w pułapce, z której jedynym wyjściem jest szybkie i precyzyjne rozbrojenie bomby podłożonej w pociągu. Na szczęście nie jest sam – jego przyjaciel, znajdujący się w centrali, po usłyszeniu o sytuacji postanowił jak najszybciej sięgnąć po manual *How Not to Explode (Maybe)* i poprowadzić kompana, udzielając mu podpowiedzi oraz konkretnych instrukcji.

### 2. Sterowanie
Podczas całej gry gracz korzysta wyłącznie z myszki. W trakcie rozbrajania bomby może przybliżyć ekran spacją, a klawiszem *Escape* powrócić do menu.

### 3. Jak gra nawiązuje do tematu?
Temat *"Zegar tyka"* wiąże się bezpośrednio z głównym celem gry – rozbrojeniem bomby przed upływem czasu. Dodatkowo podczas rozbrajania bomby gracz słyszy faktyczne tykanie zegara.

### 4. Proces twórczy
- **Grafiki** zostały wykonane w stylu pixel art 16x16 w programach Aseprite i Adobe Photoshop. Wyjątkiem jest tło za bombą – rozpikselowane zdjęcie stockowe.
- **Audio** to dźwięki stockowe, przepuszczone przez filtr *Time Machine* w Audacity, nadający im oldschoolowy charakter.
- **Inspiracja** – gra *Keep Talking and Nobody Explodes*.

### 5. Linki do assetów
Niektóre grafiki i dźwięki pochodzą z pixabay.com (licencja CC0). Dźwięki 8-bitowe wygenerowano na sfxr.me. Użyty font to *Joystix Monospace*, a grafiki kursora pochodzą z *kenney_cursor-pixel-pack*.

### 6. Instrukcja uruchomienia
Zbudowaną grę pobiera się z zakładki *Releases* i uruchamia przez *Defuse or Lose.exe*. Manual jest wymagany do rozgrywki i leży w katalogu głównym repozytorium:
- [Manual Defuse or Lose (PL).pdf](<Manual Defuse or Lose (PL).pdf>) – po polsku
- [Manual Defuse or Lose (EN).pdf](<Manual Defuse or Lose (EN).pdf>) – po angielsku
