# AGENTS.md – Heizungskonfigurator

## Projekt

Heizungskonfigurator mit Kostenschätzung, aufgeteilt auf zwei Git-Repositories:

- `heizungskonfigurator`: öffentliche Kundenanwendung ohne Kundenkonto
- `verwaltungsapp`: interne Leadverwaltung für angemeldete Benutzer

Jedes Repo enthält sein Angular/TypeScript-Frontend und sein C#/.NET-Backend. Datenbankschema und EF-Core-Migrationen liegen im Backend des Repos, das die jeweiligen Daten fachlich verwaltet. Was nur für dieses Repo gilt, steht unter „Repo-spezifisch“ am Ende.

## Befehle und Struktur

Noch nicht festgelegt. Keine Build-, Test- oder Setup-Befehle raten und keine Projektstruktur eigenständig anlegen; bei Bedarf nachfragen.

## Dokumente

- `docs/Konventionen.md`: verbindlicher Coding-Stil
- `docs/Anforderungen.md`: priorisierte Anforderungen und Prüfkriterien
- `docs/Fachliches_Konzept.md`: fachliche Abläufe
- `docs/Stakeholeranalyse.md`: Stakeholder und Abstimmungsbedarf

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
- Leads, Bausteine und Preise werden jeweils von genau einem Backend verwaltet. Nie aus beiden Backends dieselben Tabellen schreiben.
- Keine Frameworks, Abhängigkeiten, Projektstruktur, gemeinsamen Pakete oder neuen Repositories ohne Freigabe.
- Bestehende Tests nicht löschen oder abschwächen, um sie zum Bestehen zu bringen.

## Fachliche Leitplanken

- Keine Wärmebedarfsformeln, Grenzwerte, Preise, Standardwerte, Produktregeln oder Prioritätsgewichtungen erfinden. Fehlen freigegebene Angaben, nachfragen.
- Artikel und Preise stammen aus der Firmendatenbank; Zugriff darauf nur lesend.
- Antworten eines Leads bleiben dem Fragenstand zum Anfragezeitpunkt zugeordnet.
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
- Konkrete Fragen, Standardwerte, Wärmebedarfs- und Preisregeln, Importdetails
- Energieberatung (in den Anforderungen erwähnt, fachlich nicht ausgearbeitet)

## Repo-spezifisch

<!-- In jedem Repo nur den zutreffenden Block behalten. Befehle und Struktur oben ergänzen, sobald festgelegt. -->

### heizungskonfigurator (Kundenanwendung)

- Öffentlich erreichbar, kein Kundenlogin. reCAPTCHA und Rate-Limiting schützen den Konfigurator.
- Bedingte Fragen zu bestehender Heizung, Gebäude, Warmwasser und gewünschtem Heizsystem. Fragen, Antwortoptionen und Bedingungen sind konfigurierbar.
- „Ich weiß nicht“ nur mit freigegebenem Standardwert behandeln; Wert und Annahme in Lead und PDF festhalten.
- Wärmebedarf nach freigegebenen Regeln aus dem Verbrauch, ersatzweise aus Fläche und Gebäudezustand. Preise nur als unverbindliche Spanne.
- Ein Angebot enthält genau einen passenden, aktiven Wärmeerzeuger plus regelbasierte Zusatzbausteine. Kein passender Baustein: kein automatisches Angebot, Lead für persönliche Beratung markieren. Bei bedingter Eignung Hinweis auf Prüfung vor Ort.
- Nach dem Absenden: Lead mit Antworten, Bausteinen und Schätzung speichern, PDF mit Kostenspanne, Bausteinen, Annahmen und Hinweisen erstellen, Auftraggeber per E-Mail benachrichtigen. Ein fehlgeschlagener Versand darf die Leadspeicherung nicht verhindern.
- Datenschutz-Zustimmung mit Zeitpunkt speichern.

### verwaltungsapp (Leadverwaltung)

- Nur für angemeldete Benutzer. Rollen und Berechtigungen serverseitig durchsetzen.
- Authentifizierungsdetails sind offen (siehe oben) und vor jeder Arbeit daran zu klären.