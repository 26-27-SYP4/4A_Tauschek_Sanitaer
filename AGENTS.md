# AGENTS.md – Heizungskonfigurator

## Projekt

Heizungskonfigurator mit Kostenschätzung in einem Git-Repository mit zwei Anwendungen:

- Heizungskonfigurator: öffentliche Kundenanwendung ohne Kundenkonto
- Verwaltungsapp: interne Leadverwaltung für angemeldete Benutzer

Jede Anwendung hat ihr eigenes Angular/TypeScript-Frontend und ihr eigenes C#/.NET-Backend. Eine spätere Aufteilung auf zwei Repositories ist möglich, aber nicht ohne Freigabe vorzunehmen. Was nur für eine Anwendung gilt, steht unter „Anwendungsspezifisch“ am Ende.

## Datenspeicherung

- Leads: speichert und verwaltet das Backend der Verwaltungsapp. Der Heizungskonfigurator übergibt neue Leads an dieses Backend.
- Bausteine inklusive Artikel und Preise: speichert das Backend des Heizungskonfigurators. Artikel und Preise werden über eine API-Schnittstelle aus der Firmendatenbank des AG übernommen.
- Fragen, Regeln und Versionen des Fragenkatalogs: speichert das Backend des Heizungskonfigurators. Die Verwaltungsapp bearbeitet sie ausschließlich über dessen API.
- Datenbankschema und EF-Core-Migrationen liegen im Backend, das die jeweiligen Daten speichert.

## Befehle und Struktur

Noch nicht festgelegt. Keine Build-, Test- oder Setup-Befehle raten und keine Projektstruktur eigenständig anlegen; bei Bedarf nachfragen.

## Dokumente

- `docs/Konventionen.md`: verbindlicher Coding-Stil
- `docs/Glossar.md`: verbindliche Begriffe (z. B. LeadDB, KundenDB)
- `docs/Anforderungen.md`: priorisierte Anforderungen und Prüfkriterien
- `docs/Fachliches_Konzept.md`: fachliche Abläufe
- `docs/Stakeholderanalyse.md`: Stakeholder und Abstimmungsbedarf

Vor Änderungen die relevanten Dokumente und den vorhandenen Code lesen. Bei Widersprüchen zwischen Dokumenten, Code und Auftrag nachfragen.

## Arbeitsweise

1. Stand analysieren und einen kurzen Plan vorlegen: betroffene Bereiche, geplante Prüfungen, offene Fragen.
2. Ausdrückliche Freigabe abwarten, bevor Dateien geändert werden.
3. Nur den freigegebenen Umfang umsetzen. Neue fachliche oder technische Fragen zuerst klären.
4. Abschließend knapp berichten: was geändert wurde und welche Prüfungen tatsächlich ausgeführt wurden, mit Ergebnis. Nicht Ausgeführtes nie als erfolgreich darstellen.

## Git

- Jeder arbeitet auf einem eigenen Feature-Branch.
- Keine Commits, Pushes, Merges oder Releases ohne ausdrücklichen Auftrag.

## Technische Regeln

- Datenbankzugriff ausschließlich über Entity Framework Core. Keine weiteren ORMs, kein direktes SQL.
- Daten werden nur von dem Backend geschrieben, das sie laut „Datenspeicherung“ speichert. Nie aus beiden Backends dieselben Tabellen schreiben; die andere Anwendung greift über die API des zuständigen Backends zu.
- Keine Frameworks, Abhängigkeiten, Projektstruktur, gemeinsamen Pakete oder neuen Repositories ohne Freigabe.
- Bestehende Tests nicht löschen oder abschwächen, um sie zum Bestehen zu bringen.

## Fachliche Leitplanken

- Keine Wärmebedarfsformeln, Grenzwerte, Preise, Standardwerte, Produktregeln oder Prioritätsgewichtungen erfinden. Fehlen freigegebene Angaben, nachfragen.
- Artikel und Preise stammen aus der Firmendatenbank und werden über eine API-Schnittstelle übernommen; Zugriff darauf nur lesend.
- Antworten eines Leads bleiben dem Fragenstand zum Anfragezeitpunkt zugeordnet: Der Lead speichert Versionsnummer, Fragetexte, Antworten und die Begründung (zutreffende Regeln) selbst. Regeln neuer Versionen werden nie auf alte Leads angewendet.
- Veröffentlichte Versionen des Fragenkatalogs sind unveränderlich. Der Key ist die Variable, in der eine Antwort gespeichert wird; Regeln beziehen sich auf Keys. Pro Version befüllt jeder Key genau eine Frage.
- Details zu Abläufen stehen in `docs/Fachliches_Konzept.md`, nicht hier.

## Sicherheit und Datenschutz

- Gesamter Verkehr über HTTPS.
- Eingaben serverseitig validieren. Keine ungeprüften Eingaben als HTML rendern.
- Passwörter nie im Klartext speichern.
- Keine echten Kundendaten, Passwörter, Tokens oder Geheimnisse in Logs, Tests oder im Repository. Für Tests nur fiktive Daten.

## Offen – nicht selbst entscheiden

Ohne Freigabe nichts festlegen oder konfigurieren zu:

- SQL-Produkt und -Version, Angular- und .NET-Versionen
- Hosting, CI-Anbieter, Deployment
- Datenbankverschlüsselung, Passwort-Hashing-Verfahren
- Authentifizierung: JWT laut Anforderungen, aber Benutzer, Rollen und Zeitpunkt offen
- Backupverfahren, Aufbewahrung und Löschung von Leads
- Konkrete Fragen, Standardwerte, Wärmebedarfs- und Preisregeln, Details der API-Anbindung an die Firmendatenbank
- Energieberatung (in den Anforderungen erwähnt, fachlich nicht ausgearbeitet)

## Anwendungsspezifisch

<!-- Befehle und Struktur oben ergänzen, sobald festgelegt. -->

### Heizungskonfigurator (Kundenanwendung)

- Öffentlich erreichbar, kein Kundenlogin. Rate-Limiting schützt den Konfigurator (Must); reCAPTCHA ist vorgesehen (Should).
- Bedingte Fragen zu bestehender Heizung, Gebäude, Warmwasser und gewünschtem Heizsystem. Fragen, Antwortoptionen, Reihenfolge, Bedingungen und Regeln sind konfigurierbar und versioniert; Ablauf siehe `docs/Fachliches_Konzept.md` („Pflege von Fragen und Regeln“) und `docs/Solution_Design.md`.
- „Ich weiß nicht“ nur mit freigegebenem Standardwert behandeln; Wert und Annahme in Lead und PDF festhalten.
- Wärmebedarf nach freigegebenen Regeln aus dem Verbrauch, ersatzweise aus Fläche und Gebäudezustand. Preise nur als unverbindliche Spanne.
- Ein Angebot enthält genau einen passenden, aktiven Wärmeerzeuger plus regelbasierte Zusatzbausteine. Kein passender Baustein: kein automatisches Angebot, Lead für persönliche Beratung markieren. Unlogische oder technisch ungünstige Kombinationen schon bei der Eingabe erkennen und den Endkunden darauf hinweisen; bei bedingter Eignung zusätzlich Hinweis „Prüfung vor Ort nötig“ speichern und ins PDF übernehmen.
- Nach dem Absenden: Lead mit Antworten, Bausteinen und Schätzung an das Backend der Verwaltungsapp übergeben, PDF mit Kostenspanne, Bausteinen, Annahmen und Hinweisen erstellen, den AG und alle weiteren Benutzer mit aktivierter Benachrichtigung per E-Mail über den neuen Lead informieren (nur Hinweis und Link, Details stehen in der Verwaltungsapp). Ein fehlgeschlagener Versand darf die Leadspeicherung nicht verhindern.
- Datenschutz-Zustimmung mit Zeitpunkt speichern.

### Verwaltungsapp (Leadverwaltung)

- Speichert die Leads (siehe „Datenspeicherung“).
- Nur für angemeldete Benutzer. Rollen und Berechtigungen serverseitig durchsetzen.
- Fragen und Regeln bearbeiten und veröffentlichen dürfen Administrator und Geschäftsführer; Mitarbeiter nur lesen. Gespeichert werden sie im Backend des Heizungskonfigurators (siehe „Datenspeicherung“).
- Authentifizierungsdetails sind offen (siehe oben) und vor jeder Arbeit daran zu klären.