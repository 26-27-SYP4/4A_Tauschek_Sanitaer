# Anforderungen: Heizungskonfigurator
Version: 0.2.0  
Datum: 04.11.2026  
Autor(en): Geßwagner David, Tauschek Stefan, Weißenhofer Moritz, Wiesner Felix  
Auftraggeber: Tauschek Sanitär Heizung GmbH  
Grundlage: `Fachliches_Konzept.md` v0.3.0, `Solution_Design.md` v0.1.0, Entwurf „Heizungs-Konfigurator“ des AG (Oktober 2026). Beide genannten Markdown-Dateien liegen im selben Ordner wie dieses Dokument.
 
AG = Auftraggeber, Endkunde = Kunde des AG
 
## Legende
- **Priorität nach MoSCoW:** M = Must (muss in der ersten Version sein), S = Should (soll, wenn möglich), C = Could (kann, wenn Zeit bleibt)
- **Bezug:** Kapitel im Fachlichen Konzept, aus dem die Anforderung stammt
# Funktionale Anforderungen
 
## 1. Konfigurator (Endkunde)
Bezug: Fachkonzept „Soll-Zustand“, „Fragenkatalog“
 
| Anforderung | Prio |
| ----------- | ---- |
| Der Konfigurator ist ohne Anmeldung als eigener Abschnitt auf der Website des AG erreichbar. | M |
| Der Endkunde gibt die Angaben zu bestehender Heizung, Gebäude, Warmwasser und gewünschtem Heizsystem laut Fragenkatalog ein. | M |
| Der Konfigurator führt Schritt für Schritt durch die Fragen (eine Frage pro Seite, Fortschrittsanzeige, „Zurück“ und „Weiter“). | M |
| Folgefragen werden abhängig von vorherigen Antworten ein- oder ausgeblendet (z. B. Wärmequelle nur bei Wärmepumpe). | M |
| Bei Fragen, die Endkunden oft nicht beantworten können, gibt es die Antwort „Ich weiß nicht“. | M |
| Eingaben werden geprüft: Pflichtfelder, gültige Zahlenbereiche (z. B. Fläche, Personen), gültige E-Mail-Adresse. | M |
| Das System erkennt unlogische oder technisch ungünstige Kombinationen und weist den Endkunden darauf hin (z. B. Kühlen mit Heizkörpern, Wärmepumpe bei hoher Vorlauftemperatur). | M |
| Am Ende gibt der Endkunde seine Kontaktdaten ein; Absenden ist nur mit Zustimmung zu den Datenschutzbestimmungen möglich. | M |
| Der Konfigurator ist gegen automatisierte Anfragen geschützt (reCaptcha und Begrenzung der Anfragen pro Zeit). | M |
| Auswahlfragen werden als Kacheln mit kurzem Erklärungstext und Hinweisen dargestellt. | S |
| Zu einer Frage kann ein Hilfetext angezeigt werden (z. B. wo man den Verbrauch findet). | S |
| Bereits eingegebene Antworten gehen beim Neuladen der Seite nicht verloren. | C |
| Der Endkunde kann Fotos vom Heizraum und einen Hausplan hochladen. | C |
 
## 2. Pflege der Fragen
Bezug: Fachkonzept „Fragenkatalog“ (Erweiterbarkeit der Fragen)
 
| Anforderung | Prio |
| ----------- | ---- |
| Fragen, Antwortoptionen und Einblendbedingungen werden in der Datenbank gespeichert, nicht im Programmcode. | M |
| Ein Lead speichert Fragen und Antworten so, wie sie zum Zeitpunkt der Anfrage waren; spätere Änderungen an Fragen verändern alte Leads nicht. | M |
| Der Administrator kann Fragen in der Verwaltungskonsole anlegen, ändern, sortieren und deaktivieren, ohne Programmierung. | S |
| Der Administrator kann Einblendbedingungen, Hilfetexte und Standardwerte für „Ich weiß nicht“ festlegen. | S |
 
## 3. Bausteine und Kostenschätzung
Bezug: Fachkonzept „Bausteine“
 
| Anforderung | Prio |
| ----------- | ---- |
| Das System schätzt den Wärmebedarf aus dem bekannten Verbrauch oder, wenn dieser fehlt, aus Fläche und Gebäudezustand. | M |
| Das System wählt genau einen Wärmeerzeuger der gewünschten Technologie, dessen Eignungsbereich zum Wärmebedarf passt. | M |
| Das System ergänzt Zusatzbausteine (Speicher, Zubehör, Dienstleistungen) über Regeln der Form „Bedingung → Baustein, Menge“. | M |
| Das System berechnet auf Basis der Bausteine automatisch eine Kostenspanne (von–bis). | M |
| Bei „Ich weiß nicht“ verwendet das System einen Standardwert und speichert ihn als Annahme. | M |
| Gibt es keinen passenden Baustein (z. B. sehr hoher Wärmebedarf, Sonderwunsch), wird kein automatisches Angebot erstellt und der Lead für persönliche Beratung markiert. | M |
| Bei technisch nur bedingt passenden Kombinationen wird ein Hinweis „Prüfung vor Ort nötig“ gespeichert. | M |
| Ein Baustein hat die Merkmale Bezeichnung, Kategorie, Technologie, Heizleistung, Eignung, Hersteller/Modell, Preis, Artikelnummer und Aktiv. | M |
| Artikel und Preise werden regelmäßig aus der Firmendatenbank des AG importiert; der Zugriff auf die Firmendatenbank ist nur lesend. | S |
| Der Administrator pflegt Leistung, Eignung und Regeln der Bausteine in der Verwaltungskonsole, ohne Programmierung. | S |
| Bausteine können aktiviert und deaktiviert werden; deaktivierte Bausteine werden nicht ausgewählt. | S |
 
## 4. PDF mit Kostenschätzung
Bezug: Fachkonzept „Soll-Zustand“, „Sonderfälle“
 
| Anforderung | Prio |
| ----------- | ---- |
| Nach dem Absenden wird automatisch ein PDF mit der Kostenspanne und den gewählten Bausteinen erstellt. | M |
| Das PDF weist die Kostenschätzung deutlich als unverbindlichen Richtwert aus, nicht als verbindliche Feinplanung. | M |
| Das PDF enthält alle Annahmen (Standardwerte bei „Ich weiß nicht“) und Hinweise (z. B. „Prüfung vor Ort nötig“). | M |
| Der Endkunde kann das PDF nach dem Absenden sofort herunterladen. | M |
| Das PDF enthält Logo und Kontaktdaten des AG. | S |
| Der Endkunde bekommt das PDF zusätzlich per E-Mail. | S |
| Kann kein automatisches Angebot erstellt werden, bekommt der Endkunde statt des PDFs eine Bestätigung, dass sich der AG persönlich meldet. | S |
 
## 5. Energieberatung
Bezug: Meeting 19.10.2026 („automatische Energieberatung mit aktuellen Tagespreisen“). **Noch nicht im Fachkonzept beschrieben**, Kapitel muss ergänzt werden.
 
| Anforderung | Prio |
| ----------- | ---- |
| Das PDF enthält die geschätzten jährlichen Energiekosten der neuen Heizung. | S |
| Bei bekanntem bisherigen Verbrauch werden die Energiekosten der alten und der neuen Heizung verglichen. | S |
| Der Administrator kann die Energiepreise (Heizöl, Gas, Strom, Pellets, Holz) manuell pflegen. | S |
| Energiepreise werden täglich automatisch aus einer öffentlichen Quelle übernommen; bei Ausfall wird der letzte bekannte Preis verwendet. | C |
 
## 6. Leadverwaltung
Bezug: Fachkonzept „Leadverwaltung“
 
| Anforderung | Prio |
| ----------- | ---- |
| Jede abgeschlossene Konfiguration wird automatisch als Lead mit allen Antworten, Bausteinen und Kostenschätzung gespeichert. | M |
| Die Verwaltungskonsole ist nur nach Anmeldung erreichbar. | M |
| Der Zugriff richtet sich nach der Rolle des Benutzers (Geschäftsführer, Mitarbeiter, Administrator). | M |
| Benutzer sehen alle für sie freigegebenen Leads in einer Liste mit Sortierung, Filter und Suche. | M |
| Jeder Lead hat einen Status, der manuell geändert werden kann; Änderungen werden mit Zeitpunkt und Benutzer protokolliert. | M |
| Die Detailansicht zeigt alle Angaben des Kunden, die Bausteine und das PDF. | M |
| Das System schlägt für jeden Lead eine Priorität (hoch, mittel, niedrig) vor; der AG kann sie ändern. | S |
| Das System zeigt an, ob der Kunde bereits Bestandskunde ist, und wie sicher der Treffer ist. | S |
| Beim Status „Verloren“ kann ein Grund erfasst werden. | C |
| Einfache Auswertung (Anzahl Leads, Anteil gewonnen/verloren) für den Geschäftsführer. | C |
| Leads lassen sich als Datei exportieren (z. B. CSV). | C |
| Merkmale und Gewichtung der Priorität sind in der Verwaltungskonsole anpassbar. | C |

<!--| Leads können einem Mitarbeiter zugewiesen werden. | S |
| Mitarbeiter können Notizen zu einem Lead erfassen. | S |-->
 
## 7. Benachrichtigungssystem
Bezug: Fachkonzept „Benachrichtigungssystem“
 
| Anforderung | Prio |
| ----------- | ---- |
| Bei jedem neuen Lead wird der AG per E-Mail benachrichtigt (Kunde, Kontakt, wichtigste Angaben, Kostenschätzung, Link zum Lead). | M |
| Die Benachrichtigung wird automatisch und ohne Verzögerung nach Abschluss der Konfiguration ausgelöst. | M |
| Ein Lead wird gespeichert, auch wenn der Versand fehlschlägt; fehlgeschlagene Versuche werden wiederholt und protokolliert. | M |
| Der Benachrichtigungsstatus ist in der Verwaltungskonsole beim Lead sichtbar. | S |
| Dringende Leads werden in der Benachrichtigung hervorgehoben. | S |
| Der Administrator wird informiert, wenn Angebot oder PDF nicht erstellt werden konnten. | S |
| Das System ist so aufgebaut, dass weitere Kanäle (Push, SMS, WhatsApp) ohne Umbau ergänzt werden können. | S |
| Benutzer wählen selbst, über welche Kanäle sie benachrichtigt werden. | C |
| Erinnerung bei Leads, die zu lange unbearbeitet bleiben. | C |
| Mindestens ein weiterer Kanal neben E-Mail (Push, SMS oder WhatsApp). | C |
 
# Nicht-funktionale Anforderungen
Jede Anforderung hat ein Prüfkriterium, damit sie im Test nachgewiesen werden kann.
 
## 8. Benutzbarkeit
| Anforderung | Prüfkriterium | Prio |
| ----------- | ------------- | ---- |
| Die Web-App funktioniert auf Smartphone, Tablet und Laptop. | Alle Seiten sind bei 360 px, 768 px und 1366 px Breite ohne waagrechtes Scrollen bedienbar; getestet in aktuellem Chrome, Firefox, Safari und Edge. | M |
| Der Konfigurator ist ein intuitiver Schritt-für-Schritt-Assistent. | 3 Testpersonen ohne Vorwissen schließen den Konfigurator ohne Hilfe ab; Ziel unter 10 Minuten. | M |
| Alle Texte für Endkunden sind auf Deutsch und ohne Fachbegriffe bzw. mit Erklärung. | Review der Texte durch den AG. | S |
 
## 9. Leistung
| Anforderung | Prüfkriterium | Prio |
| ----------- | ------------- | ---- |
| Seitenwechsel innerhalb des Konfigurators laden schnell. | Wechsel zur nächsten Frage in unter 1 Sekunde (gemessen im Browser, normale Breitbandverbindung). | M |
| Die Kostenschätzung wird ohne spürbare Verzögerung berechnet. | Berechnung inkl. Antwort des Backends in unter 2 Sekunden; PDF-Erstellung in unter 5 Sekunden. | M |
| Die Benachrichtigung erreicht den AG zeitnah. | E-Mail wird innerhalb von 1 Minute nach dem Absenden verschickt. | S |
 
## 10. Sicherheit und Datenschutz
| Anforderung | Prüfkriterium | Prio |
| ----------- | ------------- | ---- |
| Personenbezogene Daten werden verschlüsselt in der Datenbank gespeichert. | Datenbank bzw. betroffene Spalten sind verschlüsselt; Klartext ist ohne Schlüssel nicht lesbar. | M |
| Die gesamte Kommunikation läuft verschlüsselt. | Nur HTTPS; Aufrufe über HTTP werden auf HTTPS umgeleitet. | M |
| Passwörter der Benutzer werden nur gehasht gespeichert; die Anmeldung erfolgt über JWT. | Code-Review; kein Passwort im Klartext in der Datenbank. | M |
| Das System ist gegen SQL-Injection und XSS geschützt. | Datenbankzugriff nur über Entity Framework Core; Texteingaben werden im Backend bereinigt; Test mit typischen Angriffseingaben. | M |
| Die Verarbeitung entspricht der DSGVO. | Zustimmung zum Datenschutz wird mit Zeitpunkt gespeichert; Leads können auf Wunsch gelöscht werden; Testdaten sind anonymisiert. | M |
| Logdateien enthalten keine echten Kundendaten. | Review der Logs nach einem Testdurchlauf. | M |
 
## 11. Zuverlässigkeit und Wartbarkeit
| Anforderung | Prüfkriterium | Prio |
| ----------- | ------------- | ---- |
| Die Datenbank wird täglich automatisch gesichert. | Täglicher verschlüsselter SQL-Dump auf einem separaten Speichermedium des AG; Wiederherstellung einmal erfolgreich getestet. | M |
| Fragen, Bausteine und Regeln sind ohne Änderung am Programmcode erweiterbar. | Eine neue Frage und ein neuer Baustein werden über die Verwaltungskonsole angelegt und funktionieren ohne neues Deployment. | S |
| Der Code ist automatisch getestet. | Unit-, Integrations- und End-to-End-Tests laufen in der CI/CD-Pipeline bei jedem Merge auf main. | S |
