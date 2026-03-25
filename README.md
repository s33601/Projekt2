#System Wypożyczalni Sprzętu IT
Projekt to konsolowa aplikacja w języku C# służąca do zarządzania wypożyczeniami sprzętu przez studentów i pracowników uczelni.

Instrukcja uruchomienia
1. Sklonuj repozytorium na lokalny komputer.
2. Otwórz plik rozwiązania (`Main2.sln`) w wybranym środowisku.
3. Uruchom projekt. Główny scenariusz wykona się automatycznie w oknie konsoli.
 
Decyzje projektowe i struktura kodu
Projekt został podzielony logicznie na `Sprzety`, `Uzytkownicy`, `Transakcje`, co ułatwia odnajdowanie się w projekcie oraz ewentualną późniejszą rozbudowę aplikacji.

Zasada jednej odpowiedzialności (SRP) i Kohezja: Podzieliłem projekt na sensowne foldery, żeby łatwo było znaleźć, co gdzie jest. Klasy takie jak `Laptop` czy `Student` służą po prostu do trzymania informacji o nich. Z kolei całą "czarną robotą", czyli logiką wypożyczeń, sprawdzaniem dostępności i liczeniem kar, zajmuje się jedna, osobna klasa `ZarzadzeniaWypozyczalnia`. Dzięki temu sprzęt zajmuje się byciem sprzętem i nie musi wiedzieć, jak działa system kar. To sprawia, że każda klasa robi tylko swoją robotę i kod jest spójny.

Luźne powiązania (Low Coupling): Oparcie logiki na klasach bazowych takich jak `Osoba` czy `Sprzet` zmniejsza sprzężenie w systemie. Klasa `ZarzadzeniaWypozyczalnia` operuje na ogólnych abstrakcjach. Oznacza to, że w przypadku dodania w przyszłości nowego typu sprzętu (np. Tablet) lub nowej roli użytkownika, główna logika systemu wypożyczeń nie będzie wymagała zmian.
