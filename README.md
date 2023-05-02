# **ICS Projekt**
## Cieľ:
- ### Vytvorenie aplikácie umožňujúcej jej užívateľom správu aktivít a meranie času stráveného danou aktivitou
  
# Tím: ics-2023-xjanos19
Výsledné hodnotenie

## Členovia:
- Simona Jánošíková
- Ela Fedorová
- Klára Smoleňová
- Marián Tarageľ
- Matej Keznikl

# Fázy projektu zo zadania
## Prvá fáza :white_check_mark:
-   logický návrh tříd
-   využití abstrakce, zapouzdření, polymorfismu - kde to bude dávat smysl a eliminuje duplicity
-   verzování v GITu po logických částech
-   logické rozšíření datového návrhu nad rámec zadání (bonusové body) - toto rozšíření ovšem zvažte; často se stává, že si tím založíte na spoustu komplikací v pozdějších fázích; body za rozšíření dostanete až u obhajoby, pokud je naimplementujete kompletně do výsledné aplikace
-   generovaný ER diagram (logickou strukturu)
-   Wireframy (logickou strukturu, uživatelskou přívětivost, ne kvalitu grafického zpracování)
-   využití **Entity Framework Core - Code First** přístupu na vytvoření databáze z entitních tříd
-   existenci databázových migrací (alespoň InitialMigration)

## Druhá fáza :white_check_mark:
- opravení chyb a zapracování připomínek, které jsme vám dali v rámci hodnocení fáze 1
- návrh a funkčnost repositářů
- návrh a funkčnost fasád
- čistotu kódu
- pokrytí aplikace testy - ukážete tím, že repositáře opravdu fungují
- dejte pozor na zapouzdření databázových entit pod vrstvou fasád, která je nepropaguje výše, ale přemapovává na modely/DTO
- funkční build v Azure DevOps
- výsledek testů v Azure DevOps po buildu

## Tretia fáza :white_check_mark:
- opravení chyb a zapracování připomínek, které jsme vám dali v rámci hodnocení fází 1 a 2
- funkčnost celé výsledné aplikace
- vytvoření View, ViewModelů
- zobrazení jednotlivých informací dle zadání – seznam, detail…
- správné využití data-bindingu v XAML
- čistotu kódu
- validaci vstupů

