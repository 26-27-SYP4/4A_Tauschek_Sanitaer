# Solution Design
- Version 0.4.0
- Datum: 25.11.2026
- Autor(en): Geßwagner David, Tauschek Stefan, Weißenhofer Moritz, Wiesner Felix
- Auftraggeber: Tauschek Sanitär Heizung GmbH
## Applikations-Typ:
- Web-Applikation
## System Architektur:

## Software Architektur:
- Ein Git-Repository mit zwei Anwendungen: Heizungskonfigurator (öffentlich) und Verwaltungsapp (Leadverwaltung). Stellt sich später heraus, dass zwei Repositories besser sind, wird aufgeteilt.
- Jede Anwendung: Frontend-Backend-Datenbank

## Verwendete Technologien:
- Frontend: Angular, HTML, JS, TS, CSS
- Backend: C#
- Datenbank: SQL

## API-Design
- API-Stil: REST (Representational State Transfer)
- Backend: ASP.NET Core Web API mit C#
- Datenformat: JSON für Anfrage- und Antwortdaten
- HTTP-Methoden: GET zum Lesen, POST zum Erstellen bzw. Ausführen, PUT zum Ändern und DELETE zum Löschen
- Endpunkte: Ressourcenorientierte Endpunkte unter `/api/...`; die konkreten Schnittstellen sind im Abschnitt „Schnittstellen“ beschrieben
- Dokumentation und Testen: OpenAPI/Swagger zur automatischen API-Dokumentation und zum Testen der Endpunkte
- Fehlerbehandlung: passende HTTP-Statuscodes und eine verständliche Fehlermeldung zurückgeben
- Die Schnittstellen zwischen Heizungskonfigurator und Verwaltungsapp werden nach diesem API-Design umgesetzt.
- Die API der Firmendatenbank des Auftraggebers ist noch nicht bekannt. Ihr Protokoll und Datenformat werden nach Rücksprache mit dem Auftraggeber festgelegt; der Zugriff bleibt lesend.

## Konvention
- Konventionen in `Konventionen.md` festgelegt (Datei liegt im selben Ordner wie dieses Dokument).

## Persistierung
- Datenspeicherung: Alle generierten Leads werden relational in der Datenbank der Verwaltungsapp persistiert; der Heizungskonfigurator übergibt neue Leads an deren Backend.
- Bausteine: Der Heizungskonfigurator speichert die Bausteine in seiner eigenen Datenbank. Artikel und Preise werden über eine API-Schnittstelle aus der Firmendatenbank des AG übernommen (nur lesend).
- Backups: Da Kundendaten generiert werden, muss ein automatisierter täglicher SQL-Dump eingerichtet werden. (Backups müssen verschlüsselt und DSGVO-konform auf einem separaten Speichermedium des Unternehmens abgelegt werden).
## Sicherheit
- Authentifizierung: Kein Login für Kunden, stattdessen Rate-Limiting und voraussichtlich reCaptcha gegen Bots. Installateur-Login läuft später über JWT (JSON Web Token).
- Verschlüsselung: Kompletter Traffic über HTTPS. Datenbank verschlüsselt, Passwörter hashen.
- Logging: Fehler und Systemevents aufzeichnen, niemals echte Kundendaten ins Logfile schreiben
- App-Sicherheit:
    - Datenbank: Nur Entity Framework Core (ORM) nutzen, um SQL-Injections abzublocken
    - XSS: Alle Textfelder im Backend entschärfen
## Fragen, Regeln und Versionierung
### Datenspeicherung und Zugriff
- Fragen, Regeln und Versionen speichert das Backend des Heizungskonfigurators (dort liegen auch die Bausteine, und es braucht beides bei jeder Konfiguration).
- Die Verwaltungsapp schreibt diese Daten nie direkt in die Datenbank, sondern nur über die API des Heizungskonfigurators.
- Leads inklusive Antworten und Begründung speichert das Backend der Verwaltungsapp.
- Zugriff ausschließlich über Entity Framework Core.

### Datenmodell im Backend des Heizungskonfigurators
Jede Version ist ein vollständiger, eigener Satz von Datensätzen (Schnappschuss). Damit eine Frage oder Regel über Versionen hinweg wiedererkannt wird, hat sie neben dem Primärschlüssel ihres Datensatzes eine **stabile ID** (`QuestionId` bzw. `RuleId`), die beim Kopieren in eine neue Version gleich bleibt.

| Tabelle | Wichtige Felder | Hinweise |
| ------- | --------------- | -------- |
| `CatalogVersion` | `Id`, `VersionNumber` (leer beim Entwurf), `Status` (Draft/Published), `PublishedAt`, `PublishedBy`, `Comment`, `ChangeLog` | Genau ein Datensatz mit Status Draft. Veröffentlichte Datensätze werden nie geändert. |
| `Question` | `Id`, `CatalogVersionId`, `QuestionId` (stabil), `Key`, `Text`, `AnswerType` (Enum/Number/Boolean), `HelpText`, `AllowUnknown`, `DefaultValue`, `Unit`, `Min`, `Max`, `Position`, `IsActive` | `QuestionId` identifiziert die Frage, `Key` ist der Name der Variable, in die die Antwort geschrieben wird. Beide sind je Version eindeutig; derselbe `Key` kann in späteren Versionen von einer anderen Frage befüllt werden. `Position` bestimmt die Reihenfolge; Verschieben ändert nur `Position`. |
| `AnswerOption` | `Id`, `QuestionRowId`, `Text`, `Position` | Nur bei `AnswerType = Enum` |
| `Rule` | `Id`, `CatalogVersionId`, `RuleId` (stabil), `Name`, `IsActive`, `ActionType`, `ArticleNumber`, `FixedQuantity`, `TargetQuestionId`, `HintText` | Welche Felder genutzt werden, hängt von `ActionType` ab (siehe unten). |
| `ConditionGroup` | `Id`, `RuleRowId`, `Position` | Bedingungen innerhalb einer Gruppe: UND; Gruppen untereinander: ODER |
| `Condition` | `Id`, `ConditionGroupId`, `Key`, `Operator`, `Value` | `Value` als Text; Zahlen kulturunabhängig (Punkt als Dezimaltrennzeichen) |

**ActionType und genutzte Felder:**

| ActionType | Felder |
| ---------- | ------ |
| `AddComponentFixed` | `ArticleNumber`, `FixedQuantity` |
| `ShowQuestion` | `TargetQuestionId` |
| `ShowHint` | `HintText` |
| `PersonalConsultation` | – |

Bedingungen beziehen sich auf den `Key`, nicht auf die Frage. Nur `ShowQuestion` zielt auf eine bestimmte Frage (`TargetQuestionId`).

**Operator je AnswerType:** Number: `Eq`, `Ne`, `Lt`, `Le`, `Gt`, `Ge`; Enum: `Eq`, `Ne`; Boolean: `IsTrue`, `IsFalse`; alle: `IsUnknown`. Einen Typ für Freitext gibt es nicht.

### Entwurf, Veröffentlichung und Änderungsliste
- **Entwurf anlegen:** Gibt es keinen Entwurf, wird die letzte veröffentlichte Version vollständig kopiert (neue Primärschlüssel, gleiche stabile IDs).
- **Bearbeiten:** Alle Änderungen gehen nur auf den Entwurf. Gegen gleichzeitige Bearbeitung durch zwei Benutzer schützt ein Concurrency-Token in EF Core (der zweite Speichervorgang wird mit Hinweis abgelehnt).
- **Prüfen (Validate):** Jeder in Regeln verwendete `Key` wird von genau einer aktiven Frage befüllt; Werte verweisen auf vorhandene Antwortmöglichkeiten; Bausteine sind aktiv und vorhanden; Operator passt zum `AnswerType` der befüllenden Frage; jeder `Key` und jede `QuestionId` kommt nur einmal vor; Bedingungen einer `ShowQuestion`-Regel verwenden nur Keys von Fragen mit kleinerer `Position` als die Zielfrage (verhindert Zyklen und Vorgriffe).
- **Veröffentlichen:** In einer Datenbanktransaktion: Prüfung erneut ausführen, `VersionNumber` = höchste Nummer + 1, `Status = Published`, `PublishedAt`, `PublishedBy`, `Comment` setzen.
- **Änderungsliste:** Vergleich von Entwurf und letzter veröffentlichter Version über die stabilen IDs: neu (ID nur im Entwurf), gelöscht (ID nur in der alten Version), deaktiviert, verschoben (`Position` geändert), geändert (sonstige Felder, auch `Key` und `AnswerType`). Wird als `ChangeLog` mit der Version gespeichert.
- **Wiederherstellen:** Kopiert eine alte Version als neuen Entwurf; erst das Veröffentlichen erzeugt die neue Version.

### Speicherung im Lead (Backend der Verwaltungsapp)
Der Lead ist in sich vollständig und braucht für die Anzeige keinen Zugriff auf den Heizungskonfigurator.

| Tabelle | Wichtige Felder |
| ------- | --------------- |
| `Lead` | bisherige Felder, zusätzlich `Remarks` (optionale sonstige Bemerkungen, nicht in Regeln verwendet), `CatalogVersionNumber`, `HeatingSystem` (Antwort auf die Frage mit dem Key `heizsystem`), `NeedsConsultation`, `CostMin`, `CostMax`, `AssignedUserId` (zuständiger Benutzer) |
| `LeadAnswer` | `LeadId`, `QuestionId`, `Key`, `QuestionText`, `AnswerType`, `Position`, `WasShown`, `Value`, `IsUnknown`, `AssumedValue` |
| `LeadRuleHit` | `LeadId`, `RuleId`, `RuleName`, `ConditionText` (Klartext), `MatchedValues` (Key = Wert der maßgeblichen Antworten), `ActionType`, `Result` (z. B. „2 × Art.-Nr. 4711“) |
| `LeadComponent` | `LeadId`, `ArticleNumber`, `Name`, `Quantity`, `Price` |

`LeadAnswer` enthält **alle** Fragen der Version, auch nicht eingeblendete (`WasShown = false`, ohne Wert). Antworten sind damit Key/Value-Paare mit Fragetext zum Anfragezeitpunkt.

### Schnittstellen
| Richtung | Endpunkt | Zweck |
| -------- | -------- | ----- |
| Konfigurator-Frontend → Konfigurator-Backend | `GET /api/catalog/current` | aktuelle Version: Fragen, Antwortmöglichkeiten, `ShowQuestion`- und `ShowHint`-Regeln (keine Bausteine, keine Preise) |
| Konfigurator-Frontend → Konfigurator-Backend | `POST /api/configurations` | Absenden: `CatalogVersionId`, Antworten, Kontaktdaten, optionale Bemerkungen, Datenschutz-Zustimmung |
| Verwaltungsapp → Konfigurator-Backend | `GET/PUT /api/admin/draft/...` | Entwurf lesen und bearbeiten (Fragen, Reihenfolge, Regeln) |
| Verwaltungsapp → Konfigurator-Backend | `POST /api/admin/draft/validate` | automatische Prüfung |
| Verwaltungsapp → Konfigurator-Backend | `POST /api/admin/draft/test` | Testmodus: Beispielantworten rein, Ergebnis wie beim Absenden raus, ohne Lead |
| Verwaltungsapp → Konfigurator-Backend | `POST /api/admin/draft/publish` | Veröffentlichen mit Kommentar |
| Verwaltungsapp → Konfigurator-Backend | `GET /api/admin/versions`, `GET /api/admin/versions/{nr}`, `POST /api/admin/versions/{nr}/restore` | Versionen ansehen und wiederherstellen |
| Konfigurator-Backend → Verwaltungsapp-Backend | `POST /api/leads` | neuen Lead inklusive `LeadAnswer`, `LeadRuleHit`, `LeadComponent` übergeben |

Die `/api/admin`-Endpunkte sind nicht öffentlich und prüfen Berechtigungen serverseitig (Bearbeiten und Veröffentlichen nur Administrator und Geschäftsführer, Lesen auch Mitarbeiter).

## Ablauf Schritt für Schritt

### A) Fragen und Regeln pflegen und veröffentlichen
1. Administrator oder Geschäftsführer öffnet in der Verwaltungsapp die Pflege des Fragenkatalogs. Die Verwaltungsapp lädt den Entwurf über die Konfigurator-API; gibt es keinen, wird er aus der letzten Version angelegt.
2. Der Benutzer legt Fragen an, ändert, verschiebt (neue `Position`) oder löscht sie, legt Regeln mit Bedingungsgruppen an und aktiviert, deaktiviert oder löscht sie. Jede Änderung wird sofort im Entwurf gespeichert; der öffentliche Konfigurator bleibt unverändert.
3. Der Benutzer startet die Prüfung. Fehler werden mit Verweis auf die betroffene Frage bzw. Regel angezeigt.
4. Im Testmodus gibt der Benutzer Beispielantworten ein. Das Backend führt dieselbe Berechnung wie bei C) aus und liefert eingeblendete Fragen, zutreffende Regeln, Bausteine und Kostenspanne zurück, ohne einen Lead zu erzeugen.
5. Der Benutzer bestätigt mit optionalem Kommentar. Das Backend prüft erneut, vergibt die Versionsnummer, speichert die Änderungsliste und macht die Version zur aktuellen.

### B) Endkunde füllt den Konfigurator aus
1. Das Frontend lädt `GET /api/catalog/current` und merkt sich die `CatalogVersionId`.
2. Fragen werden nach `Position` angezeigt, eine pro Seite. Vor jeder Frage prüft das Frontend ihre `ShowQuestion`-Regeln mit den bisherigen Antworten: Hat eine Frage solche Regeln, wird sie nur angezeigt, wenn mindestens eine davon zutrifft; sonst wird sie übersprungen.
3. Nach jeder Antwort prüft das Frontend die `ShowHint`-Regeln und zeigt zutreffende Hinweise sofort an (KON-07).
4. Bei „Ich weiß nicht“ wird die Antwort als unbekannt markiert; der Standardwert wird erst im Backend eingesetzt.
5. Am Ende Kontaktdaten, optional sonstige Bemerkungen und Datenschutz-Zustimmung, dann Absenden.

Die Auswertung im Frontend dient nur der Bedienung. Das Backend wertet beim Absenden alles erneut aus und vertraut den Angaben des Frontends nicht. Die Auswertungslogik existiert damit in TypeScript und C#; beide werden mit denselben Testfällen geprüft.

### C) Berechnung beim Absenden (Backend des Heizungskonfigurators)
1. **Version laden:** Version zur übergebenen `CatalogVersionId` laden (Umgang mit veralteten Versionen siehe offene Punkte).
2. **Eingaben prüfen:** Rate-Limit, Pflichtangaben, Datentyp, Minimum/Maximum, Antwortmöglichkeit vorhanden, „Ich weiß nicht“ nur wo erlaubt, Datenschutz-Zustimmung.
3. **Sichtbarkeit bestimmen:** Fragen nach `Position` durchgehen und wie in B2 entscheiden, ob sie eingeblendet waren. Antworten zu nicht eingeblendeten Fragen werden verworfen.
4. **Standardwerte einsetzen:** Bei „Ich weiß nicht“ den `DefaultValue` als Wert verwenden und als Annahme kennzeichnen. Für den Vergleich `IsUnknown` zählt die ursprüngliche Angabe.
5. **Hinweisregeln auswerten:** zutreffende aktive `ShowHint`- und `PersonalConsultation`-Regeln sammeln.
6. **Bausteine wählen:** Für jede zutreffende aktive `AddComponentFixed`-Regel den Baustein mit `FixedQuantity` übernehmen; Mengen pro `ArticleNumber` addieren; nur aktive Bausteine. Trifft eine `PersonalConsultation`-Regel zu oder wurde kein Baustein gewählt → `NeedsConsultation = true`.
7. **Kostenspanne berechnen:** Summe aus `Price` × Menge, abzüglich bzw. zuzüglich des vom AG festgelegten Prozentsatzes. Bei `NeedsConsultation` keine automatische Schätzung.
8. **Begründung erstellen:** Für jede zutreffende Regel `LeadRuleHit` mit Bedingung im Klartext, maßgeblichen Antwortwerten und Ergebnis. `HeatingSystem` aus der Antwort mit dem Key `heizsystem` übernehmen; fehlt die Frage oder wurde „Ich weiß nicht“ gewählt, bleibt es leer.
9. **Lead übergeben:** `POST /api/leads` an die Verwaltungsapp mit allen Fragen der Version (`LeadAnswer`), Begründung, Bausteinen und Schätzung.
10. **PDF erstellen** mit Kostenspanne, Bausteinen, Annahmen und Hinweisen (bzw. Bestätigung „AG meldet sich persönlich“) und dem Endkunden zum Download geben.
11. **Benachrichtigen:** Die Verwaltungsapp informiert die Benutzer per E-Mail. Ein Fehlschlag verhindert die Speicherung des Leads nicht.

### D) Lead in der Verwaltungsapp ansehen
1. Die Detailansicht liest den Lead mit `LeadAnswer`, `LeadRuleHit` und `LeadComponent` aus der eigenen Datenbank.
2. Angezeigt werden die Versionsnummer, alle Fragen nach `Position` (eingeblendet oder nicht), Antworten mit gekennzeichneten Annahmen und die Begründung je Regel.
3. Spätere Änderungen an Fragen oder Regeln wirken sich nicht aus, weil alles beim Lead gespeichert ist.

### Offene technische Punkte
- Wie sich die Verwaltungsapp gegenüber den `/api/admin`-Endpunkten und der Heizungskonfigurator gegenüber `POST /api/leads` authentifiziert (hängt an der offenen Authentifizierungsentscheidung).
- Umgang mit Absendungen zu einer inzwischen veralteten Version (fachlich offen; Vorschlag: annehmen und mit der alten Version berechnen).
- Verhalten, wenn ein Baustein nach der Veröffentlichung durch den Abgleich mit der Firmendatenbank inaktiv wird oder wegfällt.

## Verwendete Programme:
- Visual Studio Code: Frontend, Datenbank
- Visual Studio: Backend
- Browser: Testen, KI

## Developer Test Strategie:
- Unit-Tests
- Integrationstests (Schnittstelle)
- End-to-End (simuliert den echten User-Flow im Browser)
- CI/CD

