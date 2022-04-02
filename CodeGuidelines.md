# Code Guidelines

Hier sind die "Best Practices" zum Schreiben von Code aufgelistet.<br/>
Um den Code konsistent und lesbar für alle zu machen, sollten diese Guidelines von allen auch aufgenommen werden.<br/>
Die Guidelines können noch abgesprochen, erweitert oder verändert werden.<br/>
Kann evtl. auch ins Wiki übertragen/ verlinkt werden.

---
## Sprache
- Da alle Englisch sprechen können, sollten Variablen, Funktionen und Kommentare in Englisch verfasst werden

## Allgemeines
- Gruppierung von Eigenschaften, z.B. public/ private, Properties, ja nach Zusammenhang der Variablen
- Die Datei sollte nach dem Klassennamen benannt sein
- Keine "magischen Hacks" also z.B. Funktionen die irgendwie und auf wundersame Weise funktionieren
    - Wenn doch: Gut und ausführlich durch Kommentare erläutern was passiert und warum

## Namensgebung
### Dont's
- Polnische/ Ungarische/ ... Notation
- Keine Präfixe wie m_, _, ... (für lokale Variablen `this` benutzen)

### Do's
- camelCase für member Variablen
- camelCase für Parameter
- camelCase für lokale Variablen
- PascalCase für Funktionen, Properties, Events und Klassen
- Interfaces werden mit dem Präfix "I" geschrieben

## Klammern
### Formatierung
- Öffnende Klammern sollten am Anfang der Zeile sein, nach dem Statement, welches den Block beginnt.
- Beispiel:
```cs
    if (condition)
    {
       DoSomething()
    }
    else
    {
       DoSomethingElse()
    }
```
- Dabei auch auf die richtige Einrückung achten
- Statements und Klammern **nicht** in dieselbe Zeile schreiben, z.B.
    ```cs
    for (int i = 0; i < vec.length(); i++) { DoSomething(); }
    ```
    - Ausnahme für z.B. Properties
    ```cs
        get { return bar; }
        set { bar = value; }
    ```
### Wann Klammern?
- Klammern sollten immer geschrieben werden, selbst für einzeilige Code- Funktionalität

## Kommentare
- Kommentare sollten dafür verwendet werden, um die Intention, die Funktionsweise oder den logischen Fluss zu erklären
- Wenn möglich Kommentare über dem Code schreiben statt dahinter
    - Bei kurzen Kommentare ist aber auch hinter den Code schreiben erlaubt
- Lieber zu viele Kommentare als zu wenig
- Nicht vergessen Kommentare zu aktualisieren!
- Gerne können auch xml Kommentare genutzt werden, viele Editoren bieten dann besseres intellisense z.B.
```cs
    /// <summary> Adds two doubles and returns the result. </summary>
    /// <returns> The sum of two doubles. </returns>
    /// <param name="a">A double precision number.</param>
    /// <param name="b">A double precision number.</param>
    public static double Add(double a, double b)
```

*Quelle*: unter anderem: http://wiki.unity3d.com/index.php/Csharp_Coding_Guidelines
