# TestTask
Ten repozytorium zawiera rozwiązanie testowego zadania `System prowizji`


## Treść
* TestTask.ConsoleApp
* TestTask.Logic
* TestTask.Tests


## TestTask.ConsoleApp
Projekt konsolowy, do działania wymaga pliki `struktura.xml` oraz `przelewy.xml`, znajdujące się w tym samym folderze, co plik wykonywalny


## TestTask.Logic
Biblioteka zawierająca główną logikę programu, mianowicie klasę `CommissionSystem`


## TestTask.Tests
Projekt testowy, uwzględniający kilka testowych przypadków oraz `SpeedTest`, który szacuje czas wykonania programu dla różnej ilości oraz struktury danych wejściowych


## Opis
Głównym wyzwaniem podczas wykonania tego zadania, było otrzymanie jak najlepszej złożoności czasowej, przy sensownej złożoności przestrzennej 
To są najważniejsze zastosowane techniki
* `HashTables`: Używałem w programie tablic z haszowaniem, które pozwoliły na szybkie wyciąganie elementów, co znacznie skróciło całkowitą złożoność obliczeniową w krytycznych miejscach
* `Struktura XML`: Dzięki strukturze pliku `xml`, mianowicie faktu, że dziecko zawsze idzie po rodzicu, udało się przyspieszyć niektóre części programu

Bardziej szczegółowo program jest opisany w komentarzach do kodu
