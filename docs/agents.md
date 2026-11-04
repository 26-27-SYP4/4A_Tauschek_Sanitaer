# AGENTS.md – Heizungskonfigurator

Gemeinsame Arbeitsregeln für OpenAI Codex und Claude Code in den beiden Projekt-Repositories. Das Projektteam verantwortet fachliche Entscheidungen, Prüfung und Freigabe der KI-Arbeit.

## Maßgebliche Dokumente

- `Konventionen.md` bestimmt den Coding-Stil.
- `docs/Anforderungen.md` v0.2.0 enthält priorisierte Anforderungen und Prüfkriterien.
- `docs/Fachliches_Konzept.md` v0.3.0 beschreibt fachliche Abläufe.
- `docs/Stakeholeranalyse.md` beschreibt Stakeholder und Abstimmungsbedarf.
- Vor Änderungen zuerst die relevanten Dokumente und den vorhandenen Code lesen. Bei Widersprüchen nachfragen.

## Arbeitsweise

1. Bestehenden Stand analysieren und einen kurzen Plan mit betroffenen Bereichen, Prüfungen und offenen Entscheidungen vorlegen.
2. Ausdrückliche Freigabe abwarten, bevor Dateien geändert oder Funktionen implementiert werden.
3. Nur den freigegebenen Umfang umsetzen. Neue fachliche oder technische Fragen zuerst klären.
4. Änderungen und tatsächlich ausgeführte Prüfungen knapp zusammenfassen. Keine ungeprüften Ergebnisse als erfolgreich darstellen.

## Technik und Zusammenarbeit

- Das Projekt besteht aus zwei Git-Repositories: `heizungskonfigurator` für die öffentliche Kundenanwendung und `verwaltungsapp` für die interne Leadverwaltung. Namen können an die tatsächlichen Repository-Namen angepasst werden.
- Jedes Repository enthält sein Angular/TypeScript-Frontend und sein C#/.NET-Backend gemeinsam. Frontend und Backend werden vorerst nicht in eigene Repositories aufgeteilt.
- Datenbankschema und EF-Core-Migrationen gehören zum Backend, das die jeweiligen Daten fachlich verwaltet. Kein separates Datenbank-Repository anlegen. Weitere gemeinsame Pakete oder Repositories erst nach ausdrücklicher Architekturentscheidung.
- Zuständigkeit und Schreibzugriff für Leads, Bausteine und Preise müssen eindeutig sein. Nicht mehrere Backends dieselben Daten unabhängig verwalten oder dieselben Tabellen parallel verändern lassen.
- VS Code wird für Frontend und Datenbankarbeiten, Visual Studio für Backendarbeiten verwendet.
- Für Datenbankzugriffe ausschließlich Entity Framework Core verwenden. Keine weiteren ORMs oder direkten SQL-Zugriffswege einführen.
- Jeder arbeitet auf einem eigenen GitHub-Feature-Branch. Keine Commits, Pushes, Merges oder Releases ohne ausdrücklichen Auftrag.
- Vorhandene Unit-, Integrations- und End-to-End-Tests beachten. Keine Frameworks, Abhängigkeiten oder Projektstruktur ohne Freigabe ändern.
- `Konventionen.md` und bestehende Architektur einhalten; Änderungen klein, verständlich und wartbar halten.

## Fachliche Regeln

- Der öffentliche Konfigurator ist ohne Kundenkonto erreichbar und führt anhand bedingter Fragen durch bestehende Heizung, Gebäude, Warmwasser und gewünschtes Heizsystem.
- Fragen, Antwortoptionen und Bedingungen sollen konfigurierbar sein. Antworten eines Leads müssen dem Fragenstand zum Anfragezeitpunkt zugeordnet bleiben.
- Ein Angebot enthält genau einen passenden, aktiven Wärmeerzeuger sowie durch freigegebene Regeln bestimmte Zusatzbausteine. Artikel und Preise stammen aus der Firmendatenbank; Zugriffe darauf sind nur lesend.
- Wärmebedarf wird laut freigegebener Fachregeln aus Verbrauch oder ersatzweise aus Fläche und Gebäudezustand geschätzt. Preise werden als unverbindliche Spanne ausgewiesen.
- „Ich weiß nicht“ darf nur mit einem freigegebenen Standardwert behandelt werden; Wert und Annahme sind im Lead und PDF festzuhalten.
- Gibt es keinen passenden Baustein, wird kein automatisches Angebot erstellt und der Lead für persönliche Beratung markiert. Bei bedingter Eignung ist ein Hinweis auf Prüfung vor Ort erforderlich.
- Jede abgeschlossene Konfiguration wird als Lead mit Antworten, Bausteinen und Schätzung gespeichert. Die Verwaltung ist angemeldeten Benutzern mit festgelegten Rollen vorbehalten.
- Nach dem Absenden wird ein PDF mit Kostenspanne, Bausteinen, Annahmen und Hinweisen erstellt. Der Auftraggeber wird per E-Mail über neue Leads benachrichtigt. Ein fehlgeschlagener Versand darf die Leadspeicherung nicht verhindern.
- Keine Wärmebedarfsformeln, Grenzwerte, Preise, Standardwerte, Produktregeln oder Prioritätsgewichtungen erfinden. Fehlen freigegebene fachliche Angaben, nachfragen.

## Sicherheit und Datenschutz

- Gesamter Verkehr über HTTPS. Kundendaten werden verschlüsselt gespeichert; Passwörter niemals im Klartext, sondern mit geeigneten Passwort-Hashing-Verfahren.
- Kunden erhalten keinen Login. reCAPTCHA und Rate-Limiting schützen den öffentlichen Konfigurator. Für die angemeldete Verwaltung sind Rollen und Berechtigungen durchzusetzen.
- Die Anforderungen nennen JWT für Benutzeranmeldung; die frühere Vorgabe nennt Installateur-Login per JWT als spätere Funktion. Vor Authentifizierungsarbeit klären, welche Benutzer ab wann erfasst sind und wie Rollen aussehen.
- Eingaben serverseitig validieren und Ausgaben gegen XSS schützen. Keine ungeprüften Eingaben als HTML rendern.
- Logs dürfen Fehler und Systemereignisse, aber keine echten Kundendaten, Passwörter, Tokens oder Geheimnisse enthalten. Für Tests ausschließlich fiktive Daten verwenden.
- Datenschutz-Zustimmung mit Zeitpunkt speichern. Löschung und Aufbewahrung von Leads sowie Backupkopien mit dem Auftraggeber abstimmen.

## Backups und CI/CD

- Vorgesehen ist ein automatisiertes tägliches, verschlüsseltes SQL-Backup auf einem separaten Unternehmensspeicher. Speicher, Verfahren, Schlüsselverwaltung, Aufbewahrung und Wiederherstellung sind vor Umsetzung festzulegen.
- Tests sollen laut Anforderungen in der CI/CD-Pipeline bei Merges auf `main` laufen. CI-Anbieter, Hosting und Deployment-Ziele sind offen; nicht eigenständig auswählen oder konfigurieren.

## Vor Umsetzung zu klären

Keine unbelegten Entscheidungen treffen zu SQL-Produkt/-Version, Angular/.NET-Versionen, Hosting, CI-Anbieter, Datenbankverschlüsselung, Authentifizierungsdetails oder Backupverfahren. Ebenfalls fachlich offen sind konkrete Fragen/Standardwerte, Wärmebedarfs- und Preisregeln sowie Importdetails.

Die bereitgestellte `docs/Fachliches_Konzept.md` enthält ungelöste Git-Konfliktmarker. Vor fachlicher Umsetzung muss das Team die gültige Fassung bestätigen. `Anforderungen.md` verweist außerdem auf ein `Solution_Design.md`, das nicht mitgeliefert wurde; vor Architekturentscheidungen anfordern. Energieberatung wird in den Anforderungen erwähnt, ist im Fachkonzept aber nicht ausgearbeitet und braucht daher eine separate fachliche Freigabe.
