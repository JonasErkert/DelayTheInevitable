# Commit Guidelines

Hier sind die "Best Practices" zum committen aufgelistet.<br/>
Diese sollten noch abgesprochen, erweitert oder verändert werden.<br/>
Kann evtl. auch ins Wiki übertragen/ verlinkt werden.<br/>

## Sprache
- Da alle Englisch sprechen können, sollten die commit Nachrichten in Englisch geschrieben werden

---
## Do's
### 1. **"Commit early, commit often"**
- Nicht zu lange damit warten, einen commit zu machen- selbst wenn die Aufgabe nicht komplett erledigt ist
- Der Code sollte nach Möglichkeit jedoch nicht die Funktionsfähigkeit des Programms zerstören
- Dementsprechend darauf achten, auf welchen Branch man pusht!

### 2. *Gute* commit- Nachrichten schreiben
- Informationen in commit- Nachrichten:
    - Nicht davon ausgehen, dass der Leser versteht was das originale Problem war
    - Nicht davon ausgehen, dass der Code selbsterklärend ist
    - Beschreiben, warum eine Änderung gemacht wurde
    - Beschreiben, was verändert wurde. Bei dateien, die nicht als text gedifft (verglichen) werden können besonders wichtig
    - Wenn möglich zusammenhängendes issue referenzieren

### 3. Kurzfassen
- Auch wenn bei `2.`viel geschrieben werden kann, trotzdem versuchen die commit Nachrichten so kurz und prägnant wie möglich zu schreiben

### 4. Kleine Änderungen
- Werden z.B. nur kleine Tippfehler gefixt oder sehr geringe Änderungen im Code vorgenommen, reicht auch ein `m` wie `minor`

### 5. **Zeit nehmen**
- Genug Zeit für einen commit einplanen, um z.B. alle zu comittenden Dateien noch einmal kurz anzuschauen und eine gute Nachricht zu verfassen
- Kein "Ich muss gleich weg, kurz noch schnell was comitten"

### 6. Allgemeines
- Auch `.meta` Dateien von Unity zum VCS hinzufügen
- Assets wie Modelle, Texturen, Sounds usw. auf [seperates Repo](https://gitlab.com/JonasErkert/seriousubahnvr-assets) pushen
- Nicht vergessen, nach dem **committen** auch noch zu **pushen**, damit andere Zugriff auf die Datein haben!
- Es muss nicht zwangsläufig nach jedem commit gepusht werden, sollte aber regelmäßg passieren

---
## Dont's
### 1. Nichts- sagende Nachrichten 
- wie z.B. nur "Fixed", "Changed things" oder "Added"

### 2. Unnötige Dateien
- Keine von einer Software generierten Datein commiten, dies sollte schon durch die. gitignore(s) verhindert werden, aber generell gilt:
- Evtl. selbst neue .gitignore Regeln erstellen, aber davor ablären

### 3. Nur Dateien committen, die einem selbst bekannt sind
- Sind sie das nicht, wurden sie vermutlich generiert und sollten nicht committed werden

### 4. Große Dateien comitten
- Wie abgesprochen sollten große Dateien wie 3D Modelle, .blend files und sehr große Texturen in den "LRZ sync and share" geschoben werden

### 5. Whitespace Änderungen mit funktionalen Änderungen vermischen
- Formatierungs-, Whitespace- und sonstige Formatierungsänderungen wenn möglich getrennt von funktionalen Änderungen committen
- Wenn in einem großen Skript z.B. 50 Whitespace Änderungen und 2 funktionale Änderungen gemacht worden sind, ist es nicht einfach diese funktionalen Änderungen zu finden

### 6. Zwei unabhängige funktionale Änderungen vermischen
- Verschiedene Aufgaben/ Issues getrennt committen

### 7. Ein rießiger commit zu einer rießigen Änderung
- Die Änderung/ ein neues Feature wenn möglich in kleinere Aufgaben herunterbrechen und dann in kleinen Teilen committen (nach und nach)  (siehe `1.`)

### 8. Ausgeklammerter Code
- Zum Debuggen kann Code natürlich nach Belieben schnell ausgeklammert werden
- Nicht aber, um nicht funktionsfähigen (schon committeten)/ alten Code "lieber mal zu behalten"- durch git-tools können solche Änderungen nachvollzogen werden (Blame, Bisect, ...)