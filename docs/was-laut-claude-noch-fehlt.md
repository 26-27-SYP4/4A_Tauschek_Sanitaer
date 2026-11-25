# Was laut Claude noch fehlt
Datum: 25.11.2026

## Offene Fragen

### Fachlich: Das liefert oder entscheidet der AG
1. **Konkrete Fragen mit Keys, Antworttypen und Antwortmöglichkeiten.** Damit wird die erste Version befüllt.
2. **Standardwerte für „Ich weiß nicht“** bei jeder Frage, die diese Antwort erlaubt.
3. **Konkrete Regeln:** Welche Bedingung führt zu welchem Baustein in welcher Menge, und welche Kombinationen sind unlogisch?
4. **Prozentsatz der Preisspanne:** Um wie viel Prozent wird die Summe der Preise nach unten und oben erweitert?
5. **Laufende Konfigurationen** bei einer neuen Version: Bleiben sie bei der alten Version?
6. **Status-Werte der Leads** (vorläufige Liste), **Merkmale für die Priorität** und Gewichtung.
7. **Benachrichtigungskanal** neben E-Mail: ja oder nein, und welcher?
8. **Energieberatung:** Was genau ist gemeint? Es gibt ENB-01 bis 04, aber kein Kapitel im Fachkonzept.
9. **Aufbewahrung und Löschung von Leads** (DSGVO), wer die Website betreut und wie der Konfigurator eingebunden wird.
10. **Kontaktdaten und Datenschutz** sind ein fester Teil am Ende, nicht Teil des Fragenkatalogs. Muss noch bestätigt werden.

### Technisch: Das entscheidet das Team
1. **Versionen:** Im Repo stecken schon Angular 22 und .NET 10. Laut AGENTS.md ist das aber noch offen und muss bestätigt werden.
2. **SQL-Produkt**, z. B. SQL Server, PostgreSQL oder SQLite für die Entwicklung.
3. **Authentifizierung:** JWT ist gesetzt. Offen sind: Wer legt Benutzer an, wie wird das Passwort gehasht, und wie authentifizieren sich die beiden Backends gegenseitig für `/api/admin` und `POST /api/leads`?
4. **Anbindung an die Firmendatenbank:** Welche API gibt es überhaupt, mit welchen Feldern, wie oft wird abgeglichen, und was passiert mit Bausteinen, die dort wegfallen? Das ist das größte technische Risiko, weil darüber noch nichts bekannt ist.
5. **Hosting, Testumgebung und Deployment.** Der AG will jederzeit testen können.
6. **Verschlüsselung der Datenbank** (SICH-01) und das **Backup-Verfahren** (WART-01).
7. **PDF-Bibliothek** und **E-Mail-Versand** (SMTP-Anbieter).

## Was zum Programmieren noch fehlt
1. **Projektstruktur.** Das ist der eigentliche Blocker:
   - Im Repo liegt nur die BookLoans-Vorlage. Einen Ordner für die Verwaltungsapp gibt es nicht.
   - `Konventionen.md` sagt „Code im Ordner `src`“, der Code liegt aber unter `Heizungskonfigurator/`. Das widerspricht sich.
   - Festzulegen sind: Ordnerstruktur für beide Apps, Projektnamen, wie die Backend-Projekte aufgeteilt werden (API, Datenbankzugriff, Logik, Tests) und Befehle für Build, Test und Start. Die Befehle gehören dann auch in AGENTS.md unter „Befehle und Struktur“.
   - Außerdem die schon eingecheckten `bin`/`obj`/`.vs`-Dateien aus Git entfernen.
2. **Coding-Konventionen für den Code.** `Konventionen.md` regelt bisher nur Markdown und Git. Es fehlt: Benennung (Englisch oder Deutsch im Code?), Formatierung (.editorconfig für C#), Aufbau der API (REST-Stil, Fehlerformat) und Aufbau der Angular-Komponenten.
3. **Datenmodell als ERD für beide Datenbanken.** Im Solution Design steht das Modell für Fragen, Regeln und Leads. Es fehlen Bausteine, Benutzer und Rollen, Statusverlauf, Notizen und Benachrichtigungen. Im Protokoll vom 21.10. war ein ERD ohnehin schon gefordert.
4. **API-Vertrag** zwischen den beiden Backends und zu den Frontends, am besten als OpenAPI. Im Frontend ist der openapi-generator schon eingerichtet (`openapitools.json`).
5. **Backlog für den ersten Sprint:** Die Anforderungen in Issues zerlegen, eines pro Person, und festlegen, wann ein Issue als fertig gilt. Laut Protokoll ist das Pflicht.
6. **CI:** Es gibt bisher nur einen Frontend-Build. Ein Backend-Build mit Tests fehlt (WART-03).
7. **Testdaten:** Ein fiktiver Fragenkatalog, fiktive Bausteine und fiktive Regeln, damit ohne die Angaben des AG weitergearbeitet werden kann.

## Vorschlag
Vieles ist gar nicht vom AG blockiert. Wenn die Projektstruktur und die technischen Punkte 1 bis 3 (Versionen, SQL-Produkt, Authentifizierung) entschieden sind, kann mit fiktiven Testdaten begonnen werden:
- Pflege von Fragen und Regeln mit Versionierung
- Auswertung der Regeln
- Konfigurator, der die Fragen dynamisch aus der Datenbank aufbaut
- Grundgerüst der Leadverwaltung

Konkrete Regeln, Preise und die Anbindung an die Firmendatenbank kommen dazu, sobald der AG die Angaben liefert.
